using DriveWorks;
using Library.Tools.Debug;
using Library.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Service.DWProject.Data
{
    public class SearchRuleProcess
    {
        #region Public CONSTRUCTORS

        public SearchRuleProcess(Project iProject)
        {
            _Project = iProject;
        }

        #endregion

        #region Public METHODS

        public List<DriveWorks.Utility.SearchItem> GetSearchResult(string iSearchString)
        {
            var searchProcess = _Project.Utility.CreateNameSearchProcess(iSearchString);
            _SearchFinished = false;
            searchProcess.Finished += SearchProcess_Finished;
            searchProcess.Search();

            while (_SearchFinished == false)
                Thread.Sleep(100);

            return searchProcess.FoundItems.Enum().ToList();
        }

        #endregion

        #region Private FIELDS

        private Project _Project;
        private bool _SearchFinished;

        #endregion

        #region Private METHODS

        private void SearchProcess_Finished(object sender, EventArgs e)
        {
            _SearchFinished = true;
        }

        #endregion
    }
}