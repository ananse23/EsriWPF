using Sopfim.CustomControls;
using SOPFIM.DataLayer;
using SOPFIM.Domain;

namespace Sopfim.ViewModels
{
    public class ViewModelFactory<TList, TEntity> : IViewModelFactory<TList, TEntity>
        where TEntity : EditableEntity, new()
        where TList : EditableListViewModel<TEntity>, new()
    {


        public IMainWindowViewModel<TList, TEntity> CreateViewModel(IDataService dataService, IMapControl mapControl)
        {
            return new MainWindowViewModel<TList, TEntity>(dataService, mapControl);
        }
    }
}