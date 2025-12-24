using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class MovieShopDbContext: DbContext
{
    public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options): base(options)
    {
        
    }
    
    public DbSet<Genre>  Genres { get; set; }
    public DbSet<Movie> Movie { get; set; }
    public DbSet<Trailer> Trailers { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<Cast> Casts { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Movie>(entity =>
        // {
        //     entity.Property(e => e.Title).HasColumnType("varchar(20)");
        // });
        modelBuilder.Entity<Movie>(ConfigureMovie);
        modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
        modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
        modelBuilder.Entity<Favorite>(ConfigureFavorite);
        modelBuilder.Entity<Review>(ConfigureReview);
        modelBuilder.Entity<Purchase>(ConfigurePurchase);
        modelBuilder.Entity<UserRole>(ConfigureUserRole);
    }

    private void ConfigureUserRole(EntityTypeBuilder<UserRole> ModelBuilder)
    {
        ModelBuilder.HasKey(ur => new { ur.UserId, ur.RoleId });
        ModelBuilder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
        ModelBuilder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);
    }

    private void ConfigureReview(EntityTypeBuilder<Review> ModelBuilder)
    {
        ModelBuilder.HasKey(x => new { x.UserId, x.MovieId });
        ModelBuilder.HasOne(x => x.User)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.UserId);
        ModelBuilder.HasOne(x => x.Movie)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.MovieId);
    }

    private void ConfigurePurchase(EntityTypeBuilder<Purchase> ModelBuilder)
    {
        ModelBuilder.HasKey(x => new { x.UserId, x.MovieId });
        ModelBuilder.HasOne(x => x.User)
            .WithMany(x => x.Purchases)
            .HasForeignKey(x => x.UserId);
        ModelBuilder.HasOne(x => x.Movie)
            .WithMany(x => x.Purchases)
            .HasForeignKey(x => x.MovieId);
    }

    private void ConfigureFavorite(EntityTypeBuilder<Favorite> ModelBuilder)
    {
        ModelBuilder.HasKey(x => new { x.UserId, x.MovieId });
        ModelBuilder.HasOne(x => x.Movie)
            .WithMany(x => x.Favorites)
            .HasForeignKey(x => x.MovieId);
        ModelBuilder.HasOne(x => x.User)
            .WithMany(x => x.Favorites)
            .HasForeignKey(x => x.UserId);
    }

    private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> ModelBuilder)
    {
        ModelBuilder.HasKey(x => new { x.CastId, x.MovieId });
        ModelBuilder.HasOne(x => x.Cast)
            .WithMany(x => x.MovieCasts)
            .HasForeignKey(x => x.CastId);
        ModelBuilder.HasOne(x => x.Movie)
            .WithMany(x => x.MovieCasts)
            .HasForeignKey(x => x.MovieId);
    }

    private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> ModelBuilder)
    {
        ModelBuilder.HasKey(x => new {x.MovieId, x.GenreId});
        ModelBuilder.HasOne(x   => x.Movie)
            .WithMany()
            .HasForeignKey(x => x.MovieId);
        ModelBuilder.HasOne(x  => x.Genre)
            .WithMany()
            .HasForeignKey(x => x.GenreId);
    }

    public void ConfigureMovie(EntityTypeBuilder<Movie> builder)
    {
        //fluent API
        //specify all the rules for the entity
        builder.ToTable("Movies");
        builder.HasKey(m => m.Id);
        // builder.Property(m => m.Title).HasColumnType("varchar(20)");
        builder.Property(m => m.Overview).HasColumnType("varchar(512)");
        builder.Property(m => m.Title).HasColumnType("varchar(500)");
    }
}