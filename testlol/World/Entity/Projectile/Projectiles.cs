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
        public readonly List<Projectile> ProjectileList = new List<Projectile>(); 
        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Projectile projectile in ProjectileList)
            {
                target.Draw((Drawable)projectile);
            }
        }

        public void Update(Time dt)
        {
            foreach (IUpdatable projectile in ProjectileList.Cast<IUpdatable>())
            {
                projectile.Update(dt);
            }
            ProjectileList.RemoveAll(x => x.LifeTime >= x.MaxTime);
        }

        public void Shoot<T>(Vector2f p, Sprite sprite) where T : class
        {
            ProjectileList.Add((Projectile)Activator.CreateInstance(typeof(T), new object[] { p, sprite }) );
        }

    }
}
