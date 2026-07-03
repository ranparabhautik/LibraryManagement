namespace LibraryManagement.Model;

public class Book : BaseEntity,IsDeletable
{
    public string BookName { get; set; }
    public string AuthorName { get; set; }
    public int Stock {  get; set; }
    public int CopyIssued { get; set; }
    public bool IsDeletable { get; set; } = false;
    public ICollection<BookCopy> Copies { get; set; } = new List<BookCopy>();   
}


