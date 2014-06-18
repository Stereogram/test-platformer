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
        public float Mass { get; set; }
        public Vector2f Force { get; set; }
        protected Sprite Sprite { get; private set; }
        public Vector2f Position { get { return Sprite.Position; } set { Sprite.Position = value; BoundingBox.Position = value; } }
        public Vector2u Size { get; private set; }
        public bool Jumping { get; set; }

        protected Entity(Texture t)
        {
            Sprite = new Sprite(t);
            Size = t.Size;
            BoundingBox = new RectangleShape((Vector2f)Size)
            {
                Position = Sprite.Position,
                FillColor = Color.Transparent,
                OutlineThickness = 5,
                OutlineColor = Color.Red
            };
        }
        public abstract void Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states);

    }
}
