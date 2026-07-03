using LibraryManagement.Model;

namespace LibraryManagement.Repositories.Interface;

public interface IMemberRepository:IGenericRepository<Member>
{
    Task<Member> GetMemberBorrowHistory(int id);
    Task<IEnumerable<Member>> GetMemberOverDueMoreThan3();

}
