using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FC_NDIS.Models
{
    public partial class NDISINT18Apr2021Context : DbContext
    {
        public NDISINT18Apr2021Context()
        {
        }
        private readonly IntegrationAppSettings _integrationAppSettings;
        public NDISINT18Apr2021Context(IntegrationAppSettings integrationAppSettings)
        {
            this._integrationAppSettings = integrationAppSettings;
        }

        public NDISINT18Apr2021Context(DbContextOptions<NDISINT18Apr2021Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; }
        public virtual DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        public virtual DbSet<BillingCustomerTrip> BillingCustomerTrips { get; set; }
        public virtual DbSet<BillingDriverTrip> BillingDriverTrips { get; set; }
        public virtual DbSet<BillingLinesNew> BillingLinesNews { get; set; }
        public virtual DbSet<BillingRate> BillingRates { get; set; }
        public virtual DbSet<ConnxCc> ConnxCcs { get; set; }
        public virtual DbSet<CostCenterGrid> CostCenterGrids { get; set; }
        public virtual DbSet<CostCenterType> CostCenterTypes { get; set; }
        public virtual DbSet<CostCentre> CostCentres { get; set; }
        public virtual DbSet<Counter> Counters { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerPauseResumeTrip> CustomerPauseResumeTrips { get; set; }
        public virtual DbSet<CustomerServiceLine> CustomerServiceLines { get; set; }
        public virtual DbSet<CustomerServiceLinesError> CustomerServiceLinesErrors { get; set; }
        public virtual DbSet<CustomerStatus> CustomerStatuses { get; set; }
        public virtual DbSet<CustomerTrip> CustomerTrips { get; set; }
        public virtual DbSet<CustomerTripCategory> CustomerTripCategories { get; set; }
        public virtual DbSet<CustomerTripLocation> CustomerTripLocations { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<DriverLocation> DriverLocations { get; set; }
        public virtual DbSet<DriverPauseResumeTrip> DriverPauseResumeTrips { get; set; }
        public virtual DbSet<DriverType> DriverTypes { get; set; }
        public virtual DbSet<GridList> GridLists { get; set; }
        public virtual DbSet<GridSetting> GridSettings { get; set; }
        public virtual DbSet<Hash> Hashes { get; set; }
        public virtual DbSet<HistoricalCustomerTrip> HistoricalCustomerTrips { get; set; }
        public virtual DbSet<HistoricalDriverTrip> HistoricalDriverTrips { get; set; }
        public virtual DbSet<IntegrationActivity> IntegrationActivities { get; set; }
        public virtual DbSet<IntegrationActivityList> IntegrationActivityLists { get; set; }
        public virtual DbSet<IntegrationActivityStatus> IntegrationActivityStatuses { get; set; }
        public virtual DbSet<ItemOverclaimStatus> ItemOverclaimStatuses { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobParameter> JobParameters { get; set; }
        public virtual DbSet<JobQueue> JobQueues { get; set; }
        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<RoleAccessLevel> RoleAccessLevels { get; set; }
        public virtual DbSet<RoleGroup> RoleGroups { get; set; }
        public virtual DbSet<SalesForceService> SalesForceServices { get; set; }
        public virtual DbSet<SalesforceRate> SalesforceRates { get; set; }
        public virtual DbSet<SalesforceRateType> SalesforceRateTypes { get; set; }
        public virtual DbSet<Schema> Schemas { get; set; }
        public virtual DbSet<Server> Servers { get; set; }
        public virtual DbSet<ServiceAgreementStatus> ServiceAgreementStatuses { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
        public virtual DbSet<SharedTrip> SharedTrips { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<TripCoordinate> TripCoordinates { get; set; }
        public virtual DbSet<TripCoordinatesArchive> TripCoordinatesArchives { get; set; }
        public virtual DbSet<TripStatus> TripStatuses { get; set; }
        public virtual DbSet<TripType> TripTypes { get; set; }
        public virtual DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public virtual DbSet<UserGridSetting> UserGridSettings { get; set; }
        public virtual DbSet<UserRoleNew> UserRoleNews { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleCategory> VehicleCategories { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(this._integrationAppSettings.AppConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK_HangFire_CounterAggregated");

                entity.ToTable("AggregatedCounter", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_AggregatedCounter_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<ApplicationSetting>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK__Applicat__C41E0288A3CE1792");

                entity.ToTable("ApplicationSetting");

                entity.Property(e => e.Key).HasMaxLength(200);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<BillingCustomerTrip>(entity =>
            {
                entity.HasKey(e => e.CustomerTripId)
                    .HasName("PK__BillingC__1C046D8824DBD0B2");

                entity.ToTable("BillingCustomerTrip");

                entity.Property(e => e.CustomerTripId)
                    .ValueGeneratedNever()
                    .HasColumnName("CustomerTripID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerKm).HasColumnName("CustomerKM");

                entity.Property(e => e.CustomerTripDescription).HasMaxLength(500);

                entity.Property(e => e.EndAddress).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.EndGps)
                    .HasMaxLength(500)
                    .HasColumnName("EndGPS");

                entity.Property(e => e.EndKm).HasColumnName("EndKM");

                entity.Property(e => e.IsCancelled).HasColumnName("isCancelled");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OnHold).HasDefaultValueSql("((0))");

                entity.Property(e => e.PauseKm).HasColumnName("PauseKM");

                entity.Property(e => e.ResumeKm).HasColumnName("ResumeKM");

                entity.Property(e => e.SharedEndKm).HasColumnName("SharedEndKM");

                entity.Property(e => e.SharedKm).HasColumnName("SharedKM");

                entity.Property(e => e.SharedStartKm).HasColumnName("SharedStartKM");

                entity.Property(e => e.SharedTripId).HasColumnName("SharedTripID");

                entity.Property(e => e.StartAddress).HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StartGps)
                    .HasMaxLength(500)
                    .HasColumnName("StartGPS");

                entity.Property(e => e.StartKm).HasColumnName("StartKM");

                entity.Property(e => e.TripId).HasColumnName("TripID");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.BillingCustomerTrips)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK_BillingCustomerTrip_TripID");
            });

            modelBuilder.Entity<BillingDriverTrip>(entity =>
            {
                entity.HasKey(e => e.TripId)
                    .HasName("PK__BillingD__51DC711EE11513D7");

                entity.ToTable("BillingDriverTrip");

                entity.Property(e => e.TripId)
                    .ValueGeneratedNever()
                    .HasColumnName("TripID");

                entity.Property(e => e.BillingLineId).HasColumnName("BillingLineID");

                entity.Property(e => e.ConvertedToBillable).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.EndAddress).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.EndGps)
                    .HasMaxLength(500)
                    .HasColumnName("EndGPS");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.JobNumber).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OnHold).HasDefaultValueSql("((0))");

                entity.Property(e => e.SalesForceUserId)
                    .HasMaxLength(50)
                    .HasColumnName("SalesForceUserID");

                entity.Property(e => e.SoloKm).HasColumnName("SoloKM");

                entity.Property(e => e.StartAddress).HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StartGps)
                    .HasMaxLength(500)
                    .HasColumnName("StartGPS");

                entity.Property(e => e.TotalKm).HasColumnName("TotalKM");

                entity.Property(e => e.TripCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TripDescription).HasMaxLength(500);

                entity.Property(e => e.TripTypeId).HasColumnName("TripTypeID");

                entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

                entity.Property(e => e.VehicleRegistrationNumber).HasMaxLength(100);
            });

            modelBuilder.Entity<BillingLinesNew>(entity =>
            {
                entity.HasKey(e => e.BillingId)
                    .HasName("PK__BillingL__F1656D130A2A2367");

                entity.ToTable("BillingLinesNew");

                entity.Property(e => e.BillingId).HasColumnName("BillingID");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerTripId).HasColumnName("CustomerTripID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTransferred).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.LumaryId)
                    .HasMaxLength(255)
                    .HasColumnName("LumaryID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SalesForceBillingId)
                    .HasMaxLength(100)
                    .HasColumnName("SalesForceBillingID");

                entity.Property(e => e.SalesforceRatesId).HasColumnName("SalesforceRatesID");

                entity.Property(e => e.SentToSalesForceDescription).HasMaxLength(750);

                entity.Property(e => e.ServiceAgreementId)
                    .HasMaxLength(100)
                    .HasColumnName("ServiceAgreementID");

                entity.Property(e => e.ServiceAgreementItemId)
                    .HasMaxLength(50)
                    .HasColumnName("ServiceAgreementItemID");

                entity.Property(e => e.SiteGlcode)
                    .HasMaxLength(50)
                    .HasColumnName("SiteGLCode");

                entity.Property(e => e.TripId).HasColumnName("TripID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ValidatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CustomerTrip)
                    .WithMany(p => p.BillingLinesNews)
                    .HasForeignKey(d => d.CustomerTripId)
                    .HasConstraintName("FK_BillingLinesNew_CustomerTripID");

                entity.HasOne(d => d.SalesforceRates)
                    .WithMany(p => p.BillingLinesNews)
                    .HasForeignKey(d => d.SalesforceRatesId)
                    .HasConstraintName("FK_BillingLinesNew_SalesforceRateID");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.BillingLinesNews)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK_BillingLinesNew_TripID");
            });

            modelBuilder.Entity<BillingRate>(entity =>
            {
                entity.HasKey(e => e.RateId)
                    .HasName("PK__BillingR__58A7CCBC3C8EC3A2");

                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<ConnxCc>(entity =>
            {
                entity.ToTable("ConnxCC");

                entity.Property(e => e.ConnxCcid).HasColumnName("ConnxCCID");

                entity.Property(e => e.ConnxCcvalue)
                    .HasMaxLength(255)
                    .HasColumnName("ConnxCCValue");

                entity.Property(e => e.CostCentreId).HasColumnName("CostCentreID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CostCentre)
                    .WithMany(p => p.ConnxCcs)
                    .HasForeignKey(d => d.CostCentreId)
                    .HasConstraintName("FK_Cost_Centre_ConnxCC");
            });

            modelBuilder.Entity<CostCenterGrid>(entity =>
            {
                entity.ToTable("CostCenterGrid");

                entity.Property(e => e.CostCenterGridId).HasColumnName("CostCenterGridID");

                entity.Property(e => e.ConnXccorderNo).HasColumnName("ConnXCCOrderNo");

                entity.Property(e => e.FormId).HasColumnName("FormID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CostCenterType>(entity =>
            {
                entity.ToTable("CostCenterType");

                entity.Property(e => e.CostCenterTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("CostCenterTypeID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(100);
            });

            modelBuilder.Entity<CostCentre>(entity =>
            {
                entity.Property(e => e.CostCentreId).HasColumnName("CostCentreID");

                entity.Property(e => e.BusinessUnit)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ConnXcc)
                    .HasMaxLength(255)
                    .HasColumnName("ConnXCC");

                entity.Property(e => e.CostCentre1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("CostCentre");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ISdeleted).HasColumnName("iSDeleted");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Counter", "HangFire");

                entity.HasIndex(e => e.Key, "CX_HangFire_Counter")
                    .IsClustered();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustId);

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.LumaryId)
                    .HasMaxLength(255)
                    .HasColumnName("LumaryID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OnHold).HasDefaultValueSql("((0))");

                entity.Property(e => e.PostalCode).HasMaxLength(50);

                entity.Property(e => e.State).HasMaxLength(100);

                entity.Property(e => e.Street).HasMaxLength(100);

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_Customer_Status_ID");
            });

            modelBuilder.Entity<CustomerPauseResumeTrip>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CustomerPauseResumeTrip");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerTripId).HasColumnName("CustomerTripID");

                entity.Property(e => e.Gps)
                    .HasMaxLength(256)
                    .HasColumnName("GPS");

                entity.Property(e => e.IsPaused).HasColumnName("isPaused");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.CustomerTrip)
                    .WithMany()
                    .HasForeignKey(d => d.CustomerTripId)
                    .HasConstraintName("FK_CustomerTrip_ID");
            });

            modelBuilder.Entity<CustomerServiceLine>(entity =>
            {
                entity.Property(e => e.CustomerServiceLineId).HasColumnName("CustomerServiceLineID");

                entity.Property(e => e.CategoryItemId)
                    .HasMaxLength(50)
                    .HasColumnName("CategoryItemID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ServiceAgreementCustomerId).HasColumnName("ServiceAgreementCustomerID");

                entity.Property(e => e.ServiceAgreementEndDate).HasColumnType("date");

                entity.Property(e => e.ServiceAgreementFundingManagement)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ServiceAgreementFundingType)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ServiceAgreementId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ServiceAgreementID");

                entity.Property(e => e.ServiceAgreementItemId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ServiceAgreementItemID");

                entity.Property(e => e.ServiceAgreementItemName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ServiceAgreementName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ServiceId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ServiceID");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.SiteGlcode)
                    .HasMaxLength(50)
                    .HasColumnName("SiteGLCode");

                entity.Property(e => e.SiteId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SiteID");

                entity.Property(e => e.SiteName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SiteServiceProgramId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SiteServiceProgramID");

                entity.Property(e => e.TransportServiceId)
                    .HasMaxLength(75)
                    .HasColumnName("TransportServiceID");

                entity.Property(e => e.TravelServiceId)
                    .HasMaxLength(75)
                    .HasColumnName("TravelServiceID");

                entity.HasOne(d => d.ItemOverclaimNavigation)
                    .WithMany(p => p.CustomerServiceLines)
                    .HasForeignKey(d => d.ItemOverclaim)
                    .HasConstraintName("FK_Customer_Service_Line_Item_Claim_ID");

                entity.HasOne(d => d.ServiceAgreementCustomer)
                    .WithMany(p => p.CustomerServiceLines)
                    .HasForeignKey(d => d.ServiceAgreementCustomerId)
                    .HasConstraintName("FK_ServiceAgreementCustomerID");

                entity.HasOne(d => d.ServiceAgreementStatusNavigation)
                    .WithMany(p => p.CustomerServiceLines)
                    .HasForeignKey(d => d.ServiceAgreementStatus)
                    .HasConstraintName("FK_Customer_Service_Line_Status_ID");
            });

            modelBuilder.Entity<CustomerServiceLinesError>(entity =>
            {
                entity.HasKey(e => e.CustomerServiceLineId)
                    .HasName("PK__Customer__8A5E749C91C331C2");

                entity.ToTable("CustomerServiceLinesError");

                entity.Property(e => e.CustomerServiceLineId).HasColumnName("CustomerServiceLineID");

                entity.Property(e => e.CategoryItemId)
                    .HasMaxLength(50)
                    .HasColumnName("CategoryItemID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ServiceAgreementCustomerId).HasColumnName("ServiceAgreementCustomerID");

                entity.Property(e => e.ServiceAgreementEndDate).HasColumnType("date");

                entity.Property(e => e.ServiceAgreementFundingManagement)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ServiceAgreementFundingType)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ServiceAgreementId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ServiceAgreementID");

                entity.Property(e => e.ServiceAgreementItemId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ServiceAgreementItemID");

                entity.Property(e => e.ServiceAgreementItemName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ServiceAgreementName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ServiceId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ServiceID");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.SiteGlcode)
                    .HasMaxLength(50)
                    .HasColumnName("SiteGLCode");

                entity.Property(e => e.SiteId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SiteID");

                entity.Property(e => e.SiteName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SiteServiceProgramId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SiteServiceProgramID");

                entity.Property(e => e.TransportServiceId)
                    .HasMaxLength(75)
                    .HasColumnName("TransportServiceID");

                entity.Property(e => e.TravelServiceId)
                    .HasMaxLength(75)
                    .HasColumnName("TravelServiceID");
            });

            modelBuilder.Entity<CustomerStatus>(entity =>
            {
                entity.ToTable("CustomerStatus");

                entity.Property(e => e.CustomerStatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("CustomerStatusID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(75);
            });

            modelBuilder.Entity<CustomerTrip>(entity =>
            {
                entity.ToTable("CustomerTrip");

                entity.Property(e => e.CustomerTripId).HasColumnName("CustomerTripID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerKm).HasColumnName("CustomerKM");

                entity.Property(e => e.CustomerTripDescription).HasMaxLength(500);

                entity.Property(e => e.EndAddress).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.EndGps)
                    .HasMaxLength(500)
                    .HasColumnName("EndGPS");

                entity.Property(e => e.EndKm).HasColumnName("EndKM");

                entity.Property(e => e.IsCancelled).HasColumnName("isCancelled");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OnHold).HasDefaultValueSql("((0))");

                entity.Property(e => e.PauseKm).HasColumnName("PauseKM");

                entity.Property(e => e.ResumeKm).HasColumnName("ResumeKM");

                entity.Property(e => e.SharedEndKm).HasColumnName("SharedEndKM");

                entity.Property(e => e.SharedKm).HasColumnName("SharedKM");

                entity.Property(e => e.SharedStartKm).HasColumnName("SharedStartKM");

                entity.Property(e => e.SharedTripId).HasColumnName("SharedTripID");

                entity.Property(e => e.StartAddress).HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StartGps)
                    .HasMaxLength(500)
                    .HasColumnName("StartGPS");

                entity.Property(e => e.StartKm).HasColumnName("StartKM");

                entity.Property(e => e.TripId).HasColumnName("TripID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerTrips)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Customer_Trip_Customer_ID");

                entity.HasOne(d => d.CustomerTripCategoryNavigation)
                    .WithMany(p => p.CustomerTrips)
                    .HasForeignKey(d => d.CustomerTripCategory)
                    .HasConstraintName("FK_CustomerTrip_CategoryID");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.CustomerTrips)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK_CustomerTrip_TripID");

                entity.HasOne(d => d.TripStatusNavigation)
                    .WithMany(p => p.CustomerTrips)
                    .HasForeignKey(d => d.TripStatus)
                    .HasConstraintName("FK_CustomerTrip_TripStatus");
            });

            modelBuilder.Entity<CustomerTripCategory>(entity =>
            {
                entity.ToTable("CustomerTripCategory");

                entity.Property(e => e.CustomerTripCategoryId).HasColumnName("CustomerTripCategoryID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Value).HasMaxLength(75);
            });

            modelBuilder.Entity<CustomerTripLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__Customer__E7FEA4777FE561E6");

                entity.ToTable("CustomerTripLocation");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CustomerTripId).HasColumnName("CustomerTripID");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasMaxLength(75);

                entity.Property(e => e.Longitude).HasMaxLength(75);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Department).HasMaxLength(100);

                entity.Property(e => e.EmployeeCode).HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.IsTerminated)
                    .HasColumnName("isTerminated")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.JobDescription).HasMaxLength(500);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.ManagerName).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Otp).HasColumnName("OTP");

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.PreferedName).HasMaxLength(256);

                entity.Property(e => e.SalesForceUserId)
                    .HasMaxLength(50)
                    .HasColumnName("SalesForceUserID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.HasOne(d => d.CostCenterNavigation)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.CostCenter)
                    .HasConstraintName("FK_Cost_Centre_Drivers");
            });

            modelBuilder.Entity<DriverLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__DriverLo__E7FEA47732D11F4A");

                entity.ToTable("DriverLocation");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.DriverId)
                    .HasMaxLength(100)
                    .HasColumnName("DriverID");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasMaxLength(75);

                entity.Property(e => e.Longitude).HasMaxLength(75);

                entity.Property(e => e.TripId).HasColumnName("TripID");
            });

            modelBuilder.Entity<DriverPauseResumeTrip>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DriverPauseResumeTrip");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Gps)
                    .HasMaxLength(256)
                    .HasColumnName("GPS");

                entity.Property(e => e.IsPaused).HasColumnName("isPaused");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.TripId).HasColumnName("TripID");

                entity.HasOne(d => d.Trip)
                    .WithMany()
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK_DriverTrip_ID");
            });

            modelBuilder.Entity<DriverType>(entity =>
            {
                entity.ToTable("DriverType");

                entity.Property(e => e.DriverTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("DriverTypeID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(75);
            });

            modelBuilder.Entity<GridList>(entity =>
            {
                entity.HasKey(e => e.GridId)
                    .HasName("PK__GridList__FB2F3BAECBF7061E");

                entity.ToTable("GridList");

                entity.Property(e => e.GridId).HasColumnName("GridID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GridName).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<GridSetting>(entity =>
            {
                entity.Property(e => e.GridSettingId).HasColumnName("GridSettingID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GridId).HasColumnName("GridID");

                entity.Property(e => e.GridOrderSetting).HasColumnType("xml");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Grid)
                    .WithMany(p => p.GridSettings)
                    .HasForeignKey(d => d.GridId)
                    .HasConstraintName("FK_Grid_List_GridID");
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Field })
                    .HasName("PK_HangFire_Hash");

                entity.ToTable("Hash", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Hash_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Field).HasMaxLength(100);
            });

            modelBuilder.Entity<HistoricalCustomerTrip>(entity =>
            {
                entity.HasKey(e => e.CustomerTripId)
                    .HasName("PK__Historic__1C046D88A0AD0F49");

                entity.ToTable("HistoricalCustomerTrip");

                entity.Property(e => e.CustomerTripId)
                    .ValueGeneratedNever()
                    .HasColumnName("CustomerTripID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerKm).HasColumnName("CustomerKM");

                entity.Property(e => e.CustomerTripDescription).HasMaxLength(500);

                entity.Property(e => e.EndAddress).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.EndGps)
                    .HasMaxLength(500)
                    .HasColumnName("EndGPS");

                entity.Property(e => e.EndKm).HasColumnName("EndKM");

                entity.Property(e => e.IsCancelled).HasColumnName("isCancelled");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OnHold).HasDefaultValueSql("((0))");

                entity.Property(e => e.PauseKm).HasColumnName("PauseKM");

                entity.Property(e => e.ResumeKm).HasColumnName("ResumeKM");

                entity.Property(e => e.SharedEndKm).HasColumnName("SharedEndKM");

                entity.Property(e => e.SharedKm).HasColumnName("SharedKM");

                entity.Property(e => e.SharedStartKm).HasColumnName("SharedStartKM");

                entity.Property(e => e.SharedTripId).HasColumnName("SharedTripID");

                entity.Property(e => e.StartAddress).HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StartGps)
                    .HasMaxLength(500)
                    .HasColumnName("StartGPS");

                entity.Property(e => e.StartKm).HasColumnName("StartKM");

                entity.Property(e => e.TripId).HasColumnName("TripID");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.HistoricalCustomerTrips)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK_HistoricalCustomerTrip_TripID");
            });

            modelBuilder.Entity<HistoricalDriverTrip>(entity =>
            {
                entity.HasKey(e => e.TripId)
                    .HasName("PK__Historic__51DC711EFBEA9066");

                entity.ToTable("HistoricalDriverTrip");

                entity.Property(e => e.TripId)
                    .ValueGeneratedNever()
                    .HasColumnName("TripID");

                entity.Property(e => e.BillingLineId).HasColumnName("BillingLineID");

                entity.Property(e => e.ConvertedToBillable).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.EndAddress).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.EndGps)
                    .HasMaxLength(500)
                    .HasColumnName("EndGPS");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.JobNumber).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OnHold).HasDefaultValueSql("((0))");

                entity.Property(e => e.SalesForceUserId)
                    .HasMaxLength(50)
                    .HasColumnName("SalesForceUserID");

                entity.Property(e => e.SoloKm).HasColumnName("SoloKM");

                entity.Property(e => e.StartAddress).HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StartGps)
                    .HasMaxLength(500)
                    .HasColumnName("StartGPS");

                entity.Property(e => e.TotalKm).HasColumnName("TotalKM");

                entity.Property(e => e.TripCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TripDescription).HasMaxLength(500);

                entity.Property(e => e.TripTypeId).HasColumnName("TripTypeID");

                entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

                entity.Property(e => e.VehicleRegistrationNumber).HasMaxLength(100);
            });

            modelBuilder.Entity<IntegrationActivity>(entity =>
            {
                entity.ToTable("IntegrationActivity");

                entity.Property(e => e.IntegrationActivityId).HasColumnName("IntegrationActivityID");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.FailedException).HasMaxLength(500);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.IntegrationActivityNameNavigation)
                    .WithMany(p => p.IntegrationActivities)
                    .HasForeignKey(d => d.IntegrationActivityName)
                    .HasConstraintName("FK_IANameID");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.IntegrationActivities)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_IAStatusID");
            });

            modelBuilder.Entity<IntegrationActivityList>(entity =>
            {
                entity.HasKey(e => e.IntegrationActivityNameId)
                    .HasName("PK__Integrat__48E8AFA5F9FC62AB");

                entity.ToTable("IntegrationActivityList");

                entity.Property(e => e.IntegrationActivityNameId).HasColumnName("IntegrationActivityNameID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IntegrationActivityName).HasMaxLength(100);

                entity.Property(e => e.IsCompleted).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<IntegrationActivityStatus>(entity =>
            {
                entity.ToTable("IntegrationActivityStatus");

                entity.Property(e => e.IntegrationActivityStatusId).HasColumnName("IntegrationActivityStatusID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IntegrationActivityStatus1)
                    .HasMaxLength(100)
                    .HasColumnName("IntegrationActivityStatus");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ItemOverclaimStatus>(entity =>
            {
                entity.ToTable("ItemOverclaimStatus");

                entity.Property(e => e.ItemOverclaimStatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("ItemOverclaimStatusID");

                entity.Property(e => e.Status).HasMaxLength(75);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Job_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.HasIndex(e => e.StateName, "IX_HangFire_Job_StateName")
                    .HasFilter("([StateName] IS NOT NULL)");

                entity.Property(e => e.Arguments).IsRequired();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.InvocationData).IsRequired();

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Name })
                    .HasName("PK_HangFire_JobParameter");

                entity.ToTable("JobParameter", "HangFire");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameters)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_JobParameter_Job");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.HasKey(e => new { e.Queue, e.Id })
                    .HasName("PK_HangFire_JobQueue");

                entity.ToTable("JobQueue", "HangFire");

                entity.Property(e => e.Queue).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Id })
                    .HasName("PK_HangFire_List");

                entity.ToTable("List", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_List_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<RoleAccessLevel>(entity =>
            {
                entity.HasKey(e => e.AccessLevelId)
                    .HasName("PK__RoleAcce__5E44DFF535A4CB76");

                entity.ToTable("RoleAccessLevel");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccessLevelName).HasMaxLength(75);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<RoleGroup>(entity =>
            {
                entity.ToTable("RoleGroup");

                entity.Property(e => e.RoleGroupId).HasColumnName("RoleGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GroupName).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SalesForceService>(entity =>
            {
                entity.ToTable("SalesForceService");

                entity.Property(e => e.SalesForceServiceId).HasColumnName("SalesForceServiceID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ServiceId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ServiceID");
            });

            modelBuilder.Entity<SalesforceRate>(entity =>
            {
                entity.HasKey(e => e.SalesforceRatesId)
                    .HasName("PK__Salesfor__684452FD7E3AE4C1");

                entity.Property(e => e.SalesforceRatesId).HasColumnName("SalesforceRatesID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Negotiation).HasMaxLength(100);

                entity.Property(e => e.PostalCode).HasMaxLength(30);

                entity.Property(e => e.RateId)
                    .HasMaxLength(75)
                    .HasColumnName("RateID");

                entity.Property(e => e.RateName).HasMaxLength(100);

                entity.Property(e => e.ServiceId)
                    .HasMaxLength(75)
                    .HasColumnName("ServiceID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.RateTypeNavigation)
                    .WithMany(p => p.SalesforceRates)
                    .HasForeignKey(d => d.RateType)
                    .HasConstraintName("FK_SalesForceRate_RateType");
            });

            modelBuilder.Entity<SalesforceRateType>(entity =>
            {
                entity.ToTable("SalesforceRateType");

                entity.Property(e => e.SalesforceRateTypeId).HasColumnName("SalesforceRateTypeID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RateType).HasMaxLength(75);
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_HangFire_Schema");

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.HasIndex(e => e.LastHeartbeat, "IX_HangFire_Server_LastHeartbeat");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<ServiceAgreementStatus>(entity =>
            {
                entity.HasKey(e => e.SastatusId)
                    .HasName("PK__ServiceA__D7B2C05E8DD6CE48");

                entity.ToTable("ServiceAgreementStatus");

                entity.Property(e => e.SastatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("SAStatusID");

                entity.Property(e => e.Status).HasMaxLength(75);
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Value })
                    .HasName("PK_HangFire_Set");

                entity.ToTable("Set", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Set_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.HasIndex(e => new { e.Key, e.Score }, "IX_HangFire_Set_Score");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<SharedTrip>(entity =>
            {
                entity.ToTable("SharedTrip");

                entity.Property(e => e.SharedTripId).HasColumnName("SharedTripID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.EndAddress).HasMaxLength(500);

                entity.Property(e => e.EndGps)
                    .HasMaxLength(500)
                    .HasColumnName("EndGPS");

                entity.Property(e => e.EndKm).HasColumnName("EndKM");

                entity.Property(e => e.StartAddress).HasMaxLength(500);

                entity.Property(e => e.StartGps)
                    .HasMaxLength(500)
                    .HasColumnName("StartGPS");

                entity.Property(e => e.StartKm).HasColumnName("StartKM");

                entity.Property(e => e.TripId).HasColumnName("TripID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SharedTrips)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Shared_Trip_Customer_ID");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.SharedTrips)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK_SharedTrip_TripID");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Id })
                    .HasName("PK_HangFire_State");

                entity.ToTable("State", "HangFire");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.ToTable("Trip");

                entity.Property(e => e.TripId).HasColumnName("TripID");

                entity.Property(e => e.BillingLineId).HasColumnName("BillingLineID");

                entity.Property(e => e.ConvertedToBillable).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.EndAddress).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.EndGps)
                    .HasMaxLength(500)
                    .HasColumnName("EndGPS");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.JobNumber).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OnHold).HasDefaultValueSql("((0))");

                entity.Property(e => e.PauseKm).HasColumnName("PauseKM");

                entity.Property(e => e.ResumeKm).HasColumnName("ResumeKM");

                entity.Property(e => e.SalesForceUserId)
                    .HasMaxLength(50)
                    .HasColumnName("SalesForceUserID");

                entity.Property(e => e.SoloKm).HasColumnName("SoloKM");

                entity.Property(e => e.StartAddress).HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StartGps)
                    .HasMaxLength(500)
                    .HasColumnName("StartGPS");

                entity.Property(e => e.TotalKm).HasColumnName("TotalKM");

                entity.Property(e => e.TripCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TripDescription).HasMaxLength(500);

                entity.Property(e => e.TripTypeId).HasColumnName("TripTypeID");

                entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

                entity.Property(e => e.VehicleRegistrationNumber).HasMaxLength(100);

                entity.HasOne(d => d.CostCenterNavigation)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.CostCenter)
                    .HasConstraintName("FK_Cost_Centre_Trip");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_Trip_DriverID");

                entity.HasOne(d => d.TripStatusNavigation)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.TripStatus)
                    .HasConstraintName("FK_DriverTrip_Trip_Status");

                entity.HasOne(d => d.TripType)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.TripTypeId)
                    .HasConstraintName("FK_DriverTrip_TripType_ID");

                entity.HasOne(d => d.VehicleCategoryNavigation)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.VehicleCategory)
                    .HasConstraintName("FK_DriverTrip_Vehicle_Category");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Trip_Vehicle_ID");

                entity.HasOne(d => d.VehicleTypeNavigation)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.VehicleType)
                    .HasConstraintName("FK_DriverTrip_Vehicle_Type");
            });

            modelBuilder.Entity<TripCoordinate>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Latitude).HasMaxLength(50);

                entity.Property(e => e.Longitude).HasMaxLength(50);

                entity.Property(e => e.TripId).HasColumnName("TripID");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.TripCoordinates)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK_TripCoordinates_TripID");
            });

            modelBuilder.Entity<TripCoordinatesArchive>(entity =>
            {
                entity.ToTable("TripCoordinatesArchive");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Latitude).HasMaxLength(50);

                entity.Property(e => e.Longitude).HasMaxLength(50);

                entity.Property(e => e.TripId).HasColumnName("TripID");
            });

            modelBuilder.Entity<TripStatus>(entity =>
            {
                entity.ToTable("TripStatus");

                entity.Property(e => e.TripStatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("TripStatusID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(100);
            });

            modelBuilder.Entity<TripType>(entity =>
            {
                entity.Property(e => e.TripTypeId).HasColumnName("TripTypeID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ISdeleted).HasColumnName("iSDeleted");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.DefaultCostCenterNavigation)
                    .WithMany(p => p.TripTypes)
                    .HasForeignKey(d => d.DefaultCostCenter)
                    .HasConstraintName("FK_Trip_Type_Cost_CentreID");
            });

            modelBuilder.Entity<UnitOfMeasure>(entity =>
            {
                entity.ToTable("UnitOfMeasure");

                entity.Property(e => e.UnitOfMeasureId)
                    .ValueGeneratedNever()
                    .HasColumnName("UnitOfMeasureID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<UserGridSetting>(entity =>
            {
                entity.HasKey(e => e.Ugsid)
                    .HasName("PK__UserGrid__D8258ACBC91EACE4");

                entity.Property(e => e.Ugsid).HasColumnName("UGSID");

                entity.Property(e => e.ColumnPairVal).IsRequired();

                entity.Property(e => e.FormName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.GridName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserRoleNew>(entity =>
            {
                entity.HasKey(e => e.UserRoleId)
                    .HasName("PK__UserRole__3D978A5596C34C42");

                entity.ToTable("UserRoleNew");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PageNameId).HasColumnName("PageNameID");

                entity.Property(e => e.RoleGroupId).HasColumnName("RoleGroupID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.AccessLevel)
                    .WithMany(p => p.UserRoleNews)
                    .HasForeignKey(d => d.AccessLevelId)
                    .HasConstraintName("FK_UserRole_AccessLevelID");

                entity.HasOne(d => d.PageName)
                    .WithMany(p => p.UserRoleNews)
                    .HasForeignKey(d => d.PageNameId)
                    .HasConstraintName("FK_UserRole_PageNameID");

                entity.HasOne(d => d.RoleGroup)
                    .WithMany(p => p.UserRoleNews)
                    .HasForeignKey(d => d.RoleGroupId)
                    .HasConstraintName("FK_UserRole_RoleGroupID");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.AssetId)
                    .HasMaxLength(100)
                    .HasColumnName("AssetID");

                entity.Property(e => e.Availability).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.Make).HasMaxLength(500);

                entity.Property(e => e.Model).HasMaxLength(500);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Registration)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.Category)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_Categoty");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_Vehicle_DriverID");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_Type");
            });

            modelBuilder.Entity<VehicleCategory>(entity =>
            {
                entity.ToTable("VehicleCategory");

                entity.Property(e => e.VehicleCategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("VehicleCategoryID");

                entity.Property(e => e.Category).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("VehicleType");

                entity.Property(e => e.VehicleTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("VehicleTypeID");

                entity.Property(e => e.Type).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
