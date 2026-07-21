using LibraryManagement.Model.Identity;

namespace LibraryManagement.Model;

public class Member : BaseEntity
{
    public Guid AppUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }



    public ICollection<BorrowRecord>  BorrowRecords { get; set; } = new List<BorrowRecord>();
}