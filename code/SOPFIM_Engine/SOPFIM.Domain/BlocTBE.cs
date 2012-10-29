using System;
using ORMapping;

namespace SOPFIM.Domain
{
    public class BlocTBE : EditableEntity
    {
        [MappedField("NoBloc", 8)]
        public string NoBloc { get; set; }

        [MappedField("TypeBloc", 20)]
        public string TypeBloc { get; set; }

        [MappedField("Statut", 30)]
        public string Statut { get; set; }

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

        [MappedField("NoCarte", 25)]
        public string NoCarte { get; set; }

        [MappedField("SupHa")]
        public double? SupHa { get; set; }

        [MappedField("Municipalite", 50)]
        public string Municipalite { get; set; }

        [MappedField("DateAnalyse")]
        public DateTime? DateAnalyse { get; set; }

        [MappedField("DateRecolte")]
        public DateTime? DateRecolte { get; set; }

        [MappedField("MoyIDP")]
        public double? MoyIDP { get; set; }

        [MappedField("MoyIDI")]
        public double? MoyIDI { get; set; }

        [MappedField("MoyLarvesBr")]
        public double? MoyLarvesBr { get; set; }

        [MappedField("MoyLarvesBg")]
        public double? MoyLarvesBg { get; set; }

        [MappedField("MoyDefoliation")]
        public double? MoyDefoliation { get; set; }

        [MappedField("Annee")]
        public int? Annee { get; set; }
    }
}
