using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.ItemData;
using TextRPG.Class.Manager;

namespace TextRPG.Class.Manager
{
    public class ShopManager
    {
        private static ShopManager instance;
        public static ShopManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ShopManager();
                return instance;
            }
        }

        private List<Item> shopItems;

        public ShopManager()
        {
            shopItems = new List<Item>();
        }

        public void AddItems()
        {
            shopItems.Add(GameManager.Instance.CreateManager.ItemDatabase.ItemDictionary["basicLeatherArmor"]);
            shopItems.Add(GameManager.Instance.CreateManager.ItemDatabase.ItemDictionary["ironMail"]);
            shopItems.Add(GameManager.Instance.CreateManager.ItemDatabase.ItemDictionary["shortSword"]);
            shopItems.Add(GameManager.Instance.CreateManager.ItemDatabase.ItemDictionary["basicSword"]);
        }

        public void ShowStoreItems()
        {
            int i = 1;
            foreach (Item item in shopItems)
            {
                bool isHaving = false;

                foreach (Item playerItem in GameManager.Instance.CreateManager.Player.Inventory)
                {
                    if (item.Id != playerItem.Id)
                    {
                        isHaving = false;
                    }
                    else
                    {
                        isHaving = true;
                        break;
                    }
                }

                if (!isHaving)
                {
                    Console.WriteLine(" - " + i + " " + item.Name + " " + item.Gold + "G");
                }
                else
                {
                    Console.WriteLine(" - " + i + " " + item.Name + " 구매 불가");
                }
                i++;
            }
        }

        public void ChooseMode()
        {
            bool isInput = false;
            while (!isInput)
            {
                Console.WriteLine("아이템 구매는 1번, 아이템 판매 2번, 0번을 입력하시면 되돌아갑니다.");
                Console.Write(">>> ");
                string rawInput = Console.ReadLine();
                int input;

                if (int.TryParse(rawInput, out input))
                {
                    switch (input)
                    {
                        case 0:
                            isInput = true;
                            break;
                        case 1:
                            BuyItem();
                            isInput = true;
                            break;
                        case 2:
                            // 아이템 판매 함수
                            SellItem();
                            isInput = true;
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                            break;
                    }
                }
            }
        }

        public void SellItem()
        {
            GameManager.Instance.CreateManager.Player.ShowInventoryWithNum();

            bool isInput = false;
            while (!isInput)
            {
                Console.WriteLine("판매하고 싶은 아이템의 번호를 입력해주세요. 0번을 입력하시면 되돌아갑니다.");
                Console.Write(">>> ");

                string rawInput = Console.ReadLine();
                int input;
                if (int.TryParse(rawInput,out input))
                {
                    if (input > 0 && input <= GameManager.Instance.CreateManager.Player.Inventory.Count)
                    {
                        GameManager.Instance.CreateManager.Player.SellItem(input - 1);
                        isInput = true;
                    }
                    else if (input == 0)
                    {
                        isInput = true;
                        ChooseMode();
                    }
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다. 다시 입력해주세요.");
                }
            }
        }

        public void BuyItem()
        {
            bool isInput = false;
            while (!isInput)
            {
                Console.WriteLine("\n구매하고 싶은 아이템의 번호를 입력해주세요. 0번을 입력하시면 되돌아갑니다.");
                Console.Write(">>> ");
                string rawInput = Console.ReadLine();
                int input;

                if (int.TryParse(rawInput, out input) && input > 0 && input <= shopItems.Count)
                {
                    bool isHaving = false;
                    foreach(Item playeritem in GameManager.Instance.CreateManager.Player.Inventory)
                    {
                        if (playeritem.Id == shopItems[input - 1].Id)
                        {
                            isHaving = true;
                            break;
                        }
                    }

                    // 가지고 있지 않다면
                    if (!isHaving)
                    {
                        // 골드를 아이템가격만큼 소비한 후 플레이어의 인벤토리에 추가하는 함수
                        ComparePrice(shopItems[input - 1]);
                        AddPlayerInventory(shopItems[input - 1]);
                    }
                    // 가지고 있다면
                    else
                    {
                        Console.WriteLine("\n가지고 있는 아이템입니다.");
                    }
                }
                else if (int.TryParse(rawInput, out input) && input == 0)
                {
                    isInput = true;
                    Console.WriteLine();
                    ChooseMode();
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다. 다시 입력해주세요.");
                }
            }
        }

        public void ComparePrice(Item chooseItem)
        {
            if (GameManager.Instance.CreateManager.Player.Gold >= chooseItem.Gold)
            {
                GameManager.Instance.CreateManager.Player.SpendGold(chooseItem.Gold);
            }
            else
            {
                Console.WriteLine("\n가격이 부족합니다.");
            }
        }

        public void AddPlayerInventory(Item chooseItem)
        {
            if (GameManager.Instance.CreateManager.Player.Inventory == null)
            {
                return;
            }

            GameManager.Instance.CreateManager.Player.Inventory.Add(chooseItem);

            Console.WriteLine("\n구매완료! 인벤토리에 아이템을 추가하였습니다.");
        }
    }
}
