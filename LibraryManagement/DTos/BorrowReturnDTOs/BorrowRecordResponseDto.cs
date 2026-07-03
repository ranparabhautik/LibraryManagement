namespace LibraryManagement.DTos.BorrowReturnDTOs;

public class BorrowRecordResponseDto
{
    public int RecordId { get; set; }
    public string BookName { get; set; }
    public string MemberName { get; set; }
    public string BookCopyCode { get; set; }
    public DateTime BorrowedAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
}



