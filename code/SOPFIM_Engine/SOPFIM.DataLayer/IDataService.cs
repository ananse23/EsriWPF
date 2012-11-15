using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using SOPFIM.Domain;

namespace SOPFIM.DataLayer
{
    public interface IDataService
    {
        List<DomainRecord> GetDomain(string domainName);
        List<DomainRecord> GetDomain(string domainName, bool addEmptyRecord);
        IEnvelope GetBlocExtent(string blocNumber);
        ITable GetTable(string tableName);
        List<T> GeneralQuery<T>(ITable table, string whereClause) where T : EditableEntity, new();
        List<T> GeneralQuery<T>(ITable table, string whereClause, string orderClause) where T : EditableEntity, new();
        void Save<T>(List<T> listToSave, ITable table) where T: EditableEntity, new();
    }
}