﻿using System;
using FluentValidation;

namespace MangoAPI.DTO.ApiCommands.Chats
{
    public class CreateDirectChatCommandValidator : AbstractValidator<CreateDirectChatCommand>
    {
        public CreateDirectChatCommandValidator()
        {
            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(2, 300);

            RuleFor(x => x.UserId).Must(x => Guid.TryParse(x, out _))
                .WithMessage("Create direct chat: User Id cannot be parsed.");

            RuleFor(x => x.PartnerId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(2, 300);

            RuleFor(x => x.PartnerId).Must(x => Guid.TryParse(x, out _))
                .WithMessage("Create direct chat: Partner Id cannot be parsed.");
        }
    }
}