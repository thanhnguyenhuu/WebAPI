using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using WebAPI.Models;

#nullable disable

namespace WebAPI.DataAccess
{
    public partial class SmarterCityDBContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;

        public virtual DbSet<Customer> Customers { get; set; }

        public SmarterCityDBContext()
        {
        }

        public SmarterCityDBContext(DbContextOptions<SmarterCityDBContext> options) : base(options)
        {
        }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SmarterCityDB;Integrated Security=True;");
                optionsBuilder.UseSqlServer("Name=SmarterCity");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address1).HasMaxLength(250);
                entity.Property(e => e.Address2).HasMaxLength(250);
                entity.Property(e => e.FirstName).HasMaxLength(20);
                entity.Property(e => e.LastName).HasMaxLength(20);
                entity.Property(e => e.MobileNumber).HasMaxLength(20);
                entity.Property(e => e.PostCode).HasMaxLength(4);
                entity.Property(e => e.State).HasMaxLength(3);
                entity.Property(e => e.Suburb).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
        {
            if (_currentTransaction != null)
            {
                return;
            }
            _currentTransaction = await base.Database.BeginTransactionAsync(isolationLevel).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction == null)
            {
                return;
            }

            try
            {
                await SaveChangesAsync().ConfigureAwait(false);
                await _currentTransaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
