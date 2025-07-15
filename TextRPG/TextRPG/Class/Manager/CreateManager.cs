using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.Monster;
using TextRPG.Class.Data;
using TextRPG.Class.Database.Player;

namespace TextRPG.Class.Manager
{
    internal class CreateManager
    {
        private Player player;
        public Player Player
        {
            get { return player; }
        }
        private MonsterDatabase monsterDatabase;
        public MonsterDatabase MonsterDatabase
        {
            get { return monsterDatabase; }
        }
        /*
        private ItemDatabase _itemDatabas;
        private ItemDatabase ItemDatabase
        {
            get { return _itemDatabas; }
        }

        private MainScene mainScene;
        public MainScene MainScene
        {
            get { return mainScene; }
        }*/

        public CreateManager()
        {
            monsterDatabase = new MonsterDatabase();
            monsterDatabase.createMonster();
            Console.WriteLine("CreateManager initialized!");
        }
        public void CreateMonster()
        {
            // 인스턴스 생성 메소드 호출
            Console.WriteLine("Monster database created!");
        }
        public void CreateItem()
        {
            // 인스턴스 생성 메소드 호출
            Console.WriteLine("Item database created!");
        }
        public void CreateScene()
        {
            // 인스턴스 생성 메소드 호출
            Console.WriteLine("Scene created!");
        }

        public void CreatePlayerData()
        {
            string playerName = Console.ReadLine();
            player = new Player(playerName);
            Console.WriteLine("Player data created!");
        }
        public void CreateGameWorld()
        {
            CreateMonster();
            CreateItem();
            CreateScene();
            CreatePlayerData();
            Console.WriteLine("Game world created!");
        }

    }
}
