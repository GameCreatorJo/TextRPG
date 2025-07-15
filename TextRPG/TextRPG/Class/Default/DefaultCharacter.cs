using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.ItemData;

namespace TextRPG.Class.Data
{
    internal abstract class DefaultCharacter
    {
        protected int _lv;
        protected string _name;
        protected string _job;

        // 기본 공격력
        protected int _str;
        // 아이템 장착 공격력
        protected int plusStr;

        // 기본 방어력
        protected int _armorPoint;
        // 아이템 장착 방어력
        protected int plusArmorPoint;

        // 기본 최대 Hp
        protected int _maxHp;
        // 아이템 장착 Hp
        protected int _plusHp;
        // 현재 체력
        protected int _hp;

        // 현재 보유 골드
        protected int _gold;
        // 캐릭터의 경험치
        protected int _exp;

        // 캐릭터의 인벤토리
        protected List<Item> _inventory;
        public List<Item> Inventory
        {
            get { return _inventory; }
        }
        public int Lv
        {
            get { return _lv; }
        }
        public string Name
        {
            get { return _name; }
        }
        public string Job
        {
            get { return _job; }
        }
        public int Str
        {
            get { return _str + plusStr; }
        }
        public int ArmorPoint
        {
            get { return _armorPoint + plusArmorPoint; }
        }
        public int MaxHp
        {
            get { return _maxHp + _plusHp; }
        }
        public int Hp
        {
            get { return _hp; }
        }
        public int Gold
        {
            get { return _gold; }
        }
        public int Exp
        {
            get { return _exp; }
        }



        public DefaultCharacter()
        {
            
        }

        public virtual void ShowInfo()
        {
            // 캐릭터의 능력치를 보여주는 메소드
            Console.WriteLine("Lv. " + _lv);
            Console.WriteLine("Chad ( " + _name + " )");
            Console.WriteLine("공격력: " + _str);
            Console.WriteLine("방어력: " + _armorPoint);
            Console.WriteLine("체력: " + _maxHp);
            Console.WriteLine("Gold: " + _gold + " G");
        }

        public virtual void TakeDamage(int damage)
        {
            // 캐릭터끼리 데미지를 주고받는 메소드
            this._hp -= damage;

            if (this._hp <= 0)
            {
                this._hp = 0;
                Death();
            }
        }

        public virtual void Death()
        {
            // 캐릭터의 체력이 0이 되면 발생하는 메소드
        }

        // public void UseItem(itemData item)

        // public void UseSkill()
    }
}
