using System;
using System.IO;
using SFML.Window;

namespace testlol.World
{
    public struct Map
    {
        private readonly int[,] _map;
        public int X { get { return _map.GetLength(1); } }
        public int Y { get { return _map.GetLength(0); } }

        public Map(Vector2u size)
        {
            _map = new int[size.Y,size.X];
        }

        public Map(int x, int y)
        {
            _map = new int[y, x];
        }

        public Map(int[,] a)
        {
            _map = a;
        }

        public int this[int a, int b]
        {
            get { return _map[a, b]; }
            set { _map[a, b] = value; }
        }

        public static implicit operator Map(int[,] a)
        {
            return new Map(a);
        }

        public static implicit operator int[,](Map a)
        {
            return a._map;
        }

        public static Map LoadMap(string s)
        {
            Map m = null;
            using (BinaryReader br = new BinaryReader(new FileStream(s, FileMode.Open)))
            {
                int x = br.ReadInt32();
                int y = br.ReadInt32();
                m = new Map(x,y);
                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        m[i, j] = br.ReadInt32();
                    }
                }
            }
            return m;
        }

        public void WriteMap(string s)
        {
            using (BinaryWriter bw = new BinaryWriter(new FileStream(s, FileMode.Create)))
            {
                bw.Write(X);
                bw.Write(Y);
                
                for (int i = 0; i < Y; i++)
                {
                    for (int j = 0; j < X; j++)
                    {
                        bw.Write(_map[i,j]);
                    }
                }
                bw.Flush();
            }
            
        }

    }
}
