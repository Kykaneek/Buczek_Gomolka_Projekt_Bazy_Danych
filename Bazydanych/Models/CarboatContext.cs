using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bazydanych.Models
{
    public partial class CarboatContext : DbContext
    {
        public CarboatContext()
        {
        }

        public CarboatContext(DbContextOptions<CarboatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<Contractor> Contractors { get; set; } = null!;
        public virtual DbSet<ContractorLocation> ContractorLocations { get; set; } = null!;
        public virtual DbSet<Loading> Loadings { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<PlannedTrace> PlannedTraces { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Trace> Traces { get; set; } = null!;
        public virtual DbSet<UnLoading> UnLoadings { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Userpermission> Userpermissions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQL2022;Database=Carboat;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.BuyDate)
                    .HasColumnType("date")
                    .HasColumnName("buy_date");

                entity.Property(e => e.Driver).HasColumnName("driver");

                entity.Property(e => e.IsAvailable).HasColumnName("is_available");

                entity.Property(e => e.IsTruck).HasColumnName("IS_truck");

                entity.Property(e => e.Loadingsize).HasColumnName("loadingsize");

                entity.Property(e => e.Mileage)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mileage");

                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("registration_number");
            });

            modelBuilder.Entity<Contractor>(entity =>
            {
                entity.HasIndex(e => e.LocationId, "IXFK_Contractors_Contractor_location");

                entity.HasIndex(e => e.LocationId, "IXFK_Contractors_Location");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Nip).HasColumnName("NIP");

                entity.Property(e => e.Pesel).HasColumnName("PESEL");
            });

            modelBuilder.Entity<ContractorLocation>(entity =>
            {
                entity.ToTable("Contractor_location");

                entity.HasIndex(e => e.ContractorId, "IXFK_Contractor_location_Contractors");

                entity.HasIndex(e => e.LocationId, "IXFK_Contractor_location_Location");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ContractorId).HasColumnName("Contractor_id");

                entity.Property(e => e.LocationId).HasColumnName("Location_id");

                entity.HasOne(d => d.Contractor)
                    .WithMany(p => p.ContractorLocations)
                    .HasForeignKey(d => d.ContractorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contractor_location_Contractors");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.ContractorLocations)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contractor_location_Location");
            });

            modelBuilder.Entity<Loading>(entity =>
            {
                entity.ToTable("Loading");

                entity.HasIndex(e => e.CarId, "IXFK_Loading_Cars");

                entity.HasIndex(e => e.TraceId, "IXFK_Loading_Trace");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CarId).HasColumnName("carID");

                entity.Property(e => e.Pickupdate)
                    .HasColumnType("datetime")
                    .HasColumnName("pickupdate");

                entity.Property(e => e.TimeToLoading).HasColumnName("time_to_loading");

                entity.Property(e => e.TraceId).HasColumnName("TraceID");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Loadings)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Loading_Cars");

                entity.HasOne(d => d.Trace)
                    .WithMany(p => p.Loadings)
                    .HasForeignKey(d => d.TraceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Loading_Trace");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Number)
                    .HasMaxLength(50)
                    .HasColumnName("number");

                entity.Property(e => e.Street)
                    .HasMaxLength(50)
                    .HasColumnName("street");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PlannedTrace>(entity =>
            {
                entity.ToTable("Planned_traces");

                entity.HasIndex(e => e.CarId, "IXFK_Planned_traces_Cars");

                entity.HasIndex(e => e.LoadingId, "IXFK_Planned_traces_Loading");

                entity.HasIndex(e => e.TraceId, "IXFK_Planned_traces_Trace");

                entity.HasIndex(e => e.UserId, "IXFK_Planned_traces_Users");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CarId).HasColumnName("car_id");

                entity.Property(e => e.LoadingId).HasColumnName("LoadingID");

                entity.Property(e => e.NextPlannedTraceId).HasColumnName("next_planned_trace_id");

                entity.Property(e => e.TraceId).HasColumnName("trace_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.PlannedTraces)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Planned_traces_Cars");

                entity.HasOne(d => d.Loading)
                    .WithMany(p => p.PlannedTraces)
                    .HasForeignKey(d => d.LoadingId)
                    .HasConstraintName("FK_Planned_traces_Loading");

                entity.HasOne(d => d.Trace)
                    .WithMany(p => p.PlannedTraces)
                    .HasForeignKey(d => d.TraceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Planned_traces_Trace");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PlannedTraces)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Planned_traces_Users");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IXFK_Roles_Users");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Driver).HasColumnName("driver");

                entity.Property(e => e.Planner).HasColumnName("planner");

                entity.Property(e => e.TmsAdmin).HasColumnName("tms_admin");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Roles_Users");
            });

            modelBuilder.Entity<Trace>(entity =>
            {
                entity.ToTable("Trace");

                entity.HasIndex(e => e.ContractorId, "IXFK_Trace_Contractors");

                entity.HasIndex(e => e.StartLocation, "IXFK_Trace_Location");

                entity.HasIndex(e => e.FinishLocation, "IXFK_Trace_Location_02");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ContractorId).HasColumnName("contractor_id");

                entity.Property(e => e.Distance).HasColumnName("distance");

                entity.Property(e => e.FinishLocation).HasColumnName("Finish_location");

                entity.Property(e => e.StartLocation).HasColumnName("Start_location");

                entity.Property(e => e.TravelTime).HasColumnName("travel_time");

                entity.HasOne(d => d.Contractor)
                    .WithMany(p => p.Traces)
                    .HasForeignKey(d => d.ContractorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trace_Contractors");

                entity.HasOne(d => d.FinishLocationNavigation)
                    .WithMany(p => p.TraceFinishLocationNavigations)
                    .HasForeignKey(d => d.FinishLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trace_Location_02");

                entity.HasOne(d => d.StartLocationNavigation)
                    .WithMany(p => p.TraceStartLocationNavigations)
                    .HasForeignKey(d => d.StartLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trace_Location");
            });

            modelBuilder.Entity<UnLoading>(entity =>
            {
                entity.ToTable("UnLoading");

                entity.HasIndex(e => e.LoadingId, "IXFK_UnLoading_Loading");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.LoadingId).HasColumnName("loading_ID");

                entity.Property(e => e.TimeToUnloading).HasColumnName("time_to_unloading");

                entity.HasOne(d => d.Loading)
                    .WithMany(p => p.UnLoadings)
                    .HasForeignKey(d => d.LoadingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnLoading_Loading");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Id, "IXFK_Users_Roles");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsDriver).HasColumnName("is_driver");

                entity.Property(e => e.IsInBase).HasColumnName("is_in_base");

                entity.Property(e => e.Licence)
                    .HasMaxLength(50)
                    .HasColumnName("licence");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.Pass)
                    .HasMaxLength(50)
                    .HasColumnName("pass");

                entity.Property(e => e.PauseTime).HasColumnName("pause_time");

                entity.Property(e => e.Phone).HasColumnName("phone");
            });

            modelBuilder.Entity<Userpermission>(entity =>
            {
                entity.ToTable("userpermission");

                entity.HasIndex(e => e.PermissionId, "IXFK_userpermission_Permission");

                entity.HasIndex(e => e.UserId, "IXFK_userpermission_Users");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.Userpermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_userpermission_Permission");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userpermissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_userpermission_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
