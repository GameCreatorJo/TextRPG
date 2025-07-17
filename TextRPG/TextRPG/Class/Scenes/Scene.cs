using System;
using TextRPG.Class.Default;
using TextRPG.Class.Manager;

namespace TextRPG.Class.Scenes
{
    public class Scene : DefaultScene
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Scene()
        {
            Name = "MainScene";
            Description = "첫 화면입니다.";
        }

        public Scene(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void ChangeScene(string name)
        {
            Name = name;
            Description = GameManager.Instance.CreateManager.SceneDatabase.SceneDictionary[name].Description;
            Render();
        }

        public override void Render()
        {
            switch (Name)
            {
                case "MainScene":
                    RenderMainScene();
                    break;
                case "StatusScene":
                    RenderStatusScene();
                    break;
                case "ShopScene":
                    RenderShopScene();
                    break;
                case "DungeonScene":
                    //RenderDungeonScene();
                    GameManager.Instance.BattleManager.Battle(GameManager.Instance.CreateManager.Player);
                    break;
                case "QuestScene":
                    RenderQuestScene();
                    break;
                case "InnScene":
                    RenderInnScene();
                    break;
                default:
                    Console.WriteLine("알 수 없는 씬입니다.");
                    break;
            }
        }

        public void RenderMainScene()
        {
            Console.Clear();
            Console.WriteLine("+=======================================+");
            Console.WriteLine("|            스파르타 마을              |");
            Console.WriteLine("+=======================================+");
            Console.WriteLine("| 1. 상태창 보기                        |");
            Console.WriteLine("| 2. 던전으로 이동                      |");
            Console.WriteLine("| 3. 상점으로 이동                      |");
            Console.WriteLine("| 4. 맵으로 이동                        |");
            Console.WriteLine("| 5. 퀘스트 메뉴                        |");
            Console.WriteLine("| 6. 종료                               |");
            Console.WriteLine("+======================================+");
        }

        public void RenderStatusScene()
        {
            Console.Clear();
            Console.WriteLine("+======================================+");
            Console.WriteLine("|               상태창                 |");
            Console.WriteLine("+======================================+");
            GameManager.Instance.CreateManager.Player.ShowInfo();
            GameManager.Instance.CreateManager.Player.ShowInventory();
            Console.WriteLine("+======================================+");
            Console.WriteLine("1번을 눌러 장착 관리, 0번을 눌러 돌아갑니다.");

            bool isInput = false;
            while (!isInput)
            {
                string rawInput = Console.ReadLine();
                int input;

                if (int.TryParse(rawInput, out input))
                {
                    switch (input)
                    {
                        case 0:
                            isInput = true;
                            break;
                        case 1:
                            isInput = true;
                            GameManager.Instance.CreateManager.Player.ManageEquipment();
                            RenderStatusScene();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다. 올바른 번호를 입력해주세요.\n");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 정수를 입력하세요.");
                }
            }
        }

        public void RenderShopScene()
        {
            Console.Clear();
            Console.WriteLine("+======================================+");
            Console.WriteLine("|                 상점                 |");
            Console.WriteLine("+======================================+");
            Console.WriteLine("| - 포션: 50G                          |");
            Console.WriteLine("| - 강화석: 100G                       |");
            Console.WriteLine("| - 무기: 500G                         |");
            Console.WriteLine("+======================================+");
            Console.WriteLine("엔터를 눌러 돌아갑니다.");
            Console.ReadLine();
        }
        public void RenderInnScene()
        {
            Console.Clear();
            Console.WriteLine("+======================================+");
            Console.WriteLine("|                여관                  |");
            Console.WriteLine("+======================================+");
            Console.WriteLine("| 1. 휴식: 100G                        |");
            Console.WriteLine("| 2. 저장: 0G                          |");
            Console.WriteLine("+======================================+");
            Console.WriteLine("1. 휴식 2. 저장 3. 나가기");
            string input = Console.ReadLine();
            switch(input)
            {
                case "1":
                    if (GameManager.Instance.CreateManager.Player.Gold >= 100)
                    {
                        GameManager.Instance.CreateManager.Player.SpendGold(100);
                        GameManager.Instance.CreateManager.Player.TakeDamage(-(GameManager.Instance.CreateManager.Player.MaxHp- GameManager.Instance.CreateManager.Player.Hp), 0);
                        Console.WriteLine("휴식을 취했습니다. 체력이 회복되었습니다.");
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다.");
                    }
                    break;
                case "2":
                    GameManager.Instance.CreateManager.CreateSave();
                    Console.WriteLine("게임이 저장되었습니다.");
					Console.WriteLine("enter를 눌러 나갑니다.");
					string nt = Console.ReadLine();

					break;
                case "3":
                    Console.WriteLine("여관을 나갑니다.");
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    break;

            }
        }

        public void RenderDungeonScene()
        {
            Console.Clear();
            Console.WriteLine("+=======================================+");
            Console.WriteLine("|               던전 입구               |");
            Console.WriteLine("+=======================================+");
            Console.WriteLine("| 1. 쉬운 난이도                        |");
            Console.WriteLine("| 2. 보통 난이도                        |");
            Console.WriteLine("| 3. 어려운 난이도                      |");
            Console.WriteLine("| 0. 마을로 돌아가기                    |");
            Console.WriteLine("+======================================+");
            Console.WriteLine("엔터를 눌러 돌아갑니다.");
            Console.ReadLine();
        }
        //퀘스트씬 작업
        public void RenderQuestScene()
        {
            Console.Clear();
            Console.WriteLine("+======================================+");
            Console.WriteLine("|           퀘스트 관리소              |");
            Console.WriteLine("+======================================+");

            //QuestManager.Instance.ShowQuestMenu();

            


        }
    }
}