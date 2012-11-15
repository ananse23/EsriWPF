using System.Configuration;
using System.IO;
using ESRI.ArcGIS.ADF.BaseClasses;
using Microsoft.Win32;
using SOPFIM.DataLayer;
using SOPFIM.Domain;
using Xceed.Wpf.Toolkit;

namespace Sopfim.CustomControls
{
    public abstract class GenerateExcelReportCommand : BaseCommand
    {
        private readonly string _excelFile;
        private readonly IDataService _service;

        protected GenerateExcelReportCommand(string menuCaption, string excelFileName, IDataService dataService)
        {
            m_caption = menuCaption;
            m_toolTip = "generate Excel report for " + menuCaption;
            _excelFile = "ExcelReports\\" + excelFileName;
            _service = dataService;
        }

        protected GenerateExcelReportCommand(string menuCaption, string excelFileName) :
            this(menuCaption, excelFileName, new DataService(ConfigurationManager.AppSettings["fileGeodatabase"]))
        {}

        public override void OnCreate(object hook)
        {
            
        }

        public override void OnClick()
        {
            if (!File.Exists(_excelFile))
            {
                MessageBox.Show("cannot find file: " + _excelFile);
                return;
            }
            var saveDialog = new SaveFileDialog {DefaultExt = "xlsx", Filter = "Excel documents (.xlsx)|*.xlsx"};
            bool? result = saveDialog.ShowDialog();
            if (!result.Value)
                return;
            string outputFile = saveDialog.FileName;
            File.Copy(_excelFile, outputFile, true);

            var app = new ExcelExorter();
            app.Open(outputFile);
            //app.Export();
            var list =
                _service.GeneralQuery<SuiviMessage>(
                    _service.GetTable(ConfigurationManager.AppSettings["MessageTableName"]), null, "MessagesID, NoBloc");
            app.Export(list);
            app.SetVisible();
        }
    }
}