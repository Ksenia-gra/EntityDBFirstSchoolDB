using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab2_SchoolDataBase;

public partial class SchoolDbContext : DbContext
{
    public SchoolDbContext()
    {
    }

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<KindsOfClass> KindsOfClasses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\Users\\kseniagrafova\\SchoolDB.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.ToTable("classes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassLetter)
                .HasColumnType("CHAR")
                .HasColumnName("class_letter");
            entity.Property(e => e.ClassMainteacherId).HasColumnName("class_mainteacher_id");
            entity.Property(e => e.CreateYear).HasColumnName("create_year");
            entity.Property(e => e.KindId).HasColumnName("kind_id");
            entity.Property(e => e.StudentsCount).HasColumnName("students_count");
            entity.Property(e => e.StudyYear).HasColumnName("study_year");

            entity.HasOne(d => d.Kind).WithMany(p => p.Classes).HasForeignKey(d => d.KindId);
        });

        modelBuilder.Entity<KindsOfClass>(entity =>
        {
            entity.ToTable("kinds_of_classes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("students");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdditInfo).HasColumnName("addit_info");
            entity.Property(e => e.Adress).HasColumnName("adress");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.FatherFio).HasColumnName("father_fio");
            entity.Property(e => e.Fio).HasColumnName("fio");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.MotherFio).HasColumnName("mother_fio");

            entity.HasOne(d => d.Class).WithMany(p => p.Students).HasForeignKey(d => d.ClassId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
