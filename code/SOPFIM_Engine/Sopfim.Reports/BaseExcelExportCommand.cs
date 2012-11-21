using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows;
using ESRI.ArcGIS.ADF.BaseClasses;
using Microsoft.Win32;
using OfficeOpenXml;
using SOPFIM.DataLayer;
using SOPFIM.Domain;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace Sopfim.Reports
{
    public abstract class BaseExcelExportCommand<T> : BaseCommand, IBaseExcelExportCommand<T> where T: EditableEntity, new()
    {
        private readonly string _excelFile;
        private readonly IDataService _service;
        protected ExcelPackage ExcelPackage;
        protected ExcelWorksheet Worksheet;

        protected BaseExcelExportCommand(IDataService dataService)
        {
            m_caption = MenuCaption;
            m_toolTip = "generate Excel report for " + MenuCaption;
            _excelFile = "ExcelReports\\" + ExcelTemplateFileName;
            _service = dataService;
        }

        protected BaseExcelExportCommand() :
            this(new DataService(ConfigurationManager.AppSettings["fileGeodatabase"]))
        { }

        public override void OnCreate(object hook)
        {

        }

        protected abstract string MenuCaption { get; }
        protected abstract string ExcelTemplateFileName { get; }
        protected abstract string TableName { get; }
        protected abstract string WhereClause { get; }
        protected abstract string OrderClause { get; }

        public override void OnClick()
        {
            if (!File.Exists(_excelFile))
            {
                MessageBox.Show("cannot find file: " + _excelFile);
                return;
            }
            var saveDialog = new SaveFileDialog { DefaultExt = "xlsx", Filter = "Excel documents (.xlsx)|*.xlsx" };
            bool? result = saveDialog.ShowDialog();
            if (!result.Value)
                return;
            string outputFile = saveDialog.FileName;
            File.Copy(_excelFile, outputFile, true);
            var excelFile = new FileInfo(outputFile);
            using (ExcelPackage = new ExcelPackage(excelFile))
            {
                Worksheet = ExcelPackage.Workbook.Worksheets[1];
                var table = _service.GetTable(TableName);
                List<T> dataToExport = _service.GeneralQuery<T>(table, WhereClause, OrderClause);
                int counter = 2;
                dataToExport.ForEach(x =>
                                         {
                                             ExportToExcel(x, counter);
                                             counter++;
                                         });
                ExcelPackage.Save();
            }
            var application = new Application();
            var workbook = application.Workbooks.Open(outputFile);
            application.Visible = true;
        }

        protected abstract void ExportToExcel(T rowToExport, int counter);
    }
}