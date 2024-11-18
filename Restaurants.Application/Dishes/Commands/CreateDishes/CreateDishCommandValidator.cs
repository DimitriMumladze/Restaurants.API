using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDishes;

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(d => d.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price should not be a negative number.");
    }
}
