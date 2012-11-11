using System;
using ORMapping;

namespace SOPFIM.Domain
{
    public class SuiviMessage : EditableEntity
    {
        [MappedField("MessagesID")]
        public virtual int? MessagesID { get; set; }

        [MappedField("DateMessages")]
        public virtual DateTime? DateMessages { get; set; }

        [MappedField("NomBase", 30)]
        public virtual string NomBase { get; set; }

        [MappedField("NoBloc", 8)]
        public virtual string NoBloc { get; set; }

        [MappedField("TypeBloc", 20)]
        public virtual string TypeBloc { get; set; }

        [MappedField("TimingIDI", 3)]
        public virtual string TimingIDI { get; set; }

        [MappedField("Produit", 20)]
        public virtual string Produit { get; set; }

        [MappedField("LarvesBr")]
        public virtual double? LarvesBr { get; set; }

        [MappedField("Prescription", 8)]
        public virtual string Prescription { get; set; }

        [MappedField("AppPrevue", 6)]
        public virtual string AppPrevue { get; set; }

        [MappedField("Application", 1)]
        public virtual string Application { get; set; }

        [MappedField("InterApp")]
        public virtual int? InterApp { get; set; }

        [MappedField("PrioriteEtat", 25)]
        public virtual string PrioriteEtat { get; set; }

        [MappedField("Ouvert_1jr")]
        public virtual double? Ouvert_1jr { get; set; }

        [MappedField("NbreLv30m")]
        public virtual int? NbreLv30m { get; set; }

        [MappedField("DatePrevision")]
        public virtual DateTime? DatePrevision { get; set; }

        [MappedField("DateOuverture")]
        public virtual DateTime? DateOuverture { get; set; }

        [MappedField("LvOuv30m", 50)]
        public virtual string LvOuv30m { get; set; }

        [MappedField("LvOuv80m", 50)]
        public virtual string LvOuv80m { get; set; }

        [MappedField("LvOuv100m", 50)]
        public virtual string LvOuv100m { get; set; }

        [MappedField("DateTr")]
        public virtual DateTime? DateTr { get; set; }

        [MappedField("LvTr", 50)]
        public virtual string LvTr { get; set; }

        [MappedField("EtatBloc", 20)]
        public virtual string EtatBloc { get; set; }

        [MappedField("Remarque", 255)]
        public virtual string Remarque { get; set; }
    }
}