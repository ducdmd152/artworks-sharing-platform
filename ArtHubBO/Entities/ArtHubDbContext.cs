using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ArtHubBO.Entities
{
    public partial class ArtHubDbContext : DbContext
    {
        public ArtHubDbContext()
        {
        }

        public ArtHubDbContext(DbContextOptions<ArtHubDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Artist> Artists { get; set; } = null!;
        public virtual DbSet<Bookmark> Bookmarks { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Fee> Fees { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostCategory> PostCategories { get; set; } = null!;
        public virtual DbSet<Reaction> Reactions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Subscriber> Subscribers { get; set; } = null!;
        public virtual DbSet<SystemConfig> SystemConfigs { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(this.GetConnectionString());
            }
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in this.ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified)){
                var now = DateTime.Now;
                entry.Property("UpdatedDate").CurrentValue = now;
                if(entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedDate").IsModified = false;
                }

                if(entry.State == EntityState.Added)
                {
                    entry.Property("CreatedDate").CurrentValue = now;
                }
            }

            var numberChange = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            this.ChangeTracker.Clear();
            return numberChange;
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var strConn = config.GetConnectionString("DBDefault");

            return strConn;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("account_pk");
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("artist_pk");

                entity.HasOne(d => d.EmailNavigation)
                    .WithOne(p => p.Artist)
                    .HasForeignKey<Artist>(d => d.Email)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("artist_account_FK");
            });

            modelBuilder.Entity<Bookmark>(entity =>
            {
                entity.HasOne(d => d.AccountEmailNavigation)
                    .WithMany(p => p.Bookmarks)
                    .HasForeignKey(d => d.AccountEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bookmark_account_FK");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Bookmarks)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bookmark_post_FK");
            });

            modelBuilder.Entity<Fee>(entity =>
            {
                entity.HasOne(d => d.ArtistEmailNavigation)
                    .WithMany(p => p.Fees)
                    .HasForeignKey(d => d.ArtistEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fee_artist_FK");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("image_post_FK");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasOne(d => d.ArtistEmailNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.ArtistEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("post_artist_FK");
            });

            modelBuilder.Entity<PostCategory>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany()
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("post_category_category_FK");

                entity.HasOne(d => d.Post)
                    .WithMany()
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("post_category_post_FK");
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.HasOne(d => d.AccountEmailNavigation)
                    .WithMany(p => p.Reactions)
                    .HasForeignKey(d => d.AccountEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reaction_account_FK");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Reactions)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reaction_post_FK");
            });

            modelBuilder.Entity<Subscriber>(entity =>
            {
                entity.HasOne(d => d.EmailArtistNavigation)
                    .WithMany(p => p.Subscribers)
                    .HasForeignKey(d => d.EmailArtist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subscriber_artist_FK");

                entity.HasOne(d => d.EmailUserNavigation)
                    .WithMany(p => p.Subscribers)
                    .HasForeignKey(d => d.EmailUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subscriber_account_FK");
            });

            modelBuilder.Entity<SystemConfig>(entity =>
            {
                entity.HasKey(e => e.ConfigId)
                    .HasName("system_config_pk");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasOne(d => d.Fee)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.FeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transaction_fee_FK");

                entity.HasOne(d => d.Subscriber)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.SubscriberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("transaction_subscriber_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
