using System.Collections.Generic;
using System.Linq;
using FakeP2P.Data;
using FakeP2P.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeP2P.Controllers
{
    /// <summary>
    /// Controller for requesting available servers.
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("Api/[controller]")]
    public class MasterServerController : Controller
    {
        private readonly ServerService serverService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterServerController"/> class.
        /// </summary>
        /// <param name="serverService">The server service.</param>
        public MasterServerController(ServerService serverService)
            => this.serverService = serverService;

        /// <summary>
        /// Gets all servers.
        /// </summary>
        /// <returns>All servers available.</returns>
        [HttpGet("[action]")]
        public IEnumerable<Server> GetServers()
            => serverService.GetAllServers();

        /// <summary>
        /// Gets the servers of the given type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>All servers available of the given type.</returns>
        [HttpGet("[action]/{type}")]
        public IEnumerable<Server> GetServers(string type)
            => serverService.GetAllServers().Where(x => x.Type == type);
    }
}
