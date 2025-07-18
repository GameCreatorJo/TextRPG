using System;
using System.Collections.Generic;
using TextRPG.Class.Database.MonsterData;
using TextRPG.Class.Database.PlayerData;
using TextRPG.Class.Default;
using TextRPG.Class.Manager;
using TextRPG.Class.Scenes;

namespace TextRPG.Class.Database.MapData
{
    public class Map : DefaultMap
    {
        private bool isInBattle = false;
        private Monster currentBattleMonster = null;
        private string battleMessage = "";
        private int battleMenuCursor = 0;

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
                    if (playerX == 3 && playerY == 5)
                    {
                        spawnX = 10; spawnY = 17; // Inn 시작 좌표
                        return "Inn";
                    }
                    else if (playerX == 8 && playerY == 5)
                    {
                        spawnX = 10; spawnY = 17; // Shop 시작 좌표
                        return "Shop";
                    }
                    else if (playerX == 15 && playerY == 5)
                    {
                        spawnX = 10; spawnY = 17; // Guild 시작 좌표
                        return "Guild";
                    }
                    else if (playerX == 4 && playerY == 16)
                    {
                        spawnX = 10; spawnY = 17; // Dungeon 시작 좌표
                        return "Dungeon";
                    }
                    else if (playerX == 15 && playerY == 17)
                    {
                        spawnX = 10; spawnY = 17; // Dungeon 시작 좌표
                        return "Dungeon2";
                    }
                    break;

                case "Inn":
                    if (playerX == 10 && playerY == 18)
                    {
                        spawnX = 2; spawnY = 6;
                        return "Town";
                    }
                    else if (playerX == 10 && playerY == 10)
                    {
                        GameManager.Instance.Scene.ChangeScene("InnScene");
                        break;
                    }
                    break;
                case "Shop":
                    if (playerX == 10 && playerY == 18)
                    {
                        spawnX = 8; spawnY = 6;
                        return "Town";
                    }
                    else if (playerX == 10 && playerY == 10)
                    {
                        GameManager.Instance.Scene.ChangeScene("ShopScene");
                        break;
                    }
                    break;
                case "Guild":
                    if (playerX == 10 && playerY == 18)
                    {
                        spawnX = 15; spawnY = 6;
                        return "Town";
                    }
                    else if (playerX == 10 && playerY == 10)
                    {
                        GameManager.Instance.Scene.ChangeScene("QuestScene");
                        break;
                    }
                    break;

                case "Dungeon":
                    if (playerX == 10 && playerY == 18)
                    {
                        spawnX = 4; spawnY = 18; // Town 던전 입구 위치로 귀환
                        return "Town";
                    }
                    else if (playerX == 10 && playerY == 11)
                    {
                        spawnX = 10; spawnY = 11; // Battle 시작 좌표
                        return "Battle";
                    }
                    break;
                case "Dungeon2":
                    if (playerX == 10 && playerY == 18)
                    {
                        spawnX = 15; spawnY = 18; // Town 던전 입구 위치로 귀환
                        return "Town";
                    }
                    else if (playerX == 5 && playerY == 11)
                    {
                        spawnX = 5; spawnY = 11; // Battle 시작 좌표
                        return "Battle";
                    }
                    else if (playerX == 5 && playerY == 6)
                    {
                        spawnX = 5; spawnY = 6; // Battle 시작 좌표
                        return "Battle";
                    }
                    else if (playerX == 15 && playerY == 6)
                    {
                        spawnX = 15; spawnY = 6; // Battle 시작 좌표
                        return "Battle";
                    }
                    else if (playerX == 15 && playerY == 11)
                    {
                        spawnX = 15; spawnY = 11; // Battle 시작 좌표
                        return "Battle";
                    }
                    break;
            }

            return null;
        }

        public void Draw(int inventoryCursor)
        {
            var player = GameManager.Instance.CreateManager.Player;

            string[] statLines = new string[Height];
            statLines[0] = "=== Status ===";
            statLines[1] = $"Player: @{PlayerX},{PlayerY}";
            statLines[2] = $"Lv    : {player.Lv}";
            statLines[3] = $"HP    : {player.Hp} / {player.MaxHp}";
            statLines[4] = $"MP    : {player.Mp} / {player.MaxMp}";
            statLines[5] = $"Gold  : {player.Gold}";
            statLines[6] = $"Job   : {player.Job}";
            statLines[7] = $"Name  : {player.Name}";
            statLines[8] = $"Weapon: {player.Weapon}";
            statLines[9] = $"Str   : {player.Str} + {player.PlusStr}";
            statLines[10] = $"Armor : {player.Armor}";
            statLines[11] = $"ArmorPoint : {player.ArmorPoint} + {player.PlusArmorPoint}";
            statLines[12] = $"Exp   : {player.Exp}";

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
