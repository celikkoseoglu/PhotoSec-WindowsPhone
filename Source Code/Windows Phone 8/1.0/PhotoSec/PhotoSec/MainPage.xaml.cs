using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhotoSec.Resources;
using System.IO;
using System.IO.IsolatedStorage;

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
                checkPassword();
            }
            catch
            {
                passwordLabel.Text = "Choose a new Password:";
            }
        }

        private bool checkPassword()
        {
            IsolatedStorageFileStream savedPassword = storage.OpenFile("password.txt", FileMode.Open, FileAccess.Read);
            passwordCreated = true;
            StreamReader reader = new StreamReader(savedPassword);
            if (reader.ReadLine() == password.Password)
                return true;
            reader.Close();
            savedPassword.Close();
            return false;
        }

        private void enter_Button_Click(object sender, RoutedEventArgs e)
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
                writer.Close();
                MessageBox.Show("Reinstall the app to reset the password.");
                NavigationService.Navigate(new Uri("/photos.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/options.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}