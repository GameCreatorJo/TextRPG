using System;
using System.Collections.Generic;
using TextRPG.Class.Database.MonsterData;
using TextRPG.Class.Default;
using TextRPG.Class.Manager;

namespace TextRPG.Class.Database.MapData
{
    public class Map : DefaultMap
    {
        private readonly List<BuildingInfo> buildings;
        private readonly HashSet<(int x, int y)> portalEntrances = new();

        private readonly List<Monster> spawnTable;
        private readonly Random spawnRng = new Random();

        public Map(int width, int height, double encounterRate, List<BuildingInfo> buildings)
            : base(width, height, encounterRate)
        {
            this.buildings = buildings ?? new List<BuildingInfo>();
            this.spawnTable = new List<Monster>();
        }

        internal Map(int width, int height, double encounterRate, List<Monster> monsters)
            : base(width, height, encounterRate)
        {
            this.buildings = new List<BuildingInfo>();
            this.spawnTable = monsters ?? new List<Monster>();
        }

        public void BuildBuildings()
        {
            portalEntrances.Clear();
            if (buildings == null) return;

            foreach (var b in buildings)
            {
                int patternRows = b.Pattern.GetLength(0);
                int patternCols = b.Pattern.GetLength(1);
                int startY = b.Y - patternRows / 2;
                int startX = b.X - patternCols / 2;

                for (int dy = 0; dy < patternRows; dy++)
                {
                    for (int dx = 0; dx < patternCols; dx++)
                    {
                        int mapX = startX + dx;
                        int mapY = startY + dy;
                        if (mapX >= 0 && mapX < Width && mapY >= 0 && mapY < Height)
                        {
                            char symbol = b.Pattern[dy, dx];
                            if (symbol != ' ')
                                tiles[mapY, mapX] = TileType.Building;
                            if (symbol == '@')
                                portalEntrances.Add((mapX, mapY));
                        }
                    }
                }
            }
        }

        protected override bool IsWalkable(int x, int y)
        {
            return tiles[y, x] == TileType.Empty || portalEntrances.Contains((x, y));
        }

        public string GetAutoTransferTarget(string currentMapName, int playerX, int playerY, out int? spawnX, out int? spawnY)
        {
            spawnX = null;
            spawnY = null;

            switch (currentMapName)
            {
                case "Town":
                    if (playerX == 6 && playerY == 8)
                    {
                        spawnX = 10; spawnY = 17; // Inn 시작 좌표
                        return "Inn";
                    }
                    else if (playerX == 14 && playerY == 8)
                    {
                        spawnX = 10; spawnY = 17; // Shop 시작 좌표
                        return "Shop";
                    }
                    else if (playerX == 10 && playerY == 16)
                    {
                        spawnX = 10; spawnY = 17; // Dungeon 시작 좌표
                        return "Dungeon";
                    }
                    break;

                case "Inn":
                    if (playerX == 10 && playerY == 18)
                    {
                        spawnX = 6; spawnY = 9;
                        return "Town";
                    }
                    break;
                case "Shop":
                    if (playerX == 10 && playerY == 18)
                    {
                        spawnX = 14; spawnY = 9;
                        return "Town";
                    }
                    break;

                case "Dungeon":
                    if (playerX == 10 && playerY == 18)
                    {
                        spawnX = 10; spawnY = 16; // Town 던전 입구 위치로 귀환
                        return "Town";
                    }
                    break;
            }

            return null;
        }


        internal Monster GetRandomEncounter()
        {
            if (spawnTable == null || spawnTable.Count == 0)
                throw new InvalidOperationException("스폰 테이블 비어있음");
            var template = spawnRng.Next(spawnTable.Count);
            return new Monster(spawnTable[template]);
        }

        public override void Draw()
        {
            var player = GameManager.Instance.CreateManager.Player;

            string[] statLines = new string[Height];
            statLines[0] = "=== Status ===";
            statLines[1] = $"Player: @{PlayerX},{PlayerY}";
            statLines[2] = $"Lv    : {player.Lv}";
            statLines[3] = $"HP    : {player.Hp} / {player.MaxHp}";
            statLines[4] = $"MP    : 10 / 10";  // 임시
            statLines[5] = $"Gold  : {player.Gold}";
            statLines[6] = $"Job   : {player.Job}";
            statLines[7] = $"Name  : {player.Name}";

            for (int i = 8; i < Height; i++)
                statLines[i] = "";
            for (int y = 0; y < Height; y++)
            {
                Console.SetCursorPosition(0, y);
                for (int x = 0; x < 20; x++)
                {
                    char c;
                    if (tiles[y, x] == TileType.Wall) c = '#';
                    else if (tiles[y, x] == TileType.Empty) c = '.';
                    else if (tiles[y, x] == TileType.Obstacle) c = 'X';
                    else if (tiles[y, x] == TileType.Building)
                    {
                        bool isPortal = portalEntrances.Contains((x, y));
                        c = isPortal ? '@' : GetBuildingPatternChar(x, y);
                    }
                    else c = '?';

                    Console.Write(x == PlayerX && y == PlayerY ? '♥' : c);
                }
                Console.Write(" │ ");
                if (y < statLines.Length && !string.IsNullOrEmpty(statLines[y]))
                    Console.Write(statLines[y].PadRight(18));
                else
                    Console.Write(new string(' ', 18));
            }
        }

        private char GetBuildingPatternChar(int x, int y)
        {
            foreach (var b in buildings)
            {
                int patternRows = b.Pattern.GetLength(0);
                int patternCols = b.Pattern.GetLength(1);
                int startY = b.Y - patternRows / 2;
                int startX = b.X - patternCols / 2;
                int dx = x - startX;
                int dy = y - startY;
                if (dx >= 0 && dx < patternCols && dy >= 0 && dy < patternRows)
                {
                    char symbol = b.Pattern[dy, dx];
                    if (symbol != ' ')
                        return symbol == '@' ? '.' : symbol;
                }
            }
            return 'B';
        }
    }
}
