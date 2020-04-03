using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
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
        public photos()
        {
            InitializeComponent();
            try
            {
                refreshView();
            }
            catch
            {
                MessageBox.Show("double tap to close images. Reinstall the app to reset.\n(PhotoSec Alpha)");
            }

        }

        private void refreshView()
        {
            string[] fileNames = storage.GetFileNames();
            photoList.Items.Add(storage.GetFileNames());


            /*IsolatedStorageFileStream savedNote = storage.OpenFile("photos.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(savedNote);
            long photoCount = CountLinesInString(reader.ReadToEnd());
            reader.BaseStream.Seek(0, SeekOrigin.Begin);

            for (int i = 0; i < photoCount; i++)
            {
                load(reader.ReadLine());
            }

            reader.Close();
            savedNote.Close();*/
        }

        static long CountLinesInString(string s)
        {
            long count = 1;
            int start = 0;
            while ((start = s.IndexOf('\n', start)) != -1)
            {
                count++;
                start++;
            }
            return count - 1;
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

        public void SaveImage(Stream imageStream, string fileName, int orientation, int quality)
        {
            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isolatedStorage.FileExists(fileName))
                    isolatedStorage.DeleteFile(fileName);

                var fileStream = isolatedStorage.CreateFile(fileName);
                var bitmap = new BitmapImage();
                bitmap.SetSource(imageStream);

                var wb = new WriteableBitmap(bitmap);
                wb.SaveJpeg(fileStream, wb.PixelWidth, wb.PixelHeight, orientation, quality);
                fileStream.Close();

                StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream("photos.txt", FileMode.Append, FileAccess.Write, storage));
                writer.WriteLine(fileName);
                writer.Close();
            }
            photoList.Items.Add(fileName);
        }

        private void load(string fileName)
        {
            Image image = new Image();
            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isolatedStorage.FileExists(fileName))
                {
                    using (var isoStream = isolatedStorage.OpenFile(fileName, FileMode.Open, FileAccess.Read))
                    {
                        var len = isoStream.Length;
                        BitmapImage b = new BitmapImage();
                        b.SetSource(isoStream);
                        image.Source = b;
                        photoList.Items.Add(image);
                        image.Width = photoList.Width;
                    }
                }
            }
        }
    }
}