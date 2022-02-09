using api.Database.Models;
using api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace api.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BooksAuthors { get; set; }
        public DbSet<BookRating> BooksRatings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo((string message) =>
            {
                Console.WriteLine(message);
                Debug.WriteLine(message);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(build =>
            {
                build.HasKey(e => e.Id);

                build.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                build.HasIndex(e => e.Email)
                    .IsUnique();

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

                build.HasIndex(e => e.Name)
                    .IsUnique();

                build.HasData(new Book[]
                {
                    new Book() { Id = 1, Name = "Name 1"},
                    new Book() { Id = 2, Name = "Name 2"}
                });
            });

            modelBuilder.Entity<Author>(build =>
            {
                build.HasKey(e => e.Id);

                build.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<BookAuthor>(build =>
            {
                build.HasKey(e => new { e.BookId, e.AuthorId });

                build.HasOne(bc => bc.Book)
                    .WithMany(b => b.BooksAuthors)
                    .HasForeignKey(bc => bc.BookId);

                build.HasOne(bc => bc.Author)
                    .WithMany(c => c.BooksAuthors)
                    .HasForeignKey(bc => bc.AuthorId);
            });

            modelBuilder.Entity<BookRating>(build =>
            {
                build.HasKey(e => new { e.BookId, e.UserId });

                build.HasOne(bc => bc.Book)
                    .WithMany(b => b.BooksRatings)
                    .HasForeignKey(bc => bc.BookId);

                build.HasOne(bc => bc.User)
                    .WithMany(c => c.BooksRatings)
                    .HasForeignKey(bc => bc.UserId);

                build.Property(e => e.Grade)
                    .IsRequired();
            });
        }
    }
}