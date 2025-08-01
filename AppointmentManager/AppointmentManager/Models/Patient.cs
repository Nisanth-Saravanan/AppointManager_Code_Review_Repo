using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppointmentManager.Models;

public partial class Patient
{
    [Key]
    public required int PatientId { get; set; }

    public string? PatientName { get; set; }

    public string? PatientEmail { get; set; }

    public int? PatientPhone { get; set; }

    public DateOnly? PatientDob { get; set; }

    public string? PatientGender { get; set; }

    public virtual Appointment? Appointment { get; set; }
}
