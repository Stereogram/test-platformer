using System;
using System.Collections.Generic;
using System.Linq;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;

namespace testlol.World.Entity.Projectile
{
    public class Projectiles : Drawable, IUpdatable
    {
        public readonly List<Entity> ProjectileList = new List<Entity>(); 
        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Entity projectile in ProjectileList)
            {
                target.Draw(projectile);
            }
        }

        public void Update(Time dt)
        {
            foreach (IUpdatable projectile in ProjectileList.Cast<IUpdatable>())
            {
                projectile.Update(dt);
            }

            ProjectileList.RemoveAll(x => ((ITemporal)x).LifeTime >= ((ITemporal)x).MaxTime);
        }

        public void Shoot<T>(Vector2f p, Texture t, List<Tuple<string, int>>  anims) where T : class
        {
            ProjectileList.Add((Entity)Activator.CreateInstance(typeof(T), new object[] { p, t, anims }) );
        }

        public void Remove(Entity a)
        {
            ProjectileList.Remove(a);
        }

    }
}
