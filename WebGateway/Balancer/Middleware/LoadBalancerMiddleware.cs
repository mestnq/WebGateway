namespace Gateway.Balancer.Middleware;

public class LoadBalancerMiddleware
{
    private readonly RequestDelegate next;
    private readonly Balancer.Balancer loadBalancer;

    public LoadBalancerMiddleware(RequestDelegate next, Balancer.Balancer loadBalancer)
    {
        this.next = next;
        this.loadBalancer = loadBalancer;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var server = new Dictionary<string, string>();
        var keys = new List<string>();
        bool serversnotfound = true;
        while (serversnotfound)
        {
            server = loadBalancer.ChooseServer();
            keys = server.Keys.ToList();
            if (server == null)
            {
                break;
            }
            foreach (var key in keys)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(server[key]);
                        response.EnsureSuccessStatusCode();
                        serversnotfound = false;
                    }
                    catch (Exception)
                    {
                        serversnotfound = true;
                        Balancer.Balancer.RemoveServer(key, server[key]);
                    }
                }
            }

            foreach (var key in keys)
            {
                httpContext.Request.Headers.Add(key, server[key]);
            }
        }

        await next(httpContext);
    }
}