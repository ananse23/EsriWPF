using System.Collections.Generic;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ORMapping;
using SOPFIM.Domain;

namespace SOPFIM.DataLayer
{
    public class Repository<T> : IRepository<T> where T : EditableEntity, new()
    {
        private readonly IDataService _service;
        private readonly ITable _table;
        private readonly FileGDBWorkspaceFactory _workspaceFactory;
        private readonly IFeatureWorkspace _featureWorkspace;


        public Repository(IDataService service, string tableName)
        {
            _service = service;
            _table = _service.GetTable(tableName);
        }

        public List<T> QueryData(string whereClause)
        {
            return _service.GeneralQuery<T>(this._table, whereClause);
        }

        public void Save(List<T> listToSave)
        {
            throw new System.NotImplementedException();
        }
    }
}