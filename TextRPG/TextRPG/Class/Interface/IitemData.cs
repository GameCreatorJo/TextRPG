using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Data;
using TextRPG.Class.Database.ItemData;
using static TextRPG.Class.Data.DefaultItem;

namespace TextRPG.Class.Interface
{
    public class IitemData
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int PlusStr { get; set; }
        public int PlusArmorPoint { get; set; }
        public int Gold { get; set; }
        public ItemID ItemType { get; set; }
        // 기본 생성자 필요 (역직렬화용)
        public IitemData() { }
    }
}
