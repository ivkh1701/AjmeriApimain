using Ajmera_Api.Controllers;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace Ajmera_Api.Middleware
{
    public class ErrorMiddleWare
    {
        #region fields

        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorMiddleWare> _logger;


        #endregion

        #region ctor
        public ErrorMiddleWare(RequestDelegate next, ILogger<ErrorMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        #endregion

        #region invoke method
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                _logger.LogError(error?.Message);
                await response.WriteAsync(result);
            }
        }

        #endregion

        #region utilities
        public class AppException : Exception
        {
            public AppException() : base() { }

            public AppException(string message) : base(message) { }

            public AppException(string message, params object[] args)
                : base(String.Format(CultureInfo.CurrentCulture, message, args))
            {
            }
        }

        #endregion
    }
}
