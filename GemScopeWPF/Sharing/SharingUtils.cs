﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GemScopeWPF.Utils;
using SKYPE4COMLib;
using System.Windows.Forms;
using GemScopeWPF.WebcamFacade;

namespace GemScopeWPF.Sharing
{
    public class SharingUtils
    {
        static public bool OpenNewEmailWithOutlook(List<string> attachments)
        {

            OutlookEmailerLateBinding outlook = new OutlookEmailerLateBinding();
          
            outlook.SendOutlookEmail("", "Diamonds from GemScope", "These Images were generated by http://www.diamondsview.com",attachments);

            return true;
        }
        static public void OpenSkype()
        {
           // SkypeClass skype = new SkypeClass();
            //skype.la
            MessageBox.Show("To be able to use Video in skype we have to stop the camera in GemScope");

            Capture capture = Capture.GetInstance();
            capture.Stop();

            Skype skype = new Skype();
            if (!skype.Client.IsRunning)
            {
                try
                {
                    skype.Client.Start();

                }
                catch (Exception ex)
                {

                    MessageBox.Show("There was a problem opennig skype:\n" + ex.Message);

                }

            }
            else
            {
                skype.Client.Focus();
            }
            
        }


    }
}