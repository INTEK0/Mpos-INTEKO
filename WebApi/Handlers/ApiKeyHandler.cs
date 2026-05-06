using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Handlers
{
    public class ApiKeyHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("X-Api-Key"))
                return request.CreateResponse(HttpStatusCode.Unauthorized);

            var key = request.Headers.GetValues("X-Api-Key").First();

            if (key != "b35345c0-bf0d-4a8d-91d0-6ebfc6f66cd8") //API KEY
                return request.CreateResponse(HttpStatusCode.Unauthorized);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}