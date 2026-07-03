namespace LibraryManagement.Model;

public class BookCopy:BaseEntity
{
    public int BookId { get; set; }
    public Book Books { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string CopyId { get; set; }
    public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
}
    