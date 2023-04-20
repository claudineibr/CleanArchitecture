using MediatR;

namespace CleanArchitecture.Application.Commands.UserCommands.CreateUserCommand;

public sealed record CreateUserCommand(string Email, string Name) : IRequest<CreateUserResponse>;