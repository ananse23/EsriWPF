using System.IO;
using ESRI.ArcGIS.ADF.BaseClasses;
using ExcelWrapper;
using Microsoft.Win32;
using Xceed.Wpf.Toolkit;

namespace Sopfim.CustomControls
{
    public abstract class GenerateExcelReportCommand : BaseCommand
    {
        private readonly string _excelFile;
        protected Wrapper ReportFile;

        protected GenerateExcelReportCommand(string menuCaption, string excelFileName)
        {
            m_caption = menuCaption;
            m_toolTip = "generate Excel report for " + menuCaption;
            _excelFile = "ExcelReports\\" + excelFileName;
        }

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
            var saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel documents (.xlsx)|*.xlsx";
            bool? result = saveDialog.ShowDialog();
            if (!result.Value)
                return;
            string outputFile = saveDialog.FileName;
            File.Copy(_excelFile, outputFile, true);
            ReportFile = new Wrapper();
            ReportFile.Open(outputFile, true);
        }
    }
}