using System;
using System.Net;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.IO;
using System.IO.IsolatedStorage;

namespace PhotoSec
{
    public partial class Settings : PhoneApplicationPage
    {
        IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
        bool wasChecked = false;
        public Settings()
        {
            InitializeComponent();
            try
            {
                IsolatedStorageFileStream optionReader = storage.OpenFile("Files/DisplayPhotoTiles.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(optionReader);
                if (reader.ReadLine() == "checked")
                {
                    displayPhotoTiles.IsChecked = true;
                    wasChecked = true;
                }
                reader.Dispose();
                optionReader.Dispose();
                
            }
            catch { }
            displayPhotoTiles.Checked += displayPhotoTiles_Checked;
        }

        private void changePassword_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/changePassword.xaml", UriKind.RelativeOrAbsolute));
        }

        private void displayPhotoTiles_Checked(object sender, RoutedEventArgs e)
        {
            StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream("Files/DisplayPhotoTiles.txt", FileMode.Create, FileAccess.Write, storage));
            writer.WriteLine("checked");
            writer.Dispose();
            if (wasChecked == true)
            {
                warning.Text = "May degrade performance. Not recommended on low memory phones.";
            }
            else
            {
                warning.Text = "Restart PhotoSec to apply changes";
            }
        }

        private void displayPhotoTiles_UnChecked(object sender, RoutedEventArgs e)
        {
            StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream("Files/DisplayPhotoTiles.txt", FileMode.Create, FileAccess.Write, storage));
            writer.WriteLine("unchecked");
            writer.Dispose();
            if (wasChecked == true)
            {
                warning.Text = "Restart PhotoSec to apply changes";
            }
            else
            {
                warning.Text = "May degrade performance. Not recommended on low memory phones.";
            }
        }
    }
}