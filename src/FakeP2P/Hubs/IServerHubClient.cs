using System;
using System.Threading.Tasks;
using FakeP2P.Data;

namespace FakeP2P.Hubs
{
    /// <summary>
    /// Client interface for server hub clients.
    /// </summary>
    public interface IServerHubClient
    {
        /// <summary>
        /// Message sent when the user has joined a server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns>A task sending the message.</returns>
        Task JoinedServer(Server server);

        /// <summary>
        /// Message sent when a user has left the server.
        /// </summary>
        /// <param name="serverId">The server identifier.</param>
        /// <returns>A task sending the message.</returns>
        Task LeftServer(Guid serverId);

        /// <summary>
        /// Message sent when a different user has joined a server.
        /// </summary>
        /// <param name="serverId">The server identifier.</param>
        /// <param name="player">The player.</param>
        /// <returns>A task sending the message.</returns>
        Task PlayerJoined(Guid serverId, Player player);

        /// <summary>
        /// Message sent when a different user has left a server.
        /// </summary>
        /// <param name="serverId">The server identifier.</param>
        /// <param name="playerId">The player identifier.</param>
        /// <returns>A task sending the message.</returns>
        Task PlayerLeft(Guid serverId, string playerId);

        /// <summary>
        /// Performs an action of the client.
        /// </summary>
        /// <param name="serverId">The id of the server.</param>
        /// <param name="action">The action.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="data">The data.</param>
        /// <returns>A task sending the message.</returns>
        Task Action(Guid serverId, string action, string sender, object data);
    }
}
