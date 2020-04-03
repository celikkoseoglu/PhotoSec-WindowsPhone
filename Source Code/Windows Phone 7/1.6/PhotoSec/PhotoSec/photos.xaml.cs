using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using System.IO;
using System.IO.IsolatedStorage;
using System.ComponentModel;
using Microsoft.Phone;
using System.Threading;
using System.Windows;

namespace PhotoSec
{
    public partial class photos : PhoneApplicationPage
    {
        IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
        short i = 0;
        bool usingTextView = false;
        BackgroundWorker bw = new BackgroundWorker();

        public photos()
        {
            InitializeComponent();

            try
            {
                IsolatedStorageFileStream optionReader = storage.OpenFile("Files/DisplayPhotoTiles.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(optionReader);
                if (reader.ReadLine() == "checked")
                {
                    reader.Dispose();
                    optionReader.Dispose();
                    bw.DoWork += bw_DoWork;
                    bw.RunWorkerAsync();
                    bw.RunWorkerCompleted += bw_RunWorkerCompleted;
                }
                else
                {
                    usingTextView = true;
                    reader.Dispose();
                    optionReader.Dispose();
                    refreshView();
                }
            }
            catch
            {
                usingTextView = true;
                refreshView();
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string[] fileNames = storage.GetFileNames();
            if (i < fileNames.Length)
            {
                bw.RunWorkerAsync();
                i++;
            }
            else
            {
                fileNames = null;
                GC.Collect();
            }
        }

        public void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(30);
            string[] fileNames = storage.GetFileNames();
            FileStream isoStream = storage.OpenFile(fileNames[i], FileMode.Open, FileAccess.Read);

            photoList.Dispatcher.BeginInvoke(() =>
            {
                Image image = new Image();
                image.Source = PictureDecoder.DecodeJpeg(isoStream, 155, 155);
                image.Width = 142;
                image.Height = 142;
                image.Stretch = System.Windows.Media.Stretch.UniformToFill;
                image.Margin = new Thickness(5, 0, 5, 10);
                photoList.Items.Add(image);
                isoStream.Dispose();
            });
        }

        private void refreshView()
        {
            string[] fileNames = storage.GetFileNames();
            for (int i = 0; i < fileNames.Length; i++)
            {
                photoList.Items.Add(fileNames[i]);
            }
            fileNames = null;
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
                string originalFileName = Path.GetFileName(e.OriginalFileName);
                SavePhoto savePhoto = new SavePhoto();
                savePhoto.SaveImageToIsolatedStorage(e.ChosenPhoto, originalFileName, 0, 100);
                savePhoto = null;
                if (usingTextView == false)
                {
                    photoList.Items.Clear();
                    i = 0;
                    bw.RunWorkerAsync();
                }
                else 
                {
                    photoList.Items.Clear();
                    refreshView();
                }
                GC.Collect();
            }
            catch {}
        }

        private void photoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] fileNames = storage.GetFileNames();
            NavigationService.Navigate(new Uri("/PicView.xaml?picName=" + fileNames[photoList.SelectedIndex]  , UriKind.Relative));
            fileNames = null;
        }

        #region appbar

        private void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/about.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.RelativeOrAbsolute));
        }
    }
        #endregion
}