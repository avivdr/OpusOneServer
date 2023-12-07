using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OpusOneServerBL.Models;

public partial class OpusOneDbContext : DbContext
{
    public OpusOneDbContext()
    {
    }

    public OpusOneDbContext(DbContextOptions<OpusOneDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Composer> Composers { get; set; }

    public virtual DbSet<Forum> Forums { get; set; }

    public virtual DbSet<ForumComment> ForumComments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Work> Works { get; set; }

    public virtual DbSet<WorksUser> WorksUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=OpusOneDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comments__3214EC2792A12639");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.UploadDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Creator).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__Creato__2B3F6F97");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__PostID__2A4B4B5E");
        });

        modelBuilder.Entity<Composer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Composer__3214EC0738F5F4B2");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CompleteName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Complete_Name");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Forum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Forums__3214EC27134749C9");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.ForumDescription).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Creator).WithMany(p => p.Forums)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Forums__CreatorI__2E1BDC42");
        });

        modelBuilder.Entity<ForumComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ForumCom__3214EC270AB36D3C");

            entity.ToTable("ForumComment");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.ForumId).HasColumnName("ForumID");
            entity.Property(e => e.UploadDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Creator).WithMany(p => p.ForumComments)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ForumComm__Creat__31EC6D26");

            entity.HasOne(d => d.Forum).WithMany(p => p.ForumComments)
                .HasForeignKey(d => d.ForumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ForumComm__Forum__30F848ED");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Posts__3214EC271D6D1CAA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UploadDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Creator).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Posts__CreatorID__276EDEB3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27635F0734");

            entity.HasIndex(e => e.Username, "UC_Username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<Work>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Works__3214EC07082CC0D8");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ComposerId).HasColumnName("Composer_Id");
            entity.Property(e => e.Genre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Composer).WithMany(p => p.Works)
                .HasForeignKey(d => d.ComposerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Works__Composer___36B12243");
        });

        modelBuilder.Entity<WorksUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Works_Us__3214EC27C1129E4C");

            entity.ToTable("Works_Users");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.WorkId).HasColumnName("WorkID");

            entity.HasOne(d => d.User).WithMany(p => p.WorksUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Works_Use__UserI__5812160E");

            entity.HasOne(d => d.Work).WithMany(p => p.WorksUsers)
                .HasForeignKey(d => d.WorkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Works_Use__WorkI__59063A47");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
