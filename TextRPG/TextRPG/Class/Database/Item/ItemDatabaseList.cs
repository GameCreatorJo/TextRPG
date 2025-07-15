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
            new DefaultItem.Item(1, "초보?�의 갑옷", "초보?�의 ?�술??가죽갑?�이??", 5, 0, 100),
            new DefaultItem.Item(2, "초보?�의 철�?", "초보?�의 조잡??철�??�다.", 0, 5, 150),
            new DefaultItem.Item(3, "강철검", "강철�?만든 ???��???검?�다.", 10, 0, 200),
            new DefaultItem.Item(4, "강철갑옷", "강철�?만든 ???��???갑옷?�다.", 0, 10, 250),


        };
        
        public static DefaultItem? GetDefaultItemById(int Id)
        {
            return DefaultItems.Find(item => item.Id == Id);
        }
       
        

    }
}
