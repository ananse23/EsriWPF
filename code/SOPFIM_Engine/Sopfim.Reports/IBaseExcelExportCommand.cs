using SOPFIM.Domain;

namespace Sopfim.Reports
{
    public interface IBaseExcelExportCommand<out T> where T: EditableEntity, new ()
    {
        void OnClick();
    }
}