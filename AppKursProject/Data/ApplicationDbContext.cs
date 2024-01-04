using AppKursProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppKursProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<HotelService> HotelServices { get; set; } = null!;
        public virtual DbSet<ProvidedService> ProvidedServices { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomPrice> RoomPrices { get; set; } = null!;
        public virtual DbSet<RoomService> RoomServices { get; set; } = null!;
        public virtual DbSet<Stay> Stays { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("CustomerID");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PassportData)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HotelService>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("PK__HotelSer__C51BB0EA9CCD2A67");

                entity.Property(e => e.ServiceId)
                    .ValueGeneratedNever()
                    .HasColumnName("ServiceID");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<ProvidedService>(entity =>
            {
                entity.HasKey(e => e.ServiceRecordId)
                    .HasName("PK__Provided__7C38944E079BD00B");

                entity.Property(e => e.ServiceRecordId)
                    .ValueGeneratedNever()
                    .HasColumnName("ServiceRecordID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.StayId).HasColumnName("StayID");

                entity.Property(e => e.TotalCost).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ProvidedServices)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__ProvidedS__Emplo__4BAC3F29");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ProvidedServices)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__ProvidedS__Servi__4AB81AF0");

                entity.HasOne(d => d.Stay)
                    .WithMany(p => p.ProvidedServices)
                    .HasForeignKey(d => d.StayId)
                    .HasConstraintName("FK__ProvidedS__StayI__49C3F6B7");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.RoomId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoomID");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoomPrice>(entity =>
            {
                entity.HasKey(e => e.PriceId)
                    .HasName("PK__RoomPric__4957584F12F53F31");

                entity.Property(e => e.PriceId)
                    .ValueGeneratedNever()
                    .HasColumnName("PriceID");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomPrices)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__RoomPrice__RoomI__3D5E1FD2");
            });

            modelBuilder.Entity<RoomService>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("PK__RoomServ__C51BB0EA2AF27AE0");

                entity.ToTable("RoomService");

                entity.Property(e => e.ServiceId)
                    .ValueGeneratedNever()
                    .HasColumnName("ServiceID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.RoomServices)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__RoomServi__Emplo__412EB0B6");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomServices)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__RoomServi__RoomI__403A8C7D");
            });

            modelBuilder.Entity<Stay>(entity =>
            {
                entity.Property(e => e.StayId)
                    .ValueGeneratedNever()
                    .HasColumnName("StayID");

                entity.Property(e => e.CheckInDate).HasColumnType("date");

                entity.Property(e => e.CheckOutDate).HasColumnType("date");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Stays)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Stays__CustomerI__45F365D3");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Stays)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Stays__RoomID__46E78A0C");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}