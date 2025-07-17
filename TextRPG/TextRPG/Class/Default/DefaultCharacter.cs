using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.ItemData;

namespace TextRPG.Class.Data
{
    public abstract class DefaultCharacter
    {
        private Random random = new Random();

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

        // 기본 마나
        protected int _maxMp;
        // 아이템 장착 마나
        protected int _plusMp;
        // 현재 마나
        protected int _mp;

        // 현재 보유 골드
        protected int _gold;
        // 캐릭터의 경험치
        protected int _exp;

        // 캐릭터의 치명타 확률
        protected int _criticalRate;
        // 캐릭터의 회피 확률
        protected int _dodgeRate;

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
        public int MaxMp
        {
            get { return  (_maxMp + _plusMp); }
        }
        public int Mp
        {
            get { return _mp; }
        }
        public int Gold
        {
            get { return _gold; }
        }
        public int Exp
        {
            get { return _exp; }
        }
        public int CriticalRate
        {
            get { return _criticalRate; }
        }
        public int DodgeRate
        {
            get { return _dodgeRate; }
        }
        public int PlusStr
        {
            get { return plusStr; }
		}
        public int PlusArmorPoint
        {
            get { return plusArmorPoint; }
        }
        public int PlusHp
        {
            get { return _plusHp; }
        }
        public int PlusMp
        {
            get { return _plusMp; }
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

        public virtual void TakeDamage(int damage, int opponentCritRate)
        {
            // 캐릭터끼리 데미지를 주고받는 메소드
            // 데미지를 받을 때 적의 치명타 확률과 본인의 회피율을 통해 데미지를 계산한다.
            this._hp -= ( damage * CriticalDamage(opponentCritRate) * AvoidDamage() );

            if (this._hp <= 0)
            {
                this._hp = 0;
                Death();
            }
        }

        // 치명타 확률은 받는 캐릭터의 치명타 확률이 아니라 데미지를 주는 캐릭터의 치명타 확률에 따름
        public virtual int CriticalDamage(int criticalRate)
        {
            int randomNum = random.Next(0, 100);

            if (randomNum < criticalRate)
                return 2;
            else
                return 1;
        }

        public virtual int AvoidDamage()
        {
            int randomNum = random.Next(0, 100);

            if (randomNum < this._dodgeRate)
                return 0;
            else
                return 1;
        }

        public virtual void Death()
        {
            // 캐릭터의 체력이 0이 되면 발생하는 메소드
        }

        // public void UseItem(itemData item)

        // public void UseSkill()
    }
}
