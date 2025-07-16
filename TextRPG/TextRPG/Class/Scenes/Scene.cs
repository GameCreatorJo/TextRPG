using System;
using TextRPG.Class.Default;
using TextRPG.Class.Manager;

namespace TextRPG.Class.Scenes
{
    internal class Scene : DefaultScene
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
                default:
                    Console.WriteLine("알 수 없는 씬입니다.");
                    break;
            }
        }

        public void RenderMainScene()
        {
            Console.Clear();
            Console.WriteLine("+======================================+");
            Console.WriteLine("|         🌟 스파르타 마을 🌟         |");
            Console.WriteLine("+======================================+");
            Console.WriteLine("| 1. 상태창 보기                         |");
            Console.WriteLine("| 2. 던전으로 이동                       |");
            Console.WriteLine("| 3. 상점으로 이동                       |");
            Console.WriteLine("| 4. 맵으로 이동                        |");
            Console.WriteLine("| 5. 종료                               |");
            Console.WriteLine("+======================================+");
        }

        public void RenderStatusScene()
        {
            Console.Clear();
            Console.WriteLine("+======================================+");
            Console.WriteLine("|            🛡 상태창 🛡            |");
            Console.WriteLine("+======================================+");
            GameManager.Instance.CreateManager.Player.ShowInfo();
            GameManager.Instance.CreateManager.Player.ShowInventory();
            Console.WriteLine("+======================================+");
            Console.WriteLine("엔터를 눌러 돌아갑니다.");
            Console.ReadLine();
        }

        public void RenderShopScene()
        {
            Console.Clear();
            Console.WriteLine("+======================================+");
            Console.WriteLine("|             🛒 상점 🛒             |");
            Console.WriteLine("+======================================+");
            Console.WriteLine("| - 포션: 50G                          |");
            Console.WriteLine("| - 강화석: 100G                       |");
            Console.WriteLine("| - 무기: 500G                         |");
            Console.WriteLine("+======================================+");
            Console.WriteLine("엔터를 눌러 돌아갑니다.");
            Console.ReadLine();
        }

        public void RenderDungeonScene()
        {
            Console.Clear();
            Console.WriteLine("+======================================+");
            Console.WriteLine("|           ⚔ 던전 입구 ⚔           |");
            Console.WriteLine("+======================================+");
            Console.WriteLine("| 1. 쉬운 난이도                        |");
            Console.WriteLine("| 2. 보통 난이도                        |");
            Console.WriteLine("| 3. 어려운 난이도                      |");
            Console.WriteLine("| 0. 마을로 돌아가기                    |");
            Console.WriteLine("+======================================+");
            Console.WriteLine("엔터를 눌러 돌아갑니다.");
            Console.ReadLine();
        }
    }
}