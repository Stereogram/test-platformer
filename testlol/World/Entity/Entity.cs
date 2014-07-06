using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using testlol.Util;

namespace testlol.World.Entity
{
    /// <summary>
    /// Base abstract class for entities, contains many properties.
    /// </summary>
    public abstract class Entity : Drawable
    {
        /// <summary>
        /// Hitbox used for collisions.
        /// </summary>
        public abstract FloatRect HitBox { get; protected set; }
        /// <summary>
        /// Position offset for the hitbox.
        /// </summary>
        protected abstract Vector2i OffSet { get; set; }
        public Vector2f Velocity { get; set; }
        /// <summary>
        /// -1 Left, 1 Right.
        /// </summary>
        public int Direction { get; set; }
        protected AnimatedSprite Sprite { get; private set; }

        public Vector2f Position
        {
            get { return Sprite.Position; }
            set
            {
                Sprite.Position = value;
                HitBox = new FloatRect(OffSet.X + value.X, OffSet.Y + value.Y, Size.X, Size.Y);
            }
        }
        /// <summary>
        /// Size of sprite.
        /// </summary>
        public abstract Vector2u Size { get; protected set; }
        public bool Jumping { get; set; }
        public Vector2i Location { get { return (Vector2i) Position/32; } }
        public void Play(string s, bool b)
        {
            Sprite.Play(s,b);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="texture">The sprite's texture</param>
        /// <param name="size">Size of the sprite, can be different from hitbox.</param>
        /// <param name="aList">animations, "name"|"number of frames"</param>
        protected Entity(Texture texture, Vector2u size, List<Tuple<string, int>> aList )
        {
            Sprite = new AnimatedSprite(texture, size, aList);
        }
        public abstract void Draw(RenderTarget target, RenderStates states);

    }
}
