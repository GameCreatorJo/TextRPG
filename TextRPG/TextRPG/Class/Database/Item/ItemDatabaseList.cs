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
        public static List<DefaultItem> DefaultItems { get; } = new ()
        {
            new DefaultItem.Item(1, "Basic Leather Armor", "It's a crude leather armor of a beginner", 5, 0, 100),
            new DefaultItem.Item(2, "Basic Sword", "It's a crude iron sword of a beginner.", 0, 5, 150),
            new DefaultItem.Item(3, "Short Sword", "It's a short sword.", 10, 0, 200),
            new DefaultItem.Item(4, "Iron Mail", "It's a Iron mail.", 0, 10, 250),


        };
        
        public static DefaultItem? GetDefaultItemById(int Id)
        {
            return DefaultItems.Find(item => item.Id == Id);
        }
       
        

    }
}
