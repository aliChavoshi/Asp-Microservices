using FluentValidation;

namespace Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandValidators : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidators()
    {
        RuleFor(x => x.CardNumber).NotEmpty().WithMessage("card number is required").MaximumLength(50)
            .WithMessage("{CardNumber} check the length");
    }
}