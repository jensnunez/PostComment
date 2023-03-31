using FluentValidation;

namespace RESTAPI_CORE.Modelos
{
    public class PostValidator : AbstractValidator<Post>
    {
        public PostValidator()
        {
            
            RuleFor(post => post.userId).NotNull().NotEmpty();
            RuleFor(post => post.title).NotNull().NotEmpty();
            RuleFor(post => post.body).NotNull().NotEmpty().WithMessage("campo requerido por favor");



        }
    }
}