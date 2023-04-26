using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NoteApplication.Persistence.Entities;
using NoteApplication.Persistence.Interfaces.Context;
using System.Data.SqlTypes;

namespace NoteApplication.Persistence.Contexts
{
    public class NoteDBContext : DbContext, INoteContext
    {
        public NoteDBContext()
        {
        }

        public NoteDBContext(DbContextOptions<NoteDBContext> options)
            : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; } // Note

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS01;Initial Catalog=NoteApplication;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=false;TrustServerCertificate=true");
            }
        }

        public bool IsSqlParameterNull(SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == DBNull.Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new NoteConfiguration());
        }
    }
}
