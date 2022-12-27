using LessonMonitor.Core;
using LessonMonitor.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonMonitor.DataAccess.MSSQL.Repositories
{
    public class MembersRepository : IMembersReposittory
    {
        private readonly LessonMonitorDbContext _context;

        public MembersRepository(LessonMonitorDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Member newMember)
        {
            if (newMember is null)
            {
                throw new ArgumentNullException(nameof(newMember));
            }
            var newMemmberEntity = new Entities.Member
            {
                Name = newMember.Name,
                YoutubeAccountId = newMember.YoutubeUsereId
            };
            await _context.Members.AddAsync(newMemmberEntity);
            await _context.SaveChangesAsync();
            return newMember.Id;
        }

        public async Task<Member[]> Get()
        {
            var members = await _context.Members.
                AsNoTracking()
                .Select(x => new Member
                {
                    Name = x.Name,
                    Id = x.Id,
                    YoutubeUsereId = x.YoutubeAccountId
                })
                .ToArrayAsync();
            return members;
        }
    }
}
