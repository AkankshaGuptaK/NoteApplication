namespace NoteApplication.ViewModels
{
    public class NoteDetailsViewModel
    {
        public Guid Id { get; set; } // Id (Primary key)
        public string Title { get; set; } // Title (length: 50)
        public string Description { get; set; } // Description
        public DateTime LastEdited { get; set; } // LastEdited
        public string CreatedBy { get; set; } // CreatedBy (length: 450)
        public bool IsDeleted { get; set; } // IsDeleted
        public int EditCount { get; set; } // EditCount
    }
}
