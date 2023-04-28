namespace NoteApplication.ViewModels
{
    public class NoteViewModel
    {
        public Guid Id { get; set; } // Id (Primary key)
        public string Title { get; set; } // Title (length: 50)
        public string Description { get; set; } // Description
        public int EditCount { get; set; } // EditCount
    }
}
