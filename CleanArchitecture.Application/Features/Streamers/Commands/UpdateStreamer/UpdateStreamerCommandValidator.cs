using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
    {
        public UpdateStreamerCommandValidator()
        {
            RuleFor(x => x.Nombre)
                .NotNull().WithMessage("{Nombre} no acepta NULL")
                .NotEmpty().WithMessage("{Nombre} no puede ser vacío");

            RuleFor(x => x.Url)
                .NotNull().WithMessage("{Url} no acepta NULL")
                .NotEmpty().WithMessage("{Url} no puede ser vacío");
        }
    }
}
