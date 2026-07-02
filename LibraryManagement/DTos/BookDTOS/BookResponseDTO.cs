namespace LibraryManagement.DTos.BookDTOS;

public class BookResponseDTO
{
    public int Id { get; set; }
    public string BookName { get; set; }
    public string AuthorName { get; set; }
    public int Stock { get; set; }
    public int CopyIssued { get; set; }
    public int AvailableCopies { get; set; }

}
