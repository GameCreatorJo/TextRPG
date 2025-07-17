using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Interface;

namespace TextRPG.Class.Database.ItemData
{
    public class ItemSaveData : IitemData
    {

        public ItemSaveData()
        {
        }
        public ItemSaveData(Item item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.Description = item.Description;
            this.PlusStr = item.PlusStr;
            this.PlusArmorPoint = item.PlusArmorPoint;
            this.Gold = item.Gold;
            this.ItemType = item.itemID;
        }
    }
}
