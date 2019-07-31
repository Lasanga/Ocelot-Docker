using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BringIt.Gateway.Api
{
    public class ClientHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request.RequestUri);
            Console.WriteLine(cancellationToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
    