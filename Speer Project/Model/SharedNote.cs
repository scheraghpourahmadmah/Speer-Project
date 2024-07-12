namespace Speer_Project.Model
{
    public class SharedNote
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
        public string SharedWithUserId { get; set; }
    }
}
