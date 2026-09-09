using Microsoft.EntityFrameworkCore;
using SupportTicket.Core.Models;

namespace SupportTicket.Infrastructure.Data
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketNote> TicketNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Email)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(c => c.Phone)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.HasIndex(c => c.Email)
                      .IsUnique();
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.ToTable("Agents");

                entity.HasKey(a => a.Id);

                entity.Property(a => a.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(a => a.Email)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(a => a.Department)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(a => a.IsActive)
                      .HasDefaultValue(true);

                entity.HasIndex(a => a.Email)
                      .IsUnique();
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Tickets");

                entity.HasKey(t => t.Id);

                entity.Property(t => t.Title)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(t => t.Description)
                      .IsRequired();

                entity.HasOne(t => t.Customer)
                      .WithMany(c => c.Tickets)
                      .HasForeignKey(t => t.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Agent)
                      .WithMany(a => a.Tickets)
                      .HasForeignKey(t => t.AgentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(t => t.CustomerId);
                entity.HasIndex(t => t.AgentId);
                entity.HasIndex(t => t.Status);
            });

            modelBuilder.Entity<TicketNote>(entity =>
            {
                entity.ToTable("TicketNotes");

                entity.HasKey(n => n.Id);

                entity.Property(n => n.NoteText)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.HasOne(n => n.Ticket)
                      .WithMany(t => t.TicketNotes)
                      .HasForeignKey(n => n.TicketId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        
    }
    }
}