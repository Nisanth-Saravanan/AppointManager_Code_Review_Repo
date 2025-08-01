﻿using AppointmentManager.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManager.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
        
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;user=root;password=root;database=HospitalDb;port=3306", ServerVersion.Parse("8.0.41-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PRIMARY");

            entity.ToTable("appointments");

            entity.HasIndex(e => e.DoctorId, "DoctorId").IsUnique();

            entity.HasIndex(e => e.PatientId, "PatientId").IsUnique();

            entity.Property(e => e.StatusofAppoint).HasMaxLength(80);
            entity.Property(e => e.TimeofAppoint).HasColumnType("time");

            entity.HasOne(d => d.Doctor).WithOne(p => p.Appointment)
                .HasForeignKey<Appointment>(d => d.DoctorId)
                .HasConstraintName("appointments_ibfk_2");

            entity.HasOne(d => d.Patient).WithOne(p => p.Appointment)
                .HasForeignKey<Appointment>(d => d.PatientId)
                .HasConstraintName("appointments_ibfk_1");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PRIMARY");

            entity.ToTable("doctors");

            entity.Property(e => e.Availability).HasMaxLength(80);
            entity.Property(e => e.DoctorName).HasMaxLength(80);
            entity.Property(e => e.Specialization).HasMaxLength(100);
        });

        modelBuilder.Entity<Patient>(entity =>
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
