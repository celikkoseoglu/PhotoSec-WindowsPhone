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
                try
                {
                    IsolatedStorageFileStream savedPassword2 = storage.OpenFile("password.txt", FileMode.Open, FileAccess.Read);
                    savedPassword2.Dispose();
                    storage.CreateDirectory("Files");
                    storage.MoveFile("password.txt", "Files/password.txt");
                }
                catch { }
                IsolatedStorageFileStream savedPassword = storage.OpenFile("Files/password.txt", FileMode.Open, FileAccess.Read);
                savedPassword.Dispose();
                passwordCreated = true;
            }
            catch
            {
                passwordLabel.Text = "Choose a new password";
            }
        }

        private bool checkPassword()
        {
            IsolatedStorageFileStream savedPassword = storage.OpenFile("Files/password.txt", FileMode.Open, FileAccess.Read);
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
            //GC.Collect();
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
                AdvancedTextIO advancedTextIO = new AdvancedTextIO();
                MessageBox.Show("aaa" + "/" + "helloworld");
                advancedTextIO.SaveTextToSpecifiedLocation("password.txt", password.Password, "Files");
                advancedTextIO = null;
                /*storage.CreateDirectory("Files");
                StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream("Files/password.txt", FileMode.Create, FileAccess.Write, storage));
                writer.WriteLine(password.Password);
                writer.Dispose();*/
                passwordCreated = true;
                passwordLabel.Text = "Enter Your Password";
                NavigationService.Navigate(new Uri("/photos.xaml", UriKind.RelativeOrAbsolute));
            }
        }
    }
}