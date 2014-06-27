using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;

namespace testlol.World.Entity.Projectile
{
    public class Bullet : Projectile, Drawable, IUpdatable
    {
        
        Vector2f Position { get { return _sprite.Position; } set { _sprite.Position = value; } }

        public Bullet(Vector2f p)
        {
            _velocity = 1000;
            MaxTime = Time.FromSeconds(2.0f);
            Position = p;
            LifeTime = Time.Zero;
            _sprite = new Sprite(new Texture(@"assets\player\bullet.png"));
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
