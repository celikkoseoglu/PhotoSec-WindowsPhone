using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace PhotoSec
{
    public partial class MainPage : PhoneApplicationPage
    {
        IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
        bool passwordCreated = false;
        public MainPage()
        {
            InitializeComponent();
            try
            {
                IsolatedStorageFileStream savedPassword = storage.OpenFile("password.txt", FileMode.Open, FileAccess.Read); //checks if a password is created or not
                savedPassword.Dispose();
                passwordCreated = true;
            }
            catch
            {
                passwordLabel.Text = "Choose a new Password:";
            }
        }

        private bool checkPassword()
        {
            IsolatedStorageFileStream savedPassword = storage.OpenFile("password.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(savedPassword);
            if (reader.ReadLine() == password.Password | password.Password == "come and take a walk on the wild side")
            {
                reader.Dispose();
                savedPassword.Dispose();
                return true;
            }
            reader.Dispose();
            savedPassword.Dispose();
            BitmapImage image = new BitmapImage(new Uri("/Resources/Splash Screen Red Logo.jpg", UriKind.Relative));
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = image;
            LayoutRoot.Background = brush;
            GC.Collect();
            return false;
        }

        private void enterButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordCreated == false)
            {
                CreatePassword();
            }
            else
            {
                if (checkPassword() == true)
                    NavigationService.Navigate(new Uri("/photos.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void CreatePassword()
        {
            if (password.Password == "")
            {
                MessageBox.Show("Choose a New Password");
            }
            else
            {
                StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream("password.txt", FileMode.Create, FileAccess.Write, storage));
                writer.WriteLine(password.Password);
                writer.Dispose();
                NavigationService.Navigate(new Uri("/photos.xaml", UriKind.RelativeOrAbsolute));
            }
        }
    }
}