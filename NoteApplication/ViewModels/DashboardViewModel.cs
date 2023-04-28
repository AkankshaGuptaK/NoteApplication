namespace NoteApplication.ViewModels
{
    public class DashboardViewModel
    {
        public int AddCount { get; set; }
        public int EditCount { get; set; } // EditCount
        public int DeleteCount { get; set; }
        public List<NoteViewModel> Notes { get; set; }
    }
}
