using System;
using System.Collections.Generic;
using AppointmentManager.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace AppointmentManager.Data;

public partial class appdbcontext : DbContext
{
    public appdbcontext()
    {
        // Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
    }

    public appdbcontext(DbContextOptions<appdbcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<appointment> Appointments { get; set; }

    public virtual DbSet<doctor> Doctors { get; set; }

    public virtual DbSet<patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;user=root;password=root;database=HospitalDb;port=3306", ServerVersion.Parse("8.0.41-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PRIMARY");

            entity.ToTable("appointments");

            entity.HasIndex(e => e.DoctorId, "DoctorId").IsUnique();

            entity.HasIndex(e => e.PatientId, "PatientId").IsUnique();

            entity.Property(e => e.StatusofAppoint).HasMaxLength(80);
            entity.Property(e => e.TimeofAppoint).HasColumnType("time");

            entity.HasOne(d => d.Doctor).WithOne(p => p.Appointment)
                .HasForeignKey<appointment>(d => d.DoctorId)
                .HasConstraintName("appointments_ibfk_2");

            entity.HasOne(d => d.Patient).WithOne(p => p.Appointment)
                .HasForeignKey<appointment>(d => d.PatientId)
                .HasConstraintName("appointments_ibfk_1");
        });

        modelBuilder.Entity<doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PRIMARY");

            entity.ToTable("doctors");

            entity.Property(e => e.Availability).HasMaxLength(80);
            entity.Property(e => e.DoctorName).HasMaxLength(80);
            entity.Property(e => e.Specialization).HasMaxLength(100);
        });

        modelBuilder.Entity<patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PRIMARY");

            entity.ToTable("patients");

            entity.Property(e => e.PatientPhone).HasMaxLength(10);
            entity.Property(e => e.PatientEmail).HasMaxLength(80);
            entity.Property(e => e.PatientGender).HasMaxLength(30);
            entity.Property(e => e.PatientName).HasMaxLength(80);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
