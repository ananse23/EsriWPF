using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using ESRI.ArcGIS.Geodatabase;
using Esri.CommonUtils;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using SOPFIM.DataLayer;
using SOPFIM.Domain;
using Sopfim.CustomControls;


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

        public IMapControl MapService { get; set; }
        protected ITable DataListTable;
        public ObservableCollection<T> DataList { get; set; }
        public List<BlocTBE> Blocks { get; set; }
        


    private IDataService _dataService;
        public IDataService DataService
        {
            get { return _dataService; }
            set { 
                _dataService = value;
                DataListTable = _dataService.GetTable(this.TableName);
            }
        }
        
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

        private ObservableCollection<T> _displayList;
        public ObservableCollection<T> DisplayList
        {
            get { return _displayList; }
            set { _displayList = value;
                RaisePropertyChanged("DisplayList");
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
                    , () => IsReadOnly && CanEdit()));
            }
        }

        protected virtual bool CanEdit()
        {
            return true;
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
            var dirtyList = this.DataList.Where(x => x.IsDirty).ToList();
            DataService.Save(dirtyList, DataListTable);
            dirtyList.ForEach(x => x.IsDirty = false);
            this.IsReadOnly = true;
            MapService.RefreshMap();
        }

        protected virtual void CancelData()
        {
            QueryCurrentCriteria();
            this.IsReadOnly = true;
        }

        protected abstract string GenerteWhereClause();
        protected abstract string TableName { get; }
        public abstract void InitialQuery();
        public abstract Func<T, bool> FilterCriteria { get; }

        public virtual void QueryCurrentCriteria()
        {
            this.DataList = new ObservableCollection<T>(this.QueryListData(GenerteWhereClause()));
            RefreshFilter();
        }

        public virtual void RefreshFilter()
        {
            DisplayList = new ObservableCollection<T>(DataList.Where(FilterCriteria));
        }

        protected virtual List<T> QueryListData(string whereClause)
        {
            try
            {
                var query = whereClause;
                var list = new ObservableCollection<T>(DataService.GeneralQuery<T>(this.DataListTable, query));
                Logger.Log("retreived data with the where clause: " + query + ", and the result: " + list.Count.ToString(CultureInfo.InvariantCulture) + " records");
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