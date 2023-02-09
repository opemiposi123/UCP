using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Dto;
using UCP.Application.ResponseModel;
using UCP.Domain.Entity;

namespace UCP.Application.Interface.Service
{
    public interface IMemberService
    {
        Task<CreateResponseModel> CreateMember(MemberDto request);
        Task<List<MemberDto>> LoadAllMember();
        Task<MemberDto> LoadMemberDetail(Guid id);
        Task<CreateResponseModel> UpdateMember(Guid id, MemberDto updateMemberDto);
        Task<CreateResponseModel> DeleteMember(Guid id);
    }
}
