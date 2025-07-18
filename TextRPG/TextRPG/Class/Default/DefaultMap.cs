using System;

namespace TextRPG.Class.Default
{
    public enum TileType
    {
        Empty,
        Wall,
        Obstacle,
        Building
    }

    public class DefaultMap
    {
        public int Width { get; }
        public int Height { get; }
        protected TileType[,] tiles;

        public int PlayerX { get; set; }
        public int PlayerY { get; set; }

        private readonly Random rng;
        public double EncounterRate { get; set; }

        public DefaultMap(int width, int height, double encounterRate = 0.0)
        {
            if (width < 3 || height < 3)
                throw new ArgumentException("Width와 Height는 최소 3 이상이어야 합니다.");

            Width = width;
            Height = height;
            EncounterRate = encounterRate;

            tiles = new TileType[Height, Width];
            rng = new Random();

            PlayerX = Width / 2;
            PlayerY = Height / 2;
        }

        public void Initialize()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    tiles[y, x] = (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                        ? TileType.Wall : TileType.Empty;
        }

        public void AddObstacle(int x, int y)
        {
            if (InBounds(x, y) && tiles[y, x] == TileType.Empty)
                tiles[y, x] = TileType.Obstacle;
        }

        public void SetTile(int x, int y, TileType type)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
                tiles[y, x] = type;
        }

        protected bool InBounds(int x, int y)
            => x >= 0 && x < Width && y >= 0 && y < Height;

        public virtual void Draw()
        {
            for (int y = 0; y < Height; y++)
            {
                Console.SetCursorPosition(0, y);
                for (int x = 0; x < Width; x++)
                {
                    char c = tiles[y, x] switch
                    {
                        TileType.Wall => '#',
                        TileType.Empty => '.',
                        TileType.Obstacle => 'X',
                        TileType.Building => 'B',
                        _ => '?'
                    };
                    Console.Write(x == PlayerX && y == PlayerY ? '@' : c);
                }
            }
        }

        public virtual bool TryMove(ConsoleKey key)
        {
            int nx = PlayerX, ny = PlayerY;
            switch (key)
            {
                case ConsoleKey.UpArrow: ny--; break;
                case ConsoleKey.DownArrow: ny++; break;
                case ConsoleKey.LeftArrow: nx--; break;
                case ConsoleKey.RightArrow: nx++; break;
                default: return false;
            }
            if (InBounds(nx, ny) && IsWalkable(nx, ny))
            {
                PlayerX = nx; PlayerY = ny;
                return true;
            }
            return false;
        }

        protected virtual bool IsWalkable(int x, int y)
        {
            return tiles[y, x] == TileType.Empty || tiles[y, x] == TileType.Building;
        }

        public bool CheckEncounter()
            => rng.NextDouble() < EncounterRate;
    }
}
