using System.Runtime.InteropServices;
using EquinoxeExtend.Shared.Object.Release;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;


namespace Service.PDCExcelFile.Data
{
   

    public class DataService
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        #region Public METHODS

        public List<ExternalProject> GetExternalProjectListFromFile(string iFilePath)
        {
            Process excelProcess = null;
            try
            {
                Excel.Application excel = null;
                Excel.Workbook wkb = null;

                //Ouverture par process piur killer
                excel = new Excel.Application();
                uint processId;
                IntPtr h = new IntPtr(excel.Hwnd);
                GetWindowThreadProcessId(h, out processId);
                excelProcess = Process.GetProcessById((int)processId);

                wkb = OpenBook(excel, iFilePath, true, false, false);
                var sheet = wkb.Sheets[1] as Excel.Worksheet;

                var rowIndex = 3;
                var result = new List<ExternalProject>();

                //bouclage sur les lignes
                while (sheet.Cells[rowIndex, PROJECTNUMBERCOLUMNINDEX].Value != null)
                {
                    var newExternalProject = new ExternalProject();

                    //Priority
                    double? priority = sheet.Cells[rowIndex, PRIORITYCOLUMNINDEX].Value;
                    newExternalProject.Priority = (priority!= null) ? (int?)Convert.ToInt32(priority) : null;

                    //project Number
                    newExternalProject.ProjectNumber = sheet.Cells[rowIndex, PROJECTNUMBERCOLUMNINDEX].Value;

                    //Status
                    string theStatus = sheet.Cells[rowIndex, STATUSCOLUMNINDEX].Value;
                    if (theStatus.IsNullOrEmpty())
                        newExternalProject.Status = EquinoxeExtend.Shared.Enum.ExternalProjectStatusEnum.Waiting;
                    else if (theStatus == "En cours")
                        newExternalProject.Status = EquinoxeExtend.Shared.Enum.ExternalProjectStatusEnum.InProgress;
                    else if (theStatus == "Clôturé")
                        newExternalProject.Status = EquinoxeExtend.Shared.Enum.ExternalProjectStatusEnum.Completed;
                    else if (theStatus == "Annulé")
                        newExternalProject.Status = EquinoxeExtend.Shared.Enum.ExternalProjectStatusEnum.Canceled;
                    else
                        throw new Exception("Le statut n'est pas supporté");

                    //Nom projet
                    newExternalProject.ProjectName = sheet.Cells[rowIndex, PROJECTNAMECOLUMNINDEX].Value;

                    //Description
                    newExternalProject.Description = sheet.Cells[rowIndex, DESCRIPTIONCOLUMNINDEX].Value;

                    //Pilote
                    newExternalProject.Pilote = sheet.Cells[rowIndex, PILOTECOLUMNINDEX].Value;

                    //BE
                    string beImpacted = sheet.Cells[rowIndex, BECOLUMNINDEX].Value;
                    newExternalProject.BEImpacted = (beImpacted.IsNullOrEmpty()) ? false : true;

                    //Date cloture prev
                    DateTime? dateInit = sheet.Cells[rowIndex, DATEOBJECTIFENDINITCOLUMNINDEX].Value;
                    if (dateInit != null)
                    {
                        newExternalProject.DateObjectiveEnd = (DateTime?)Convert.ToDateTime(dateInit);
                    }
                    else
                    {
                        DateTime? datePrev = sheet.Cells[rowIndex, DATEOBJECTIFENDPREVCOLUMNINDEX].Value;
                        newExternalProject.DateObjectiveEnd = datePrev;
                    }

                    result.Add(newExternalProject);

                    rowIndex += 1;
                }

                wkb.Close(0);
                excel.Quit();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                excelProcess.Close();
            }
        }

        #endregion

        #region Private FIELDS

        private const int PRIORITYCOLUMNINDEX = 1;

        private const int PROJECTNUMBERCOLUMNINDEX = 2;

        private const int STATUSCOLUMNINDEX = 4;

        private const int PROJECTNAMECOLUMNINDEX = 5;

        private const int DESCRIPTIONCOLUMNINDEX = 6;

        private const int PILOTECOLUMNINDEX = 7;

        private const int DATEOBJECTIFENDINITCOLUMNINDEX = 9;

        private const int DATEOBJECTIFENDPREVCOLUMNINDEX = 10;

        private const int BECOLUMNINDEX = 31;

        #endregion

        #region Private METHODS

        private static Excel.Workbook OpenBook(Excel.Application excelInstance, string fileName, bool readOnly, bool editable,
                                                                                bool updateLinks)
        {
            Excel.Workbook book = excelInstance.Workbooks.Open(
                fileName, updateLinks, readOnly,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, editable, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);
            return book;
        }

        private static void ReleaseRCM(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch
            {
            }
            finally
            {
                o = null;
            }
        }

        #endregion
    }
}