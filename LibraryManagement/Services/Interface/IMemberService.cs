using LibraryManagement.DTos.MemberDTOs;

namespace LibraryManagement.Services.Interface;

public interface IMemberService
{
    Task<IEnumerable<MemberResponseDTO>> GetAllMember();
    Task<MemberResponseDTO?> GetMemberById(int id);
    Task<MemberResponseDTO> CreateMember(MemberCreateDTO dto);
    Task<MemberResponseDTO> UpdateMember(int id,MemberUpdateDTOs dto);
    Task<bool> Delete(int id);
}
