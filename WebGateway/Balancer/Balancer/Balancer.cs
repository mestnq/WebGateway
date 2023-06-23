using System.Collections.Concurrent;

namespace Gateway.Balancer.Balancer;

public class Balancer
{
    private static readonly Dictionary<string, ConcurrentDictionary<string, int>> apiServers = new Dictionary<string, ConcurrentDictionary<string, int>>();
    private static readonly Random random = new Random();

    public Balancer(string envFilePath)
    {
        /*todo Считывать сервера из .env
         Формат: 
            APIName(ApiID)=url1
            .
            .
            .
            UrlN 
         */
    }

    public Dictionary<string, string> ChooseServer()
    {
        Dictionary<string, int> serverCounts = new Dictionary<string, int>();

        // вычисляем количество запросов для каждого сервера
        foreach (var apiServers in apiServers.Values)
        {
            foreach (var serverCount in apiServers)
            {
                int count = serverCount.Value;
                serverCounts[serverCount.Key] = serverCounts.GetValueOrDefault(serverCount.Key) + count;
            }
        }

        // группируем серверы по API
        var apiGroups = from apiServers in apiServers
            from serverCount in apiServers.Value
            group serverCount by apiServers.Key;

        Dictionary<string, string> leastLoadedServers = new Dictionary<string, string>();

        // находим для каждого API наименьше загруженный сервер
        foreach (var apiGroup in apiGroups)
        {
            int minCount = apiGroup.Min(x => x.Value);
            List<string> leastLoadedServersForApi = apiGroup.Where(x => x.Value == minCount).Select(x => x.Key).ToList();

            if (leastLoadedServersForApi.Count == 0)
            {
                throw new Exception($"No available servers are least loaded for API '{apiGroup.Key}'.");
            }

            // выбираем любой свободный наименьше загруженный сервер
            foreach (var server in leastLoadedServersForApi)
            {
                if (serverCounts.GetValueOrDefault(server) == minCount)
                {
                    leastLoadedServers[apiGroup.Key] = server;
                    break;
                }
            }
        }

        // выбираем любой из наименьше загруженных серверов
        return leastLoadedServers;
    }

    public static void AddServer(string apiId, string server)
    {
        apiServers.TryAdd(apiId, new ConcurrentDictionary<string, int>());
        apiServers.GetValueOrDefault(apiId)?.TryAdd(server, 0);
    }

    public static void RemoveServer(string apiId, string server)
    {
        ConcurrentDictionary<string, int> servers = apiServers.GetValueOrDefault(apiId);
        if (servers != null)
        {
            int value;
            servers.TryRemove(server, out value);
        }
    }
    
    public static void AddRequestToServer(string apiId, string server)
    {
        ConcurrentDictionary<string, int> servers = apiServers.GetValueOrDefault(apiId);
        if (servers != null)
        {
            servers.AddOrUpdate(
                server, 
                1, (key, value) => value + 1);
        }
    }

    public static void RemoveRequestFromServer(string apiId, string server)
    {
        ConcurrentDictionary<string, int> servers = apiServers.GetValueOrDefault(apiId);
        if (servers != null)
        {
            servers.AddOrUpdate(
                server,
                0, (key, value) => value > 0 ? value - 1: 0);
        }
    }
}