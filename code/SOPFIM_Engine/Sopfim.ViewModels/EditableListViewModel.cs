using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using ESRI.ArcGIS.Geodatabase;
using Esri.CommonUtils;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using SOPFIM.DataLayer;
using SOPFIM.Domain;
using Sopfim.CustomControls;
using Xceed.Wpf.Toolkit;


namespace Sopfim.ViewModels
{
    public abstract class EditableListViewModel<T> : NotificationObject where T : EditableEntity, new()
    {
        protected EditableListViewModel()
        {
            EditEffectedCommands = new List<DelegateCommand>() {BeginEdit, CancelEdit, SaveEdit};
            IsReadOnly = true;
        }

        #region properties

        private IDataService _dataService;
        public IDataService DataService
        {
            get { return _dataService; }
            set { 
                _dataService = value;
                DataListTable = _dataService.GetTable(this.TableName);
            }
        }
        public IMapControl MapService { get; set; }
        protected ITable DataListTable;
        
        

        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                _isReadOnly = value;
                RaisePropertyChanged("IsReadOnly");
                EditEffectedCommands.ForEach(x => x.RaiseCanExecuteChanged());
            }
        }

        private ObservableCollection<T> _dataList;
        public ObservableCollection<T> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                RaisePropertyChanged("DataList");
            }
        }

        private T _selectedRecord;
        public T SelectedRecord
        {
            get { return _selectedRecord; }
            set
            {
                _selectedRecord = value;
                RaisePropertyChanged("SelectedRecord");
            }
        }

        public void SetSelectedRecordAsDirty()
        {
            if (SelectedRecord != null)
                SelectedRecord.IsDirty = true;
        }

        #endregion 

        #region commands
        protected List<DelegateCommand> EditEffectedCommands;


        private DelegateCommand _beginEdit;
        public DelegateCommand BeginEdit
        {
            get
            {
                return _beginEdit ?? (_beginEdit = new DelegateCommand(() => this.IsReadOnly = false
                    , () => IsReadOnly));
            }
        }

        private DelegateCommand _saveEdit;
        public DelegateCommand SaveEdit
        {
            get
            {
                return _saveEdit ?? (_saveEdit = new DelegateCommand(SaveData, () => !this.IsReadOnly));
            }
        }

        private DelegateCommand _cancelEdit;
        public DelegateCommand CancelEdit
        {
            get
            {
                return _cancelEdit ?? (_cancelEdit = new DelegateCommand(CancelData, () => !this.IsReadOnly));
            }
        }
        #endregion 

        #region data methods
        protected virtual void SaveData()
        {
            SaveDataList();
            this.IsReadOnly = true;
        }

        protected virtual void CancelData()
        {
            QueryCurrentSelection();
            this.IsReadOnly = true;
        }

        protected virtual void SaveDataList()
        {
            var dirtyList = this.DataList.Where(x => x.IsDirty).ToList();
            DataService.Save(dirtyList, DataListTable);
            dirtyList.ForEach(x => x.IsDirty = false);
        }

        protected abstract string GenerteWhereClause();
        protected abstract string TableName { get; }
        public abstract void InitialQuery();

        public virtual void QueryCurrentSelection()
        {
            this.DataList = new ObservableCollection<T>(this.QueryListData(GenerteWhereClause()));
        }

        public virtual List<T> QueryListData(string whereClause)
        {
            try
            {
                var query = whereClause;
                Logger.Log("retreiving data with where caluse: " + query);
                var list = new ObservableCollection<T>(DataService.GeneralQuery<T>(this.DataListTable, query));
                Logger.Log("retreived " + list.Count.ToString(CultureInfo.InvariantCulture) + " records");
                var newList = list.ToList();
                newList.ForEach(x => x.IsDirty = false);
                return newList;
            }
            catch (Exception exception)
            {
                Logger.Error("Error reading the data", exception);
                throw;
            }

        }
        #endregion

       
    
    }
}