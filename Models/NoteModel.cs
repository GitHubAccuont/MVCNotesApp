public struct NoteModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Status Status { get; set; }
}

public enum Status
{
    InProgress,
    Overdue,
    Cancelled
}