using System;
using System.Collections.Generic;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;

namespace testlol.World.Entity.Projectile
{
    public class Bullet : Entity, IUpdatable, ITemporal
    {
        public Time LifeTime { get; set; }
        public Time MaxTime { get; private set; }
        public Bullet(Vector2f p, Texture texture, List<Tuple<string, int>> anims) : base(texture,anims)
        {
            Velocity = new Vector2f(500,0);
            MaxTime = Time.FromSeconds(1.5f);
            Position = new Vector2f(p.X+32,p.Y+16);
            LifeTime = Time.Zero;
        }

        public void Update(Time dt)
        {
            Vector2f pos = Position;
            pos.X += Velocity.X * (float) dt.Seconds;
            Position = pos;
            LifeTime += dt;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Sprite);
        }

    }

}
