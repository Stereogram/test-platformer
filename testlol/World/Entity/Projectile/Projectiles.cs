using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;
namespace testlol.World.Entity.Projectile
{
    public class Projectiles : Drawable, IUpdatable
    {
        public readonly List<Entity> ProjectileList = new List<Entity>();

        private readonly List<Explosion> _explosions = new List<Explosion>();

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Entity projectile in ProjectileList)
            {
                target.Draw(projectile);
            }
            foreach (Explosion explosion in _explosions)
            {
                target.Draw(explosion);
            }
        }

        public void Update(Time dt)
        {
            foreach (IUpdatable projectile in ProjectileList.Cast<IUpdatable>())
            {
                projectile.Update(dt);
            }
            ProjectileList.RemoveAll(x => !((ITemporal)x).Enabled);
            foreach (Explosion explosion in _explosions)
            {
                explosion.Update(dt);
            }
            _explosions.RemoveAll(x => !x.Enabled);
        }

        public void Shoot<T>(Vector2f p, bool b) where T : class
        {
            ProjectileList.Add((Entity)Activator.CreateInstance(typeof(T), new object[] { p, b }) );
        }

        public void Explode(Entity a)
        {
            ((ITemporal) a).Enabled = false;
            _explosions.Add(new Explosion(a.Position));
        }

    }
}
