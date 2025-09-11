using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    validators.Select(validator => validator
                        .ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .Where(result => result.Errors.Count != 0)
                    .SelectMany(result => result.Errors)
                    .ToList();

                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }

            return await next();
        }
    }

}
