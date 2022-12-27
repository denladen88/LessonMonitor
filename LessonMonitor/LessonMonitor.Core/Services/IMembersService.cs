using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LessonMonitor.Core.Services
{
    public interface IMembersService
    {
        Task<int> Create(Member newMember);
        Task<Member[]> Get();
    }
}
