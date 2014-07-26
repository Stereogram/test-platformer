using System.Collections.Generic;
using System.IO;
using System.Linq;
using SFML.Window;
using testlol.Util;

namespace testlol.World.Level
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
            List<List<int>> list = new List<List<int>>();

            foreach (string line in File.ReadAllLines(s))
            {
                List<int> temp = new List<int>();
                temp.AddRange(line.Split(',').Select(int.Parse));
                list.Add(temp);
            }

            int x = list[0].Count;
            int y = list.Count;
            int[,] r = new int[y, x];
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    r[i, j] = list[i][j];
                }
            }
            return r;
        }

        public void WriteMap(string s)
        {
            File.WriteAllText(s,_map.Print());
        }


    }
}