﻿using DriveWorks.Forms;
using DriveWorks.Helper.Object;
using DriveWorks.Reporting;
using DriveWorks.Specification;
using Library.Tools.Comparator;
using Library.Tools.Debug;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DriveWorks.Helper.Manager
{
    public static class MessageManager
    {

        public static void AddErrorMessage(this Project iProject, string iMessage, Exception iException)
        {
            iProject.AddMessage(iMessage);
            iProject.AddMessage(iException.Message);
            iProject.SpecificationContext.Report.WriteEntry(ReportingLevel.Minimal, ReportEntryType.Error, "Specification Management", iMessage, iException.Message, null);

            iProject.SetErrorConstant(true);
        }

        public static void AddMessage(this Project iProject, string iMessage)
        {
            TextBox theControl = null;
            iProject.Navigation.TryGetControl<TextBox>(iProject.GetProjectSettings().UserDebugControlName, ref theControl);
            var message = theControl.Text;
            if (message.IsNotNullAndNotEmpty())
                message += Environment.NewLine;
            message += iMessage;
            theControl.Text = message;
        }

        public static void SetErrorConstant(this Project iProject, bool iIsError)
        {
            ProjectConstant theConstant = null;
            iProject.Constants.TryGetConstant(iProject.GetProjectSettings().ErrorConstantName, ref theConstant);
            theConstant.Value = iIsError;
        }

        public static List<string> GetErrorMessageList(this Project iProject)
        {
            var result = new List<string>();
            var formList = iProject.Navigation.GetForms(true, true);

            //Bouclage sur les forms
            foreach (var item in formList)
            {
                //Ignore les formes si besoin
                if (item.Form.Tag.ToLower().Trim().Contains(SettingsManager.GetProjectSettings(iProject).TagIgnore))
                    continue;

                var formControlList = item.Form.Controls;
                //Bouclage sur les controls
                foreach (var controlItem in formControlList)
                {
                    if (controlItem.Enabled == false)
                        continue;

                    var errorMessage = iProject.GetErrorMessageFromControlBase(controlItem, SettingsManager.GetProjectSettings(iProject).ErrorColorName);
                    if (errorMessage.IsNotNullAndNotEmpty())
                        result.Add(errorMessage);
                }
            }
            return result;
        }

    }
}