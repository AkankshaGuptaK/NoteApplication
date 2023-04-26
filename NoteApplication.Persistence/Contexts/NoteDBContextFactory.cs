using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.Persistence.Contexts
{
    internal class NoteDBContextFactory : IDesignTimeDbContextFactory<NoteDBContext>
    {
        public NoteDBContext CreateDbContext(string[] args)
        {
            return new NoteDBContext();
        }
    }
}
