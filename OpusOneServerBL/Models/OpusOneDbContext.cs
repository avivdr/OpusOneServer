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

    public virtual DbSet<Forum> Forums { get; set; }

    public virtual DbSet<ForumComment> ForumComments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WorksUser> WorksUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=OpusOneDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comments__3214EC270F3C590D");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.UploadDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Creator).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__Creato__2E1BDC42");
        });

        modelBuilder.Entity<Forum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Forums__3214EC27289A368F");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.ForumDescription).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Creator).WithMany(p => p.Forums)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Forums__CreatorI__30F848ED");
        });

        modelBuilder.Entity<ForumComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ForumCom__3214EC27B5599879");

            entity.ToTable("ForumComment");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.ForumId).HasColumnName("ForumID");
            entity.Property(e => e.UploadDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Creator).WithMany(p => p.ForumComments)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ForumComm__Creat__34C8D9D1");

            entity.HasOne(d => d.Forum).WithMany(p => p.ForumComments)
                .HasForeignKey(d => d.ForumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ForumComm__Forum__33D4B598");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Posts__3214EC276CF91C5C");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UploadDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Creator).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Posts__CreatorID__3F466844");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27744E63DB");

            entity.HasIndex(e => e.Username, "UC_Username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Pwsd).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<WorksUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Works_Us__3214EC2721E49028");

            entity.ToTable("Works_Users");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.WorkId).HasColumnName("WorkID");

            entity.HasOne(d => d.User).WithMany(p => p.WorksUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Works_Use__UserI__276EDEB3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
