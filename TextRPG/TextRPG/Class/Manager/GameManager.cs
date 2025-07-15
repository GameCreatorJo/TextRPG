using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG.Class.Scenes;

namespace TextRPG.Class.Manager
{
    internal class GameManager
    {
        private CreateManager _createManager;
        public CreateManager CreateManager
        {
            get { return _createManager; }
        }
        private BattleManager battleManager;
        public BattleManager BattleManager
        {
            get { return battleManager; }
        }
        private Scene _scene;
        public Scene Scene
        {
            get { return _scene; }
        }

        public static GameManager Instance { get; private set; }

        public GameManager()
        {
            Instance = this;
            _createManager = new CreateManager();
            _scene = new Scene(); // Initialize with a default scene
            battleManager = new BattleManager();
        }

        public void StartGame()
        {
            Console.WriteLine("Game started!");
            InitializeGame();
            GameManager.Instance.Scene.Render();
            SelectAction();

            // Initialize game components here
        }
        public void InitializeGame()
        {
            _createManager.CreateGameWorld();
            Console.WriteLine("Game initialized!");
            // Load game settings, characters, etc.
        }
        public void SelectAction()
        {
            while (true)
            {
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("상태창 선택됨.");
                        _scene = _createManager.SceneDatabase.SceneDictionary["StatusScene"]; // Use SceneDatabase dictionary
                        _scene.Render(); // 상태창 씬 렌더링
                        break;
                    case "2":
                        _scene = _createManager.SceneDatabase.SceneDictionary["DungeonScene"]; // Use SceneDatabase dictionary
                        _scene.Render(); // 상태창 씬 렌더링
                        GameManager.Instance.BattleManager.Battle(GameManager.Instance.CreateManager.Player);
                        Console.WriteLine("전투하기 선택됨.");
                        // 전투 호출
                        break;
                    case "3":
                        _scene = _createManager.SceneDatabase.SceneDictionary["ShopScene"]; // Use SceneDatabase dictionary
                        _scene.Render(); // 상태창 씬 렌더링
                        Console.WriteLine("상점가기 선택됨.");
                        break;
                    case "4":
                        Console.WriteLine("게임 종료 선택됨.");
                        // 게임 종료
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                        continue;
                }
                _scene = _createManager.SceneDatabase.SceneDictionary["MainScene"]; // Use SceneDatabase dictionary
                _scene.Render();
            }
        }
    }
}
