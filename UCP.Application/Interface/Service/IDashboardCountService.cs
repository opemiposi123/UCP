using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCP.Application.Dto;

namespace UCP.Application.Interface.Service
{
    public interface IDashboardCountService
    {
        Task<CountDto> DashBoardCount();
    }
}
