namespace LibraryManagement.Model;

public class Member : BaseEntity
{
    public string MemberName { get ; set ; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public ICollection<BorrowRecord>  BorrowRecords { get; set; } = new List<BorrowRecord>();
}