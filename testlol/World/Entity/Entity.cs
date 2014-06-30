using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using testlol.Util;

namespace testlol.World.Entity
{
    /// <summary>
    /// Base abstract class for entities contains many properties.
    /// </summary>
    public abstract class Entity : Drawable
    {
        public FloatRect HitBox { get; private set; }
        public Vector2f Velocity { get; set; }
        public int Direction { get; set; }
        protected AnimatedSprite Sprite { get; private set; }

        public Vector2f Position
        {
            get { return Sprite.Position; }
            set
            {
                Sprite.Position = value;
                HitBox = new FloatRect(value.X + 16, value.Y, 32, 64);
            }
        }
        public Vector2u Size { get; private set; }
        public bool Jumping { get; set; }
        public Vector2i Location { get { return (Vector2i) Position/32; } }
        public void Play(string s, bool b)
        {
            Sprite.Play(s,b);
        }
        
        protected Entity(Texture t, List<Tuple<string, int>> aList )
        {
            Sprite = new AnimatedSprite(t, aList);
            Size = new Vector2u(64,64);
            HitBox = new FloatRect(Position.X-16,Position.Y,32,64);
        }
        public abstract void Draw(RenderTarget target, RenderStates states);

    }
}
