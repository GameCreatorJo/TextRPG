using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG.Class.Database.MapData;
using TextRPG.Class.Scenes;

namespace TextRPG.Class.Manager
{
    internal class GameManager
    {
        private CreateManager _createManager;
        public CreateManager CreateManager => _createManager;

        private BattleManager _battleManager;
        public BattleManager BattleManager => _battleManager;

        private Scene _scene;
        public Scene Scene => _scene;

        public static GameManager Instance { get; private set; }

        public GameManager()
        {
            Instance = this;
            _createManager = new CreateManager();
            _battleManager = new BattleManager();
            _scene = new Scene();

        }

        public void StartGame()
        {
            Console.WriteLine("Game started!");
            InitializeGame();
            // 초기 씬
            _scene = _createManager.SceneDatabase.SceneDictionary["MainScene"];
            _scene.Render();
            SelectAction();

        }

        public void InitializeGame()
        {
            _createManager.CreateGameWorld();
            Console.WriteLine("Game initialized!");
        }

        public void SelectAction()
        {
            while (true)
            {
                Console.WriteLine("1: 상태창, 2: 던전, 3: 상점, 4: 맵 이동, 5: 종료");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        _scene = _createManager.SceneDatabase.SceneDictionary["StatusScene"];
                        _scene.Render();
                        break;
                    case "2":
                        _scene = _createManager.SceneDatabase.SceneDictionary["DungeonScene"];
                        _scene.Render();
                        _battleManager.Battle(_createManager.Player);GameManager.Instance.CreateManager.Player.ShowInfo();
                        break;
                    case "3":
                        _scene = _createManager.SceneDatabase.SceneDictionary["ShopScene"];
                        _scene.Render();
                        break;
                    case "4":
                        var mapDb = _createManager.MapDatabase;
                        Map currentMap = mapDb.GetMap("Town");
                        currentMap.Initialize();
                        currentMap.BuildBuildings();

                        // 최초 플레이어 위치 중앙 또는 원하는 위치 지정
                        currentMap.PlayerX = currentMap.Width / 2;
                        currentMap.PlayerY = currentMap.Height / 2;

                        Console.Clear();

                        while (true)
                        {
                            currentMap.Draw();
                            Console.SetCursorPosition(0, currentMap.Height + 1);
                            Console.WriteLine("화살표 이동, Q: 종료");
                            var key = Console.ReadKey(true).Key;
                            if (key == ConsoleKey.Q) break;

                            bool moved = currentMap.TryMove(key);

                            if (moved)
                            {
                                string nextMap = currentMap.GetAutoTransferTarget(
                                    mapDb.Maps.FirstOrDefault(x => x.Value == currentMap).Key,
                                    currentMap.PlayerX,
                                    currentMap.PlayerY,
                                    out int? spawnX,
                                    out int? spawnY);

                                if (!string.IsNullOrEmpty(nextMap))
                                {
                                    currentMap = mapDb.GetMap(nextMap);
                                    currentMap.Initialize();
                                    currentMap.BuildBuildings();

                                    if (spawnX.HasValue && spawnY.HasValue)
                                    {
                                        currentMap.PlayerX = spawnX.Value;
                                        currentMap.PlayerY = spawnY.Value;
                                    }
                                    else
                                    {
                                        currentMap.PlayerX = currentMap.Width / 2;
                                        currentMap.PlayerY = currentMap.Height / 2;
                                    }

                                    Console.Clear();
                                    Console.WriteLine($"{nextMap} 맵으로 이동!");
                                    continue;
                                }

                                // Encounter 처리 등은 여기에 추가 가능
                            }
                        }

                        _scene = _createManager.SceneDatabase.SceneDictionary["MainScene"];
                        _scene.Render();
                        break;
                    case "5":
                        Console.WriteLine("게임 종료");
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }
    }
}