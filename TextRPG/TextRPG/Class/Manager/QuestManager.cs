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


        
        private QuestUI? _questUI;

        public void Initialize(QuestDatabase database, QuestUI? questUI = null)
        {
            _questDatabase = database;
            _questUI = questUI;
            _questDatabase.CreateQuest();
        }

        //퀘스트 수락
        public void SelectQuest(int questId)
        {
            AcceptQuest(questId);
            QuestData quest = _questDatabase.GetQuestById(questId);
            if (quest != null )
            {
                
                _activeQuest = quest;
                Console.WriteLine($"퀘스트 '{_activeQuest.Title}'을(를) 수락했습니다.");
                //Console.WriteLine("Enter를 누르면 퀘스트 메뉴로 돌아갑니다.");
                ShowQuestList();
            }
            else
            {
                Console.WriteLine("퀘스트를 찾을 수 없습니다.");
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

            _questDatabase.AcceptedQuests[questId] = quest;
           
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
        public void ShowQuestMenu()
        {
            bool isrunning = true;
            while (isrunning)
            {
                Console.WriteLine("+========== 📜 퀘스트 메뉴 ==========+");
                Console.WriteLine("| 1. 진행 중인 퀘스트 보기            |");
                Console.WriteLine("| 2. 전체 퀘스트 목록                 |");
                Console.WriteLine("| 0. 뒤로가기                         |");
                Console.WriteLine("+=====================================+");
                Console.WriteLine("\n1. 진행중인 퀘스트 보기 2. 전체 퀘스트 목록 0. 뒤로가기");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        //QuestData active = QuestManager.Instance.GetActiveQuest();
                        var activeQuests = QuestManager.Instance.GetActiveQuests();
                        foreach (var quest in activeQuests.Values)
                        {
                            Console.WriteLine(quest.GetQuestInfo()); // ✅ 개별 퀘스트에 대해 호출
                        }

                        Console.WriteLine("\n[진행 중인 퀘스트]");
                        if (activeQuests != null)
                        {
                            //Console.WriteLine(activeQuests.GetQuestInfo()); //퀘스트 정보출력
                            while (true)
                            {
                                Console.WriteLine("0. 돌아가기");
                                string back = Console.ReadLine();
                                if (back == "0")
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");


                                }
                            } 
                            
                        }
                        else
                        { 
                            Console.WriteLine("진행 중인 퀘스트가 없습니다.\n퀘스트를 수락해주세요.");

                            while (true)
                            {
                                Console.WriteLine("0. 돌아가기");
                                string back = Console.ReadLine();
                                if (back == "0")
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("잘못된 입력입니다.");


                                }
                            } 
                            
                        }
                            
                        
                        break;

                    case "2":
                        ShowQuestList();
                                             
                        break;
                    case "0":

                         
                        return;

                    default:
                        Console.WriteLine("잘못된 선택입니다.");
                        break;
                }
            }
        }
        public void PromptKillProgress(Monster? monster)
        {
            if (monster == null)
            {
                Console.WriteLine("몬스터 정보가 없습니다.");
                return;
            }

            Console.WriteLine("\n처치 이벤트 발생!");
            UpdateQuestKillCount(monster.Name); // 또는 monster.Job, monster.Key
        }


        public void HandleQuestSelection()
        {
            Console.WriteLine("\n[수락 가능한 퀘스트 목록]");
            foreach (var questPair in _questDatabase.GetAllQuests())
            {
                if (!_questDatabase.IsQuestAccepted(questPair.Key))
                {
                    //퀘스트 ID와 해당 퀘스트 데이터 가져오기
                    Console.WriteLine($"[{questPair.Key}] {questPair.Value.Title} - {questPair.Value.Description}");
                }
            }
            
            Console.Write("\n퀘스트 ID를 입력해 상세보기: ");
            if (int.TryParse(Console.ReadLine(), out int questId))
            {
                QuestData quest = _questDatabase.GetQuestById(questId);
                if (quest != null)
                {
                    if (_questUI.ConfirmQuest(quest))
                    {
                        Console.WriteLine("퀘스트를 수락합니다.");
                        SelectQuest(questId);
                    }
                    else
                    {
                        Console.WriteLine("퀘스트를 거절했습니다.");
                        //퀘스트 목록 호출
                        ShowQuestList();
                        
                    }
                        //_questUI.ConfirmQuest(quest); // 정보 확인 + 수락/거절 입력
                        Console.WriteLine();
                    
                }
                else
                {
                    Console.WriteLine("❌ 해당 퀘스트가 존재하지 않습니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        public void ShowQuestList()
        {
            Console.WriteLine("\n[퀘스트 목록]");
            foreach (QuestData quest in GetAvailableQuests())
            {
                Console.WriteLine($"[{quest.Id}] {quest.Title} - {quest.Description}");
            }
            Console.WriteLine("1. 퀘스트 선택 0. 돌아가기\n");
            string questChoice = Console.ReadLine();
            switch (questChoice)
            {
                case "1":
                    Console.Write("\n선택할 퀘스트 ID 입력: ");
                    HandleQuestSelection();
                    break;
                case "0":
                   return;
                default:
                    Console.WriteLine("⚠️ 올바르지 않은 선택입니다.");
                    break;
            }
        }

    }

}
