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
    {// �׽�Ʈ ����
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
        //?�스??매니?� 추�?
        private QuestManager _questManager;
        public QuestManager QuestManager
        {
            get { return _questManager; }
        }

        private ShopManager _shopManager;
        public ShopManager ShopManager
        {
            get { return _shopManager; }
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
            //?�스?�시?�템 초기??추�?
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
            // ?�스?�스 ?�성 메소???�출
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
				Console.WriteLine("?�이�??�이?��? ?�습?�다.!");
                // Console.WriteLine("?�이�??�이?��? ?�습?�다.!");
                Console.WriteLine("?�성???�레?�어 ?�름???�력?�주?�요!");
                // Console.WriteLine("?�성???�레?�어 ?�름???�력?�주?�요!");
				string playerName = Console.ReadLine();

				string inputJob = "";
				while (inputJob != "1" && inputJob != "2")
				{
					Console.WriteLine("직업???�택?�주?�요!");
					// Console.WriteLine("직업???�택?�주?�요!");
					Console.WriteLine("1. ?�사 2. 마법??");
					// Console.WriteLine("1. ?�사 2. 마법??);
					inputJob = Console.ReadLine();
					if (inputJob != "1" && inputJob != "2")
						Console.WriteLine("?�못???�력?�니?? 1 ?�는 2�??�력?�주?�요.");
						// Console.WriteLine("?�못???�력?�니?? 1 ?�는 2�??�력?�주?�요.");
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
					Console.WriteLine("?�이�??�이?��? ?�습?�다. 불러?�니�?");
					// Console.WriteLine("?�이�??�이?��? ?�습?�다. 불러?�니�?");
					Console.WriteLine("1. ??2. ?�니??);
					// Console.WriteLine("1. ??2. ?�니??);
					input = Console.ReadLine();
					if (input != "1" && input != "2")
						Console.WriteLine("?�못???�력?�니?? 1 ?�는 2�??�력?�주?�요.");
						// Console.WriteLine("?�못???�력?�니?? 1 ?�는 2�??�력?�주?�요.");
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
					Console.WriteLine("게임 ?�이?��? ?�공?�으�?불러?�졌습?�다.");
					// Console.WriteLine("게임 ?�이?��? ?�공?�으�?불러?�졌습?�다.");
					input = Console.ReadLine();
                    Console.WriteLine("게임???�작?�니??");
                    // Console.WriteLine("게임???�작?�니??");
                    Console.WriteLine($"?�벤?�리1�?player.Inventory[0].Name} 2�?player.Inventory[1].Name} 3�?player.Inventory[2].Name}");
                    // Console.WriteLine($"?�벤?�리1�?player.Inventory[0].Name} 2�?player.Inventory[1].Name} 3�?player.Inventory[2].Name}");


				}
				else
				{
					// ?�로 ?�성
					Console.WriteLine("?�성???�레?�어 ?�름???�력?�주?�요!");
					// Console.WriteLine("?�성???�레?�어 ?�름???�력?�주?�요!");
					string playerName = Console.ReadLine();

					string inputJob = "";
					while (inputJob != "1" && inputJob != "2")
					{
						Console.WriteLine("직업???�택?�주?�요!");
						// Console.WriteLine("직업???�택?�주?�요!");
						Console.WriteLine("1. ?�사 2. 마법??);
						// Console.WriteLine("1. ?�사 2. 마법??);
						inputJob = Console.ReadLine();
						if (inputJob != "1" && inputJob != "2")
							Console.WriteLine("?�못???�력?�니?? 1 ?�는 2�??�력?�주?�요.");
							// Console.WriteLine("?�못???�력?�니?? 1 ?�는 2�??�력?�주?�요.");
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
            //?�스??추�?
            CreateQuest();
            CreateShop();
            Console.WriteLine("Game world created!");
        }
        //?�스?��???
        public void CreateQuest()
        { 
            QuestDatabase questDatabase = new QuestDatabase();
            QuestUI questUI = new QuestUI(questDatabase);
            _questManager = QuestManager.Instance;
            _questManager.Initialize(questDatabase, questUI);
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
			Console.WriteLine("게임???�?�되?�습?�다.");
			// Console.WriteLine("게임???�?�되?�습?�다.");
		}

        public void CreateShop()
        {
            _shopManager = ShopManager.Instance;
            _shopManager.AddItems();
        }
    }
}
