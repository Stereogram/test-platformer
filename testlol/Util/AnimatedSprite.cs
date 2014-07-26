﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NetEXT.Animation;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using testlol.World.Entity;

namespace testlol.Util
{
    public class AnimatedSprite: IUpdatable, Drawable
    {
        private readonly List<Tuple<string,int>> _animationList;
        private readonly Animator<Sprite, string> _animator;
        private readonly AnimatedObject<Sprite> _animatedObject;
        private readonly Sprite _sprite;
        private readonly Vector2u _tSize;
        private readonly Vector2u _size;

        public Vector2f Position
        {
            get { return _sprite.Position; }
            set { _sprite.Position = value; }
        }

        public AnimatedSprite(Texture texture, Vector2u size, List<Tuple<string,int>> aList = null)
        {
            _sprite = new Sprite(texture);
            _size = size;
            if (aList == null) return;
            _animationList = aList;
            
            _animatedObject = new AnimatedObject<Sprite>(_sprite);
            _tSize = texture.Size;
            _animator = new Animator<Sprite, string>();
            AddFrames();
            _animator.PlayAnimation(aList[0].Item1, true);
        }

        private void AddFrames()
        {
            int total = 0;
            foreach (Tuple<string, int> t in _animationList)
            {
                FrameAnimation<Sprite> temp = new FrameAnimation<Sprite>();
                for (int i = 0; i < t.Item2; i++, total++)
                {
                    int tu = (int) ((total % (_tSize.X / _size.Y)) * _size.X);
                    int tv = (int) ((total / (_tSize.Y / _size.Y)) * _size.Y);
                    temp.AddFrame(1, new IntRect(tu, tv, (int)_size.X, (int)_size.Y));
                }
                _animator.AddAnimation(t.Item1,temp,Time.FromSeconds(1));
            }
        }

        public void Play(string s, bool b)
        {
            _animator.PlayAnimation(s, b);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if(_animatedObject != null)
                _animator.Animate(_animatedObject);
            target.Draw(_sprite);
        }

        public void Update(Time dt)
        {
            if(_animatedObject != null)
                _animator.Update(dt);

        }

        public void WriteAnimations(string s)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(s, FileMode.Create)))
            {
                foreach (Tuple<string, int> t in _animationList)
                {
                    sw.Write(t.Item1 + " " + t.Item2 + '\n');
                }
                sw.Flush();
            }
        }

        public static List<Tuple<string, int>> ReadAnimations(string s)
        {
            return File.ReadAllLines(s).Select(line => line.Split(' ')).Select(a => new Tuple<string, int>(a[0], int.Parse(a[1]))).ToList();
        }
    }
}
