using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Class.Data
{
    internal abstract class DefaultCharacter
    {
        protected int _lv;
        protected string _name = "";
        protected string _job = "";

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

        protected int _gold;

        // 캐릭터의 인벤토리
        // private List<ItemData> _item;

        public DefaultCharacter()
        {
            
        }

        public void ShowInfo()
        {
            // 캐릭터의 능력치를 보여주는 메소드
        }

        public void TakeDamage(int damage)
        {
            // 캐릭터끼리 데미지를 주고받는 메소드
        }

        public void Death()
        {
            // 캐릭터의 체력이 0이 되면 발생하는 메소드
        }

        // public void UseItem(itemData item)

        // public void UseSkill()
    }
}
