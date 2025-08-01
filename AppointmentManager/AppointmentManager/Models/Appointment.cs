using System;
using System.Collections.Generic;

namespace AppointmentManager.Models;

public partial class appointment
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public DateOnly DateofAppoint { get; set; }

    public TimeOnly TimeofAppoint { get; set; }

    public string StatusofAppoint { get; set; }

    public virtual doctor Doctor { get; set; }

    public virtual patient Patient { get; set; }
}

