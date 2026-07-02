namespace LibraryManagement.Model;

public abstract class  RecordEntity:BaseEntity
{
    public DateTime BorrowedAt { get; set; }
    public DateTime? ReturnAt { get; set; }
}
