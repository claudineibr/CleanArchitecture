﻿using AutoMapper;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Queries.UserQueries.GetAllUserQuery;

public sealed class GetAllUserMapper : Profile
{
    public GetAllUserMapper()
    {
        CreateMap<User, GetAllUserResponse>();
    }
}