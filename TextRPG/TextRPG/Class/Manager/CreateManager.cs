using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TextRPG.Class.Data;
using TextRPG.Class.Database.ItemData;
using TextRPG.Class.Database.MapData;
using TextRPG.Class.Database.MonsterData;
using TextRPG.Class.Database.PlayerData;
using TextRPG.Class.Database.QuestData;
using TextRPG.Class.Interface;
using TextRPG.Class.Scenes;
using TextRPG.Class.UI;

namespace TextRPG.Class.Manager
{
    public class CreateManager
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
		{
			if (!File.Exists("player.json"))
			{
				Console.WriteLine("세이브 데이터가 없습니다.!");
				Console.WriteLine("생성할 플레이어 이름을 입력해주세요!");
				string playerName = Console.ReadLine();

				string inputJob = "";
				while (inputJob != "1" && inputJob != "2")
				{
					Console.WriteLine("직업을 선택해주세요!");
					Console.WriteLine("1. 전사 2. 마법사");
					inputJob = Console.ReadLine();
					if (inputJob != "1" && inputJob != "2")
						Console.WriteLine("잘못된 입력입니다. 1 또는 2를 입력해주세요.");
				}

				player = new Player(playerName, inputJob);
				player.TakeItem();
				Console.WriteLine("Player data created!");
			}
			else
			{
				string input = "";
				while (input != "1" && input != "2")
				{
					Console.WriteLine("세이브 데이터가 있습니다. 불러옵니까?");
					Console.WriteLine("1. 예 2. 아니오");
					input = Console.ReadLine();
					if (input != "1" && input != "2")
						Console.WriteLine("잘못된 입력입니다. 1 또는 2를 입력해주세요.");
				}

				if (input == "1")
				{
					string json = File.ReadAllText("player.json");
					var options = new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					};
					PlayerSaveData loadedPlayer = JsonSerializer.Deserialize<PlayerSaveData>(json, options);
					player = new Player(loadedPlayer);
					Console.WriteLine("게임 데이터가 성공적으로 불러와졌습니다.");
					input = Console.ReadLine();
                    Console.WriteLine("게임을 시작합니다.");
                    Console.WriteLine($"인벤토리1번{player.Inventory[0].Name} 2번{player.Inventory[1].Name} 3번{player.Inventory[2].Name}");


				}
				else
				{
					// 새로 생성
					Console.WriteLine("생성할 플레이어 이름을 입력해주세요!");
					string playerName = Console.ReadLine();

					string inputJob = "";
					while (inputJob != "1" && inputJob != "2")
					{
						Console.WriteLine("직업을 선택해주세요!");
						Console.WriteLine("1. 전사 2. 마법사");
						inputJob = Console.ReadLine();
						if (inputJob != "1" && inputJob != "2")
							Console.WriteLine("잘못된 입력입니다. 1 또는 2를 입력해주세요.");
					}

					player = new Player(playerName, inputJob);
					player.TakeItem();
					Console.WriteLine("Player data created!");
				}
			}
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
        public void CreateSave()
        {
            PlayerSaveData playerSaveData = new PlayerSaveData(GameManager.Instance.CreateManager.Player);

			var options = new JsonSerializerOptions
			{
				WriteIndented = true
			};

			string json = JsonSerializer.Serialize(playerSaveData, options);
			File.WriteAllText("player.json", json);
			Console.WriteLine("게임이 저장되었습니다.");
		}
    }
}
