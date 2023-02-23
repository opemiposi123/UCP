using Identity.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Dto;
using UCP.Application.Interface.Repository;
using UCP.Domain.Entity;

namespace UCP.Persistence.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly UCPDbContext _dbContext;
        public MemberRepository(UCPDbContext dbContex)
        {
            _dbContext = dbContex;
        }
        public List<Member> GetAllMember()
        {
            return _dbContext.Members.ToList();
        }

        public async Task<List<MemberDto>> GetAllMemberDto()
        {
            return await _dbContext.Members.Select(x => new MemberDto
            {
                Id = Guid.Parse(x.Id),
                Surname = x.Surname,
                UserName = x.UserName,
                Email = x.Email,
                Gender = x.Gender,
                AccountNumber = x.AccountNumber
            }).ToListAsync();
        }

        public async Task<MemberDto?> LoadMemberDetailAsync(Guid id) =>
            await _dbContext.Members
                            .Where(x => x.Id == id.ToString())
                            .Select(x => new MemberDto
                            {
                                Id = Guid.Parse(x.Id),
                                UserName = x.UserName,
                                Surname = x.Surname,
                                PhoneNumber = x.PhoneNumber,
                                DateOfBirth = x.DateOfBirth,
                                OtherName = x.OtherName,
                                AccountNumber = x.AccountNumber,
                                Email = x.Email,
                                Gender = x.Gender,
                                Datejoined = (DateTime)x.DateJoined,
                                CreateBy = x.CreatedBy,
                                ModifiedBy = x.ModifiedBy,
                                ModifiedDate = x.ModifiedDate
                            })
                            .FirstOrDefaultAsync();

        public async Task<Member> GetMemberByAccountNo(string acc)
        {
            return await _dbContext.Members.Where(s => s.AccountNumber == acc).FirstOrDefaultAsync();
        }

        public async Task<Member> GetMemberById(Guid id)
        {
            return await _dbContext.Members.Where(s => s.Id == id.ToString()).FirstOrDefaultAsync();
        }

        public async Task<Member> GetMemberByUserName(string username)
        {
            return await _dbContext.Members.Where(s => s.UserName == username).FirstOrDefaultAsync();
        }

    }
}
