﻿using Microsoft.EntityFrameworkCore;
using react_Api.Database.Models;

namespace react_Api.Database
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(build =>
            {
                build.HasKey(e => e.Id);

                build.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                build.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(150);

                build.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(150);

                build.HasData(new User
                {
                    Id = 1,
                    Email = "ulyanovskiy.01@mail.ru",
                    Login = "fefrues",
                    Password = "rqhdJQb/Oi7AvOFUJsnFlo99n6F7ct0B+Sgudw7kNMM=",
                    RoleId = 1
                });
            });

            modelBuilder.Entity<Role>(build =>
            {
                build.HasKey(e => e.Id);

                build.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                build.HasData(new Role[]
                {
                    new Role() { Id = 1, Name = "Admin" },
                    new Role() { Id = 2, Name = "User" }
                });
            });

            modelBuilder.Entity<Token>(build =>
            {
                build.HasKey(e => e.Id);

                build.Property(e => e.RefreshToken)
                    .IsRequired();

                build.HasIndex(e => e.RefreshToken);
            });

            modelBuilder.Entity<Book>(build =>
            {
                build.HasKey(e => e.Id);

                build.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                build.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10000);
            });
        }
    }
}