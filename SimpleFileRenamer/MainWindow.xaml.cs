using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.Win32;
using WinForms = System.Windows.Forms;

namespace SimpleFileRenamer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog FBD = new WinForms.FolderBrowserDialog();
            if (FBD.ShowDialog() == WinForms.DialogResult.OK )
            {
                folderPath.Text = FBD.SelectedPath;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String SearchFor = searchFor.Text.ToString();
            String ReplaceWith = replaceWith.Text.ToString();
            String directory = folderPath.Text.ToString();

            DirectoryInfo d = new DirectoryInfo(directory);
            FileInfo[] Files = d.GetFiles("*");

            System.Diagnostics.Process process = new System.Diagnostics.Process();

            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WorkingDirectory = directory,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                FileName = "cmd.exe"
            };

            foreach (FileInfo file in Files)
            {

                String newFileName = file.Name.Replace(SearchFor, ReplaceWith);
                String command = "ren " + file.Name + " " + newFileName;

                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.Arguments = "/c " + command;
                process.StartInfo = startInfo;
                process.Start();
            }

            MessageBox.Show("Files have been renamed", "Operation complete");

        }
    }
}

// Replace ( with "" (nothing) -PowerShell
//get-childitem *.jpg | foreach {rename-item $_ $_.name.replace("(","")}