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
        private readonly List<Projectile> _projectiles = new List<Projectile>(); 
        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Projectile projectile in _projectiles)
            {
                target.Draw((Drawable)projectile);
            }
        }

        public void Update(Time dt)
        {
            foreach (IUpdatable projectile in _projectiles.Cast<IUpdatable>())
            {
                projectile.Update(dt);
            }
            _projectiles.RemoveAll(x => x.LifeTime >= x.MaxTime);
        }

        public void Shoot<T>(Vector2f p) where T : class
        {
            _projectiles.Add((Projectile)Activator.CreateInstance(typeof(T), new object[] { p }) );
        }

    }
}
