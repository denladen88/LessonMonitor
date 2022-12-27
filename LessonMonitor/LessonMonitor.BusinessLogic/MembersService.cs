using LessonMonitor.BusinessLogic.Validators;
using LessonMonitor.Core;
using LessonMonitor.Core.Repositories;
using LessonMonitor.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonMonitor.BusinessLogic
{
    public class MembersService : IMembersService
    {
        private readonly IMembersReposittory _membersRepository;

        public MembersService(IMembersReposittory membersRepository)
        {
            _membersRepository = membersRepository;
        }

        public async Task<int> Create(Member newMember)
        {
            if (newMember is null)
            {
                throw new ArgumentNullException(nameof(newMember));
            }
            var validator = new MemberValidator();
            var validationResult =  await validator.ValidateAsync(newMember);
            
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ToString(",");
                throw new InvalidOperationException(errors);
            }

            return await _membersRepository.Add(newMember);
        }

        public async Task<Member[]> Get()
        {
            return await _membersRepository.Get();
        }
    }
}
