using SFML.Graphics;
using SFML.System;
using testlol.Util;

namespace testlol.World.Entity.Projectile
{
    public sealed class Laser : Entity, IUpdatable, ITemporal
    {
        public Time LifeTime { get; set; }
        public Time MaxTime { get; private set; }
        public bool Enabled { get; set; }
        public override FloatRect HitBox { get; protected set; }
        protected override Vector2i OffSet { get; set; }
        public override Vector2u Size { get; protected set; }
        private static readonly Texture _texture = Game.Textures[@"assets\player\laser.png"];

        public Laser(Vector2f p, bool direction)
            : base(_texture, new Vector2u(16, 16), null)//todo make not null
        {
            Position = new Vector2f(direction ? p.X + 32 : p.X - 32, p.Y + 16);
            OffSet = new Vector2i(16, 4);
            Size = new Vector2u(0, 0);
            HitBox = new FloatRect(Position.X, Position.Y, Size.X, Size.Y);
            Velocity = new Vector2f(direction ? 500 : -500, 0);
            MaxTime = Time.FromSeconds(1.5f);
            LifeTime = Time.Zero;
            Enabled = true;
        }

        public void Update(Time dt)
        {
            Vector2f pos = Position;
            pos.X += Velocity.X * dt.AsSeconds();
            Position = pos;
            LifeTime += dt;
            if (LifeTime >= MaxTime)
                Enabled = false;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (Enabled)
                target.Draw(Sprite);
#if DEBUG
            var t = HitBox.ToRectangleShape();
            t.FillColor = Color.Green;
            target.Draw(t);
#endif
        }

    }

}
