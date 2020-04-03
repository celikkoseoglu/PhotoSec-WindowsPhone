using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.IO;

namespace PhotoSec
{
    public partial class changePassword : PhoneApplicationPage
    {
        IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
        public changePassword()
        {
            InitializeComponent();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFileStream savedPassword = storage.OpenFile("password.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(savedPassword);
            string olderPassword = reader.ReadLine();
            reader.Close();
            savedPassword.Close();

            if (oldPassword.Password == olderPassword & newPassword.Password != "")
            {
                StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream("password.txt", FileMode.Create, FileAccess.Write, storage));
                writer.WriteLine(newPassword.Password);
                writer.Close();
                MessageBox.Show("You changed your password successfully");
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Old password wrong or New Password Box empty");
            }
        }
    }
}