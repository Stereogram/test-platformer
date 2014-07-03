using System;
using System.Collections.Generic;
using System.Linq;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using testlol.Util;

namespace testlol.World.Entity.Projectile
{
    public class Projectiles : Drawable, IUpdatable
    {
        public readonly List<Entity> ProjectileList = new List<Entity>();

        private readonly List<Explosion> _explosions = new List<Explosion>();

        private readonly Texture _explosion = new Texture(@"assets/explosion.png");

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
            ProjectileList.RemoveAll(x => ((ITemporal)x).LifeTime >= ((ITemporal)x).MaxTime);
            foreach (Explosion explosion in _explosions)
            {
                explosion.Update(dt);
            }
            _explosions.RemoveAll(x => x.LifeTime >= x.MaxTime);
        }

        public void Shoot<T>(Vector2f p, Texture t, List<Tuple<string, int>>  anims) where T : class
        {
            ProjectileList.Add((Entity)Activator.CreateInstance(typeof(T), new object[] { p, t, anims }) );
        }

        public void Explode(Entity a)
        {
            _explosions.Add(new Explosion(a.Position,_explosion));
        }

    }
}
