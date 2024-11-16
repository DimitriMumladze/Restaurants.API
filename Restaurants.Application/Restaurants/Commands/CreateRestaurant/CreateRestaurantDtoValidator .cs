using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;
public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100)
            .WithMessage("Please use a name length in range of [3 - 100] !");

        RuleFor(dto => dto.Category)
            .NotEmpty()
            .WithMessage("Insert a valid Category!");

        RuleFor(dto => dto.Description)
            .NotEmpty()
            .WithMessage("Please provide a valid Description!");

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide a valid Email!");

        RuleFor(dto => dto.ContactNumber)
            .NotEmpty()
            .WithMessage("Please prove a valid Number!");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please use a postal code type [xx - xxx]!");

    }
}
