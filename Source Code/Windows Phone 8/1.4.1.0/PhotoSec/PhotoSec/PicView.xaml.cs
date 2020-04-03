using System;
using System.Net;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;
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
            string picName;
            if (NavigationContext.QueryString.TryGetValue("picName", out picName))
            {
                FileStream isoStream = storage.OpenFile(picName, FileMode.Open, FileAccess.Read);
                /*long len = isoStream.Length;
                BitmapImage b = new BitmapImage();
                b.SetSource(isoStream);*/
                //picture.Source = b;
                picture.Source = PictureDecoder.DecodeJpeg(isoStream, 1280, 1280);
                isoStream.Dispose();
                imageName = picName;
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            IsolatedStorageFileStream fileStream = storage.OpenFile(imageName, FileMode.Open, FileAccess.Read);
            MediaLibrary mediaLibrary = new MediaLibrary();
            Picture pic = mediaLibrary.SavePictureToCameraRoll(imageName, fileStream);
            fileStream.Dispose();
            mediaLibrary.Dispose();
            pic.Dispose();
            storage.DeleteFile(imageName.Remove(imageName.IndexOf(".")) + "_jpg.jpg");
            storage.DeleteFile(imageName.Remove(imageName.IndexOf(".")) + "_png.jpg");
            MessageBox.Show("Image Saved to Camera Roll!");
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete this picture?", "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                storage.DeleteFile(imageName);
                NavigationService.Navigate(new Uri("/Photos.xaml?picName=" + imageName, UriKind.Relative));
            }
        }
    }
}