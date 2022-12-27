using AutoFixture;
using FluentAssertions;
using LessonMonitor.Core;
using LessonMonitor.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LessonMonitor.BusinessLogic.XTests
{
    public class MembersServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IMembersReposittory> _membersRepositiryMock;
        private readonly MembersService _service;

        public MembersServiceTests()
        {
            _fixture = new Fixture();
            _membersRepositiryMock = new Mock<IMembersReposittory>();
            _service = new MembersService(_membersRepositiryMock.Object);
        }
        [Fact]
        public async Task Create_Valide_ShouldReturneCreateMemberId()
        {
            //arrange
            
            var expectedMemberId = _fixture.Create<int>();
            var member = _fixture.Build<Member>().Without(x => x.Id).Create();
   
            _membersRepositiryMock.Setup(x => x.Add(member)).ReturnsAsync(expectedMemberId);

            //act
            var memmberId = await _service.Create(member);
            //assert
            Assert.Equal(expectedMemberId, memmberId);
            _membersRepositiryMock.Verify(x => x.Add(member), Times.Once);

        }

        [Fact]
        public async Task Create_MemberIsNull_ShouldThrowExeption()
        {
            //arrange
            //act
           await Assert.ThrowsAsync<ArgumentNullException>(()=> _service.Create(null));

            //assert
            
            _membersRepositiryMock.Verify(x => x.Add(It.IsAny<Member>()), Times.Never);

        }

        [Theory]
        [InlineData(1241, null, null)]
        [InlineData(0, "test", "test")]
        [InlineData(-52, null, null)]
        public async Task Create_InvalidMember_ShouldThrowExeption(int id, string name, string youtubeUserId)
        {
            //arrange
            var member = new Member
            {
                Id = id,
                Name = name,
                YoutubeUsereId = youtubeUserId
            };
            //act
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.Create(member));

            //assert

            _membersRepositiryMock.Verify(x => x.Add(It.IsAny<Member>()), Times.Never);

        }
        [Fact]
        public async Task Gate_ShouldReturneMembers()
        {
            //arrange

            var expectedMembes = _fixture.CreateMany<Member>(42).ToArray();

            _membersRepositiryMock.
                Setup(x => x.Get()).
                ReturnsAsync(expectedMembes);

            //act
            var members = await _service.Get();

            //assert
            members.Should().NotBeNullOrEmpty().
                And.HaveCount(expectedMembes.Length);
            
            _membersRepositiryMock.Verify(x => x.Get(), Times.Once);

        }
    }
}
