using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.Quest;
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
                Quest selectedQuest = _questDatabase.GetQuestById(selectedId);
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
        private void ConfirmQuest(Quest quest)
        {
            //Console.WriteLine($"\n 퀘스트 이름: {quest.Title}");
            //Console.WriteLine($" 설명: {quest.Description}");
            //Console.WriteLine($" 목표: {quest.KillTarget}마리 처치");
            
            //Console.WriteLine("\n1. 수락   2. 거절");

            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║             📜 퀘스트 정보               ║");
            Console.WriteLine("║────────────────────────────────────────────║");
            Console.WriteLine($"║ 제목       : {quest.Title}".PadRight(42) + "║");
            Console.WriteLine($"║ 설명       : {quest.Description}".PadRight(42) + "║");
            Console.WriteLine($"║ 목표 처치  : {quest.KillTarget} 마리".PadRight(42) + "║");
            Console.WriteLine($"║ 수락 여부  : 1. 수락   2. 거절".PadRight(42) + "║");
            Console.WriteLine("╚════════════════════════════════════════════╝");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                QuestManager.Instance.SelectQuest(quest.Id);
            }
            else if(choice == "2")
            {
                Console.WriteLine(" 퀘스트 수락 취소됨.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        public void ShowActiveQuest()
        {
            Console.WriteLine("\n [진행 중인 퀘스트]");
            Quest active = QuestManager.Instance.GetActiveQuest();
                

            if (active != null)
            {
                Console.WriteLine(active.GetQuestInfo());
            }
            else
            {
                Console.WriteLine(" 진행 중인 퀘스트가 없습니다.");
            }
        }

        public void PromptAbandonQuest()
        {
            Console.WriteLine("\n 퀘스트를 포기하시겠습니까?");
            Console.WriteLine("1. 포기   2. 유지");
            string input = Console.ReadLine();
            if (input == "1")
            {
                QuestManager.Instance.AbandonQuest();
            }
            else if (input == "2")
            {
                Console.WriteLine(" 퀘스트를 유지합니다.");
            }
            else 
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
        //진행도
        public void PromptKillProgress()
        {
            Console.WriteLine("\n 처치 이벤트 발생!");
            QuestManager.Instance.UpdateQuestKillCount();
        }
    }

}
