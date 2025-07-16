using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.QuestData;
using TextRPG.Class.Manager;

namespace TextRPG.Class.UI
{
    public class QuestUI
    {
        private QuestDatabase _questDatabase;

        public QuestUI(QuestDatabase database)
        {
            _questDatabase = database;
        }

        public void ShowQuestList()//퀘스트목록
        {
            Console.WriteLine("\n[퀘스트 목록]");
            foreach (var questPair in _questDatabase.GetAllQuests())
            {
                var quest = questPair.Value;
                Console.WriteLine($"{quest.Id}. {quest.Title} - {quest.Description}");
            }

            Console.Write("\n수락할 퀘스트 ID 입력: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int selectedId))
            {
                QuestData selectedQuest = _questDatabase.GetQuestById(selectedId);
                if (selectedQuest != null)
                {
                    ConfirmQuest(selectedQuest);
                }
                else
                {
                    Console.WriteLine("해당 ID의 퀘스트를 찾을 수 없습니다.");
                }
            }
        }
        //퀘스트 수락
        public bool ConfirmQuest(QuestData quest)
        {
            

            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║             📜 퀘스트 정보               ║");
            Console.WriteLine("║────────────────────────────────────────────║");
            Console.WriteLine($"║ 제목       : {quest.Title}".PadRight(42) + "║");
            Console.WriteLine($"║ 설명       : {quest.Description}".PadRight(42) + "║");
            Console.WriteLine($"║ 목표 처치  : {quest.KillTarget} 마리".PadRight(42) + "║");
            Console.WriteLine($"║ 수락 여부  : 1. 수락   2. 거절".PadRight(42) + "║");
            Console.WriteLine("╚════════════════════════════════════════════╝");

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



            //if (choice == "1")
            //{
            //    return true;//QuestManager.Instance.SelectQuest(quest.Id);
            //}
            //else if(choice == "2")
            //{
            //    return false;
            //    Console.WriteLine(" 퀘스트 수락 취소됨.");
            //}
            //else
            //{
            //    Console.WriteLine("잘못된 입력입니다.");
            //    return false;
            //}
        }

        public void ShowActiveQuest()
        {
            Console.WriteLine("\n [진행 중인 퀘스트]");
            QuestData active = QuestManager.Instance.GetActiveQuest();
                

            if (active != null)
            {
                Console.WriteLine(active.GetQuestInfo());
            }
            else
            {
                Console.WriteLine(" 진행 중인 퀘스트가 없습니다.");
            }
        }
        
        // 퀘스트 보상을 넣으면 좋을지도..?

        //진행도
        public void PromptKillProgress()
        {
            Console.WriteLine("\n 처치 이벤트 발생!");
            QuestManager.Instance.UpdateQuestKillCount();
        }
        


    }


}
