using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.ItemData;
using TextRPG.Class.Interface;
using TextRPG.Class.Manager;

namespace TextRPG.Class.Database.PlayerData
{
    public class PlayerSaveData : IPlayerData
    {
        public PlayerSaveData() { }
        public PlayerSaveData(Player player)
        {
            Lv = player.Lv;
            Name = player.Name;
            Job = player.Job;
            Str = player.Str;
            PlusStr = player.PlusStr;
            ArmorPoint = player.ArmorPoint;
            PlusArmorPoint = player.PlusArmorPoint;
            MaxHp = player.MaxHp;
            PlusHp = player.PlusHp;
            Hp = player.Hp;
            MaxMp = player.MaxMp;
            PlusMp = player.PlusMp;
            Mp = player.Mp;
            Gold = player.Gold;
            Exp = player.Exp;
            CriticalRate = player.CriticalRate;
            DodgeRate = player.DodgeRate;
            InventorySaveData = player.Inventory
                .Select(item => new ItemSaveData(item))
                .ToList();
            Console.WriteLine("PlayerSaveData 생성자 호출: 플레이어 데이터를 저장합니다.");
            Console.WriteLine($"이름: {Name}, 직업: {Job}, 레벨: {Lv}, 공격력: {Str}, 방어력: {ArmorPoint}, 최대 체력: {MaxHp}, 현재 체력: {Hp}, 골드: {Gold}");
            Console.WriteLine($"인벤토리 아이템 수: {InventorySaveData.Count}");
            Weapon = player.Weapon;
            Armor = player.Armor;

        }
    }
}
