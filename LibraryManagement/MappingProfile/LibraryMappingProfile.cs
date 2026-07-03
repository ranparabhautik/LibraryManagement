using AutoMapper;
using LibraryManagement.DTos.BookDTOS;
using LibraryManagement.DTos.BorrowReturnDTOs;
using LibraryManagement.DTos.MemberDTOs;
using LibraryManagement.Model;

namespace LibraryManagement.MappingProfile;

public class LibraryMappingProfile:Profile
{
    public LibraryMappingProfile()
    {
        CreateMap<BookCreateDTO, Book>().ReverseMap();
        CreateMap<BookUpdateDTO, Book>().ReverseMap();
        CreateMap<Book, BookResponseDTO>().ForMember(dest => dest.AvailableCopies, opt => opt.MapFrom(src => src.Stock - src.CopyIssued));


        CreateMap<MemberCreateDTO, Member>().ReverseMap();
        CreateMap<MemberUpdateDTOs,Member>().ReverseMap();
        CreateMap<Member, MemberResponseDTO>();

        CreateMap<BorrowRecord, BorrowRecordResponseDto>().ForMember(dest => dest.BookName, opt => opt.MapFrom(src=>src.BookCopy.Books.BookName)).ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.MemberName)).ForMember(dest=> dest.BookCopyCode,opt=> opt.MapFrom(src=>src.BookCopy.CopyId)).ForMember(dest=> dest.RecordId,opt=> opt.MapFrom(src=> src.Id));

    }
}
