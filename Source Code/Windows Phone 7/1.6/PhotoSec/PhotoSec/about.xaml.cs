using System;
using System.Net;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace PhotoSec
{
    public partial class options : PhoneApplicationPage
    {
        public options()
        {
            InitializeComponent();
        }

        private void ReviewTask_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        private void ProvideFeedBack_Click(object sender, EventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();
            emailComposeTask.To = "celikkoseoglu@yahoo.com";
            emailComposeTask.Subject = "Feedback for PhotoSec";
            emailComposeTask.Show();
        }
    }
}