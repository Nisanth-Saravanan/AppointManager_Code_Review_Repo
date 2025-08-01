using System;
using System.Collections.Generic;

namespace AppointmentManager.Models;

public partial class doctor
{
    public int DoctorId { get; set; }

    public string DoctorName { get; set; }

    public string Specialization { get; set; }

    public string Availability { get; set; }

    public virtual appointment Appointment { get; set; }
}

