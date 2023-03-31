using FluentValidation;

namespace RESTAPI_CORE.Modelos
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {

            RuleFor(comment => comment.postId).NotNull().NotEmpty();
            RuleFor(comment => comment.name).NotNull().NotEmpty();
            RuleFor(comment => comment.body).NotNull().NotEmpty().WithMessage("campo requerido por favor");
            RuleFor(comment => comment.email).NotNull().NotEmpty().WithMessage("campo requerido por favor");


        }
    }
}