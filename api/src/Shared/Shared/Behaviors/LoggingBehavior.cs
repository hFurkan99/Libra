using MediatR;
using Microsoft.Extensions.Logging;

namespace Shared.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request={Request} - Response={Response} - RequestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            try
            {
                var response = await next(cancellationToken);
                logger.LogInformation("[END] Handled {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[ERROR] Exception handling {Request}: {Message}", typeof(TRequest).Name, ex.Message);
                throw;
            }
        }
    }
}