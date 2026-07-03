using AutoMapper;
using LibraryManagement.DTos.MemberDTOs;
using LibraryManagement.Model;
using LibraryManagement.Repositories.Interface;
using LibraryManagement.Services.Interface;

namespace LibraryManagement.Services.Implementation;

public class MemberService(IUnitOfWork uow,IMapper _mapper) : IMemberService
{
    public async Task<MemberResponseDTO> CreateMember(MemberCreateDTO dto)
    {
        var member = _mapper.Map<Member>(dto);
        await uow.MemberUOW.Create(member);
        await uow.SaveChangesAsync();
        return _mapper.Map<MemberResponseDTO>(member);
    }

    public async Task<bool> Delete(int id)
    {
        var member = await uow.MemberUOW.GetById(id);
        if(member == null)
        {
            return false;
        }
        if (member is IsDeletable softdel)
        {
            softdel.IsDeletable = true;
            await uow.MemberUOW.Update(member);
            await uow.SaveChangesAsync();
            return true;
        }
        else
        {
            await uow.MemberUOW.Delete(id);
            await uow.SaveChangesAsync();
            return true;
        }
    }

    public async Task<IEnumerable<MemberResponseDTO>> GetAllMember()
    {
        var memberlist = await uow.MemberUOW.GetAll();
        return _mapper.Map<IEnumerable<MemberResponseDTO>>(memberlist);

    }

    public async Task<MemberResponseDTO?> GetMemberById(int id)
    {
        var member = await uow.MemberUOW.GetById(id);
        if(member == null)
        {
            return null;
        }
        return _mapper.Map<MemberResponseDTO>(member);
    }

    public async Task<IEnumerable<MemberResponseDTO>> MemberOverdueMorethan3()
    {
        var members = await uow.MemberUOW.GetMemberOverDueMoreThan3();
        return _mapper.Map<IEnumerable<MemberResponseDTO>>(members);
    }

    public async Task<MemberResponseDTO> UpdateMember(int id,MemberUpdateDTOs dto)
    {
        var member = await uow.MemberUOW.GetById(id);
        if(member == null)
        {
            return null;
        }
        var mapped_member = _mapper.Map(dto,member);
        mapped_member.UpdatedAt = DateTime.Now;
        await uow.MemberUOW.Update(mapped_member);
        await uow.SaveChangesAsync();
        return _mapper.Map<MemberResponseDTO>(mapped_member);

}}
