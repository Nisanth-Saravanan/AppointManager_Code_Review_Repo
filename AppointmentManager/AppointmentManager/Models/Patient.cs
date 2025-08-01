using System;
using System.Collections.Generic;

namespace AppointmentManager.Models;

public partial class patient
{
    public int PatientId { get; set; }

    public string PatientName { get; set; }

    public string PatientEmail { get; set; }

    public int PatientPhone { get; set; }

    public DateOnly PatientDob { get; set; }

    public string PatientGender { get; set; }

    public virtual appointment Appointment { get; set; }
}
