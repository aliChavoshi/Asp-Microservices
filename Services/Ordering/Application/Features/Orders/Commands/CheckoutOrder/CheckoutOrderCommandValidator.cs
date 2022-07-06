using FluentValidation;

namespace Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(x => x.CardNumber).NotEmpty().WithMessage("card number is required").MaximumLength(50)
            .WithMessage("{CardNumber} check the length");
    }
}