namespace FakeP2P.Data
{
    /// <summary>
    /// Data class for storing player information.
    /// </summary>
    /// <seealso cref="BaseData" />
    public class Player : BaseData
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string Id { get; set; }
    }
}
