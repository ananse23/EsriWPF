using ORMapping;

namespace SOPFIM.Domain
{
    public abstract class EditableEntity : MappableFeatureViewModel
    {
        private bool _isDirty;
        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
                RaisePropertyChanged("IsDirty");
            }
        }
    }
}