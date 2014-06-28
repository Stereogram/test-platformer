using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;

namespace testlol.World.Entity.Projectile
{
    public class Bullet : Projectile, Drawable, IUpdatable
    {
        public Bullet(Vector2f p, Sprite sprite)
        {
            _sprite = sprite;
            _velocity = 500;
            MaxTime = Time.FromSeconds(1.5f);
            Position = new Vector2f(p.X+32,p.Y+16);
            LifeTime = Time.Zero;
        }

        public void Update(Time dt)
        {
            Vector2f pos = Position;
            pos.X += _velocity * (float) dt.Seconds;
            Position = pos;
            LifeTime += dt;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_sprite);
        }

    }

}
