using System.Collections.Generic;
using System.Globalization;
using Microsoft.Office.Interop.Excel;
using ORMapping;
using SOPFIM.Domain;

namespace Sopfim.CustomControls
{// where T : MappableFeature
    public class ExcelExorter
    {
        private Application _appliation;
        private Workbook _workbook;
        private Worksheet _filledDataSheet;

         public void Open(string path)
         {
             _appliation = new Application();
             _workbook = _appliation.Workbooks.Open(path);
             _filledDataSheet = (Worksheet)_workbook.Worksheets[1];
         }

        public void Export(List<SuiviMessage> data)
        {
            var rowCounter = 2;
            data.ForEach(x =>
                             {
                                 var count = rowCounter.ToString(CultureInfo.InvariantCulture);
                                 _filledDataSheet.Range["B" + count].Value2 = x.MessagesID;
                                 _filledDataSheet.Range["C" + count].Value2 = x.DateMessages.HasValue ? x.DateMessages.Value.ToString("yyyy/MMM/dd ") + 
                                     (x.DateMessages.Value.Hour > 11 ? "PM" : "AM") : string.Empty;
                                 _filledDataSheet.Range["D" + count].Value2 = x.NomBase;
                                 _filledDataSheet.Range["F" + count].Value2 = "'" + x.NoBloc;
                                 _filledDataSheet.Range["G" + count].Value2 = x.TypeBloc;
                                 _filledDataSheet.Range["H" + count].Value2 = x.TimingIDI;
                                 _filledDataSheet.Range["I" + count].Value2 = x.Produit;
                                 _filledDataSheet.Range["J" + count].Value2 = x.LarvesBr;
                                 _filledDataSheet.Range["K" + count].Value2 = x.Prescription;
                                 _filledDataSheet.Range["L" + count].Value2 = x.Application;
                                 _filledDataSheet.Range["M" + count].Value2 = x.InterApp;
                                 _filledDataSheet.Range["N" + count].Value2 = x.PrioriteEtat;
                                 
                                 rowCounter++;
                             });
            _workbook.Save();
        }

        public void SetVisible()
        {
            _appliation.Visible = true;
        }
    }
}