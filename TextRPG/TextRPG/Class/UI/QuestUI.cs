using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.QuestData;
using TextRPG.Class.Manager;
using TextRPG.Class.Database.MonsterData;

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

            int titleWidth = 45;
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
            Console.WriteLine("\n [진행 중인 퀘스트]");
            var activeQuests = QuestManager.Instance.GetActiveQuests();
                

            if (activeQuests != null)
            {
                foreach (var quest in activeQuests.Values)
                { 
                    Console.WriteLine(quest.GetQuestInfo()); 
                }
                
            }
            else
            {
                Console.WriteLine(" 진행 중인 퀘스트가 없습니다.");
            }
        }
        
        // 퀘스트 보상을 넣으면 좋을지도..?

        //진행도
        
        


    }


}
