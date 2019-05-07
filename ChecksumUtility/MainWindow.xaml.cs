using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChecksumUtility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker bgWorkerMD5;
        private BackgroundWorker bgWorkerSHA1;
        private BackgroundWorker bgWorkerSHA256;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.InitialDirectory = "C:\\";
            openFD.Filter = "All files (*.*)|*.*";
            openFD.Title = "Select a File to Generate Hash.";
            openFD.FileName = "";

            if (openFD.ShowDialog() == true)
            {
                txtFilePath.Text = openFD.FileName;
            }
        }


        private void TxtFilePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtFilePath.Text.Trim() != "")
            {
                if (this.chkMD5.IsChecked == true)
                    this.ComputeMD5();
                if (this.chkSHA1.IsChecked == true)
                    this.ComputeSHA1();
                if (this.chkSHA256.IsChecked == true)
                    this.ComputeSHA256();
            }
        }

        private void ComputeHashAsync()
        {
            ComputeSHA256();
        }


        private void ComputeMD5()
        {
            this.bgWorkerMD5 = new BackgroundWorker();
            this.chkMD5.IsEnabled = false;
            this.btnCopyMD5.IsEnabled = false;
            this.btnCopyAll.IsEnabled = false;
            this.bgWorkerMD5.WorkerSupportsCancellation = true;
            this.bgWorkerMD5.CancelAsync();
            this.bgWorkerMD5.DoWork += BgWorkerMD5_DoWork;
            this.bgWorkerMD5.RunWorkerCompleted += BgWorkerMD5_RunWorkerCompleted;
            //while (this.bgWorkerMD5.IsBusy)
            //    Application.DoEvents();
            this.bgWorkerMD5.RunWorkerAsync((object)this.txtFilePath.Text);
            this.progressBarMD5.IsIndeterminate = true;
            this.progressBarMD5.Visibility = Visibility.Visible;
        }

        private void BgWorkerMD5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null)
                return;
            this.txtMD5.Text = e.Result.ToString();
            this.progressBarMD5.IsIndeterminate = false;
            this.progressBarMD5.Value = 0;
            this.progressBarMD5.Visibility = Visibility.Hidden;
            this.chkMD5.IsEnabled = true;
            this.btnCopyMD5.IsEnabled = true;
            this.btnCopyAll.IsEnabled = true;
        }

        private void BgWorkerMD5_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!this.bgWorkerMD5.CancellationPending)
            {
                MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
                Stream inputStream = (Stream)new FileStream(e.Argument.ToString(), FileMode.Open, FileAccess.Read);
                byte[] hash = cryptoServiceProvider.ComputeHash(inputStream);
                StringBuilder stringBuilder = new StringBuilder(hash.Length);
                int num = checked(hash.Length - 1);
                int index = 0;
                while (index <= num)
                {
                    stringBuilder.Append(hash[index].ToString("X2"));
                    checked { ++index; }
                }
                e.Result = (object)stringBuilder.ToString();
                inputStream.Close();
            }
            else
                e.Cancel = true;
        }

        private void ComputeSHA1()
        {
            this.bgWorkerSHA1 = new BackgroundWorker();
            this.chkSHA1.IsEnabled = false;
            this.btnCopySHA1.IsEnabled = false;
            this.btnCopyAll.IsEnabled = false;
            this.bgWorkerSHA1.WorkerSupportsCancellation = true;
            this.bgWorkerSHA1.CancelAsync();
            this.bgWorkerSHA1.DoWork += BgWorkerSHA1_DoWork;
            this.bgWorkerSHA1.RunWorkerCompleted += BgWorkerSHA1_RunWorkerCompleted;
            //while (this.bgWorkerSHA1.IsBusy)
            //    Application.DoEvents();
            this.bgWorkerSHA1.RunWorkerAsync((object)this.txtFilePath.Text);
            this.progressBarSHA1.IsIndeterminate = true;
            this.progressBarSHA1.Visibility = Visibility.Visible;
        }


        private void BgWorkerSHA1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null)
                return;
            this.txtSHA1.Text = e.Result.ToString();
            this.progressBarSHA1.IsIndeterminate = false;
            this.progressBarSHA1.Value = 0;
            this.progressBarSHA1.Visibility = Visibility.Hidden;
            this.chkSHA1.IsEnabled = true;
            this.btnCopySHA1.IsEnabled = true;
            this.btnCopyAll.IsEnabled = true;
        }

        private void BgWorkerSHA1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!this.bgWorkerSHA1.CancellationPending)
            {
                SHA1CryptoServiceProvider cryptoServiceProvider = new SHA1CryptoServiceProvider();
                Stream inputStream = (Stream)new FileStream(e.Argument.ToString(), FileMode.Open, FileAccess.Read);
                byte[] hash = cryptoServiceProvider.ComputeHash(inputStream);
                StringBuilder stringBuilder = new StringBuilder(hash.Length);
                int num = checked(hash.Length - 1);
                int index = 0;
                while (index <= num)
                {
                    stringBuilder.Append(hash[index].ToString("X2"));
                    checked { ++index; }
                }
                e.Result = (object)stringBuilder.ToString();
                inputStream.Close();
            }
            else
                e.Cancel = true;
        }

        private void ComputeSHA256()
        {
            this.bgWorkerSHA256 = new BackgroundWorker();
            this.chkSHA256.IsEnabled = false;
            this.btnCopySHA256.IsEnabled = false;
            this.btnCopyAll.IsEnabled = false;
            this.bgWorkerSHA256.WorkerSupportsCancellation = true;
            this.bgWorkerSHA256.CancelAsync();
            this.bgWorkerSHA256.DoWork += BgWorkerSHA256_DoWork;
            this.bgWorkerSHA256.RunWorkerCompleted += BgWorkerSHA256_RunWorkerCompleted;
            //while (this.bgWorkerSHA256.IsBusy)
            //    Application.DoEvents();
            this.bgWorkerSHA256.RunWorkerAsync((object)this.txtFilePath.Text);
            this.progressBarSHA256.IsIndeterminate = true;
            this.progressBarSHA256.Visibility = Visibility.Visible;
        }


        private void BgWorkerSHA256_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null)
                return;
            this.txtSHA256.Text = e.Result.ToString();
            this.progressBarSHA256.IsIndeterminate = false;
            this.progressBarSHA256.Value = 0;
            this.progressBarSHA256.Visibility = Visibility.Hidden;
            this.chkSHA256.IsEnabled = true;
            this.btnCopySHA256.IsEnabled = true;
            this.btnCopyAll.IsEnabled = true;
        }

        private void BgWorkerSHA256_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!this.bgWorkerSHA256.CancellationPending)
            {
                SHA256CryptoServiceProvider cryptoServiceProvider = new SHA256CryptoServiceProvider();
                Stream inputStream = (Stream)new FileStream(e.Argument.ToString(), FileMode.Open, FileAccess.Read);
                byte[] hash = cryptoServiceProvider.ComputeHash(inputStream);
                StringBuilder stringBuilder = new StringBuilder(hash.Length);
                int num = checked(hash.Length - 1);
                int index = 0;
                while (index <= num)
                {
                    stringBuilder.Append(hash[index].ToString("X2"));
                    checked { ++index; }
                }
                e.Result = (object)stringBuilder.ToString();
                inputStream.Close();
            }
            else
                e.Cancel = true;
        }

        private async Task ComputeSHA256Hash()
        {
            SHA256CryptoServiceProvider cryptoServiceProvider = new SHA256CryptoServiceProvider();
            Stream inputStream = (Stream)new FileStream(txtFilePath.Text, FileMode.Open, FileAccess.Read);
            byte[] hash = cryptoServiceProvider.ComputeHash(inputStream);
            StringBuilder stringBuilder = new StringBuilder(hash.Length);
            int num = checked(hash.Length - 1);
            int index = 0;
            while (index <= num)
            {
                stringBuilder.Append(hash[index].ToString("X2"));
                checked { ++index; }
            }

            txtSHA256.Text = stringBuilder.ToString();
        }

        private void Verify_Click(object sender, RoutedEventArgs e)
        {
            string textPasteHash = this.txtPasteHash.Text.Trim().ToUpper();

            int num;

            if (textPasteHash == "" || textPasteHash == null)
            {
                num = (int)MessageBox.Show("Enter Hash to match!", "No Hash", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (textPasteHash == this.txtMD5.Text.Trim().ToUpper())
            {
                num = (int)MessageBox.Show("MD5 Hash matched.", "Matched", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else if (textPasteHash == this.txtSHA1.Text.Trim().ToUpper())
            {
                this.txtPasteHash.Text = this.txtPasteHash.Text.Trim().ToUpper();
                num = (int)MessageBox.Show("SHA-1 Hash matched.", "Matched", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else if (textPasteHash == this.txtSHA256.Text.Trim().ToUpper())
            {
                this.txtPasteHash.Text = this.txtPasteHash.Text.Trim().ToUpper();
                num = (int)MessageBox.Show("SHA-256 Hash matched.", "Matched", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                this.txtPasteHash.Text = this.txtPasteHash.Text.Trim().ToUpper();
                num = (int)MessageBox.Show("Hash does not match!", "Not match", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }


        private void CopyMD5_Click(object sender, RoutedEventArgs e)
        {
            string textMD5 = this.txtMD5.Text.Trim();
            if (textMD5 == "" || textMD5 == null)
                return;
            int num = (int)MessageBox.Show("MD5 checksum has been copied to clipboard", "Information", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Clipboard.SetDataObject((object)this.txtMD5.Text.Trim());
        }

        private void CopySHA1_Click(object sender, RoutedEventArgs e)
        {
            string textSHA1 = this.txtSHA1.Text.Trim();
            if (textSHA1 == "" || textSHA1 == null)
                return;
            int num = (int)MessageBox.Show("SHA-1 checksum has been copied to clipboard", "Information", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Clipboard.SetDataObject((object)this.txtSHA1.Text.Trim());
        }

        private void CopySHA256_Click(object sender, RoutedEventArgs e)
        {
            string textSHA256 = this.txtSHA256.Text.Trim();
            if (textSHA256 == "" || textSHA256 == null)
                return;
            int num = (int)MessageBox.Show("SHA-256 checksum has been copied to clipboard", "Information", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Clipboard.SetDataObject((object)this.txtSHA256.Text.Trim());
        }

        private void CopyAll_Click(object sender, RoutedEventArgs e)
        {
            string textMD5 = this.txtMD5.Text.Trim();
            string textSHA1 = this.txtSHA1.Text.Trim();
            string textSHA256 = this.txtSHA256.Text.Trim();
            StringBuilder sb = new StringBuilder();
            if (textMD5 != "" && textMD5 != null)
            {
                sb.Append("MD5 Checksum: ");
                sb.Append(textMD5);
                sb.Append("\r\n");
            }
            if (textSHA1 != "" && textSHA1 != null)
            {
                sb.Append("SHA-1 Checksum: ");
                sb.Append(textSHA1);
                sb.Append("\r\n");
            }
            if (textSHA256 != "" && textSHA256 != null)
            {
                sb.Append("SHA-256 Checksum: ");
                sb.Append(textSHA256);
                sb.Append("\r\n");
            }
            sb.Append("Generated by Checksum Utility WPF");
            Clipboard.SetDataObject((object)sb.ToString());
            int num = (int)MessageBox.Show("All selected checksums have been copied to clipboard", "Information", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            IDataObject dataObject = Clipboard.GetDataObject();
            if (dataObject.GetDataPresent(DataFormats.Text))
            {
                this.txtPasteHash.Text = dataObject.GetData(DataFormats.Text).ToString();
            }
            else
            {
                int num = (int)MessageBox.Show("There is no data from the clipboard.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChkSHA1_Unchecked(object sender, RoutedEventArgs e)
        {
            txtSHA1.Text = "";
        }

        private void ChkMD5_Unchecked(object sender, RoutedEventArgs e)
        {
            txtMD5.Text = "";
        }

        private void ChkSHA256_Unchecked(object sender, RoutedEventArgs e)
        {
            txtSHA256.Text = "";
        }
    }
}
