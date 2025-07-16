using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.QuestData;


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
        private QuestData _activeQuest;
        private QuestDatabase _questDatabase;


        public void Initialize(QuestDatabase database)
        {
            _questDatabase = database;
            _questDatabase.CreateQuest(); // 누락된 초기 퀘스트 등록


        }
        //퀘스트 수락
        public void SelectQuest(int questId)
        {
            QuestData quest = _questDatabase.GetQuestById(questId);
            if (quest != null)
            {
                _activeQuest = quest;
                Console.WriteLine($"퀘스트 '{_activeQuest.Title}'을(를) 수락했습니다.");
            }
            else
            {
                Console.WriteLine("퀘스트를 찾을 수 없습니다.");
            }
        }
        //퀘스트 포기
        public void AbandonQuest()
        {
            if (_activeQuest != null && !_activeQuest.IsCompleted)
            {
                Console.WriteLine($"퀘스트 '{_activeQuest.Title}'을(를) 포기했습니다.");
                _activeQuest = null;
            }
            else if (_activeQuest?.IsCompleted == true)
            {
                Console.WriteLine("완료된 퀘스트는 포기할 수 없습니다.");
            }
            else
            {
                Console.WriteLine("진행 중인 퀘스트가 없습니다.");
            }
        }

        //퀘스트 진행도
        public void UpdateQuestKillCount()
        {
            if (_activeQuest != null && !_activeQuest.IsCompleted)
            {
                _activeQuest.UpdateKill();
                Console.WriteLine(_activeQuest.GetQuestInfo());
                if (_activeQuest.IsCompleted)
                {
                    Console.WriteLine($"퀘스트 '{_activeQuest.Title}' 완료!");
                    // 여기서 보상 지급 로직 삽입 가능!
                    // 예: Player.AddItem("rewardItemId");
                }

            }
            else
            {
                Console.WriteLine("진행 중인 퀘스트가 없습니다.");
            }

        }
        public QuestData GetActiveQuest()
        {
            return _activeQuest;
        }

        public Dictionary<int, QuestData> GetAllQuests()
        {
            return _questDatabase.GetAllQuests();
        }
        public QuestData[] GetAvailableQuests()
        {
            return _questDatabase.GetAllQuests().Values.ToArray();
        }

        public void ShowQuestMenu()
        {
            while (true)
            {
                Console.WriteLine("\n╔══════ 📜 퀘스트 메뉴 ══════╗");
                Console.WriteLine("║ 1. 진행 중인 퀘스트 보기      ║");
                Console.WriteLine("║ 2. 수락 가능한 퀘스트 목록     ║");
                Console.WriteLine("║ 3. 퀘스트 수락                ║");
                Console.WriteLine("║ 4. 퀘스트 포기                ║");
                Console.WriteLine("║ 0. 뒤로가기                  ║");
                Console.WriteLine("╚═════════════════════════════╝");
                Console.WriteLine("\n1. 진행중인 퀘스트 보기 2. 수락가능한 퀘스트 목록 3. 퀘스트 수락 4. 퀘스트 포기 0. 뒤로가기");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        QuestData active = QuestManager.Instance.GetActiveQuest();
                        Console.WriteLine("\n[진행 중인 퀘스트]");
                        Console.WriteLine(active != null ? active.GetQuestInfo() : "진행 중인 퀘스트가 없습니다.");
                        Console.WriteLine("Enter를 누르면 뒤로 돌아갑니다.");
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.WriteLine("\n[퀘스트 목록]");
                        foreach (QuestData quest in GetAvailableQuests())
                        {
                            Console.WriteLine($"[{quest.Id}] {quest.Title} - {quest.Description}");
                        }
                        Console.WriteLine("Enter를 누르면 뒤로 돌아갑니다.");
                        Console.ReadLine();
                        break;

                    case "3":
                        Console.Write("\n수락할 퀘스트 ID 입력: ");
                        if (int.TryParse(Console.ReadLine(), out int questId))
                        {
                            SelectQuest(questId);
                        }
                        else Console.WriteLine("⚠️ 잘못된 ID입니다.");
                       
                        break;

                    case "4":
                        AbandonQuest();
                        
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

}
