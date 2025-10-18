using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.ItemManage;

namespace TextRPG_group5.Managers
{
     internal class ShopManager
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

          private List<ItemManagement> shopItems = new List<ItemManagement>();

          private ShopManager()
          {
               InitializeShopItems();
          }

          private void InitializeShopItems()
          {
               // 전사 아이템
               shopItems.Add(ItemInfo.GetItem("목검"));
               shopItems.Add(ItemInfo.GetItem("강철검"));
               shopItems.Add(ItemInfo.GetItem("BF대검"));
               shopItems.Add(ItemInfo.GetItem("워해머"));

               shopItems.Add(ItemInfo.GetItem("가죽갑옷"));
               shopItems.Add(ItemInfo.GetItem("강철갑옷"));
               shopItems.Add(ItemInfo.GetItem("체인메일"));

               // 마법사 아이템
               shopItems.Add(ItemInfo.GetItem("나무스태프"));
               shopItems.Add(ItemInfo.GetItem("철제스태프"));
               shopItems.Add(ItemInfo.GetItem("아크스태프"));
               shopItems.Add(ItemInfo.GetItem("스페셜완드"));

               shopItems.Add(ItemInfo.GetItem("수련생의 로브"));
               shopItems.Add(ItemInfo.GetItem("숙련자의 로브"));
               shopItems.Add(ItemInfo.GetItem("달인의 로브"));

               // 궁수 아이템
               shopItems.Add(ItemInfo.GetItem("나무 활"));
               shopItems.Add(ItemInfo.GetItem("철제 활"));
               shopItems.Add(ItemInfo.GetItem("에이전트 보우"));
               shopItems.Add(ItemInfo.GetItem("석궁"));

               shopItems.Add(ItemInfo.GetItem("가죽 보호구"));
               shopItems.Add(ItemInfo.GetItem("철제 보호구"));
               shopItems.Add(ItemInfo.GetItem("특제 보호구"));

               // 도적 아이템
               shopItems.Add(ItemInfo.GetItem("녹슨 단검"));
               shopItems.Add(ItemInfo.GetItem("강철 단검"));
               shopItems.Add(ItemInfo.GetItem("카람빗"));
               shopItems.Add(ItemInfo.GetItem("쿠나이"));

               shopItems.Add(ItemInfo.GetItem("암행복"));
               shopItems.Add(ItemInfo.GetItem("잠행복"));
               shopItems.Add(ItemInfo.GetItem("위장복"));

               // 전직업 공용 아이템
               // 테스트용 아이템인가?
               shopItems.Add(ItemInfo.GetItem("창세신의 무구"));
               shopItems.Add(ItemInfo.GetItem("창세신의 가호"));

               // 소비 아이템
               shopItems.Add(ItemInfo.GetItem("HP소형포션"));
               shopItems.Add(ItemInfo.GetItem("HP대형포션"));
               shopItems.Add(ItemInfo.GetItem("MP소형포션"));
               shopItems.Add(ItemInfo.GetItem("MP대형포션"));
          }

          public List<ItemManagement> GetShopItems()
          {
               return shopItems;
          }

          public void BuyItem(Player player, int index)
          {
               // 잘못된 입력
               if (index < 0 || index >= shopItems.Count)
               {
                    Console.WriteLine("잘못된 입력입니다.");
                    return;
               }

               var shopItem = shopItems[index];
               if (shopItem is not UsableItem && player.Inventory.CheckItemExist(shopItem) != null)
               {
                    Console.WriteLine("이미 보유 중인 아이템입니다..");
                    return;
               }

               if (player.Gold < shopItem.Price)
               {
                    Console.WriteLine("보유 중인 Gold가 부족합니다.");
                    return;
               }

               player.Gold -= shopItem.Price;
               player.Inventory.AddItem(shopItem);

               Console.WriteLine($"{shopItem.Name}을(를) 구매 하셨습니다!");
               return;
          }

          public void SellItem(Player player, int inventoryIndex)
          {
               if (inventoryIndex < 0 || inventoryIndex >= player.Inventory.GetCount())
               {
                    Console.WriteLine("잘못된 입력입니다.");
                    return;
               }

               var item = player.Inventory.GetItem(inventoryIndex);
               int sellPrice = (int)(item.Price * 0.8f);

               // 장착 중인 아이템
               if (item.IsEquip)
               {
                    while (true)
                    {
                         Console.WriteLine("장착 중인 아이템입니다. 정말로 판매하시겠습니까? (Y/N)");
                         string check = Console.ReadLine() ?? "";
                         check = check.ToLower();
                         if (check == "y")
                         {
                              Console.WriteLine("장착 중인 아이템을 판매합니다.");
                              Thread.Sleep(1000);
                              player.Gold += sellPrice;
                              player.Inventory.RemoveItem(item);
                              item.IsEquip = false;
                              break;
                         }
                         else if (check == "n")
                         {
                              Console.WriteLine("장착 중인 아이템을 판매하지 않습니다.");
                              Thread.Sleep(1000);
                              break;
                         }
                         else continue;
                    }

                    return;
               }
               else if (item is UsableItem usableItem)
               {
                    Console.WriteLine($"{usableItem.Name}을(를) 1개 판매하셨습니다.");
                    player.Inventory.DecreaseItem(usableItem);
                    player.Gold += sellPrice;
                    Thread.Sleep(1000);
                    //player.Inventory.RemoveItem(item);
               }
          }
     }
}
