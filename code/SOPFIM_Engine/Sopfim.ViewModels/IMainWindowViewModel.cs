using SOPFIM.Domain;

namespace Sopfim.ViewModels
{
    public interface IMainWindowViewModel<TList, TEntity> where TEntity : EditableEntity, new()
                                                          where TList : EditableListViewModel<TEntity>, new()
    {
        void InitializeDataModel();
        TList DataViewModel { get; set; }
    }
}