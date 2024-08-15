using System;
using System.Collections.Generic;

namespace medic_system.Models
{
    public partial class AntecedentesFamiliare
    {
        public AntecedentesFamiliare()
        {
            Consulta = new HashSet<Consultum>();
        }

        public int IdAntecedente { get; set; }
        public bool? Cardiopatia { get; set; }
        public string? ObserCardiopatia { get; set; }
        public bool? Diabetes { get; set; }
        public string? ObserDiabetes { get; set; }
        public bool? EnfCardiovascular { get; set; }
        public string? ObserEnfCardiovascular { get; set; }
        public bool? Hipertension { get; set; }
        public string? ObserHipertension { get; set; }
        public bool? Cancer { get; set; }
        public string? ObserCancer { get; set; }
        public bool? Tuberculosis { get; set; }
        public string? ObserTuberculosis { get; set; }
        public bool? EnfMental { get; set; }
        public string? ObserEnfMental { get; set; }
        public bool? EnfInfecciosa { get; set; }
        public string? ObserEnfInfecciosa { get; set; }
        public bool? MalFormacion { get; set; }
        public string? ObserMalFormacion { get; set; }
        public bool? Otro { get; set; }
        public string? ObserOtro { get; set; }
        public int? ParentescoCatalogoCardiopatia { get; set; }
        public int? ParentescoCatalogoDiabetes { get; set; }
        public int? ParentescoCatalogoEnfCardiovascular { get; set; }
        public int? ParentescoCatalogoHipertension { get; set; }
        public int? ParentescoCatalogoCancer { get; set; }
        public int? ParentescoCatalogoTuberculosis { get; set; }
        public int? ParentescoCatalogoEnfMental { get; set; }
        public int? ParentescoCatalogoEnfInfecciosa { get; set; }
        public int? ParentescoCatalogoMalFormacion { get; set; }
        public int? ParentescoCatalogoOtro { get; set; }

        public virtual Catalogo? ParentescoCatalogoCancerNavigation { get; set; }
        public virtual Catalogo? ParentescoCatalogoCardiopatiaNavigation { get; set; }
        public virtual Catalogo? ParentescoCatalogoDiabetesNavigation { get; set; }
        public virtual Catalogo? ParentescoCatalogoEnfCardiovascularNavigation { get; set; }
        public virtual Catalogo? ParentescoCatalogoEnfInfecciosaNavigation { get; set; }
        public virtual Catalogo? ParentescoCatalogoEnfMentalNavigation { get; set; }
        public virtual Catalogo? ParentescoCatalogoHipertensionNavigation { get; set; }
        public virtual Catalogo? ParentescoCatalogoMalFormacionNavigation { get; set; }
        public virtual Catalogo? ParentescoCatalogoOtroNavigation { get; set; }
        public virtual Catalogo? ParentescoCatalogoTuberculosisNavigation { get; set; }
        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
