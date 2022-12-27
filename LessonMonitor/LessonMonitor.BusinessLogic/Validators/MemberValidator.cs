using FluentValidation;
using LessonMonitor.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace LessonMonitor.BusinessLogic.Validators
{
    public class MemberValidator : AbstractValidator<Member>
    {
        public MemberValidator()
        {
            RuleFor(x=> x.Id).Empty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.YoutubeUsereId).NotEmpty();
        }
    }
}
