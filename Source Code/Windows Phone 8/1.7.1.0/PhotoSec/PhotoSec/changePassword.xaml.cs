using System;
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
        string fakePassword = null;
        public changePassword()
        {
            InitializeComponent();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFileStream savedPassword = storage.OpenFile("Files/password.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(savedPassword);
            string olderPassword = reader.ReadLine();
            savedPassword.Close();
            try
            {
                savedPassword = storage.OpenFile("Files/fakepassword.txt", FileMode.Open, FileAccess.Read);
                reader = new StreamReader(savedPassword);
                fakePassword = reader.ReadLine();
            }
            catch { }
            reader.Dispose();
            savedPassword.Dispose();
            

            if (oldPassword.Password == olderPassword & newPassword.Password != "" & newPassword.Password != fakePassword)
            {
                StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream("Files/password.txt", FileMode.Create, FileAccess.Write, storage));
                writer.WriteLine(newPassword.Password);
                writer.Dispose();
                MessageBox.Show("You successfully changed your password");
                storage.Dispose();
                NavigationService.GoBack();
            }
            else if (newPassword.Password == fakePassword)
            {
                MessageBox.Show("You cannot use your fake password as an original password");
            }
            else
            {
                MessageBox.Show("Old password wrong or New Password Box empty");
            }
        }
    }
}