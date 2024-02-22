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
            entity.HasKey(e => e.Id).HasName("PK__Comments__3214EC273954E3DF");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.UploadDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Creator).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_CreatorID");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_PostID");
        });

        modelBuilder.Entity<Composer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Composer__3214EC075B90C08B");

            entity.HasIndex(e => e.Id, "UC_Composer_Id").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__Forums__3214EC27B000A021");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.ForumDescription).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Composer).WithMany(p => p.Forums)
                .HasForeignKey(d => d.ComposerId)
                .HasConstraintName("FK_FOrums_ComposerId");

            entity.HasOne(d => d.Creator).WithMany(p => p.Forums)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Forums_CreatorID");

            entity.HasOne(d => d.Work).WithMany(p => p.Forums)
                .HasForeignKey(d => d.WorkId)
                .HasConstraintName("FK_Forums_WorkId");
        });

        modelBuilder.Entity<ForumComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ForumCom__3214EC27E77CF2D6");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.ForumId).HasColumnName("ForumID");
            entity.Property(e => e.UploadDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Creator).WithMany(p => p.ForumComments)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_ForumComments_CreatorID");

            entity.HasOne(d => d.Forum).WithMany(p => p.ForumComments)
                .HasForeignKey(d => d.ForumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ForumComments_ForumID");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Posts__3214EC27E3F55CF8");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.FileExtention)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UploadDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Composer).WithMany(p => p.Posts)
                .HasForeignKey(d => d.ComposerId)
                .HasConstraintName("FK_Posts_ComposerId");

            entity.HasOne(d => d.Creator).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Posts_CreatorID");

            entity.HasOne(d => d.Work).WithMany(p => p.Posts)
                .HasForeignKey(d => d.WorkId)
                .HasConstraintName("FK_Posts_WorkId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27080136B7");

            entity.HasIndex(e => e.Username, "UC_Username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<Work>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Works__3214EC07AD57123D");

            entity.HasIndex(e => e.Id, "UC_Work_Id").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ComposerId).HasColumnName("Composer_Id");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Composer).WithMany(p => p.Works)
                .HasForeignKey(d => d.ComposerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Works_Composer_Id");
        });

        modelBuilder.Entity<WorksUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Works_Us__3214EC2744EA710A");

            entity.ToTable("Works_Users");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.WorksUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Works_Users_UserID");

            entity.HasOne(d => d.Work).WithMany(p => p.WorksUsers)
                .HasForeignKey(d => d.WorkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Works_Users_WorkId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
