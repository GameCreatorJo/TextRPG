using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Class.Database.Quest
{
    internal class Quest
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int KillTarget { get; }
        public int KillCount { get; private set; }
        public bool IsCompleted { get; private set; }
        public Quest(int id, string name, string description, int killTarget, int killCount)
        {
            Id = id;
            Name = name;
            Description = description;
            KillTarget = killTarget;
            KillCount = killCount;
            IsCompleted = false;
        }
        public void UpdateKill()
        {
            if (!IsCompleted)
            { 
                KillCount++;
                if (KillCount >= KillTarget )
                {
                    Complete();
                }
            }
        }
        public void Complete()
        { 
            IsCompleted = true;
            Console.WriteLine($"퀘스트 '{Name}' 완료!");
            //보상 처리 메서드는 QuestManager에서 처리
        }
        public string GetQuestInfo()
        {
            return $"퀘스트 ID: {Id}, 이름: {Name}, 설명: {Description}, 목표 처치 수: {KillTarget}, 현재 처치 수: {KillCount}, 완료 여부: {IsCompleted}";
        }
    }
}
