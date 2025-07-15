using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Class.Data
{
    internal abstract class DefaultItem
    {
        // ������ �ĺ���
        protected int _id;
        // ������ �̸�
        protected string _name = "";
        // ������ ����
        protected string _description = "";
        // �������� Plus Strength
        protected int _plusStr;
        // �������� Plus Armor Point
        protected int _plusArmorPoint;
        // �������� Gold Value
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