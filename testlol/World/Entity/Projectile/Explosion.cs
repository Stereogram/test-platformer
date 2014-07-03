
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

        private readonly AnimatedSprite _sprite;

        public Vector2f Position { get { return _sprite.Position; } set { _sprite.Position = value; } }
        public Explosion(Vector2f p, Texture t)
        {
            LifeTime = Time.Zero;
            _sprite = new AnimatedSprite(t,null){Position = p};
            MaxTime = Time.FromSeconds(1);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_sprite);
        }

        public void Update(Time dt)
        {
            LifeTime += dt;
            _sprite.Update(dt);
        }
    }
}
