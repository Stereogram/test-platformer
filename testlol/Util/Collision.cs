using System;
using System.Collections.Generic;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using testlol.World.Entity;
using testlol.World.Level;

namespace testlol.Util
{
    

    public class Collision: IUpdatable
    {
        private readonly TileMap _tileMap;
        private readonly List<Entity> _entities;

        public Collision(TileMap tileMap, List<Entity> entities )
        {
            _tileMap = tileMap;
            _entities = entities;
        }

        public void Update(Time dt)
        {
            foreach (FloatRect rect in _tileMap.Collidables)
            {
                foreach (Entity entity in _entities)
                {
                    Box a = new Box(entity);
                    Box b = new Box(rect);
                    float x;
                    float y;
                    if (Box.AABB(a, b, out x, out y))
                    {
                        Vector2f pos = entity.Position;
                        pos.X += x;
                        pos.Y += y;
                        entity.Position = pos;
                        if (y > 0) entity.Velocity = new Vector2f(entity.Velocity.X, 1);
                        if (entity.Jumping) entity.Jumping = false;
                    }
                    //if (rect.Intersects(entity.HitBox))
                    //{

                    //    Vector2f pos = entity.Position;
                    //    pos += 


                    //    //if (entity.HitBox.Right() >= rect.Left && entity.Velocity.X >= 0)
                    //    //{
                    //    //    pos.X = rect.Left - 32;
                    //    //    Console.WriteLine("Right");
                    //    //}
                    //    //else if (entity.HitBox.Left <= rect.Right() && entity.Velocity.X <= 0)
                    //    //{
                    //    //    pos.X = rect.Right();
                    //    //    Console.WriteLine("Left");
                    //    //}
                    //    //if (entity.HitBox.Bottom() >= rect.Top && entity.Velocity.Y <= 0)
                    //    //{
                    //    //    pos.Y = rect.Top - 64;
                    //    //    Console.WriteLine("Bottom");
                    //    //}
                    //    //else if (entity.HitBox.Top <= rect.Bottom()&& entity.Velocity.Y >= 0)
                    //    //{
                    //    //    pos.Y = rect.Bottom();
                    //    //    Console.WriteLine("Top");
                    //    //}

                    //    entity.Position = pos;
                    //}
                }
            }
        }
    }

    // describes an axis-aligned rectangle with a velocity
    public class Box
    {
        public Box(Entity e)
        {
            X = e.HitBox.Left;
            Y = e.HitBox.Top;
            W = e.HitBox.Width;
            H = e.HitBox.Height;
            Vx = e.Velocity.X;
            Vy = e.Velocity.Y;
        }

        public Box(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
            Vx = 0.0f;
            Vy = 0.0f;
        }

        public Box(FloatRect a)
        {
            X = a.Left;
            Y = a.Top;
            W = a.Width;
            H = a.Height;
            Vx = 0.0f;
            Vy = 0.0f;
        }

        // position of top-left corner
        public float X, Y;

        // dimensions
        public float W, H;

        // velocity
        public float Vx, Vy;


        // returns true if the boxes are colliding (velocities are not used)
        // ReSharper disable once InconsistentNaming
        public static bool AABBCheck(Box b1, Box b2)
        {
            return !(b1.X + b1.W < b2.X || b1.X > b2.X + b2.W || b1.Y + b1.H < b2.Y || b1.Y > b2.Y + b2.H);
        }

        // returns true if the boxes are colliding (velocities are not used)
        // moveX and moveY will return the movement the b1 must move to avoid the collision
        // ReSharper disable once InconsistentNaming
        public static bool AABB(Box b1, Box b2, out float moveX, out float moveY)
        {
            moveX = moveY = 0.0f;

            float l = b2.X - (b1.X + b1.W);
            float r = (b2.X + b2.W) - b1.X;
            float t = b2.Y - (b1.Y + b1.H);
            float b = (b2.Y + b2.H) - b1.Y;

            // check that there was a collision
            if (l > 0 || r < 0 || t > 0 || b < 0)
                return false;

            // find the offset of both sides
            moveX = Math.Abs(l) < r ? l : r;
            moveY = Math.Abs(t) < b ? t : b;

            // only use whichever offset is the smallest
            if (Math.Abs(moveX) < Math.Abs(moveY))
                moveY = 0.0f;
            else
                moveX = 0.0f;

            return true;
        }

        // returns a box the spans both a current box and the destination box
        public static Box GetSweptBroadphaseBox(Box b)
        {
            Box broadphasebox = new Box(0.0f, 0.0f, 0.0f, 0.0f)
            {
                X = b.Vx > 0 ? b.X : b.X + b.Vx,
                Y = b.Vy > 0 ? b.Y : b.Y + b.Vy,
                W = b.Vx > 0 ? b.Vx + b.W : b.W - b.Vx,
                H = b.Vy > 0 ? b.Vy + b.H : b.H - b.Vy
            };

            return broadphasebox;
        }

        // performs collision detection on moving box b1 and static box b2
        // returns the time that the collision occurred (where 0 is the start of the movement and 1 is the destination)
        // getting the new position can be retrieved by box.x = box.x + box.vx * collisiontime
        // normalx and normaly return the normal of the collided surface (this can be used to do a response)
        // ReSharper disable once InconsistentNaming
        public static float SweptAABB(Box b1, Box b2, out float normalx, out float normaly)
        {
            float xInvEntry, yInvEntry;
            float xInvExit, yInvExit;

            // find the distance between the objects on the near and far sides for both x and y
            if (b1.Vx > 0.0f)
            {
                xInvEntry = b2.X - (b1.X + b1.W);
                xInvExit = (b2.X + b2.W) - b1.X;
            }
            else
            {
                xInvEntry = (b2.X + b2.W) - b1.X;
                xInvExit = b2.X - (b1.X + b1.W);
            }

            if (b1.Vy > 0.0f)
            {
                yInvEntry = b2.Y - (b1.Y + b1.H);
                yInvExit = (b2.Y + b2.H) - b1.Y;
            }
            else
            {
                yInvEntry = (b2.Y + b2.H) - b1.Y;
                yInvExit = b2.Y - (b1.Y + b1.H);
            }

            // find time of collision and time of leaving for each axis (if statement is to prevent divide by zero)
            float xEntry, yEntry;
            float xExit, yExit;

            if (b1.Vx == 0.0f)
            {
                xEntry = -float.PositiveInfinity;
                xExit = float.PositiveInfinity;
            }
            else
            {
                xEntry = xInvEntry/b1.Vx;
                xExit = xInvExit/b1.Vx;
            }

            if (b1.Vy == 0.0f)
            {
                yEntry = -float.PositiveInfinity;
                yExit = float.PositiveInfinity;
            }
            else
            {
                yEntry = yInvEntry/b1.Vy;
                yExit = yInvExit/b1.Vy;
            }

            // find the earliest/latest times of collision
            float entryTime = Math.Max(xEntry, yEntry);
            float exitTime = Math.Min(xExit, yExit);

            // if there was no collision
            if (entryTime > exitTime || xEntry < 0.0f && yEntry < 0.0f || xEntry > 1.0f || yEntry > 1.0f)
            {
                normalx = 0.0f;
                normaly = 0.0f;
                return 1.0f;
            }
            // calculate normal of collided surface
            if (xEntry > yEntry)
            {
                if (xInvEntry < 0.0f)
                {
                    normalx = 1.0f;
                    normaly = 0.0f;
                }
                else
                {
                    normalx = -1.0f;
                    normaly = 0.0f;
                }
            }
            else
            {
                if (yInvEntry < 0.0f)
                {
                    normalx = 0.0f;
                    normaly = 1.0f;
                }
                else
                {
                    normalx = 0.0f;
                    normaly = -1.0f;
                }
            }

            // return the time of collision
            return entryTime;
        }
    }

}
