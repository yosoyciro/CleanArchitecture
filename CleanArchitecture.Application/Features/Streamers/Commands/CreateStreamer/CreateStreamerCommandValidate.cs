using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandValidate : AbstractValidator<UpdateStreamerCommand>
    {
        public CreateStreamerCommandValidate()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("{Nombre} no puede estar en blanco")
                .NotNull().WithMessage("{Nombre} no admite NULL")
                .MaximumLength(50).WithMessage("{Nombre} no puede exceder los 50 caracteres");

            RuleFor(p => p.Url)
                .NotEmpty().WithMessage("{Url} no puede ser vacío")
                .NotNull();
        }
    }
}
