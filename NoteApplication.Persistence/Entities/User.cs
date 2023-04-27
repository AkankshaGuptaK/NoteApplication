using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.Persistence.Entities
{
    public class User
    {
        public User() 
        { 
            Notes = new List<Note>();
        }
        public  string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
