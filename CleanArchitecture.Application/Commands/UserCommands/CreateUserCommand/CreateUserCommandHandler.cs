using AutoMapper;
using CleanArchitecture.Application.Notifications.EmailNotifications;
using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Commands.UserCommands.CreateUserCommand;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
        _mediator = mediator;
    }
    
    public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        _userRepository.Create(user);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        //send syncronos 
        await _mediator.Publish(new SendEmailNotification { Email = request.Email }, cancellationToken);

        return _mapper.Map<CreateUserResponse>(user);
    }
}