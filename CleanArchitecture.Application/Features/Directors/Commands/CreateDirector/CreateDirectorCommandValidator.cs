using FluentValidation;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(e => e.Nombre)
                .NotEmpty()
                .NotNull().WithMessage("{Nombre} no puede ser nulo");

            RuleFor(e => e.Apellido)
                .NotEmpty()
                .NotNull().WithMessage("{Apellido} no puede ser nulo");

        }
    }
}
