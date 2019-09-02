using System;
using System.Collections.Generic;
using System.Linq;
using ExtensionNet.Reflective;
using FakeP2P.Data;

namespace FakeP2P.Services
{
    /// <summary>
    /// Service for mainting the server status of all servers.
    /// </summary>
    public class ServerService
    {
        private readonly Dictionary<Guid, Server> servers = new Dictionary<Guid, Server>();
        private readonly object lockObject = new object();

        /// <summary>
        /// Gets all servers.
        /// </summary>
        /// <returns>A list of all servers.</returns>
        public IEnumerable<Server> GetAllServers()
        {
            lock (lockObject)
            {
                return servers.Select(x => x.Value.Copy(true)).ToList();
            }
        }

        /// <summary>
        /// Gets the server with the given identifier.
        /// </summary>
        /// <param name="serverId">The server identifier.</param>
        /// <returns>A copy of the server with the given identifier.</returns>
        public Server GetServer(Guid serverId)
        {
            lock (lockObject)
            {
                if (servers.TryGetValue(serverId, out Server server))
                {
                    return server.Copy(true);
                }
                else
                {
                    throw ServerNotFound(serverId);
                }
            }
        }

        /// <summary>
        /// Creates a server.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="connectionId">The connection ID of the player.</param>
        /// <param name="hostName">The hosting player name.</param>
        /// <returns>A copy of a newly created server.</returns>
        public Server CreateServer(string name, string type, string connectionId, string hostName)
        {
            Server server = new Server
            {
                Id = Guid.NewGuid(),
                Name = name,
                Type = type,
            };

            Player host = new Player
            {
                Id = connectionId,
                Name = hostName,
            };

            server.Players[host.Id] = host;

            lock (lockObject)
            {
                servers[server.Id] = server;
                return server.Copy(true);
            }
        }

        /// <summary>
        /// Adds a player.
        /// </summary>
        /// <param name="guid">The unique identifier of the server.</param>
        /// <param name="connectionId">The connection ID of the player.</param>
        /// <param name="name">The name of the player.</param>
        /// <returns>The ID of the newly added player.</returns>
        public Player AddPlayer(Guid guid, string connectionId, string name)
        {
            Player player = new Player
            {
                Id = connectionId,
                Name = name,
            };

            lock (lockObject)
            {
                if (servers.TryGetValue(guid, out Server server))
                {
                    server.Players[player.Id] = player;
                    return player.Copy(true);
                }
                else
                {
                    throw ServerNotFound(guid);
                }
            }
        }

        /// <summary>
        /// Removes a player.
        /// </summary>
        /// <param name="serverId">The server identifier.</param>
        /// <param name="playerId">The player identifier.</param>
        public void RemovePlayer(Guid serverId, string playerId)
        {
            lock (lockObject)
            {
                if (servers.TryGetValue(serverId, out Server server))
                {
                    if (server.Players.TryGetValue(playerId, out Player player))
                    {
                        server.Players.Remove(playerId);

                        if (server.Players.Count <= 0)
                        {
                            servers.Remove(serverId);
                        }
                    }
                    else
                    {
                        throw PlayerNotFound(playerId);
                    }
                }
                else
                {
                    throw ServerNotFound(serverId);
                }
            }
        }

        /// <summary>
        /// Removes a player.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        public void RemovePlayer(string playerId)
        {
            lock (lockObject)
            {
                foreach (KeyValuePair<Guid, Server> pair in servers)
                {
                    if (servers.TryGetValue(pair.Key, out Server server))
                    {
                        if (server.Players.TryGetValue(playerId, out Player player))
                        {
                            server.Players.Remove(playerId);

                            if (server.Players.Count <= 0)
                            {
                                servers.Remove(pair.Key);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the players.
        /// </summary>
        /// <param name="serverId">The server identifier.</param>
        /// <returns>A list of all connection IDs of the connected players.</returns>
        public IReadOnlyList<string> GetPlayers(Guid serverId)
        {
            lock (lockObject)
            {
                if (servers.TryGetValue(serverId, out Server server))
                {
                    return server.Players.Select(x => x.Value.Id).ToList();
                }
                else
                {
                    throw ServerNotFound(serverId);
                }
            }
        }

        private Exception ServerNotFound(Guid guid)
            => new ArgumentException($"No server with id '{guid}' was found.");

        private Exception PlayerNotFound(string guid)
            => new ArgumentException($"No player with id '{guid}' was found.");
    }
}
