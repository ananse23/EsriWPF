using System.Configuration;
using System.Globalization;
using SOPFIM.Domain;

namespace Sopfim.Reports
{
    public class PulverisationExporter : BaseExcelExportCommand<SuiviPulverisation>
    {
        protected override string MenuCaption
        {
            get { return "Rapport de pulvérisation "; }
        }

        protected override string ExcelTemplateFileName
        {
            get { return "PulverisationTemplate.xlsx"; }
        }

        protected override string TableName
        {
            get { return ConfigurationManager.AppSettings["pulverisationTableName"]; }
        }

        protected override string WhereClause
        {
            get { return null; }
        }

        protected override string OrderClause
        {
            get { return null; }
        }

        protected override void ExportToExcel(SuiviPulverisation x, int counter)
        {
            var count = counter.ToString(CultureInfo.InvariantCulture);
            Worksheet.Cells["A" + count].Value = x.DateRapport.HasValue ? x.DateRapport.Value.ToString("yyyy/MMM/dd ") +
                (x.DateRapport.Value.Hour > 11 ? "PM" : "AM") : string.Empty;
            Worksheet.Cells["B" + count].Value = x.DateRapport.HasValue ? (x.DateRapport.Value.Hour > 11 ? "PM" : "AM") : string.Empty;
            Worksheet.Cells["C" + count].Value = x.Traitement;
            Worksheet.Cells["D" + count].Value = x.Raison;
            Worksheet.Cells["E" + count].Value = x.NomBase;
            Worksheet.Cells["F" + count].Value = x.NoBloc;
            Worksheet.Cells["G" + count].Value = x.NoBloc;
            Worksheet.Cells["H" + count].Value = x.Produit;
            Worksheet.Cells["I" + count].Value = x.Application;
            Worksheet.Cells["J" + count].Value = x.DateTr.HasValue ? x.DateTr.Value.ToString("yyyy/MMM/dd ") +
                (x.DateTr.Value.Hour > 11 ? "PM" : "AM") : string.Empty;
            Worksheet.Cells["K" + count].Value = x.EtatBloc;
            Worksheet.Cells["L" + count].Value = x.LvTr;
        }
    }
}