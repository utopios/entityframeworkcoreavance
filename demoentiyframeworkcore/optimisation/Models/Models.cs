using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace optimisation.Models;

public class Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom { get; set; }

        [Required]
        [MaxLength(50)]
        public string Prenom { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual ICollection<Consultation> Consultations { get; set; }
    }

    public class Medecin
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom { get; set; }

        [Required]
        [MaxLength(50)]
        public string Prenom { get; set; }

        [Required]
        [MaxLength(100)]
        public string Specialite { get; set; }

        public virtual ICollection<Consultation> Consultations { get; set; }
    }

    public class Consultation
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        [ForeignKey("Medecin")]
        public int MedecinId { get; set; }
        public virtual Medecin Medecin { get; set; }

        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }

    public class Prescription
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Dosage { get; set; }

        [ForeignKey("Consultation")]
        public int ConsultationId { get; set; }
        public virtual Consultation Consultation { get; set; }

        [ForeignKey("Medicament")]
        public int MedicamentId { get; set; }
        public virtual Medicament Medicament { get; set; }
    }

    public class Medicament
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nom { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}