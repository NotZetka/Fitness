using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppUserRole, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<FitnessPlanTemplate> FitnessPlanTemplates { get; set; }
        public DbSet<ExerciseTemplate> ExerciseTemplates { get; set; }
        public DbSet<FitnessPlan> FitnessPlans { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<BodyWeight> BodyWeights { get; set; }
        public DbSet<BodyWeightRecord> BodyWeightRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FitnessPlanTemplate>()
                .HasOne(x => x.Author)
                .WithMany(x => x.FitnessPlansTemplates);

            builder.Entity<ExerciseTemplate>()
                .HasOne(x => x.FitnessPlanTemplate)
                .WithMany(x => x.Exercises);

            builder.Entity<FitnessPlan>()
                .HasMany(x => x.Exercises)
                .WithOne(x => x.FitnessPlan);

            builder.Entity<FitnessPlan>()
                .HasOne(x=>x.User)
                .WithMany(x => x.FitnessPlans);

            builder.Entity<Exercise>()
                .HasMany(x=>x.Records)
                .WithOne(x=>x.Exercise);

            builder.Entity<Message>()
                .HasOne(x => x.Sender)
                .WithMany(x => x.MessagesSend)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Message>()
                .HasOne(x => x.Receiver)
                .WithMany(x=>x.MessagesReceived)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<BodyWeight>()
                .HasOne(x=>x.User)
                .WithOne(x=>x.BodyWeight)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<BodyWeightRecord>()
                .HasOne(x => x.BodyWeight)
                .WithMany(x => x.WeightRecords)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
                .HasOne(x => x.BodyWeight)
                .WithOne(x => x.User);
        }

    }
}
