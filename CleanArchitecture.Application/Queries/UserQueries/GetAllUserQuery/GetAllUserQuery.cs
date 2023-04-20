using MediatR;

namespace CleanArchitecture.Application.Queries.UserQueries.GetAllUserQuery;

public sealed record GetAllUserQuery : IRequest<List<GetAllUserResponse>>;