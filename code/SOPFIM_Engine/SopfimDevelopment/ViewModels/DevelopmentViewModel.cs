using System.Configuration;
using SOPFIM.Domain;
using Sopfim.ViewModels;

namespace SopfimDevelopment.ViewModels
{
    public class DevelopmentViewModel : EditableDataViewModel<SuiviDev>
    {
        protected override string WhereTemplate
        {
            get { return string.Empty; }
        }

        protected override string GenerteWhereClause()
        {
            return string.Empty;
        }

        protected override string TableName
        {
            get { return ConfigurationManager.AppSettings["DevelopmentTable"]; }
        }

        public override void InitialQuery()
        {
            throw new System.NotImplementedException();
        }
    }
}