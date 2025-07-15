using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Scene;

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
       
        private MainScene _mainScene;
        public MainScene MainScene
        {
            get { return _mainScene; }
        }
        public static GameManager Instance { get; private set; }

        public GameManager()
        {
            Instance = this;
            _createManager = new CreateManager();
            _mainScene = new MainScene("메인 씬", "게임의 시작 화면입니다."); // 적절한 이름과 설명 입력
            //battleManager = new BattleManager();

        }

        public void StartGame()
        {
            Console.WriteLine("Game started!");
            InitializeGame();
            GameManager.Instance.MainScene.Render();
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
                Console.WriteLine("어떤 행동을 하시겠습니까?");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("상태창 선택됨.");
                        GameManager.Instance.CreateManager.Player.ShowInfo(); // 상태창 보기 메소드 호출
                        GameManager.Instance.CreateManager.Player.ShowInventory(); // 인벤토리 보기 메소드 호출
                        break;
                    case "2":
                        Console.WriteLine("전투하기 선택됨.");
                        // 전투 호출
                        break;
                    case "3":
                        Console.WriteLine("게임 종료 선택됨.");
                        // 게임 종료
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                        SelectAction();
                        break;
                }
            }
        }
    }
}
