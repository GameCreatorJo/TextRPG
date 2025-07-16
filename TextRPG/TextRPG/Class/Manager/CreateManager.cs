using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Data;
using TextRPG.Class.Database.ItemData;
using TextRPG.Class.Database.MapData;
using TextRPG.Class.Database.MonsterData;
using TextRPG.Class.Database.PlayerData;
using TextRPG.Class.Database.QuestData;
using TextRPG.Class.Scenes;
using TextRPG.Class.UI;

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
        //퀘스트 매니저 추가
        private QuestManager _questManager;
        public QuestManager QuestManager
        {
            get { return _questManager; }
        }

        public SceneDatabase SceneDatabase { get; private set; }
        private MapDatabase _mapDatabase;
        public MapDatabase MapDatabase
        {
            get { return _mapDatabase; }
        }

        public CreateManager()
        {
            monsterDatabase = new MonsterDatabase();
            _itemDatabas = new ItemDatabaseList();
            SceneDatabase = new SceneDatabase();
            _mapDatabase = new MapDatabase();
            //퀘스트시스템 초기화 추가
            QuestDatabase questDatabase = new QuestDatabase();
            QuestUI questUI = new QuestUI(questDatabase);
            QuestManager.Instance.Initialize(questDatabase, questUI);


            Console.WriteLine("CreateManager initialized!");
        }
        public void CreateMonster()
        {
            monsterDatabase.CreateMonster();
            Console.WriteLine("Monster database created!");
        }
        public void CreateItem()
        {
            _itemDatabas.CreateItem();
            // 인스턴스 생성 메소드 호출
            Console.WriteLine("Item database created!");
        }
        public void CreateScene()
        {
            SceneDatabase.CreateScene();
            Console.WriteLine("Scene created!");
        }
        public void CreateMap()
        {
            _mapDatabase.CreateMap();
            Console.WriteLine("Map created!");
        }

        public void CreatePlayerData()
        {//test
            Console.WriteLine("플레이어 이름을 입력해주세요!");
            string playerName = Console.ReadLine();
            Console.WriteLine("직업을 선택해주세요!");
            Console.WriteLine("1. 전사 2. 마법사");
            string inputJob = Console.ReadLine();
            player = new Player(playerName, inputJob);
            player.TakeItem();
            Console.WriteLine("Player data created!");
        }
        public void CreateGameWorld()
        {
            CreateMonster();
            CreateItem();
            CreateScene();
            CreatePlayerData();
            CreateMap();
            //퀘스트 추가
            CreateQuest();
            Console.WriteLine("Game world created!");
        }
        //퀘스트관련
        public void CreateQuest()
        { 
            QuestDatabase questDatabase = new QuestDatabase();
            _questManager = QuestManager.Instance;
            _questManager.Initialize(questDatabase);
            Console.WriteLine("Quest database created and QuestManager initialized!");
        }
    }
}
