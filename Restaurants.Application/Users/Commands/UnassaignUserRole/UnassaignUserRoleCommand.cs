using MediatR;

namespace Restaurants.Application.Users.Commands.UnassaignUserRole;

public class UnassaignUserRoleCommand : IRequest
{
    public string UserEMail { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}
