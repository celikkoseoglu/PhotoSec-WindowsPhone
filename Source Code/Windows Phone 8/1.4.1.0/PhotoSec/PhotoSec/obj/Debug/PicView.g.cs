﻿#pragma checksum "C:\Users\Çelik\Documents\Visual Studio 2012\Projects\PhotoSec\PhotoSec\PicView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E2A3B1FA424E0DF93ED40F89B9E4CB13"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace PhotoSec {
    
    
    public partial class PicView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Image picture;
        
        internal Microsoft.Phone.Shell.ApplicationBar appBar;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton deleteButton;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/PhotoSec;component/PicView.xaml", System.UriKind.Relative));
            this.picture = ((System.Windows.Controls.Image)(this.FindName("picture")));
            this.appBar = ((Microsoft.Phone.Shell.ApplicationBar)(this.FindName("appBar")));
            this.deleteButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("deleteButton")));
        }
    }
}

