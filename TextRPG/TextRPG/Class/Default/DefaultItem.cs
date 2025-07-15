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
       
        

        protected DefaultItem(int _id, string _name, string _description, int _plusStr, int _plusArmorPoint,  int _gold)
        {
            Id = _id;
            Name = _name;
            Description = _description;
            PlusStr = _plusStr;
            PlusArmorPoint = _plusArmorPoint;
            
            Gold = _gold;

            
        }
        public class Item : DefaultItem
        {

            public Item(int _id, string _name, string _description, int _plusStr, int _plusArmorPoint, int _gold)
                : base(_id, _name, _description, _plusStr, _plusArmorPoint, _gold)
            {

            }
        }
        //public class Weapon : DefaultItem
        //{

        //    public Weapon(int _id, string _name, string _description, int _plusStr, int _plusArmorPoint, int _gold)
        //        : base(_id, _name, _description,_plusStr, _plusArmorPoint, _gold)
        //    {

        //    }
        //}
        //public class Armor : DefaultItem
        //{

        //    public Armor( _id, string _name, string _description, int _plusStr,int _plusArmorPoint, int _gold)
        //        : base(_id, _name, _description,_gold, _plusStr, _plusArmorPoint )
        //    {

        //    }
        //}

    }
}