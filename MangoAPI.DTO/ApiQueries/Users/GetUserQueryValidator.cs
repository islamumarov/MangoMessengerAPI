﻿using System;
using FluentValidation;

namespace MangoAPI.DTO.ApiQueries.Users
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotEmpty();
            RuleFor(x => x.UserId).Must(x => Guid.TryParse(x, out _))
                .WithMessage("GetUserQuery: User Id cannot be parsed.");
        }
    }
}