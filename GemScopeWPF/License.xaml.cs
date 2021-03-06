﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GemScopeWPF.Utils;
using GemScopeActivation;
namespace GemScopeWPF
{
    /// <summary>
    /// Interaction logic for License.xaml
    /// </summary>
    public partial class License : Window
    {
        public License()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Activation activation = new Activation();
            this.ActivationCode.Text = activation.GetActivationCode();

             ActivationManager am = ActivationManager.GetInstance();

             this.Key.Text = am.GetKey();

            if (am.ActivationStatus())
            {
                this.lbl_left.Visibility = Visibility.Collapsed;
                this.Key.IsEnabled = false;
                this.RegisterButtonsPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                int daysleft = 30 - am.CountDays();


                if (daysleft > 0)
                {
                    this.lbl_left.Content = "Unregistered version you have " + daysleft.ToString() + " days left";
                }
                else
                {
                    this.lbl_left.Content = "Please register you have used your 30 days trial period";
                }


            }
            
        }

        private void Window_Closed(object sender, RoutedEventArgs e)
        {
            ActivationManager am = ActivationManager.GetInstance();
            if (am.IsExipred() && !am.ActivationStatus())
            {
                MessageBox.Show("Please register you have used your 30 days trial period\nThe application will now close");
                Application.Current.Shutdown();
            }

        }

        private void TryLater_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void VerifyKey_Click(object sender, RoutedEventArgs e)
        {
            
            Activation activation = new Activation();

            string correctkey = activation.ThisComputerKey();

            string key = this.Key.Text;

            if (key == correctkey)
            {
                SettingsManager.UpdateSetting("ActivationKey", key);
                MessageBox.Show("Activation Successful");
                this.Close();
            } 
            else 
            {
                MessageBox.Show("The key is wrong, try again...");
            }

            
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
