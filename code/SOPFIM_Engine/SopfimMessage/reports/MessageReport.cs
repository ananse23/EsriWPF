using Sopfim.CustomControls;

namespace SopfimMessage.reports
{
    public class MessageReport : GenerateExcelReportCommand
    {
        public MessageReport(string menuCaption, string excelFileName) : base(menuCaption, excelFileName)
        {
        }

        public MessageReport()
            : base("Rapport message d'ouverture", "MessageOuvertureTemplate.xlsx")
        {}

        public override void OnClick()
        {
            base.OnClick();
            ReportFile.SetCurrentWorksheet(0);
            ReportFile.SetCellValue("B2", "ddd");
            ReportFile.Save();
        }
    }
}