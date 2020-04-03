using System.Windows.Media.Imaging;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Media;
using Microsoft.Phone;

namespace PhotoSec
{
    class SavePhoto
    {
        IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
        Stream imageStream;
        string fileName;
        short orientation;
        short quality;

        //public SavePhoto(){} //constructor call

        public void SaveImageToIsolatedStorage(Stream imageStream, string fileName, short orientation, short quality)
        {
            this.imageStream = imageStream;
            this.fileName = fileName;
            this.orientation = orientation;
            this.quality = quality;

            if (storage.FileExists(fileName))
                storage.DeleteFile(fileName);

            var fileStream = storage.CreateFile(fileName);
            BitmapImage bitmap = new BitmapImage();
            bitmap.SetSource(imageStream);
            WriteableBitmap wb = new WriteableBitmap(bitmap);
            wb.SaveJpeg(fileStream, wb.PixelWidth, wb.PixelHeight, orientation, quality);
            fileStream.Dispose();
        }

        public void SaveToCameraRoll(string fileName)
        {
            IsolatedStorageFileStream fileStream = storage.OpenFile(fileName, FileMode.Open, FileAccess.Read);
            MediaLibrary mediaLibrary = new MediaLibrary();
            Picture pic = mediaLibrary.SavePictureToCameraRoll(fileName, fileStream);
            fileStream.Dispose();
            mediaLibrary.Dispose();
            pic.Dispose();
            storage.DeleteFile(fileName.Remove(fileName.IndexOf(".")) + "_jpg.jpg");
            storage.DeleteFile(fileName.Remove(fileName.IndexOf(".")) + "_png.jpg");
        }

        public Stream ImageStream
        {
            get { return imageStream; }
            set { imageStream = value; }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public short Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        public short Quality
        {
            get { return quality; }
            set { quality = value; }
        }
    }
}
