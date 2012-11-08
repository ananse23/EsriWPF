using System;
using ORMapping;

namespace SOPFIM.Domain
{

    public class SuiviPulverisation : EditableEntity
    {
        [MappedField("PulverisationID")]
        public int? PulverisationID { get; set; }

        [MappedField("DateRapport")]
        public DateTime? DateRapport { get; set; }

        [MappedField("NomBase", 30)]
        public string NomBase { get; set; }

        [MappedField("NoBloc", 8)]
        public string NoBloc { get; set; }

        [MappedField("Produit", 20)]
        public string Produit { get; set; }

        [MappedField("Periode", 2)]
        public string Periode { get; set; }

        [MappedField("Traitement", 10)]
        public string Traitement { get; set; }

        [MappedField("Raison", 30)]
        public string Raison { get; set; }

        [MappedField("Application", 1)]
        public string Application { get; set; }

        [MappedField("DateTr")]
        public DateTime? DateTr { get; set; }

        [MappedField("LargeurTr", 4)]
        public string LargeurTr { get; set; }

        [MappedField("LvTr", 50)]
        public string LvTr { get; set; }

        [MappedField("EtatBloc", 20)]
        public string EtatBloc { get; set; }

        [MappedField("Remarque", 255)]
        public string Remarque { get; set; }
    }
}
