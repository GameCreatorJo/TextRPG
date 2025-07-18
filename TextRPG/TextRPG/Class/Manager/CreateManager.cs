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
    {// Å×½ºÆ® Áø¿ì
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
        //?˜ìŠ¤??ë§¤ë‹ˆ?€ ì¶”ê?
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
            //?˜ìŠ¤?¸ì‹œ?¤í…œ ì´ˆê¸°??ì¶”ê?
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
            // ?¸ìŠ¤?´ìŠ¤ ?ì„± ë©”ì†Œ???¸ì¶œ
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
				Console.WriteLine("?¸ì´ë¸??°ì´?°ê? ?†ìŠµ?ˆë‹¤.!");
                // Console.WriteLine("?¸ì´ë¸??°ì´?°ê? ?†ìŠµ?ˆë‹¤.!");
                Console.WriteLine("?ì„±???Œë ˆ?´ì–´ ?´ë¦„???…ë ¥?´ì£¼?¸ìš”!");
                // Console.WriteLine("?ì„±???Œë ˆ?´ì–´ ?´ë¦„???…ë ¥?´ì£¼?¸ìš”!");
				string playerName = Console.ReadLine();

				string inputJob = "";
				while (inputJob != "1" && inputJob != "2")
				{
					Console.WriteLine("ì§ì—…??? íƒ?´ì£¼?¸ìš”!");
					// Console.WriteLine("ì§ì—…??? íƒ?´ì£¼?¸ìš”!");
					Console.WriteLine("1. ?„ì‚¬ 2. ë§ˆë²•??");
					// Console.WriteLine("1. ?„ì‚¬ 2. ë§ˆë²•??);
					inputJob = Console.ReadLine();
					if (inputJob != "1" && inputJob != "2")
						Console.WriteLine("?˜ëª»???…ë ¥?…ë‹ˆ?? 1 ?ëŠ” 2ë¥??…ë ¥?´ì£¼?¸ìš”.");
						// Console.WriteLine("?˜ëª»???…ë ¥?…ë‹ˆ?? 1 ?ëŠ” 2ë¥??…ë ¥?´ì£¼?¸ìš”.");
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
					Console.WriteLine("?¸ì´ë¸??°ì´?°ê? ?ˆìŠµ?ˆë‹¤. ë¶ˆëŸ¬?µë‹ˆê¹?");
					// Console.WriteLine("?¸ì´ë¸??°ì´?°ê? ?ˆìŠµ?ˆë‹¤. ë¶ˆëŸ¬?µë‹ˆê¹?");
					Console.WriteLine("1. ??2. ?„ë‹ˆ??);
					// Console.WriteLine("1. ??2. ?„ë‹ˆ??);
					input = Console.ReadLine();
					if (input != "1" && input != "2")
						Console.WriteLine("?˜ëª»???…ë ¥?…ë‹ˆ?? 1 ?ëŠ” 2ë¥??…ë ¥?´ì£¼?¸ìš”.");
						// Console.WriteLine("?˜ëª»???…ë ¥?…ë‹ˆ?? 1 ?ëŠ” 2ë¥??…ë ¥?´ì£¼?¸ìš”.");
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
					Console.WriteLine("ê²Œì„ ?°ì´?°ê? ?±ê³µ?ìœ¼ë¡?ë¶ˆëŸ¬?€ì¡ŒìŠµ?ˆë‹¤.");
					// Console.WriteLine("ê²Œì„ ?°ì´?°ê? ?±ê³µ?ìœ¼ë¡?ë¶ˆëŸ¬?€ì¡ŒìŠµ?ˆë‹¤.");
					input = Console.ReadLine();
                    Console.WriteLine("ê²Œì„???œì‘?©ë‹ˆ??");
                    // Console.WriteLine("ê²Œì„???œì‘?©ë‹ˆ??");
                    Console.WriteLine($"?¸ë²¤? ë¦¬1ë²?player.Inventory[0].Name} 2ë²?player.Inventory[1].Name} 3ë²?player.Inventory[2].Name}");
                    // Console.WriteLine($"?¸ë²¤? ë¦¬1ë²?player.Inventory[0].Name} 2ë²?player.Inventory[1].Name} 3ë²?player.Inventory[2].Name}");


				}
				else
				{
					// ?ˆë¡œ ?ì„±
					Console.WriteLine("?ì„±???Œë ˆ?´ì–´ ?´ë¦„???…ë ¥?´ì£¼?¸ìš”!");
					// Console.WriteLine("?ì„±???Œë ˆ?´ì–´ ?´ë¦„???…ë ¥?´ì£¼?¸ìš”!");
					string playerName = Console.ReadLine();

					string inputJob = "";
					while (inputJob != "1" && inputJob != "2")
					{
						Console.WriteLine("ì§ì—…??? íƒ?´ì£¼?¸ìš”!");
						// Console.WriteLine("ì§ì—…??? íƒ?´ì£¼?¸ìš”!");
						Console.WriteLine("1. ?„ì‚¬ 2. ë§ˆë²•??);
						// Console.WriteLine("1. ?„ì‚¬ 2. ë§ˆë²•??);
						inputJob = Console.ReadLine();
						if (inputJob != "1" && inputJob != "2")
							Console.WriteLine("?˜ëª»???…ë ¥?…ë‹ˆ?? 1 ?ëŠ” 2ë¥??…ë ¥?´ì£¼?¸ìš”.");
							// Console.WriteLine("?˜ëª»???…ë ¥?…ë‹ˆ?? 1 ?ëŠ” 2ë¥??…ë ¥?´ì£¼?¸ìš”.");
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
            //?˜ìŠ¤??ì¶”ê?
            CreateQuest();
            CreateShop();
            Console.WriteLine("Game world created!");
        }
        //?˜ìŠ¤?¸ê???
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
			Console.WriteLine("ê²Œì„???€?¥ë˜?ˆìŠµ?ˆë‹¤.");
			// Console.WriteLine("ê²Œì„???€?¥ë˜?ˆìŠµ?ˆë‹¤.");
		}

        public void CreateShop()
        {
            _shopManager = ShopManager.Instance;
            _shopManager.AddItems();
        }
    }
}
