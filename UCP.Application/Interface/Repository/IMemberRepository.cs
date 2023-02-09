using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Dto;
using UCP.Domain.Entity;

namespace UCP.Application.Interface.Repository
{
    public interface IMemberRepository
    {
        List<Member> GetAllMember();
        Task<Member> GetMemberById(Guid id);
        Task<Member> GetMemberByAccountNo(string accountNumber);
        Task<Member> GetMemberByUserName(string username);
        Task<List<MemberDto>> GetAllMemberDto();
        Task<MemberDto?> LoadMemberDetailAsync(Guid id);
    }
}
