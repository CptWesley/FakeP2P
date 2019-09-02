using System;
using System.Threading.Tasks;
using FakeP2P.Data;
using FakeP2P.Services;
using Microsoft.AspNetCore.SignalR;

namespace FakeP2P.Hubs
{
    /// <summary>
    /// Hub for connections to a server.
    /// </summary>
    /// <seealso cref="Hub{IServerHubClient}" />
    public class ServerHub : Hub<IServerHubClient>
    {
        private readonly ServerService serverService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerHub"/> class.
        /// </summary>
        /// <param name="serverService">The server service.</param>
        public ServerHub(ServerService serverService)
            => this.serverService = serverService;

        /// <summary>
        /// Creates the server.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="serverType">Type of the server.</param>
        /// <returns>A task creating the server.</returns>
        public async Task CreateServer(string userName, string serverName, string serverType)
        {
            Server server = serverService.CreateServer(serverName, serverType, Context.ConnectionId, userName);
            await Clients.Caller.JoinedServer(server);
        }

        /// <summary>
        /// Joins the server.
        /// </summary>
        /// <param name="serverId">The server identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns>A task joining the server.</returns>
        public async Task JoinServer(Guid serverId, string userName)
        {
            Player player = serverService.AddPlayer(serverId, Context.ConnectionId, userName);
            Server server = serverService.GetServer(serverId);
            await Clients.Caller.JoinedServer(server);
            await Clients.Clients(serverService.GetPlayers(serverId)).PlayerJoined(serverId, player);
        }

        /// <summary>
        /// Leave the server.
        /// </summary>
        /// <param name="serverId">The server identifier.</param>
        /// <returns>A task leaving the server.</returns>
        public async Task LeaveServer(Guid serverId)
        {
            serverService.RemovePlayer(serverId, Context.ConnectionId);
            await Clients.Clients(serverService.GetPlayers(serverId)).PlayerLeft(serverId, Context.ConnectionId);
            await Clients.Caller.LeftServer(serverId);
        }

        /// <summary>
        /// Broadcast an action to the server.
        /// </summary>
        /// <param name="serverId">The server identifier.</param>
        /// <param name="action">The action.</param>
        /// <param name="data">The data.</param>
        /// <returns>A task sending the action to the server.</returns>
        public Task Action(Guid serverId, string action, object data)
            => Clients.Clients(serverService.GetPlayers(serverId)).Action(serverId, action, Context.ConnectionId, data);

        /// <inheritdoc/>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            serverService.RemovePlayer(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
