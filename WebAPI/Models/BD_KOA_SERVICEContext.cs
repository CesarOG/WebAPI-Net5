﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAPI.Models
{
    public partial class BD_KOA_SERVICEContext : DbContext
    {
        public BD_KOA_SERVICEContext()
        {
        }

        public BD_KOA_SERVICEContext(DbContextOptions<BD_KOA_SERVICEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source = tcp:svsqlserver2021.database.windows.net; initial catalog = BD_KOA_SERVICE; user id = koaservice; password = S4nk4r34$$$");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Clave)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
