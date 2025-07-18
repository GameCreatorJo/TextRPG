using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.QuestData;



namespace TextRPG.Class.Database.QuestData
{
    public class QuestDatabase
    {


        //전체 퀘스트 목록
        private Dictionary<int, QuestData> _questDictionary;
        public Dictionary<int, QuestData> QuestDictionary
        {
            get { return _questDictionary; }
        }

        //진행중인 퀘스트들
        private Dictionary<int, QuestData> _acceptedQuests = new Dictionary<int, QuestData>();
        public Dictionary<int, QuestData> GetAcceptedQuests()
        {
            return _acceptedQuests;
        }

        public Dictionary<int, QuestData> AcceptedQuestList
        {
            get { return _acceptedQuests; }
        }

        // 퀘스트 객체 미리 정의
        QuestData quest1 = new QuestData(1, "첫 번째 퀘스트", "몬스터 5마리 처치", 5, 0, "enemy");
        QuestData quest2 = new QuestData(2, "두 번째 퀘스트", "몬스터 10마리 처치", 10, 0,"enemy");
        QuestData quest3 = new QuestData(3, "세 번째 퀘스트", "몬스터 12마리 처치.", 12, 0, "enemy");

        public QuestDatabase()
        {
            _questDictionary = new Dictionary<int, QuestData>();
            _acceptedQuests = new Dictionary<int, QuestData>();
        }

        public void CreateQuest()
        {
            quest1.TargetMonsterKey = "enemy";
            quest2.TargetMonsterKey = "enemy";
            quest3.TargetMonsterKey = "enemy";


            AddQuest(quest1);
            AddQuest(quest2);
            AddQuest(quest3);
            // 필요시 더 추가 가능!
        }

        public void AddQuest(QuestData quest)
        {
            if (!_questDictionary.ContainsKey(quest.Id))
            {
                _questDictionary.Add(quest.Id, quest);
            }
            else
            {
                Console.WriteLine($" 퀘스트 ID {quest.Id}는 이미 존재합니다.");
            }
        }

        public QuestData GetQuestById(int id)
        {
            if (_questDictionary.TryGetValue(id, out QuestData quest))
            {
                return quest;
            }
            else
            {
                Console.WriteLine($"퀘스트 ID {id}를 찾을 수 없습니다.");
                return null;
            }
        }

        public Dictionary<int, QuestData> GetAllQuests()
        {
            return _questDictionary;
        }



        public QuestData GetActiveQuest(int id)
        {
            return _acceptedQuests.TryGetValue(id, out QuestData quest) ? quest : null;
        }

        
        public bool IsQuestAccepted(int id)
        {
            return _acceptedQuests.ContainsKey(id);
        }
        public void AddAcceptedQuest(QuestData quest)
        {
            if (!IsQuestAccepted(quest.Id))
            {
                _acceptedQuests[quest.Id] = quest;
            }



        }

       




    }
}
