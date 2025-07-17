using System;
using System.Linq;
using TextRPG.Class.Database.MapData;
using TextRPG.Class.Manager;

namespace TextRPG.Class.Manager
{
    public enum GameState
    {
        MapMove,        // 일반 맵 이동 모드
        InventorySelect // 인벤토리 메뉴 선택 모드
    }

    public class MapManager
    {
        public Map CurrentMap { get; private set; }
        private GameState _gameState = GameState.MapMove;

        private int _inventoryCursor = 0;

        public MapManager()
        {
        }

        public void StartMap(string mapName)
        {
            var mapDatabase = GameManager.Instance.CreateManager.MapDatabase;

            if (!mapDatabase.Maps.ContainsKey(mapName))
                throw new ArgumentException($"맵 '{mapName}' 없음");

            //맵 초기화
            CurrentMap = mapDatabase.GetMap(mapName);
            CurrentMap.Initialize();
            CurrentMap.BuildBuildings();

            CurrentMap.PlayerX = CurrentMap.Width / 2;
            CurrentMap.PlayerY = CurrentMap.Height / 2;

            Console.Clear();
            _gameState = GameState.MapMove;
            _inventoryCursor = 0;
        }


        public void RunMapLoop()
        {
            var mapDatabase = GameManager.Instance.CreateManager.MapDatabase;
            var player = GameManager.Instance.CreateManager.Player;

            while (true)
            {
                Console.Clear();

                switch (_gameState)
                {
                    case GameState.MapMove:
                        CurrentMap.Draw(-1); // 인벤토리 커서 숨김

                        Console.SetCursorPosition(0, CurrentMap.Height + 1);
                        Console.WriteLine("화살표 이동, ESC: 인벤토리, Q: 종료");

                        var key = Console.ReadKey(true).Key;

                        if (key == ConsoleKey.Q)
                            return;

                        if (key == ConsoleKey.Escape)
                        {
                            _gameState = GameState.InventorySelect;
                            _inventoryCursor = 0;
                            break;
                        }

                        bool moved = CurrentMap.TryMove(key);
                        if (moved)
                        {
                            string nextMap = CurrentMap.GetAutoTransferTarget(
                                mapDatabase.Maps.FirstOrDefault(x => x.Value == CurrentMap).Key,
                                CurrentMap.PlayerX,
                                CurrentMap.PlayerY,
                                out int? spawnX,
                                out int? spawnY);

                            if (!string.IsNullOrEmpty(nextMap))
                            {
                                if (nextMap == "Battle")
                                {
                                    StartBattleScene();
                                }
                                else
                                {
                                    CurrentMap = mapDatabase.GetMap(nextMap);
                                    CurrentMap.Initialize();
                                    CurrentMap.BuildBuildings();

                                    if (spawnX.HasValue && spawnY.HasValue)
                                    {
                                        CurrentMap.PlayerX = spawnX.Value;
                                        CurrentMap.PlayerY = spawnY.Value;
                                    }
                                    else
                                    {
                                        CurrentMap.PlayerX = CurrentMap.Width / 2;
                                        CurrentMap.PlayerY = CurrentMap.Height / 2;
                                    }

                                    Console.Clear();
                                    Console.WriteLine($"{nextMap} 맵으로 이동!");
                                }
                            }
                        }
                        break;

                    case GameState.InventorySelect:
                        CurrentMap.Draw(_inventoryCursor); // 인벤토리 커서 표시
                        Console.SetCursorPosition(0, CurrentMap.Height + 1);
                        Console.WriteLine("ESC: 돌아가기, ↑/↓: 이동, Enter: 선택");

                        var invKey = Console.ReadKey(true).Key;

                        if (invKey == ConsoleKey.Escape)
                        {
                            _gameState = GameState.MapMove;
                            break;
                        }

                        if (invKey == ConsoleKey.UpArrow)
                        {
                            if (_inventoryCursor > 0)
                                _inventoryCursor--;
                        }
                        else if (invKey == ConsoleKey.DownArrow)
                        {
                            if (_inventoryCursor < player.Inventory.Count - 1)
                                _inventoryCursor++;
                        }
                        else if (invKey == ConsoleKey.Enter)
                        {
                            var selectedItem = player.Inventory.ElementAtOrDefault(_inventoryCursor);
                            if (selectedItem != null)
                            {
                                Console.Clear();
                                Console.WriteLine($"선택한 아이템: {selectedItem.Name}");
                                Console.WriteLine("아무 키나 눌러 계속...");
                                Console.ReadKey(true);
                            }
                        }
                        break;
                }
            }
        }


        private void StartBattleScene()
        {
            var battleManager = GameManager.Instance.BattleManager;
            battleManager.StartDungeonBattle(GameManager.Instance.CreateManager.Player);
            Console.WriteLine("전투가 종료되었습니다. 아무 키나 눌러 계속...");
            Console.ReadKey(true);
        }

    }
}
