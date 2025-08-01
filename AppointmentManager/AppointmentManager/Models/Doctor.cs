using System.ComponentModel.DataAnnotations;

namespace AppointmentManager.Models;

public partial class Doctor
{
    [Key]
    public required int DoctorId { get; set; }

    public string? DoctorName { get; set; }

    public string? Specialization { get; set; }

    public string? Availability { get; set; }

    public virtual Appointment? Appointment { get; set; }
}

