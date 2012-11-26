using Sopfim.CustomControls;

namespace Sopfim.ViewModels
{
    public interface ISopfimMainWindow
    {
        IMapControl MapControl { get; }
        object DataContext { get; set; }
        void Show();
    }
}