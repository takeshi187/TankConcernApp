using Microsoft.EntityFrameworkCore;
using TankConcernApp.Model;

namespace TankConcernApp.database;

public partial class TankConcernDbContext : DbContext
{
    public TankConcernDbContext()
    {
    }

    public TankConcernDbContext(DbContextOptions<TankConcernDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brigade> Brigades { get; set; }

    public virtual DbSet<BrigadeWorkshopAssignment> BrigadeWorkshopAssignments { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerType> CustomerTypes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeBrigade> EmployeeBrigades { get; set; }

    public virtual DbSet<EmployeePost> EmployeePosts { get; set; }

    public virtual DbSet<EmployeeStatus> EmployeeStatuses { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<PartType> PartTypes { get; set; }

    public virtual DbSet<PartsInventory> PartsInventories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductStage> ProductStages { get; set; }

    public virtual DbSet<ProductStageType> ProductStageTypes { get; set; }

    public virtual DbSet<ProductionLog> ProductionLogs { get; set; }

    public virtual DbSet<TankPart> TankParts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Workshop> Workshops { get; set; }

    public virtual DbSet<WorkshopType> WorkshopTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-2D1FGK3;Database=TankConcernDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brigade>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<BrigadeWorkshopAssignment>(entity =>
        {
            entity.HasKey(e => new { e.WorkshopId, e.BrigadeId });

            entity.HasOne(d => d.Brigade).WithMany(p => p.BrigadeWorkshopAssignments)
                .HasPrincipalKey(p => p.BrigadeId)
                .HasForeignKey(d => d.BrigadeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BrigadeWorkshopAssignments_EmployeeBrigade");

            entity.HasOne(d => d.Workshop).WithMany(p => p.BrigadeWorkshopAssignments)
                .HasForeignKey(d => d.WorkshopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BrigadeWorkshopAssignments_Workshops");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);

            entity.HasOne(d => d.CustomerType).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CustomerTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customers_CustomerTypes");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.Phone, "IX_Employees").IsUnique();

            entity.Property(e => e.EmployeeStatusId).HasDefaultValue(1L);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasColumnType("decimal(11, 0)");
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.EmployeePost).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeePostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_EmployeePosts");

            entity.HasOne(d => d.EmployeeStatus).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_EmployeeStatuses");
        });

        modelBuilder.Entity<EmployeeBrigade>(entity =>
        {
            entity.HasKey(e => e.Ebid);

            entity.ToTable("EmployeeBrigade");

            entity.HasIndex(e => e.EmployeeId, "IX_EmployeeBrigade").IsUnique();

            entity.HasIndex(e => e.BrigadeId, "IX_EmployeeBrigade_1").IsUnique();

            entity.Property(e => e.Ebid).HasColumnName("EBId");

            entity.HasOne(d => d.Brigade).WithOne(p => p.EmployeeBrigade)
                .HasForeignKey<EmployeeBrigade>(d => d.BrigadeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeBrigade_Brigades");

            entity.HasOne(d => d.Employee).WithOne(p => p.EmployeeBrigade)
                .HasForeignKey<EmployeeBrigade>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeBrigade_Employees1");
        });

        modelBuilder.Entity<EmployeePost>(entity =>
        {
            entity.HasKey(e => e.EmployeePostId).HasName("PK_Posts");

            entity.Property(e => e.EmployeePostName).HasMaxLength(100);
        });

        modelBuilder.Entity<EmployeeStatus>(entity =>
        {
            entity.Property(e => e.EmployeeStatusName).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderStatusId).HasDefaultValue(1L);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_OrderStatuses");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Products");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.Property(e => e.OrderStatusName).HasMaxLength(100);
        });

        modelBuilder.Entity<PartsInventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId);

            entity.ToTable("PartsInventory");

            entity.HasOne(d => d.PartType).WithMany(p => p.PartsInventories)
                .HasForeignKey(d => d.PartTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartsInventory_PartTypes");

            entity.HasOne(d => d.TankPart).WithMany(p => p.PartsInventories)
                .HasForeignKey(d => d.TankPartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartsInventory_TankParts");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductName).HasMaxLength(100);

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_ProductCategory");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("ProductCategory");

            entity.Property(e => e.ProductCategoryName).HasMaxLength(255);
        });

        modelBuilder.Entity<ProductStage>(entity =>
        {
            entity.HasOne(d => d.Order).WithMany(p => p.ProductStages)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductStages_Orders");

            entity.HasOne(d => d.ProductStageType).WithMany(p => p.ProductStages)
                .HasForeignKey(d => d.ProductStageTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductStages_ProductStageTypes");

            entity.HasOne(d => d.Workshop).WithMany(p => p.ProductStages)
                .HasForeignKey(d => d.WorkshopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductStages_Workshops");
        });

        modelBuilder.Entity<ProductStageType>(entity =>
        {
            entity.Property(e => e.ProductStageTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<ProductionLog>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.HasOne(d => d.Order).WithMany(p => p.ProductionLogs)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductionLogs_Orders");

            entity.HasOne(d => d.ProductStage).WithMany(p => p.ProductionLogs)
                .HasForeignKey(d => d.ProductStageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductionLogs_ProductStages");

            entity.HasOne(d => d.BrigadeWorkshopAssignment).WithMany(p => p.ProductionLogs)
                .HasForeignKey(d => new { d.WorkshopId, d.BrigadeId })
                .HasConstraintName("FK_ProductionLogs_BrigadeWorkshopAssignments1");
        });

        modelBuilder.Entity<TankPart>(entity =>
        {
            entity.HasOne(d => d.PartType).WithMany(p => p.TankParts)
                .HasForeignKey(d => d.PartTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TankParts_PartTypes");

            entity.HasOne(d => d.Product).WithMany(p => p.TankParts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TankParts_Products");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);

            entity.HasOne(d => d.Employee).WithMany(p => p.Users)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Employees");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_UserRoles");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Roles");

            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Workshop>(entity =>
        {
            entity.HasOne(d => d.WorkshopType).WithMany(p => p.Workshops)
                .HasForeignKey(d => d.WorkshopTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workshops_WorkshopTypes");
        });

        modelBuilder.Entity<WorkshopType>(entity =>
        {
            entity.Property(e => e.WorkshopTypeName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
