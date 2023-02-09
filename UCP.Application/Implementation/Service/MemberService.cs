using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Dto;
using UCP.Application.Interface.Repository;
using UCP.Application.Interface.Service;
using UCP.Application.ResponseModel;
using UCP.Application.Shared;
using UCP.Domain.Entity;
using UCP.Domain.Enum;

namespace UCP.Application.Implementation.Service
{
    public class MemberService : IMemberService
    {
        private readonly UserManager<Member> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMemberRepository _memberRepository;
        private readonly IRepository _repository;
        private readonly SignInManager<Member> _signInManager;
        private readonly ILogger<MemberService> _logger;

        public MemberService(UserManager<Member> userManager,
                              SignInManager<Member> signInManager,
                              RoleManager<IdentityRole> roleManager,
                              IMemberRepository memberRepository,
                              IRepository repository,
                              ILogger<MemberService> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _memberRepository = memberRepository;
            _repository = repository;
            _signInManager = signInManager;
        }
        public async Task<CreateResponseModel> CreateMember(MemberDto request)
        {
            try
            {
                var status = new Status();
                var userExists = await _userManager.FindByNameAsync(request.UserName);
                if (userExists != null)
                {
                    return new CreateResponseModel(false,
                                                "",
                                                "User Already Exist");
                }
                MailAddress mailAddress = new MailAddress(request.Email);
                string accNo = Helper.GenerateAccountNo();
                var member = new Member()
                {
                    Id = new Guid().ToString(),
                    Surname = request.Surname,
                    OtherName = request.OtherName,
                    UserName = request.UserName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Gender = request.Gender,
                    DateJoined = DateTime.Now,
                    CreatedBy = "Admin",
                    AccountNumber = request.AccountNumber,
                    DateOfBirth = request.DateOfBirth
                };
                var result = await _userManager.CreateAsync(member, request.Password);
                if (!result.Succeeded)
                {
                    return new CreateResponseModel(false,
                                                 "",
                                                 "User Applied Failed");
                    _logger.LogError("User Aplied  Failed");
                }
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(member, Role.Nominal.ToString());
                    await _repository.SaveChangesAsync();
                    _logger.LogInformation("User created a new account with password.");
                    return new CreateResponseModel(true,
                             "",
                             "user created Successful");
                }
            }
            catch (Exception)
            {
                return new CreateResponseModel(false,
                                            "",
                                            "Something Went wrong..");
            }

            return new CreateResponseModel(true,
                                           "",
                                           "User Creation Successsful.");
        }

        public async Task<CreateResponseModel> DeleteMember(Guid id)
        {

            var deleteMember = await _memberRepository.GetMemberById(id);

            if (deleteMember == null)
            {
                return new CreateResponseModel(false,
                                             "",
                                              "No such student exists");
            }

            try
            {
                await _repository.RemoveAsync(deleteMember);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new CreateResponseModel(false,
                                              "",
                                              "Could not delete student..");
            }
            return new CreateResponseModel(true,
                                              "",
                                              "Delete Successful.");
        }

        public async Task<List<MemberDto>> LoadAllMember()
        {
            var users = await _memberRepository.GetAllMemberDto();

            if(users is not null)
            {
                return users;
            }

            return (List<MemberDto>)Enumerable.Empty<MemberDto>();
        }

        public async Task<MemberDto> LoadMemberDetail(Guid id)
        {
            var user = await _memberRepository.LoadMemberDetailAsync(id);

            if (user is not null)
            {
                return user;
            }

            return null;
        }

        public async Task<CreateResponseModel> UpdateMember(Guid id, MemberDto request)
        {
            try
            {

                DateTime modified = DateTime.Now;
                var std = await _memberRepository.GetMemberById(id);

                if (std != null)
                {
                    std.ModifiedDate = modified;
                    std.ModifiedBy = "Admin";
                    std.UserName = request.UserName;
                    std.Surname = request.Surname;
                    std.Gender = request.Gender;
                    std.PhoneNumber = request.PhoneNumber;
                    std.Email = request.Email;
                    std.OtherName = request.OtherName;

                    await _repository.UpdateAsync(std);
                    await _repository.SaveChangesAsync();
                }
                else
                {
                    return new CreateResponseModel(false,
                                                 "",
                                                 "Member does not exist");
                }
            }
            catch (Exception)
            {
                return new CreateResponseModel(false,
                                                 "",
                                                 "Something went wrong");
            }
            return new CreateResponseModel(true,
                                                 "",
                                                 "Member Successfully Updated");
        }
    }

}
