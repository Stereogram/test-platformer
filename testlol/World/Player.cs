using System;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;

namespace testlol.World
{
    class Player : Entity, IUpdatable
    {

        
        public Player(Texture t):base(t)
        {
            //DrawBoundingBox = true;
            Position = new Vector2f(200, 200);
            Mass = 1;
            
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Sprite, states);
            if(DrawBoundingBox)
            {
                target.Draw(BoundingBox, states);
            }
        }

        public void Update(Time dt)
        {
            Velocity += (1 / Mass * Force) * (float)dt.Seconds;

            if(Math.Abs(Velocity.X) < 50 && Direction == 0)
            {
                Velocity = new Vector2f(0, Velocity.Y);
            }

            if (Math.Abs(Velocity.X) >= 600 && Direction != 0)
            {
                Velocity = new Vector2f(Direction*600, Velocity.Y);
            }

            Position += Velocity * (float)dt.Seconds;
            Force = new Vector2f(0, 0);


            if(Position.Y >= Game.Size.Y-Size.Y)
            {
                Position = new Vector2f(Position.X, Game.Size.Y-Size.Y);
                Velocity = new Vector2f(Velocity.X, 0);
                Jumping = false;
            }
            if(Position.X < 0)
            {
                Position = new Vector2f(0, Position.Y);
                Velocity = new Vector2f(0, Velocity.Y);
            }
            else if(Position.X >= Game.Size.X - Size.X)
            {
                Position = new Vector2f(Game.Size.X - Size.X, Position.Y);
                Velocity = new Vector2f(0, Velocity.Y);
            }

        }

    }
}
