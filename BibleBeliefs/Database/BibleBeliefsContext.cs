using Microsoft.EntityFrameworkCore;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BibleBeliefs.Database
{
    public partial class BibleBeliefsContext : DbContext
    {
        public BibleBeliefsContext()
        {
        }

        public BibleBeliefsContext(DbContextOptions<BibleBeliefsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Beliefs> Beliefs { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Topics> Topics { get; set; }
        public virtual DbSet<Verses> Verses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=.\\DataBase\\BibleBeliefs.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beliefs>(entity =>
            {
                entity.ToTable("beliefs");

                entity.Property(e => e.Id)
                    .HasColumnName("_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Belief)
                    .IsRequired()
                    .HasColumnName("belief");

                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Beliefs)
                    .HasForeignKey(d => d.TopicId);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookId)
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.BookTitle).IsRequired();
            });

            modelBuilder.Entity<Topics>(entity =>
            {
                entity.ToTable("topics");

                entity.Property(e => e.Id)
                    .HasColumnName("_id")
                    .ValueGeneratedOnAdd();
                    //.ValueGeneratedNever();

                entity.Property(e => e.Topic)
                    .IsRequired()
                    .HasColumnName("topic");
            });

            modelBuilder.Entity<Verses>(entity =>
            {
                entity.ToTable("verses");

                entity.Property(e => e.Id)
                    .HasColumnName("_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BeliefId).HasColumnName("belief_id");

                entity.Property(e => e.Book).HasColumnName("book");

                entity.Property(e => e.Chapter).HasColumnName("chapter");

                entity.Property(e => e.Verse)
                    .IsRequired()
                    .HasColumnName("verse");

                entity.Property(e => e.VerseEnd).HasColumnName("verseEnd");

                entity.Property(e => e.VerseStart).HasColumnName("verseStart");

                entity.HasOne(d => d.Belief)
                    .WithMany(p => p.Verses)
                    .HasForeignKey(d => d.BeliefId);

                entity.HasOne(d => d.BookNavigation)
                    .WithMany(p => p.Verses)
                    .HasForeignKey(d => d.Book)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
