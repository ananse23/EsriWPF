using System;
using ORMapping;

namespace SOPFIM.Domain
{
    public class BufferLvTBE : EditableEntity
    {
        [MappedField("NoBloc", 8)]
        public string NoBloc { get; set; }

        [MappedField("TypeBloc", 20)]
        public string TypeBloc { get; set; }

        [MappedField("NomBase", 30)]
        public string NomBase { get; set; }

        [MappedField("Produit", 20)]
        public string Produit { get; set; }

        [MappedField("Prescription", 8)]
        public string Prescription { get; set; }

        [MappedField("AppPrevue", 6)]
        public string AppPrevue { get; set; }

        [MappedField("Application", 1)]
        public string Application { get; set; }

        [MappedField("NoLigne")]
        public int? NoLigne { get; set; }

        [MappedField("LongueurM")]
        public double? LongueurM { get; set; }

        [MappedField("PrioriteEtat", 25)]
        public string PrioriteEtat { get; set; }

        [MappedField("Ouvert_1jr")]
        public double? Ouvert_1jr { get; set; }

        [MappedField("Traitement", 10)]
        public string Traitement { get; set; }

        [MappedField("LargeurTr", 4)]
        public string LargeurTr { get; set; }

        [MappedField("DateTr")]
        public DateTime? DateTr { get; set; }

        [MappedField("EtatBloc", 20)]
        public string EtatBloc { get; set; }

        [MappedField("LineAngle", 10)]
        public string LineAngle { get; set; }

        [MappedField("Annee")]
        public int? Annee { get; set; }
    }
}
