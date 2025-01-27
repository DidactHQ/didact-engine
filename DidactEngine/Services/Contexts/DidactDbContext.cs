using DidactCore.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DidactEngine.Services.Contexts
{
    public partial class DidactDbContext : DbContext
    {
        public DidactDbContext() { }

        public DidactDbContext(DbContextOptions<DidactDbContext> options) : base(options) { }

        public virtual DbSet<FifoQueue> FifoQueues { get; set; } = null!;

        public virtual DbSet<FifoQueueInbound> FifoQueueInbounds { get; set; } = null!;

        public virtual DbSet<Flow> Flows { get; set; } = null!;

        public virtual DbSet<FlowRun> FlowRuns { get; set; } = null!;

        public virtual DbSet<BlockRun> BlockRuns { get; set; } = null!;

        public virtual DbSet<FlowSchedule> FlowSchedules { get; set; } = null!;

        public virtual DbSet<Organization> Organizations { get; set; } = null!;

        public virtual DbSet<ScheduleType> ScheduleTypes { get; set; } = null!;

        public virtual DbSet<HyperQueue> HyperQueues { get; set; } = null!;

        public virtual DbSet<HyperQueueInbound> HyperQueueInbounds { get; set; } = null!;

        public virtual DbSet<State> States { get; set; } = null!;

        public virtual DbSet<TriggerType> TriggerTypes { get; set; } = null!;

        public virtual DbSet<Engine> Engines { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .AddUserSecrets(GetType().Assembly)
                    .AddEnvironmentVariables()
                    .Build();

                var connectionString = configuration.GetConnectionString("Didact");

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new ArgumentNullException("A connection string was not found for the Didact database.");
                }

                var connectionString2 = Environment.GetEnvironmentVariable("DidactConnectionString", EnvironmentVariableTarget.User);

                var csBuilder = new SqlConnectionStringBuilder(connectionString)
                {
                    ApplicationName = "Didact",
                    PersistSecurityInfo = true,
                    MultipleActiveResultSets = true,
                    WorkstationID = Environment.MachineName,
                    TrustServerCertificate = true
                };

                var databaseProvider = configuration.GetSection("Didact").GetValue<string>("DatabaseProvider");
                switch (databaseProvider)
                {
                    case "SqlServer":
                        optionsBuilder.UseSqlServer(connectionString2);
                        break;
                    case "PostgreSQL":
                        //optionsBuilder.UsePostgreSQL
                    default:
                        optionsBuilder.UseSqlServer(csBuilder.ConnectionString);
                        break;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.FifoQueueConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.FifoQueueInboundConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.FlowConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.FlowRunConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.FlowScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.OrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ScheduleTypeConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.HyperQueueConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.HyperQueueInboundConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.TriggerTypeConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.StateConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.EngineConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.BlockRunConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
