using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.IO;
using System.IO.IsolatedStorage;

namespace PhotoSec
{
    public partial class changeFakePassword : PhoneApplicationPage
    {

        IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
        bool isPasswordCreated = false;

        public changeFakePassword()
        {
            InitializeComponent();
            try
            {
                IsolatedStorageFileStream optionReader = storage.OpenFile("Files/fakepassword.txt", FileMode.Open, FileAccess.Read);
                optionReader.Dispose();
                isPasswordCreated = true;
            }
            catch
            {
                oldPassword.IsEnabled = false;
                oldPassword.Opacity = 0.5;
            }
        }

        private void fakePassword_Click(object sender, RoutedEventArgs e)
        {
            if (isPasswordCreated == true)
            {
                IsolatedStorageFileStream savedPassword = storage.OpenFile("Files/fakepassword.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(savedPassword);
                string olderFakePassword = reader.ReadLine();
                savedPassword.Close();
                savedPassword = storage.OpenFile("Files/password.txt", FileMode.Open, FileAccess.Read);
                reader = new StreamReader(savedPassword);
                string originalPassword = reader.ReadLine();
                reader.Dispose();
                savedPassword.Dispose();

                if (oldPassword.Password == olderFakePassword & newPassword.Password != "" & newPassword.Password != originalPassword)
                {
                    StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream("Files/fakepassword.txt", FileMode.Create, FileAccess.Write, storage));
                    writer.WriteLine(newPassword.Password);
                    writer.Dispose();
                    MessageBox.Show("You successfully changed your fake password");
                    storage.Dispose();
                    NavigationService.GoBack();
                }
                else if (newPassword.Password == originalPassword)
                {
                    MessageBox.Show("You cannot use your original password as a fake password");
                }
                else
                {
                    MessageBox.Show("Old password wrong or New Password Box empty");
                }
            }
            else
            {
                IsolatedStorageFileStream savedPassword = storage.OpenFile("Files/password.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(savedPassword);
                string originalPassword = reader.ReadLine();
                savedPassword.Dispose();
                if (newPassword.Password != "" & newPassword.Password != originalPassword)
                {
                    StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream("Files/fakepassword.txt", FileMode.Create, FileAccess.Write, storage));
                    writer.WriteLine(newPassword.Password);
                    writer.Dispose();
                    storage.CreateDirectory("FakePhotos");
                    MessageBox.Show("You successfully created a new fake password");
                    storage.Dispose();
                    NavigationService.GoBack();
                }
                else if (newPassword.Password == originalPassword)
                {
                    MessageBox.Show("You cannot use your original password as a fake password");
                }
                else
                {
                    MessageBox.Show("New Password Box empty");
                }
            }
        }
    }
}