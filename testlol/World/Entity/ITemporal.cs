using NetEXT.TimeFunctions;

namespace testlol.World.Entity
{
    /// <summary>
    /// Interface used for entities with a measurable lifetime.
    /// </summary>
    public interface ITemporal
    {
        /// <summary>
        /// How long this entity has been "alive".
        /// </summary>
        Time LifeTime { get; set; }
        /// <summary>
        /// Time limit for entity.
        /// </summary>
        Time MaxTime { get; } 
    }
}