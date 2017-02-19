using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCompare;
using Newtonsoft.Json;
using VCompare.Utilities;

using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System.Threading;

namespace VCompare
{
    public partial class Form1 : Form
    {
        List<FInfo> LoadedFS
        {
            get
            {
                if (_LoadedFS == null)
                    return new List<FInfo>();

                return _LoadedFS;
            }
            set
            {
                _LoadedFS = value;
                label1.Text = string.Format("Loaded Files: {0}\nTotal Size: {1}", _LoadedFS.Count, _LoadedFS.Sum(m => m.orisize).GetFormattedFileSize());
            }
        }
        List<FInfo> _LoadedFS;


        public Form1()
        {
            InitializeComponent();
        }

        private void butLoadFS_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog
            {
                EnsurePathExists = true,
                EnsureFileExists = false,
                AllowNonFileSystemItems = false,
                Title = "Select the directory that you want to Index.",
                IsFolderPicker = true,
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                pbStatus.Style = ProgressBarStyle.Marquee;
                lblProcStatus.Text = "FS Status: Initiated";
                LoadFileSystem(dialog.FileName,
                    new Action<List<FInfo>>((a) =>
                    {
                        LoadedFS = a;
                        lblProcStatus.Text = "FS Status: Finished";
                        pbStatus.Style = ProgressBarStyle.Blocks;
                    }),
                    new Action<string>(a => lblProcStatus.Text = a));
            }
        }


        void LoadFileSystem(string path, Action<List<FInfo>> resAct = null, Action<string> UpdateStatus = null)
        {
            List<FInfo> pathlist = new List<FInfo>();
            Thread th = new Thread(() =>
            {
                var FInfo = new DirectoryInfo(path).GetFiles("*", SearchOption.AllDirectories);
                for (int i = 0; i < FInfo.Length; i++)
                {
                    var item = FInfo[i];
                    if (UpdateStatus != null) this.BeginInvoke(UpdateStatus, string.Format("FS Status: Processing {0}/{1}", i, FInfo.Length));
                    pathlist.Add(new FInfo()
                    {
                        FileName = item.Name,
                        FilePath = item.FullName.Replace(path, ""),
                        FileHash = item.ComputeHash(),
                        orisize = item.Length
                    });
                }
                if (resAct != null) this.Invoke(resAct, pathlist);
            });
            th.Start();
        }

        private void butLoadIF_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdIndx = new OpenFileDialog();

            ofdIndx.InitialDirectory = "c:\\";
            ofdIndx.Filter = "INDX files (*.indx)|*.indx";
            ofdIndx.FilterIndex = 0;
            ofdIndx.RestoreDirectory = true;

            if (ofdIndx.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(ofdIndx.FileName))
                {
                    try
                    {
                        LoadedFS = JsonConvert.DeserializeObject<List<FInfo>>(File.ReadAllText(ofdIndx.FileName));
                    }
                    catch
                    {
                        MessageBox.Show("Something went wrong, idc what exactly, but fix it!");
                    }
                }
                else
                {
                    MessageBox.Show("Retard mode ACTIVATED");
                }
            }
        }

        private void butSaveIndex_Click(object sender, EventArgs e)
        {
            if (LoadedFS != null)
            {
                SaveFileDialog sfdLoadedIndex = new SaveFileDialog();

                sfdLoadedIndex.Filter = "INDX files (*.indx)|*.indx";
                sfdLoadedIndex.FilterIndex = 2;
                sfdLoadedIndex.RestoreDirectory = true;

                if (sfdLoadedIndex.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfdLoadedIndex.FileName, JsonConvert.SerializeObject(LoadedFS));
                }
            }
        }

        private void butCompare_Click(object sender, EventArgs e)
        {
            List<string> neededCopy = new List<string>();
            var cofdSelCompareDest = new CommonOpenFileDialog
            {
                EnsurePathExists = true,
                EnsureFileExists = false,
                AllowNonFileSystemItems = false,
                DefaultFileName = "Select Folder",
                Title = "Select the directory that you want to compare the vindex with.",
                IsFolderPicker = true,
            };

            if (cofdSelCompareDest.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var path = cofdSelCompareDest.FileName;
                var FInfos = new DirectoryInfo(path).GetFiles("*", SearchOption.AllDirectories);

                foreach (var FileInfo in FInfos)
                {
                    var relPath = FileInfo.FullName.Replace(path, ""); //\unins000.exe"
                    var comEntity = LoadedFS.Where(m => m.FilePath == relPath).FirstOrDefault();

                    if (comEntity == null || comEntity.orisize != FileInfo.Length || FileInfo.ComputeHash() != comEntity.FileHash)
                        neededCopy.Add(relPath);
                }

                if (neededCopy.Count == 0)
                {
                    MessageBox.Show("Didn't find any differences, everything should be fine!");
                }
                else
                {
                    var userSelDifferDirCreation = MessageBox.Show(string.Format("Found {0} files that differ from the vindex file, do you want to create a difference directory?", neededCopy.Count), "Changes Found", MessageBoxButtons.YesNo) == DialogResult.Yes ? true : false;
                    if (userSelDifferDirCreation)
                    {
                        var cofdMoveToFolder = new CommonOpenFileDialog
                        {
                            EnsurePathExists = true,
                            EnsureFileExists = false,
                            AllowNonFileSystemItems = false,
                            DefaultFileName = "Select Folder",
                            Title = "Select the directory that you want to move the different files in.",
                            IsFolderPicker = true,
                        };

                        if (cofdMoveToFolder.ShowDialog() == CommonFileDialogResult.Ok)
                        {
                            var cTPath = cofdMoveToFolder.FileName;
                            foreach (var copItem in neededCopy)
                            {
                                Directory.CreateDirectory(new FileInfo(cTPath + copItem).DirectoryName);
                                File.Copy(path + copItem, cTPath + copItem);
                            }
                            MessageBox.Show(string.Format("Finished copying {0} files", neededCopy.Count));
                        }
                    }
                }
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {


        }

    }

    public class FInfo
    {
        public string FilePath;
        public string FileName;
        public string FileHash;
        public long orisize;
    }

    public static class Extentions
    {
        public static string ComputeHash(this FileInfo input, bool lowercase = true)
        {
            if (!input.Exists)
                throw new Exception("The requested file was not found!");

            using (Stream fs = input.OpenRead())
                return MD5.Create().ComputeHash(fs).ToHexString(lowercase);
        }


        static string[] sizeSuffixes = {
        "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static string GetFormattedFileSize(this long size)
        {
            Debug.Assert(sizeSuffixes.Length > 0);

            const string formatTemplate = "{0}{1:0.#} {2}";

            if (size == 0)
            {
                return string.Format(formatTemplate, null, 0, sizeSuffixes[0]);
            }

            var absSize = Math.Abs((double)size);
            var fpPower = Math.Log(absSize, 1000);
            var intPower = (int)fpPower;
            var iUnit = intPower >= sizeSuffixes.Length
                ? sizeSuffixes.Length - 1
                : intPower;
            var normSize = absSize / Math.Pow(1000, iUnit);

            return string.Format(
                formatTemplate,
                size < 0 ? "-" : null, normSize, sizeSuffixes[iUnit]);
        }



        public static bool Exists(this FileInfo input) => File.Exists(input.FullName);
        public static Stream OpenRead(this FileInfo input) => File.OpenRead(input.FullName);
        public static string ToHexString(this byte[] input, bool lowercase = true) => string.Concat(input.Select(b => b.ToString(lowercase ? "x2" : "X2")).ToArray());
    }
}
