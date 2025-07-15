using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Data;
using TextRPG.Class.Interface;

namespace TextRPG.Class.Database.Player
{
    internal class Player : DefaultCharacter, IPlayerData
    {
        public Player(string inputName) 
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
            this._hp = _maxHp;

            this._gold = 1500;
        }

        public void Equip(DefaultItem item)
        {
            // 아이템 장착 메소드
        }

        public void UnEquip(DefaultItem item) 
        {
            // 아아템 해제 메소드
        }

        public void TakeEXP()
        {
            // 경험치 획득 메소드
        }

        public void TakeStat()
        {
            // 캐릭터 스탯 획득 메소드
        }

        public void LevelUp()
        {
            // 레벨 업 메소드
        }

        public void SpendGold(int gold)
        {
            // 골드 사용 메소드
        }

        public void GetItem(string item)
        {
            // 아이템 획득 메소드
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
