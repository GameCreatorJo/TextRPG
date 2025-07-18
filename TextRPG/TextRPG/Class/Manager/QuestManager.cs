using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextRPG.Class.Database.MonsterData;
using TextRPG.Class.Database.QuestData;
using TextRPG.Class.UI;
using static TextRPG.Class.Database.QuestData.QuestData;



namespace TextRPG.Class.Manager
{

    public class QuestManager
    {
        private static QuestManager _instance;
        private static readonly object _lock = new object();

        public static QuestManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new QuestManager();
                    }
                    return _instance;
                }
            }

        }
        private QuestData? _activeQuest;
        private QuestDatabase? _questDatabase;

        public QuestDatabase GetDatabase()
        {
            if (_questDatabase == null)
            {
                _questDatabase = new QuestDatabase(); // 필요 시 초기화
            }
            return _questDatabase;
        }


        private QuestUI? _questUI;

        public void Initialize(QuestDatabase database, QuestUI? questUI = null)
        {
            _questDatabase = database;
            _questUI = questUI;
            _questDatabase.CreateQuest();
        }

        public void OpenQuestMenu()
        {
            if (_questUI != null)
            {
                // UI에 위임
                _questUI.ShowQuestMenu();
            }
            else
            {
                Console.WriteLine("QuestUI가 연결되지 않았습니다.");
            }
        }          
        

        public void AcceptQuest(int questId)
        {
            if (_questDatabase == null)
            {
                Console.WriteLine("퀘스트 데이터베이스가 초기화되지 않았습니다.");
                return;
            }

            if (_questDatabase.IsQuestAccepted(questId))
            {
                Console.WriteLine("이미 수락한 퀘스트입니다.");
                return;
            }

            var quest = _questDatabase.GetQuestById(questId);
            if (quest == null)
            {
                Console.WriteLine($"퀘스트 ID {questId}에 해당하는 퀘스트를 찾을 수 없습니다.");
                return;
            }

            _questDatabase.AcceptedQuestList[questId] = quest;
           
            quest.State = QuestState.InProgress;
            _activeQuest = quest;

            Console.WriteLine($"퀘스트 '{quest.Title}'을 수락했습니다.");
        }






        //퀘스트 진행도
        public void UpdateQuestKillCount(string monsterName)
        {
            var activeQuests = GetActiveQuests();

            foreach (var quest in activeQuests.Values)
            {
                if (!quest.IsCompleted && quest.TargetMonsterKey == monsterName)
                {
                    quest.UpdateKill();
                    Console.WriteLine($"퀘스트 '{quest.Title}' 진행도: {quest.KillCount}/{quest.KillTarget}");

                    if (quest.IsCompleted)
                    {
                        Console.WriteLine($"🎉 퀘스트 '{quest.Title}' 완료!");
                        // 보상 지급 로직 추가 가능
                    }
                }
            }          

        }
        

        public Dictionary<int, QuestData> GetActiveQuests()
        {
            return _questDatabase.GetAcceptedQuests();
        }

              
        public QuestData[] GetAvailableQuests()
        {
            return _questDatabase.GetAllQuests().Values.ToArray();
        }
        
        //퀘스트 메뉴 진행되는 메서드       
        public void PromptKillProgress(Monster? monster)
        {
            if (monster == null)
            {
                Console.WriteLine("몬스터 정보가 없습니다.");
                return;
            }

            Console.WriteLine("\n처치 이벤트 발생!");
            UpdateQuestKillCount(monster.Job); 
        }
             

        
        

    }

}


