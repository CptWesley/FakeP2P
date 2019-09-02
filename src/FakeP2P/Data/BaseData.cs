using ExtensionNet.Reflective;
using Newtonsoft.Json;

namespace FakeP2P.Data
{
    /// <summary>
    /// Base class for data classes.
    /// </summary>
    public abstract class BaseData
    {
        /// <inheritdoc/>
        public override bool Equals(object obj)
            => this.InternallyEquals(obj);

        /// <inheritdoc/>
        public override int GetHashCode()
            => this.GetInternalHashCode();

        /// <inheritdoc/>
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
