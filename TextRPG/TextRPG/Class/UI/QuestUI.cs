using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.MonsterData;
using TextRPG.Class.Database.QuestData;
using TextRPG.Class.Manager;
using static TextRPG.Class.Database.QuestData.QuestData;

namespace TextRPG.Class.UI
{
    public class QuestUI
    {
        private static QuestUI _instance;
        public static QuestUI Instance => _instance ??= new QuestUI(QuestManager.Instance.GetDatabase());



        private QuestDatabase _questDatabase;

        public QuestUI(QuestDatabase database)
        {
            _questDatabase = database;
        }

       

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
                        
                        ShowActiveQuest();
                        //QuestData active = QuestManager.Instance.GetActiveQuest();
                        var activeQuests = QuestManager.Instance.GetActiveQuests();
                        foreach (var quest in activeQuests.Values)
                        {
                            Console.WriteLine(quest.GetQuestInfo()); // ✅ 개별 퀘스트에 대해 호출
                        }

                        
                        break;

                    case "2":
                       
                        ShowQuestList();
                        break;
                    case "0":
                        Console.Clear();
                        isrunning = false;
                        return;

                    default:
                        Console.WriteLine("잘못된 선택입니다.");
                        break;
                }
            }
        }
        public void ShowQuestList()//퀘스트목록
        {
            Console.Clear ();
            Console.WriteLine("\n[퀘스트 목록]");
            foreach (var questPair in _questDatabase.GetAllQuests())
            {
                var quest = questPair.Value;
                bool isAccepted = _questDatabase.IsQuestAccepted(quest.Id);

                string status = isAccepted ? "(수락됨)" : "";

                Console.WriteLine($"{quest.Id}. {quest.Title} - {quest.Description}");
            }

            Console.Write("\n수락할 퀘스트 ID 입력: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int selectedId))
            {
                if (_questDatabase.IsQuestAccepted(selectedId))
                {
                    Console.WriteLine("이미 수락한 퀘스트입니다. 다시 선택해주세요.");
                    return;
                }

                QuestData selectedQuest = _questDatabase.GetQuestById(selectedId);
                if (selectedQuest != null)
                {
                    bool accepted = ConfirmQuest(selectedQuest);
                    if (accepted)
                    {
                        selectedQuest.State = QuestState.InProgress; //  상태 설정
                        _questDatabase.AddAcceptedQuest(selectedQuest); //  등록
                        Console.WriteLine($"퀘스트 '{selectedQuest.Title}'를 수락했습니다.");
                    }
                                        
                }
                else
                {
                    Console.WriteLine("퀘스트를 거절했습니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

        }
        //퀘스트 수락
        public bool ConfirmQuest(QuestData quest)
        {

            
            Console.WriteLine("+===========================================+");
            Console.WriteLine("|                 퀘스트 정보               |");
            Console.WriteLine("|-------------------------------------------|");
            Console.WriteLine($"| 제목       : {quest.Title}               |");
            Console.WriteLine($"| 설명       : {quest.Description}         |");
            Console.WriteLine($"| 목표 처치  : {quest.KillTarget}마리                 |");
            Console.WriteLine($"| 수락 여부  : 1. 수락   2. 거절           |");
            Console.WriteLine("+===========================================+");
            
            string questAccept = Console.ReadLine();
            switch (questAccept)
            {
                case "1":
                    return true;
                    
                case "2":
                    return false;
                default:
                    Console.WriteLine("잘못된 입력입니다. ");
                    return false;


            }

        }

        public void ShowActiveQuest()
        {
            Console.Clear ();
            Console.WriteLine("\n [진행 중인 퀘스트]");
            var activeQuests = QuestManager.Instance.GetActiveQuests();

           //수정점

            if (activeQuests.Count > 0)
            {
                foreach (var quest in activeQuests.Values)
                {
                    Console.WriteLine(quest.GetQuestInfo());
                }
            }
            else
            {
                Console.WriteLine("진행 중인 퀘스트가 없습니다.");
            }

            Console.WriteLine("\n0. 돌아가기");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "0") break;
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
            }
                       
        }
                
    }


}
