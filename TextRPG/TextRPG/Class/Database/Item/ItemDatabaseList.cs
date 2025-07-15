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
            new DefaultItem.Item(1, "초보자의 갑옷", "초보자의 허술한 가죽갑옷이다.", 5, 0, 100),
            new DefaultItem.Item(2, "초보자의 철검", "초보자의 조잡한 철검이다.", 0, 5, 150),
            new DefaultItem.Item(3, "강철검", "강철로 만든 썩 훌륭한 검이다.", 10, 0, 200),
            new DefaultItem.Item(4, "강철갑옷", "강철로 만든 썩 훌륭한 갑옷이다.", 0, 10, 250),


        };
        
        public static DefaultItem? GetDefaultItemById(int Id)
        {
            return DefaultItems.Find(item => item.Id == Id);
        }
       
        

    }
    // test 주석입니다
}
