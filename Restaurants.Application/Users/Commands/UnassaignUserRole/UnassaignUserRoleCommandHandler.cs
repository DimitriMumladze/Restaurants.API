using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
namespace Restaurants.Application.Users.Commands.UnassaignUserRole;

public class UnassaignUserRoleCommandHandler(ILogger<UnassaignUserRoleCommandHandler> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<UnassaignUserRoleCommand>
{
    public async Task Handle(UnassaignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Unassaigning user role {@Request}", request);

        var user = await userManager.FindByEmailAsync(request.UserEMail)
            ?? throw new NotFoundException(nameof(User), request.UserEMail);

        var role = await roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        await userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}
