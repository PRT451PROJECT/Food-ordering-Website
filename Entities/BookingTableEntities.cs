namespace BookingTable.Entities.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BookingTableEntities : DbContext
    {
        public BookingTableEntities()
            : base("name=BookingTableEntities")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Floor> Floors { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<TableType> TableTypes { get; set; }
        public virtual DbSet<Offers> Offers { get; set; }
        public virtual DbSet<FeedBack> FeedBack { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.IdentityCard)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Admin)
                .HasForeignKey(e => e.PayeeId);

            modelBuilder.Entity<Admin>()
                .HasMany(e => e.Settings)
                .WithRequired(e => e.Admin)
                .HasForeignKey(e => e.UpdateByAdminId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Floor>()
                .HasMany(e => e.Tables)
                .WithRequired(e => e.Floor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Food>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Food>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.FoodPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Subtotal)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .Property(e => e.SubTotal)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .Property(e => e.Discount)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Order>()
                .Property(e => e.Tax)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .Property(e => e.DepositPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.OrdersId)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Offers>()
            //        .Property(e => e.OfferName)
            //        .IsUnicode(false);

            //modelBuilder.Entity<Offers>()
            //    .Property(e => e.OfferAmount);
                

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Admins)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Permissions)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.Type)
                .IsFixedLength();

            modelBuilder.Entity<Table>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Table)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TableType>()
                .Property(e => e.DepositPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<TableType>()
                .HasMany(e => e.Tables)
                .WithRequired(e => e.TableType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<BookingTable.Entities.Models.FeedBackModel> FeedBackModels { get; set; }
    }
}
