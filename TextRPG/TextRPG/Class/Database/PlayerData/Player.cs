using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Data;
using TextRPG.Class.Interface;
using TextRPG.Class.Database.ItemData;
using TextRPG.Class.Manager;

namespace TextRPG.Class.Database.PlayerData 
{

    internal class Player : DefaultCharacter
    {
        private int _maxEXP = 50;
        private string _weapon = "";
        private string _armor = "";

        public Player(string inputName, string input) 
        {
            if (input == "1")
            {
                this._lv = 1;
                this._name = inputName;
                this._job = "전사";

                this._str = 10;
                this.plusStr = 0;

                this._armorPoint = 5;
                this.plusArmorPoint = 0;

            this._maxHp = 100;
            this._plusHp = 0;
            this._hp = _maxHp + _plusHp;

            this._maxMp = 100;
            this._plusMp = 0;
            this._mp = _maxMp + _plusMp;

                this._gold = 1500;
                this._exp = 0;

                this._criticalRate = 0;
                this._dodgeRate = 0;

                this._inventory = new List<Item>();
            }
            else if (input == "2")
            {
                this._lv = 1;
                this._name = inputName;
                this._job = "마법사";

                this._str = 10;
                this.plusStr = 0;

                this._armorPoint = 5;
                this.plusArmorPoint = 0;

                this._maxHp = 100;
                this._plusHp = 0;
                this._hp = _maxHp;

                this._gold = 1500;
                this._exp = 0;

                this._criticalRate = 0;
                this._dodgeRate = 0;

                this._inventory = new List<Item>();
            }
            }
        public override void ShowInfo()
        {
            // 캐릭터의 능력치를 보여주는 메소드
            // 무기 칸이나 방어구 칸에 이름이 있다면 장비를 착용 중인 것으로 판별
            Console.WriteLine("Lv. " + _lv);
            Console.WriteLine("Chad ( " + _name + " )");

            if (this._armor == null || this._armor == "")
            {
                Console.WriteLine("공격력: " + _str);
            }
            else
            {
                Console.WriteLine("공격력: " + (_str + plusStr) + "( +" + plusStr + " )");
            }

            if (this._weapon == null || this._weapon == "")
            {
                Console.WriteLine("방어력: " + _armorPoint);
                Console.WriteLine("최대 체력: " + _maxHp);
            }
            else
            {
                Console.WriteLine("방어력: " + (_armorPoint + plusArmorPoint) + "( +" + plusArmorPoint + " )");
                Console.WriteLine("최대 체력: " + (_maxHp + _plusHp) + "( +" + _plusHp + " )");
            }
            
            Console.WriteLine("현재 체력: " + _hp);
            Console.WriteLine("Gold: " + _gold + " G");
        }
        public void ShowInventory()
        {
            // 인벤토리 아이템을 보여주는 메소드
            if (_inventory != null)
            {
                Console.WriteLine("인벤토리:");
                foreach (Item item in _inventory)
                {
                    Console.WriteLine($"- {item.Name} (ID: {item.Description})");
                }
            }
        }

        public void TakeItem()//아이템 받기 테스트용
        {
            _inventory.Add(GameManager.Instance.CreateManager.ItemDatabase.ItemDictionary["basicLeatherArmor"]);
            _inventory.Add(GameManager.Instance.CreateManager.ItemDatabase.ItemDictionary["basicSword"]);
            _inventory.Add(GameManager.Instance.CreateManager.ItemDatabase.ItemDictionary["shortSword"]);
            Console.WriteLine($"아이템을 받았습니다.{GameManager.Instance.CreateManager.ItemDatabase.ItemDictionary["basicLeatherArmor"]}");
            Console.WriteLine($"아이템을 받았습니다.{GameManager.Instance.CreateManager.ItemDatabase.ItemDictionary["basicSword"]}");
            Console.WriteLine($"아이템을 받았습니다.{GameManager.Instance.CreateManager.ItemDatabase.ItemDictionary["shortSword"]}");
        }

        public void Equip(Item item)
        {
            // 아이템 장착 메소드
            if (item.PlusStr > 0 && item.PlusArmorPoint == 0)
            {
                this._weapon = item.Name;    
            }
            else if (item.PlusArmorPoint > 0 && item.PlusStr == 0)
            {
                this._armor = item.Name;
            }

            // 아이템을 장착함으로써 얻는 추가 스텟 설정
            this.plusArmorPoint = item.PlusArmorPoint;
            this.plusStr = item.PlusStr;
        }

        public void UnEquip(Item item) 
        {
            // 아아템 해제 메소드
            if (this._weapon == item.Name)
            {
                this._weapon = "";
            }
            else if (this._armor == item.Name)
            {
                this._armor = "";
            }

            this.plusArmorPoint -= item.PlusArmorPoint;
            this.plusStr -= item.PlusStr;
        }

        public void TakeEXP(int gainedExp)
        {
            // 경험치 획득 메소드
            this._exp += gainedExp;
            
            // 경험치가 특정 경험치를 넘었다면 레벨업
            if (this._exp > _maxEXP)
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {
            // 레벨 업 메소드
            this._exp = ( _exp - _maxEXP );
            this._lv++;

            // 레벨 업에 따른 기초 스텟 증가도 있어야하지 않을까?
        }

        public void TakeStat()
        {
            // 캐릭터 스탯 획득 메소드
        }

        public void SpendGold(int gold)
        {
            // 골드 사용 메소드
            this._gold -= gold;
        }

        public void GetItem(Item item)
        {
            // 아이템 획득 메소드
            this._inventory.Add(item);
        }

        public void Save()
        {
            // 데이터 저장 메소드
        }

        public void Load()
        {
            // 데이터 불러오기 메소드
        }

        //public void Addstat(Defaultitem item.value)
        //{
        //    아이템의 스텟을 플레이어 스텟에 추가하는 메소드
        //}
    }
}
