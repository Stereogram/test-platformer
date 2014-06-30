using NetEXT.TimeFunctions;

namespace testlol.World.Entity
{
    public interface ITemporal
    {
        Time LifeTime { get; set; }
        Time MaxTime { get; } 
    }
}