using System;
using System.Collections.Generic;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using testlol.Util;

namespace testlol.World.Entity.Projectile
{
    public sealed class Bullet : Entity, IUpdatable, ITemporal
    {
        public Time LifeTime { get; set; }
        public Time MaxTime { get; private set; }
        public bool Enabled { get; set; }
        public override FloatRect HitBox { get; protected set; }
        protected override Vector2i OffSet { get; set; }
        public override Vector2u Size { get; protected set; }

        public Bullet(Vector2f p, Texture texture, List<Tuple<string, int>> anims) : base(texture, new Vector2u(16,16), anims)
        {
            Position = new Vector2f(p.X+32,p.Y+16);
            OffSet = new Vector2i(16,4);
            Size = new Vector2u(16, 16);
            HitBox = new FloatRect(Position.X, Position.Y, Size.X, Size.Y);
            Velocity = new Vector2f(500,0);
            MaxTime = Time.FromSeconds(1.5f);
            LifeTime = Time.Zero;
            Enabled = true;
        }

        public void Update(Time dt)
        {
            Vector2f pos = Position;
            pos.X += Velocity.X * (float) dt.Seconds;
            Position = pos;
            LifeTime += dt;
            if (LifeTime >= MaxTime)
                Enabled = false;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if(Enabled)
                target.Draw(Sprite);
#if DEBUG
            target.Draw(new RectangleShape(HitBox.ToRectangleShape()));
#endif
        }

    }

}
