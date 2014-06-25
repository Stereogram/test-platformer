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
                    if (rect.Intersects(entity.HitBox))
                    {
                        Vector2f pos = entity.Position;
                        if (entity.HitBox.Bottom() >= rect.Top && entity.PrevHitBox.Bottom() <= rect.Top)
                        {
                            pos.Y = rect.Top - 32;
                        }
                        else if (entity.HitBox.Top <= rect.Bottom() && entity.PrevHitBox.Top >= rect.Bottom())
                        {
                            pos.Y = rect.Bottom();
                        }
                        else if (entity.HitBox.Right() >= rect.Left && entity.PrevHitBox.Right() <= rect.Left)
                        {
                            pos.X = rect.Left - 32;
                        }
                        else if (entity.HitBox.Left <= rect.Right() && entity.PrevHitBox.Left >= rect.Right())
                        {
                            pos.X = rect.Right();
                        }
                        entity.Position = pos;
                    }
                }
                
            }
        }
    }
}
