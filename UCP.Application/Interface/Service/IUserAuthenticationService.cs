using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Dto;
using UCP.Domain.Entity;

namespace UCP.Application.Interface.Service
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModelDto model);
        Task LogoutAsync();
        Task<Status> ChangePasswordAsync(ChangePasswordModelDto model, string username);
    }
}
