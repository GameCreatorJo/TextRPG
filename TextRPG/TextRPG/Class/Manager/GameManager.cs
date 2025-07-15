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
        /*
        private BattleManager battleManager;
        public BattleManager BattleManager
        {
            get { return battleManager; }
        }
        
        */
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
            
            Console.WriteLine("Game initialized!");
            // Load game settings, characters, etc.
        }
        public void SelectAction()
        {
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine("상점에 가기 선택됨.");
                    // 상점 로직 호출
                    break;
                case "2":
                    Console.WriteLine("전투하기 선택됨.");
                    // 퀘스트 로직 호출
                    break;
                case "3":
                    Console.WriteLine("게임 종료 선택됨.");
                    // 전투 로직 호출
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    SelectAction();
                    break;
            }
        }
    }
}
