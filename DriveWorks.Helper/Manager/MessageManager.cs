using DriveWorks.Forms;
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
using System.Text.RegularExpressions;

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
            foreach (var formItem in formList.Enum())
            {
                //Ignore les formes si besoin
                if (formItem.Form.Tag.ToLower().Trim().Contains(SettingsManager.GetProjectSettings(iProject).TagIgnore))
                    continue;

                var formControlList = formItem.Form.Controls;
                //Bouclage sur les controls
                foreach (var controlItem in formControlList.Enum())
                {
                    if (controlItem.Enabled == false)
                        continue;

                    var errorMessage = iProject.GetErrorMessageFromControlBase(controlItem, SettingsManager.GetProjectSettings(iProject).ErrorColorName);
                    if (errorMessage.IsNotNullAndNotEmpty())
                        result.Add("{0} -> {1} : {2}".FormatString(SplitCamelCase(formItem.Name.Remove(0,3)), SplitCamelCase(controlItem.Name.Remove(0, 3)),errorMessage));
                }
            }
            return result;
        }

        private static string SplitCamelCase(string iText)
        {
            if (iText.IsNullOrEmpty())
                return iText;

            return Regex.Replace(iText, "(\\B[A-Z])", " $1");
        }
    }
}
