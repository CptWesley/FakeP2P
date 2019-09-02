using System;
using System.Collections.Generic;

namespace FakeP2P.Data
{
    /// <summary>
    /// Data class for storing server information.
    /// </summary>
    /// <seealso cref="BaseData" />
    public class Server : BaseData
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the server.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the players.
        /// </summary>
        public Dictionary<string, Player> Players { get; } = new Dictionary<string, Player>();
    }
}
