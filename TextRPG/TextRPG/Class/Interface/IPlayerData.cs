using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.ItemData;
using TextRPG.Class.Database.PlayerData;

namespace TextRPG.Class.Interface
{
    public class IPlayerData
    {
        public int Lv { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }

        // 기본 공격력
        public int Str { get; set; }
        // 아이템 장착 공격력
        public int PlusStr { get; set; }

        // 기본 방어력
        public int ArmorPoint { get; set; }
        // 아이템 장착 방어력
        public int PlusArmorPoint { get; set; }

        // 기본 최대 Hp
        public int MaxHp { get; set; }
        // 아이템 장착 Hp
        public int PlusHp { get; set; }
        // 현재 체력
        public int Hp { get; set; }

        // 기본 마나
        public int MaxMp { get; set; }
        // 아이템 장착 마나
        public int PlusMp { get; set; }
        // 현재 마나
        public int Mp { get; set; }

        // 현재 보유 골드
        public int Gold { get; set; }
        // 캐릭터의 경험치
        public int Exp { get; set; }

        // 캐릭터의 치명타 확률
        public int CriticalRate { get; set; }
        // 캐릭터의 회피 확률
        public int DodgeRate { get; set; }

        // 캐릭터의 인벤토리
        public List<ItemSaveData> InventorySaveData { get; set; } = new();
    }
}
