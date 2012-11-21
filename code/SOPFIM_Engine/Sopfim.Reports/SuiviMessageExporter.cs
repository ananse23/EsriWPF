using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using SOPFIM.Domain;

namespace Sopfim.Reports
{
    public class SuiviMessageExporter : BaseExcelExportCommand<SuiviMessage>
    {
        protected override string MenuCaption
        {
            get { return "Rapport message d'ouverture"; }
        }

        protected override string ExcelTemplateFileName
        {
            get { return "MessageOuvertureTemplate.xlsx"; }
        }

        protected override string TableName
        {
            get { return ConfigurationManager.AppSettings["MessageTableName"]; }
        }

        protected override string WhereClause
        {
            get { return null; }
        }

        protected override string OrderClause
        {
            get { return "MessagesID, NoBloc";
            }
        }

        

        protected override void ExportToExcel(SuiviMessage x, int counter)
        {
            var count = counter.ToString(CultureInfo.InvariantCulture);
            Worksheet.Cells["B" + count].Value = x.MessagesID.ToString();
            Worksheet.Cells["C" + count].Value = x.DateMessages.HasValue ? x.DateMessages.Value.ToString("yyyy/MMM/dd ") +
                (x.DateMessages.Value.Hour > 11 ? "PM" : "AM") : string.Empty;
            Worksheet.Cells["D" + count].Value = x.NomBase;
            Worksheet.Cells["F" + count].Value = x.NoBloc;
            Worksheet.Cells["G" + count].Value = x.TypeBloc;
            Worksheet.Cells["H" + count].Value = x.TimingIDI;
            Worksheet.Cells["I" + count].Value = x.Produit;
            Worksheet.Cells["J" + count].Value = x.LarvesBr;
            Worksheet.Cells["K" + count].Value = x.Prescription;
            Worksheet.Cells["L" + count].Value = x.Application;
            Worksheet.Cells["M" + count].Value = x.InterApp;
            Worksheet.Cells["N" + count].Value = x.PrioriteEtat;
        }
    }
}