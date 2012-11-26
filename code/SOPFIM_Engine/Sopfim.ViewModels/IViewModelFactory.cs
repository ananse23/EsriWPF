using Sopfim.CustomControls;
using SOPFIM.DataLayer;
using SOPFIM.Domain;

namespace Sopfim.ViewModels
{
    public interface IViewModelFactory<TList, TEntity>
        where TEntity : EditableEntity, new()
        where TList : EditableListViewModel<TEntity>, new()
    {
        IMainWindowViewModel<TList, TEntity> CreateViewModel(IDataService dataService, IMapControl mapControl);
    }
}