using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Class.Database.QuestData
{
    public class QuestData
    {
        public int Id { get; }
        public string Title { get; }
        public string Description { get; }
        public int KillTarget { get; }
        public int KillCount { get; private set; }
        public bool IsCompleted { get; private set; }

        public string TargetMonsterKey;
        public QuestState State { get; set; }
        public enum QuestState
        { 
            None,
            InProgress,
            Completed,
            Failed

        }
        public QuestData(int id, string title, string description, int killTarget, int killCount)
        {
            Id = id;// 퀘스트 ID
            Title = title;// 퀘스트 제목
            Description = description;// 퀘스트 설명
            KillTarget = killTarget;// 목표 처치 수
            KillCount = killCount;// 현재 처치 수
            IsCompleted = false;// 완료 여부
        }
        public void UpdateKill()// 퀘스트 진행도 업데이트 메서드
        {
            if (!IsCompleted)
            {
                KillCount++;
                if (KillCount >= KillTarget)
                {
                    Complete();
                }
            }
        }
        public void Complete()// 퀘스트 완료 메서드
        {
            IsCompleted = true;
            Console.WriteLine($"퀘스트 '{Title}' 완료!");
            //보상 처리 메서드는 QuestManager에서 처리
        }
        public string GetQuestInfo()// 퀘스트 정보 출력 메서드
        {           
            return $"퀘스트 ID: {Id}, 이름: {Title}, 설명: {Description}, 목표 처치 수: {KillTarget}, 현재 처치 수: {KillCount}, 완료 여부: {IsCompleted}";
        }


            
        
    }

}
