using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Data;


namespace TextRPG.Class.Database.Item
{
    internal class ItemDatabaseList 
    {
       private Dictionary<string, Item> itemDictionary;
        public Dictionary<string, Item> ItemDictionary
        {
            get { return itemDictionary; }
        }
        Item basicLeatherArmor = new Item(1, "Basic Leather Armor", "It's a crude leather armor of a beginner", 5, 0, 100);
        Item basicSword = new Item(2, "Basic Sword", "It's a crude iron sword of a beginner.", 0, 5, 150);
        Item shortSword = new Item(3, "Short Sword", "It's a short sword.", 10, 0, 200);
        Item ironMail = new Item(4, "Iron Mail", "It's a Iron mail.", 0, 10, 250);
        public ItemDatabaseList()
        {
            itemDictionary = new Dictionary<string, Item>();
            CreateItem();
        }
        public void CreateItem()
        {
            itemDictionary.Add("basicLeatherArmor", basicLeatherArmor);
            itemDictionary.Add("basicSword", basicSword);
            itemDictionary.Add("shortSword", shortSword);
            itemDictionary.Add("ironMail", ironMail);
        }

        //public static List<DefaultItem> DefaultItems { get; } = new ()
        //{
        //    new DefaultItem.Item(1, "Basic Leather Armor", "It's a crude leather armor of a beginner", 5, 0, 100),
        //    new DefaultItem.Item(2, "Basic Sword", "It's a crude iron sword of a beginner.", 0, 5, 150),
        //    new DefaultItem.Item(3, "Short Sword", "It's a short sword.", 10, 0, 200),
        //    new DefaultItem.Item(4, "Iron Mail", "It's a Iron mail.", 0, 10, 250),


        //};

        //public static DefaultItem? GetDefaultItemById(int Id)
        //{
        //    return DefaultItems.Find(item => item.Id == Id);
        //}




    }
}
