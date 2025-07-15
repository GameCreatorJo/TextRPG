using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Data;

namespace TextRPG.Class.Database.ItemData
{
    internal class Item : DefaultItem
    {
        public Item()
        {
            // 기본 생성자
        }
        public Item(int id, string name, string description, int plusStr, int plusArmorPoint, int gold)
            :base(id, name, description, plusStr, plusArmorPoint, gold)
        {
          
        }
        // 아이템의 정보를 보여주는 메소드
        public void ShowInfo()
        {
            Console.WriteLine($"ID: {_id}, Name: {_name}, Description: {_description}, Plus Strength: {_plusStr}, Plus Armor Point: {_plusArmorPoint}, Gold Value: {_gold}");
        }
    }
}
