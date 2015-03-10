using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using testlol.Util;

namespace testlol.World.Entity.Projectile
{
    public class Explosion : Drawable, ITemporal, IUpdatable
    {
        public Time LifeTime { get; set; }
        public Time MaxTime { get; private set; }
        public bool Enabled { get; set; }
        private readonly AnimatedSprite _sprite;
        private static readonly Texture _texture = Game.Textures[@"assets\explosion.png"];
        private static readonly List<Animation> _animations = Game.Animations[@"assets\explosion.ani"];

        public Vector2f Position { get { return _sprite.Position; } set { _sprite.Position = value; } }
        public Explosion(Vector2f p)
        {
            LifeTime = Time.Zero;
            _sprite = new AnimatedSprite(_texture, new Vector2u(48, 48), _animations) { Position = p };
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
