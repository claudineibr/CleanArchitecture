using AutoMapper;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Commands.UserCommands.CreateUserCommand;

public sealed class CreateUserMapper : Profile
{
    public CreateUserMapper()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<User, CreateUserResponse>();
    }
}