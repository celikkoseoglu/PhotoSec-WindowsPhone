﻿#pragma checksum "C:\Users\Çelik\Desktop\PhotoSec App\Source Code\Windows Phone 8\1.7.1.0\PhotoSec\PhotoSec\PicView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "92231ECB7A062F3063258080209A3BB7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
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
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Primitives.ViewportControl viewport;
        
        internal System.Windows.Controls.Canvas canvas;
        
        internal System.Windows.Controls.Image TestImage;
        
        internal System.Windows.Media.ScaleTransform xform;
        
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
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.viewport = ((System.Windows.Controls.Primitives.ViewportControl)(this.FindName("viewport")));
            this.canvas = ((System.Windows.Controls.Canvas)(this.FindName("canvas")));
            this.TestImage = ((System.Windows.Controls.Image)(this.FindName("TestImage")));
            this.xform = ((System.Windows.Media.ScaleTransform)(this.FindName("xform")));
            this.appBar = ((Microsoft.Phone.Shell.ApplicationBar)(this.FindName("appBar")));
            this.deleteButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("deleteButton")));
        }
    }
}

