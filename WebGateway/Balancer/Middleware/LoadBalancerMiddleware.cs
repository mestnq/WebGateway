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
        var server = loadBalancer.ChooseServer();
        var keys = server.Keys.ToList();
        foreach (var key in keys)
        {
            httpContext.Request.Headers.Add(key, server[key]);
        }
        await next(httpContext);
    }
}