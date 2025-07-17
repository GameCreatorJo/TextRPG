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
    public class GameManager
    {
        private CreateManager _createManager;
        public CreateManager CreateManager => _createManager;

        private BattleManager _battleManager;
        public BattleManager BattleManager => _battleManager;

        private MapManager _mapManager;
        public MapManager MapManager => _mapManager;

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
            _questManager = new QuestManager();
        }

        public void StartGame()
        {
            Console.WriteLine("Game started!");
            InitializeGame();
            _scene = _createManager.SceneDatabase.SceneDictionary["MainScene"];
            _scene.Render();
            SelectAction();

        }

        public void InitializeGame()
        {
            QuestManager.Instance.Initialize(new QuestDatabase());

            _createManager.CreateGameWorld();
            Console.WriteLine("Game initialized!");
        }

        public void SelectAction()
        {
            while (true)
            {
                Console.WriteLine("1: 상태창 2: 던전, 3: 상점, 4: 맵으로 이동, 5: 퀘스트 6: 게임종료");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        _scene.ChangeScene("StatusScene");
                        break;
                    case "2":
                        _scene.ChangeScene("DungeonScene");
                        break;
                    case "3":
                        _scene.ChangeScene("ShopScene");
                        break;
                    case "4":
                        _mapManager.StartMap("Town");
                        _mapManager.RunMapLoop();

                        _scene.ChangeScene("MainScene");
                        break;
                    case "5":
                        _scene.ChangeScene("QuestScene");

                        _scene = _createManager.SceneDatabase.SceneDictionary["QuestScene"];
                        _scene.Render();
                        //QuestManager.Instance.ShowQuestMenu();

                        _scene.ChangeScene("QuestScene");
                        QuestManager.Instance.ShowQuestMenu();

                        break;
                    case "6":
                        Console.WriteLine("게임을 종료합니다.");
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }
    }
}