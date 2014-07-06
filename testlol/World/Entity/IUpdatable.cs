using NetEXT.TimeFunctions;

namespace testlol.World.Entity
{
    /// <summary>
    /// Interface for any entity which needs to be updated.
    /// </summary>
    interface IUpdatable
    {
        /// <summary>
        /// Updates entity with the delta time.
        /// </summary>
        /// <param name="dt">"delta time" time between last frame and current.</param>
        void Update(Time dt);
    }
}
