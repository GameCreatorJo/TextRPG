using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Class.Data
{
    internal abstract class DefaultCharacter
    {
        protected int _lv { get; set; }
        protected string _name { get; set; } = "";
        protected string _job { get; set; } = "";

        // 기본 공격력
        protected int _str { get; set; }
        // 아이템 장착 공격력
        protected int plusStr { get; set; }

        // 기본 방어력
        protected int _armorPoint { get; set; }
        // 아이템 장착 방어력
        protected int plusArmorPoint { get; set; }

        // 기본 최대 Hp
        protected int _maxHp { get; set; }
        // 아이템 장착 Hp
        protected int _plusHp { get; set; }
        // 현재 체력
        protected int _hp { get; set; }

        // 현재 보유 골드
        protected int _gold { get; set; }
        // 캐릭터의 경험치
        protected int _exp { get; set; }

        // 캐릭터의 인벤토리
        protected List<DefaultItem.Item> _inventory = new List<DefaultItem.Item>();

        public DefaultCharacter()
        {
            
        }

        public void ShowInfo()
        {
            // 캐릭터의 능력치를 보여주는 메소드
            Console.WriteLine("Lv. " + _lv);
            Console.WriteLine("Chad ( " + _name + " )");
            Console.WriteLine("공격력: " + _str);
            Console.WriteLine("방어력: " + _armorPoint);
            Console.WriteLine("체력: " + _maxHp);
            Console.WriteLine("Gold: " + _gold + " G");
        }

        public void TakeDamage(int damage)
        {
            // 캐릭터끼리 데미지를 주고받는 메소드
            this._hp -= damage;

            if (this._hp <= 0)
            {
                this._hp = 0;
                Death();
            }
        }

        public void Death()
        {
            // 캐릭터의 체력이 0이 되면 발생하는 메소드
        }

        // public void UseItem(itemData item)

        // public void UseSkill()
    }
}
