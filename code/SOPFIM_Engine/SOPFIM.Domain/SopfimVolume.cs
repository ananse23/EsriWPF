using ORMapping;

namespace SOPFIM.Domain
{
    namespace BD_SOPFIMProject
    {
        public class SopfimVolume : EditableEntity
        {
            [MappedField("NomBase", 30)]
            public virtual string NomBase { get; set; }

            [MappedField("VolProgramme")]
            public virtual double? VolProgramme { get; set; }

            [MappedField("VolCumulatif")]
            public virtual double? VolCumulatif { get; set; }
        }
    }

}