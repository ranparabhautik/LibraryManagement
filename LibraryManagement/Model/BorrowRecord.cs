namespace LibraryManagement.Model;

public class BorrowRecord : RecordEntity
{

    public int BookCopyId { get; set; }
    public BookCopy BookCopy { get; set; }

    public int MemberId { get; set; }
    public Member Member { get; set; }

   
}
