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

namespace PhotoSec
{
    public partial class photos : PhoneApplicationPage
    {
        IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
        bool onImage = false;

        public photos()
        {
            InitializeComponent();
            try
            {
                refreshView();
            }
            catch
            {
            }
        }

        private void refreshView()
        {
            string[] fileNames = storage.GetFileNames();
            for (int i = 0; i < fileNames.Length; i++)
            {
                photoList.Items.Add(fileNames[i]);
            }
            photoList.Items.Remove("password.txt");
        }

        protected override void OnDoubleTap(System.Windows.Input.GestureEventArgs e)
        {
            base.OnDoubleTap(e);
            if (onImage == true)
            {
                image1.Visibility = System.Windows.Visibility.Collapsed;
                photoList.Visibility = System.Windows.Visibility.Visible;
                ApplicationBar.IsVisible = true;
                GC.Collect();
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            var pc = new PhotoChooserTask();
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
            catch
            {
            }
        }
        private void photoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                load(photoList.SelectedItem.ToString());
            }
            catch
            {
                storage.DeleteFile(photoList.SelectedItem.ToString());
                photoList.Items.Remove(photoList.SelectedItem.ToString());
            }
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
            fileStream.Close();

            photoList.Items.Add(fileName);
        }

        private void load(string fileName)
        {
            if (storage.FileExists(fileName))
            {
                ApplicationBar.IsVisible = false;
                onImage = true;
                FileStream isoStream = storage.OpenFile(fileName, FileMode.Open, FileAccess.Read);
                long len = isoStream.Length;
                BitmapImage b = new BitmapImage();
                b.SetSource(isoStream);
                image1.Source = b;
                isoStream.Close();
                image1.Visibility = System.Windows.Visibility.Visible;
                photoList.Visibility = System.Windows.Visibility.Collapsed;
            }
            /*onImage = true;
            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                
                    using (var isoStream = isolatedStorage.OpenFile(fileName, FileMode.Open, FileAccess.Read))
                    {
                        ApplicationBar.IsVisible = false;
                        var len = isoStream.Length;
                        BitmapImage b = new BitmapImage();
                        b.SetSource(isoStream);
                        image1.Source = b;
                        image1.Visibility = System.Windows.Visibility.Visible;
                        photoList.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }*/
        }
        #region appbar

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                storage.DeleteFile(photoList.SelectedItem.ToString());
                photoList.Items.Remove(photoList.SelectedItem.ToString()); //burda bi saçmalık var hem exception veriyo hemde çalışıyo...
            }
            catch
            {
                image1.Visibility = System.Windows.Visibility.Collapsed;
                photoList.Visibility = System.Windows.Visibility.Visible;
                ApplicationBar.IsVisible = true;
            }
        }

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