using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using ORMapping;
using SOPFIM.DataLayer;
using SOPFIM.Domain;
using Sopfim.CustomControls;

namespace Sopfim.ViewModels
{
    public abstract class EditableDataViewModel<T> : NotificationObject where T : EditableEntity, new()
    {
        private IDataService _dataService;
        public IDataService DataService
        {
            get { return _dataService; }
            set { _dataService = value;
                Repository = new Repository<T>(_dataService, TableName);
            }
        }
        public IMapControl MapService { get; set; }
        public IRepository<T> Repository { get; set; }

        protected EditableDataViewModel()
        {
            IsReadOnly = true;
        }

        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                _isReadOnly = value;
                RaisePropertyChanged("IsReadOnly");
                BeginEdit.RaiseCanExecuteChanged();
                SaveEdit.RaiseCanExecuteChanged();
                CancelEdit.RaiseCanExecuteChanged();
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

        protected virtual void SaveData()
        {
            SaveDataList();
            this.IsReadOnly = true;
        }

        protected virtual void CancelData()
        {
            QueryData();
            this.IsReadOnly = true;
        }

        private DelegateCommand _cancelEdit;
        public DelegateCommand CancelEdit
        {
            get
            {
                return _cancelEdit ?? (_cancelEdit = new DelegateCommand(CancelData, () => !this.IsReadOnly));
            }
        }

        protected virtual void SaveDataList()
        {
            this.DataList.Where(x => x.IsDirty).ToList().ForEach(y =>
            {
                if (y.OID < 0)
                {
                    y.InsertInto(DataService.GetTable(TableName));
                }
                else
                    y.Update();
                y.IsDirty = false;
            });
        }

        protected abstract string WhereTemplate { get; }
        protected abstract string GenerteWhereClause();
        protected abstract string TableName { get; }
        public abstract void InitialQuery();
        


        public virtual void QueryData()
        {
            try
            {
                var query = GenerteWhereClause();
                Logger.Log(typeof(EditableDataViewModel<T>), "retreiving data with where caluse: " + query);
                this.DataList = new ObservableCollection<T>(Repository.QueryData(query));
                Logger.Log(typeof(EditableDataViewModel<T>), "retreived " +
                            DataList.Count.ToString(CultureInfo.InvariantCulture) + " records");
            }
            catch (Exception exception)
            {
                Logger.Error("Error reading the data", exception);
                throw;
            }
        }
    }
}