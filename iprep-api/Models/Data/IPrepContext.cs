using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iprep_api.Models.Data
{
    public partial class IPrepContext : DbContext
    {
        public IPrepContext()
        {
        }

        public IPrepContext(DbContextOptions<IPrepContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnswerMaster> AnswerMasters { get; set; } = null!;
        public virtual DbSet<IprepMapper> IprepMappers { get; set; } = null!;
        public virtual DbSet<QuestionMaster> QuestionMasters { get; set; } = null!;
        public virtual DbSet<SubjectMaster> SubjectMasters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnswerMaster>(entity =>
            {
                entity.ToTable("AnswerMaster");

                entity.Property(e => e.Answer)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.LastModified).HasColumnType("datetime");
            });

            modelBuilder.Entity<IprepMapper>(entity =>
            {
                entity.ToTable("IPrepMapper");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.IprepMappers)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IPrepMapper_AnswerMaster");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.IprepMappers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IPrepMapper_QuestionMaster");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.IprepMappers)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IPrepMaster_SubjectMaster");
            });

            modelBuilder.Entity<QuestionMaster>(entity =>
            {
                entity.ToTable("QuestionMaster");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Question)
                    .HasMaxLength(2000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubjectMaster>(entity =>
            {
                entity.ToTable("SubjectMaster");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Subject)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
