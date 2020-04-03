using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using Microsoft.Phone;
//photocamera class

namespace PhotoSec
{
    public partial class PicView : PhoneApplicationPage
    {
        IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
        string imageName;

        public PicView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (NavigationContext.QueryString.TryGetValue("picName", out imageName))
            {
                FileStream isoStream = storage.OpenFile(imageName, FileMode.Open, FileAccess.Read);
                picture.Source = PictureDecoder.DecodeJpeg(isoStream, 1280, 1280);
                isoStream.Dispose();
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SavePhoto savePhoto = new SavePhoto();
            savePhoto.SaveToCameraRoll(imageName);
            savePhoto = null;
            MessageBox.Show("Image Saved to Camera Roll!");
            GC.Collect();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete this picture?", "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                storage.DeleteFile(imageName);
                NavigationService.Navigate(new Uri("/Photos.xaml?picName=" + imageName, UriKind.Relative));
            }
        }

        private void doubleTapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            picture.Width = 2000;
        }
    }
}