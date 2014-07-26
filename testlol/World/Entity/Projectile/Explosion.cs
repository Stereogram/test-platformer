
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using testlol.Util;

namespace testlol.World.Entity.Projectile
{
    public class Explosion : Drawable, ITemporal, IUpdatable
    {
        public Time LifeTime { get; set; }
        public Time MaxTime { get; private set; }
        public bool Enabled { get; set; }

        private readonly AnimatedSprite _sprite;

        public Vector2f Position { get { return _sprite.Position; } set { _sprite.Position = value; } }
        public Explosion(Vector2f p, Texture t)
        {
            LifeTime = Time.Zero;
            _sprite = new AnimatedSprite(t, new Vector2u(48,48), AnimatedSprite.ReadAnimations(@"assets/explosion.txt")) { Position = p };
            MaxTime = Time.FromSeconds(1);
            Enabled = true;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if(Enabled)
                target.Draw(_sprite);
        }

        public void Update(Time dt)
        {
            LifeTime += dt;
            if (LifeTime >= MaxTime)
                Enabled = false;
            _sprite.Update(dt);
        }
    }
}
