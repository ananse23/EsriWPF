using ORMapping;

namespace SOPFIM.Domain
{
    public class EditableEntity : MappableFeatureViewModel
    {
        public EditableEntity() : base()
        {
            
        }

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