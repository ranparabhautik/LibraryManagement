using AutoMapper;
using LibraryManagement.DTos.MemberDTOs;
using LibraryManagement.Model;
using LibraryManagement.Repositories.Interface;
using LibraryManagement.Services.Interface;

namespace LibraryManagement.Services.Implementation;

public class MemberService(IMemberRepository _repo,IMapper _mapper) : IMemberService
{
    public async Task<MemberResponseDTO> CreateMember(MemberCreateDTO dto)
    {
        var member = _mapper.Map<Member>(dto);
        await _repo.Create(member);
        return _mapper.Map<MemberResponseDTO>(member);
    }

    public async Task<bool> Delete(int id)
    {
        var member = await _repo.GetById(id);
        if(member == null)
        {
            return false;
        }
        if (member is IsDeletable softdel)
        {
            softdel.IsDeletable = true;
            await _repo.Update(member);
            return true;
        }
        else
        {
            await _repo.Delete(id);
            return true;
        }
    }

    public async Task<IEnumerable<MemberResponseDTO>> GetAllMember()
    {
        var memberlist = await _repo.GetAll();
        return _mapper.Map<IEnumerable<MemberResponseDTO>>(memberlist);

    }

    public async Task<MemberResponseDTO?> GetMemberById(int id)
    {
        var member = await _repo.GetById(id);
        if(member == null)
        {
            return null;
        }
        return _mapper.Map<MemberResponseDTO>(member);
    }

    public async Task<MemberResponseDTO> UpdateMember(int id,MemberUpdateDTOs dto)
    {
        var member = await _repo.GetById(id);
        if(member == null)
        {
            return null;
        }
        var mapped_member = _mapper.Map(dto,member);
        mapped_member.UpdatedAt = DateTime.Now;
        await _repo.Update(mapped_member);
        return _mapper.Map<MemberResponseDTO>(mapped_member);

}}
