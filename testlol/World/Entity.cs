using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;

namespace testlol.World
{
    /// <summary>
    /// Base abstract class for entities contains many properties.
    /// </summary>
    abstract class Entity : Drawable
    {
        protected RectangleShape BoundingBox { get; private set; }
        public bool DrawBoundingBox { get; set; }
        public Vector2f Velocity { get; set; }
        public int Direction { get; set; }
        protected AnimatedSprite Sprite { get; private set; }
        public Vector2f Position { get { return Sprite.Position; } set { Sprite.Position = value; BoundingBox.Position = new Vector2f(value.X+16,value.Y); } }
        public Vector2u Size { get; private set; }
        public bool Jumping { get; set; }

        public void Play(string s, bool b)
        {
            Sprite.Play(s,b);
        }

        protected Entity(Texture t, List<Tuple<string, int>> aList )
        {
            Sprite = new AnimatedSprite(t, aList);
            Size = new Vector2u(64,64);
            BoundingBox = new RectangleShape(new Vector2f(32,64))
            {
                Position = new Vector2f(Sprite.Position.X+16,Sprite.Position.Y),
                FillColor = Color.Transparent,
                OutlineThickness = 5,
                OutlineColor = Color.Red
            };
        }
        public abstract void Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states);

    }
}
