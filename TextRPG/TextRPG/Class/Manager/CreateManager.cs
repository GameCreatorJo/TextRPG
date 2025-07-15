using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Data;
using TextRPG.Class.Database.ItemData;
using TextRPG.Class.Database.Monster;
using TextRPG.Class.Database.Player;
using TextRPG.Class.Scene;

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
        
        private ItemDatabaseList _itemDatabas;
        public ItemDatabaseList ItemDatabase
        {
            get { return _itemDatabas; }
        }

        private MainScene mainScene;
        public MainScene MainScene
        {
            get { return mainScene; }
        }

        public CreateManager()
        {
            monsterDatabase = new MonsterDatabase();
            _itemDatabas = new ItemDatabaseList();

            Console.WriteLine("CreateManager initialized!");
        }
        public void CreateMonster()
        {
            monsterDatabase.CreateMonster();
            Console.WriteLine("Monster database created!");
        }
        public void CreateItem()
        {
            _itemDatabas = new ItemDatabaseList();
            _itemDatabas.CreateItem();
            // 인스턴스 생성 메소드 호출
            Console.WriteLine("Item database created!");
        }
        public void CreateScene()
        {
            // 인스턴스 생성 메소드 호출
            Console.WriteLine("Scene created!");
        }

        public void CreatePlayerData()
        {//test
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
