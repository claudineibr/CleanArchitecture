namespace CleanArchitecture.Application.Commands.UserCommands.CreateUserCommand;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        _userRepository.Create(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        user.AddDomainEvent(new SendEmailNotification(request.Email));

        return _mapper.Map<CreateUserResponse>(user);
    }
}