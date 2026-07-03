using LibraryManagement.DTos.MemberDTOs;
using LibraryManagement.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MemberController(IMemberService memberService): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllMember()
    {
        var members = await memberService.GetAllMember();
        return Ok(members);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMemberById(int id)
    {
        var member = await memberService.GetMemberById(id);
        return Ok(member);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMember(int id, MemberUpdateDTOs dto)
    {
        var member = await memberService.UpdateMember(id,dto);
        return Ok(member);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMember(int id) 
    {
        var member = await memberService.Delete(id);
        if (!member)
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMember(MemberCreateDTO dto)
    {
        var member = await memberService.CreateMember(dto);
        return Ok(member);
    }

    [HttpGet("Get-Morethan-3")]
    public async Task<IActionResult> GetMorethan3Dues()
    {
        var members = await memberService.MemberOverdueMorethan3();
        return Ok(members);
    }

}
