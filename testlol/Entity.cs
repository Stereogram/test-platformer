using System;
using SFML.Graphics;
using SFML.Window;

namespace testlol
{
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
        public Entity(Texture t)
        {
            Sprite = new Sprite(t);
            Size = t.Size;
            BoundingBox = ConstructBoundingBox();
        }
        public abstract void Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states);

        private RectangleShape ConstructBoundingBox()
        {
            RectangleShape r = new RectangleShape((Vector2f)Size);
            r.Position = Sprite.Position;
            r.FillColor = Color.Transparent;
            r.OutlineThickness = 5;
            r.OutlineColor = Color.Red;
            return r;
        }

    }
}
