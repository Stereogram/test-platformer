using System;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;

namespace testlol.World.Entity.Projectile
{
    public abstract class Projectile
    {
        protected float _velocity;
        public Time LifeTime { get; set; }
        public Time MaxTime;
        protected Sprite _sprite = new Sprite(new Texture(@"assets\player\bullet.png"));
        Vector2f Position { get { return _sprite.Position; } set { _sprite.Position = value; } }
    }
}
