using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Class.Database.QuestData
{
    public class QuestDatabase
    {
        private Dictionary<int, QuestData> _questDictionary = new Dictionary<int, QuestData>();
        public QuestDatabase()
        {
            // 퀘스트 데이터 초기화
            new QuestData(1, "첫 번째 퀘스트", "미니언 5마리 처치", 5, 0);
            new QuestData(2, "두 번째 퀘스트", "대포 미니언 처치", 1, 0);

            // 추가적인 퀘스트를 여기에 추가할 수 있습니다.
        }

        // 퀘스트 추가 메서드
        public void AddQuest(QuestData quest)
        {
            if (!_questDictionary.ContainsKey(quest.Id))
            {
                _questDictionary.Add(quest.Id, quest);
            }
            else
            {
                Console.WriteLine($"퀘스트 ID {quest.Id}는 이미 존재합니다.");
            }
        }
        public Dictionary<int, QuestData> GetAllQuests()
        {
            return _questDictionary;
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

        //리펙토링 해보는중...
        //private Dictionary<string, QuestData> questDictionary;

        //public Dictionary<string, QuestData> QuestDictionary
        //{
        //    get { return questDictionary; }
        //}

        //// 퀘스트 객체 생성
        //QuestData firstQuest = new QuestData(1, "첫 번째 퀘스트", "미니언 5마리 처치", 5, 0);
        //QuestData secondQuest = new QuestData(2, "두 번째 퀘스트", "대포 미니언 처치", 1, 0);

        //public QuestDatabase()
        //{
        //    questDictionary = new Dictionary<string, QuestData>();
        //}

        //public void CreateQuests()
        //{
        //    questDictionary.Add("firstQuest", firstQuest);
        //    questDictionary.Add("secondQuest", secondQuest);
        //}

    }

}
