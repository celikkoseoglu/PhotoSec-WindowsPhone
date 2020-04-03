using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using System.IO;
using System.IO.IsolatedStorage;
//using Microsoft.Phone;
//using System.Threading;
//using System.ComponentModel;

namespace PhotoSec
{
    public partial class photos : PhoneApplicationPage
    {
        IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
        //Image image = new Image();

        public photos()
        {
            InitializeComponent();
            refreshView();

            /*BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync();*/
        }

        /*void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            photoList.Items.Add("hello world");
        }

        public void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] fileNames = storage.GetFileNames();
            for (int i = 1; i < fileNames.Length; i++)
            {
                Thread.Sleep(500);
            }
        }*/


        private void refreshView()
        {
            string[] fileNames = storage.GetFileNames();
            for (int i = 1; i < fileNames.Length; i++)
            {
                photoList.Items.Add(fileNames[i]);
                //image = new Image();
                FileStream isoStream = storage.OpenFile(fileNames[i], FileMode.Open, FileAccess.Read);
                //image.Source = PictureDecoder.DecodeJpeg(isoStream, 200, 200);
                //image.Name = fileNames[i];
                isoStream.Dispose();
                //photoList.Items.Add(image);
            }
            //photoList.Items.Remove("password.txt");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string picName;
            if (NavigationContext.QueryString.TryGetValue("picName", out picName))
            {
                photoList.Items.Remove(picName);
            }
            GC.Collect();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            PhotoChooserTask pc = new PhotoChooserTask();
            pc.Completed += pc_Completed;
            pc.Show();
        }
        
        void pc_Completed(object sender, PhotoResult e)
        {
            try
            {
                var originalFileName = Path.GetFileName(e.OriginalFileName);
                SaveImage(e.ChosenPhoto, originalFileName, 0, 100);
            }
            catch{}
        }

        private void photoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PicView.xaml?picName=" + photoList.SelectedItem, UriKind.Relative));
        }

        public void SaveImage(Stream imageStream, string fileName, int orientation, int quality)
        {
            if (storage.FileExists(fileName))
                storage.DeleteFile(fileName);

            var fileStream = storage.CreateFile(fileName);
            BitmapImage bitmap = new BitmapImage();
            bitmap.SetSource(imageStream);
            WriteableBitmap wb = new WriteableBitmap(bitmap);
            wb.SaveJpeg(fileStream, wb.PixelWidth, wb.PixelHeight, orientation, quality);
            fileStream.Dispose();
            photoList.Items.Add(fileName);
        }

        #region appbar

        private void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/options.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ProvideFeedBack_Click(object sender, EventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();
            emailComposeTask.To = "celikkoseoglu@yahoo.com";
            emailComposeTask.Subject = "Feedback for PhotoSec";
            emailComposeTask.Show();
        }

        private void ChangePassword_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/changePassword.xaml", UriKind.RelativeOrAbsolute));
        }
    }
        #endregion
}