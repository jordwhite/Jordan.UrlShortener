using FluentValidation;
using FluentValidation.Results;
using Jordan.UrlShortener.UserInterface.Api.Client.Requests;

namespace Jordan.UrlShortener.UserInterface.Api.Validators
{
    public class GenerateValidator : AbstractValidator<GenerateShortenedUrlRequest>
    {
        public GenerateValidator() => 
            ValidateFullUrl();

        private void ValidateFullUrl()
        {
            RuleFor(request => request.FullUrl)
                .NotEmpty();

            RuleFor(request => request.FullUrl)
                .Custom((fullUrl, context) =>
                {
                    if (Uri.IsWellFormedUriString(fullUrl, UriKind.Absolute))
                        return;

                    context.AddFailure(
                        new ValidationFailure(
                            "FullUrl",
                            "Must be a well formed absolute URI with a valid protocol e.g. 'https://google.co.uk/'"
                        )
                    );
                });
        }
    }
}
