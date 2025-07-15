using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Class.Data
{
    internal abstract class DefaultItem
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int PlusStr { get; }
        public int PlusArmorPoint { get; }

        public int Gold { get; }
       
        

        protected DefaultItem(int id, string name, string description, int plusStr, int plusArmorPoint,  int gold)
        {
            Id = id;
            Name = name;
            Description = description;
            PlusStr = plusStr;
            PlusArmorPoint = plusArmorPoint;
            
            Gold = gold;

            
        }
        public class Item : DefaultItem
        {

            public Item(int id, string name, string description, int plusStr, int plusArmorPoint, int gold)
                : base(id, name, description, plusStr, plusArmorPoint, gold)
            {

            }
        }
        // 아이템 클래스는 DefaultItem을 상속받아 구현합니다.


    }
}