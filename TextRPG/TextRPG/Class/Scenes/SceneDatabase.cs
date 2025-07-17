using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.ItemData;


namespace TextRPG.Class.Scenes
{
    public class SceneDatabase
    {
        private Dictionary<string, Scene> _sceneDictionary;
        public Dictionary<string, Scene> SceneDictionary
        {
            get { return _sceneDictionary; }
        }
            
        Scene MainScene = new Scene("MainScene", "게임시작부");
        Scene ShopScene = new Scene("ShopScene", "상점");
        Scene StatusScene = new Scene("StatusScene", "상태창");
        Scene dungeonScene = new Scene("DungeonScene", "던전");
        Scene QueustScene = new Scene("QuestScene", "퀘스트 메뉴");
        Scene InnScene = new Scene("InnScene", "여관");

        public SceneDatabase()
        {
            _sceneDictionary = new Dictionary<string, Scene>();
        }

        public void CreateScene()
        {
            _sceneDictionary.Add("MainScene", MainScene);
            _sceneDictionary.Add("ShopScene", ShopScene);
            _sceneDictionary.Add("StatusScene", StatusScene);
            _sceneDictionary.Add("DungeonScene", dungeonScene);
            _sceneDictionary.Add("QuestScene", QueustScene);
            _sceneDictionary.Add("InnScene", InnScene);
        }
    }
}