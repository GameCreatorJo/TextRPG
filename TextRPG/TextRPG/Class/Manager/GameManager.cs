using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG.Class.Database.MapData;
using TextRPG.Class.Database.QuestData;
using TextRPG.Class.Scenes;


namespace TextRPG.Class.Manager
{
    internal class GameManager
    {
        private CreateManager _createManager;
        public CreateManager CreateManager => _createManager;

        private BattleManager _battleManager;
        public BattleManager BattleManager => _battleManager;

        private MapManager _mapManager;
        public MapManager MapManager => _mapManager;

        //퀘스트 매니저 삽입
        private QuestManager _questManager;
        public QuestManager QuestManager => _questManager;

        private Scene _scene;
        public Scene Scene => _scene;

        public static GameManager Instance { get; private set; }

        public GameManager()
        {
            Instance = this;
            _createManager = new CreateManager();
            _battleManager = new BattleManager();
            _scene = new Scene();
            _mapManager = new MapManager();
            //퀘스트 매니저 삽입
            _questManager = new QuestManager();
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
            // 퀘스트 매니저 초기화
            QuestManager.Instance.Initialize(new QuestDatabase());

            _createManager.CreateGameWorld();
            Console.WriteLine("Game initialized!");
        }

        public void SelectAction()
        {
            while (true)
            {
                Console.WriteLine("1: 상태창, 2: 던전, 3: 상점, 4: 맵 이동, 5: 퀘스트 메뉴 6: 종료");
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
                        var mapManager = new MapManager();
                        mapManager.StartMap("Town");
                        mapManager.RunMapLoop();

                        _scene = _createManager.SceneDatabase.SceneDictionary["MainScene"];
                        _scene.Render();
                        break;
                    case "5":
<<<<<<< Updated upstream
                        _scene = _createManager.SceneDatabase.SceneDictionary["QuestScene"];
                        _scene.Render();
                        //QuestManager.Instance.ShowQuestMenu();
=======
                        _scene.ChangeScene("QuestScene");
                        QuestManager.Instance.ShowQuestMenu();
>>>>>>> Stashed changes
                        break;
                    case "6":
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