using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Class.Data
{
    public abstract class DefaultItem
    {
		public enum ItemID
		{
			Weapon = 0,
			Armor = 1
		}

		//아이템 아이디
		protected int _id;
        // 아이템 이름
        protected string _name = "";
        // 아이템 설명
        protected string _description = "";
        // 아이템 공격력 Plus Strength
        protected int _plusStr;
        // 아이템 방어력 Plus Armor Point
        protected int _plusArmorPoint;
        // 아이템 가격 Gold Value
        protected int _gold;
        // 아이템 종류ID
        protected ItemID _itemID;

        public int Id => _id;
        public string Name => _name;
        public string Description => _description;
        public int PlusStr => _plusStr;
        public int PlusArmorPoint => _plusArmorPoint;
        public int Gold => _gold;
        public ItemID itemID => _itemID;

        public DefaultItem()
        {
            // 기본 생성자
        }

        public DefaultItem(int id, string name, string description, int plusStr, int plusArmorPoint, int gold, ItemID itemID)
        {
            _id = id;
            _name = name;
            _description = description;
            _plusStr = plusStr;
            _plusArmorPoint = plusArmorPoint;
            _gold = gold;
            _itemID = itemID;
        }
       


        

    }
}