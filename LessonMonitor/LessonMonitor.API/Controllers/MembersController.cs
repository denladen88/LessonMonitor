using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using LessonMonitor.API.Contracts;
using LessonMonitor.Core.Services;

namespace LessonMonitor.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public partial class MembersController: ControllerBase
    {
        private readonly IMembersService _membersService;

        public MembersController(IMembersService membersService)
        {
            _membersService = membersService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Member[]), 200)]
        public async Task<IActionResult> Get()
        {
            var members = await _membersService.Get(); 
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMember newMember)
        {
            var member = new Core.Member
            {
                Name = newMember.Name,
                YoutubeUsereId = newMember.YoutubeUserId
            };
            var memberId = await _membersService.Create(member);
            return Ok(memberId);
        }
    }
}
