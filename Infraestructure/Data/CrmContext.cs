using CRMSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Configuration;
using TaskStatus = CRMSystem.Models.TaskStatus;

namespace CRMSystem.Data
{
    public class CrmContext : DbContext
    {

        public CrmContext(DbContextOptions<CrmContext> options)
            : base(options)
        {
            
        }

        public DbSet<CampaignTypes> CampaignTypes { get; set; }
            public DbSet<Clients> Clients { get; set; }
            public DbSet<InteractionTypes> InteractionTypes { get; set; }
            public DbSet<Users> Users { get; set; }
            public DbSet<TaskStatus> TaskStatus { get; set; }
            public DbSet<Projects> Projects { get; set; }
            public DbSet<Tasks> Tasks { get; set; }
            public DbSet<Interactions> Interactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CampaignTypes>(entity =>
            {
                entity.ToTable("CampaignTypes");
                entity.HasKey(ct => ct.Id);

                entity.Property(ct => ct.Name)
                    .HasMaxLength(25);
            });
                       
            modelBuilder.Entity<Clients>(entity =>
            {
                entity.ToTable("Clients");
                entity.HasKey(cl => cl.ClientID);

                entity.Property(cl => cl.ClientID)
                    .ValueGeneratedOnAdd();

                entity.Property(cl => cl.Name)
                    .HasMaxLength(255);

                entity.Property(cl => cl.Email)
                    .HasMaxLength(255);

                entity.Property(cl => cl.Phone)
                    .HasMaxLength(255);

                entity.Property(cl => cl.Company)
                    .HasMaxLength(100);

                entity.Property(cl => cl.Address)
                    .HasMaxLength(int.MaxValue);

                entity.Property(cl => cl.CreateDate);
            });

            modelBuilder.Entity<InteractionTypes>(entity =>
            {
                entity.ToTable("InteractionTypes");
                entity.HasKey(it => it.Id);

                entity.Property(it => it.Name)
                    .HasMaxLength(25);
            });            

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.UserID);

                entity.Property(u => u.Name)
                    .HasMaxLength(255);

                entity.Property(u => u.Email)
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TaskStatus>(entity =>
            {
                entity.ToTable("TaskStatus");
                entity.HasKey(ts => ts.Id);

                entity.Property(ts => ts.Name)
                    .HasMaxLength(25);
            });           

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.ToTable("Projects");
                entity.HasKey(p => p.ProjectID);

                entity.Property(p => p.ProjectID)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.ProjectName)
                    .HasMaxLength(255);

                entity.HasOne(p => p.CampaignTypes)
                    .WithMany()
                    .HasForeignKey(p => p.CampaignType);

                entity.HasOne(p => p.Clients)
                    .WithMany()
                    .HasForeignKey(p => p.ClientID);

                entity.HasIndex(p => p.ProjectName)
                    .IsUnique();
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.ToTable("Tasks");
                entity.HasKey(t => t.TaskID);

                entity.Property(t => t.TaskID)
                    .ValueGeneratedOnAdd();

                entity.Property(t => t.Name)
                    .HasMaxLength(int.MaxValue);

                entity.HasOne(t => t.AssignedUser)
                    .WithMany()
                    .HasForeignKey(t => t.AssignedTo);

                entity.Property(t => t.Status)
                    .HasColumnName("Status");

                entity.HasOne(t => t.TaskStatus)
                    .WithMany()
                    .HasForeignKey(t => t.Status);

                entity.HasOne(t => t.Project)
                    .WithMany(p => p.TaskStatus)
                    .HasForeignKey(t => t.ProjectID);

                entity.Property(t => t.CreateDate);
                entity.Property(t => t.UpdateDate);
            });

            modelBuilder.Entity<Interactions>(entity =>
            {
                entity.ToTable("Interactions");
                entity.HasKey(i => i.InteractionID);

                entity.Property(i => i.InteractionID)
                    .ValueGeneratedOnAdd();

                entity.Property(i => i.Notes)
                    .HasMaxLength(int.MaxValue);

                entity.HasOne(i => i.Project)
                    .WithMany(p => p.Interaction)
                    .HasForeignKey(i => i.ProjectID);

                entity.HasOne(i => i.Interactionstype)
                    .WithMany()
                    .HasForeignKey(i => i.InteractionType);
            });

            modelBuilder.Entity<Users>().HasData(
                new Users { UserID = 1, Name = "Joe Done", Email = "jdone@marketing.com" },
                new Users { UserID = 2, Name = "Nill Amstrong", Email = "namstrong@marketing.com" },
                new Users { UserID = 3, Name = "Marlyn Morales", Email = "mmorales@marketing.com" },
                new Users { UserID = 4, Name = "Antony Orué", Email = "aorue@marketing.com" },
                new Users { UserID = 5, Name = "Jazmin Fernandez", Email = "jfernandez@marketing.com" }
            );

            modelBuilder.Entity<TaskStatus>().HasData(
                new TaskStatus { Id = 1, Name = "Pending" },
                new TaskStatus { Id = 2, Name = "In Progress" },
                new TaskStatus { Id = 3, Name = "Blocked" },
                new TaskStatus { Id = 4, Name = "Done" },
                new TaskStatus { Id = 5, Name = "Cancel" }
            );

            modelBuilder.Entity<InteractionTypes>().HasData(
                new InteractionTypes { Id = 1, Name = "Initial Meeting" },
                new InteractionTypes { Id = 2, Name = "Phone Call" },
                new InteractionTypes { Id = 3, Name = "Email" },
                new InteractionTypes { Id = 4, Name = "Presentation Of Results" }
            );

            modelBuilder.Entity<CampaignTypes>().HasData(
                new CampaignTypes { Id = 1, Name = "SEO" },
                new CampaignTypes { Id = 2, Name = "PPC" },
                new CampaignTypes { Id = 3, Name = "Social Media" },
                new CampaignTypes { Id = 4, Name = "Email Marketing" }
            );

            modelBuilder.Entity<Clients>().HasData(
                new Clients { ClientID = 1, Name = "Melina Cora", Email = "melinaccora97@gmail.com", Phone = "1153311347", Company = "Tech Solutions", Address = "calle 54 4713, Buenos Aires, Argentina", CreateDate = DateTime.Parse("2024-01-15T10:30:00") },
                new Clients { ClientID = 2, Name = "Martin Cora", Email = "martin.cora.72@gmail.com", Phone = "1123545655", Company = "Empresa Global S.A.", Address = "Av. Corrientes 1234, Buenos Aires, Argentina", CreateDate = DateTime.Parse("2024-02-20T11:45:00") },
                new Clients { ClientID = 3, Name = "Cristian Valenzuela", Email = "cvalenzuela@gmail.com", Phone = "1167812345", Company = "Ingeniería y Construcciones", Address = "Bv. Mitre 150, Rosario, Argentina", CreateDate = DateTime.Parse("2024-03-10T09:20:00") },
                new Clients { ClientID = 4, Name = "Tahiel Valenzuela", Email = "tahielbenjaminvalenzuela@gmail.com", Phone = "1123235454", Company = "Consultoría Integral", Address = "San Martín 987, Mendoza, Argentina", CreateDate = DateTime.Parse("2024-04-05T14:50:00") },
                new Clients { ClientID = 5, Name = "Lorena Franco", Email = "lofranco73@gmail.com", Phone = "1154843550", Company = "Sofland Argentina", Address = "Calle Independencia 345, La Plata, Argentina", CreateDate = DateTime.Parse("2024-05-25T16:10:00") }
                );

            base.OnModelCreating(modelBuilder);

        }
    }



}
