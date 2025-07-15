using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Class.Data
{
    internal abstract class DefaultItem
    {
        // 아이템 식별자
        protected int _id;
        // 아이템 이름
        protected string _name = "";
        // 아이템 설명
        protected string _description = "";
        // 아이템의 Plus Strength
        protected int _plusStr;
        // 아이템의 Plus Armor Point
        protected int _plusArmorPoint;
        // 아이템의 Gold Value
        protected int _gold;

        public DefaultItem(int id, string name, string description, int plusStr, int plusArmorPoint, int gold)
        {
            _id = id;
            _name = name;
            _description = description;
            _plusStr = plusStr;
            _plusArmorPoint = plusArmorPoint;
            _gold = gold;
        }
       


        

    }
}