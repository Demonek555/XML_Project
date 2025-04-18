﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace bibKliBudka
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ContentDialog()
            {
                Title = "Wcisnąłeś buttona!",
                Content = "I teraz zaczynam pracę...",
                PrimaryButtonText = "OK",
                SecondaryButtonText = "Cancel",
            };
            var result = await dlg.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                tbLewy.Text = "wybrano OK";
                tbPrawy.Text = "====";
                //wybrano ok
            }
            else
            {
                tbPrawy.Text = "wybrano Cancel";
                tbLewy.Text = "****";
                //wybrano cancel
            }
        }
    }
}
