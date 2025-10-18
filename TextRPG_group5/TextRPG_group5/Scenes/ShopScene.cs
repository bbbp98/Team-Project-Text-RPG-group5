using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.ItemManage;
using TextRPG_group5.Managers;

namespace TextRPG_group5.Scenes
{
     internal class ShopScene : Scene
     {
          Player player;

          public ShopScene(Player player)
          {
               this.player = player;
          }

          public override void HandleInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         Program.SetScene(new MainScene(player));
                         break;
                    case 1:
                         ShowBuyMenu();
                         break;
                    case 2:
                         ShowSellMenu();
                         break;
                    default:
                         Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("잘못된 입력입니다.\n");
                         Console.ForegroundColor = ConsoleColor.White;
                         break;
               }
          }

          public override void Show()
          {
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine("상점");
               Console.ForegroundColor = ConsoleColor.White;
               Console.WriteLine();

               Console.WriteLine("[보유 골드]");
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine($"{player.Gold} G");
               Console.ForegroundColor = ConsoleColor.White;
               Console.WriteLine();

               Console.WriteLine("[아이템 목록]");

               JobClass preClass = JobClass.All;
               // print item list
               foreach (ItemManagement item in ShopManager.Instance.GetShopItems())
               {
                    if (item is EquipItem equipItem)
                    {
                         if (preClass != equipItem.Job)
                         {
                              preClass = equipItem.Job;
                              string? classStr = "";
                              switch (preClass)
                              {
                                   case JobClass.All:
                                        classStr = "전직업";
                                        break;
                                   case JobClass.Warrior:
                                        classStr = "전사";
                                        break;
                                   case JobClass.Archer:
                                        classStr = "궁수";
                                        break;
                                   case JobClass.Magician:
                                        classStr = "마법사";
                                        break;
                                   case JobClass.Thief:
                                        classStr = "도적";
                                        break;
                              }
                              Console.ForegroundColor = ConsoleColor.Cyan;
                              Console.WriteLine();
                              Console.WriteLine($"[{classStr}용 아이템]");
                              Console.ForegroundColor = ConsoleColor.White;
                         }
                    }
                    Console.Write("- ");
                    ShowItems(item);
               }

               Console.WriteLine();

               Console.WriteLine("1. 아이템 구매");
               Console.WriteLine("2. 아이템 판매");
               Console.WriteLine("0. 나가기");
          }

          private void ShowItems(ItemManagement item)
          {
               string name = StringManager.Instance.PadRightForMixedText(item.Name!, 15);
               string description = "";
               if (item is Weapon weapon)
               {
                    string attack = $"공격력: +{weapon.AtkPower}";
                    attack = StringManager.Instance.PadRightForMixedText(attack, 15);
                    string crit = $"치명타 확률: +{weapon.CriPro}";
                    crit = StringManager.Instance.PadRightForMixedText(crit, 20);
                    description = $"{attack} | {crit}";
               }
               else if (item is Armor armor)
               {
                    description = $"방어력: +{armor.DefPower}";
                    description = StringManager.Instance.PadRightForMixedText(description, 38);
               }
               else if (item is Potion potion)
               {
                    description = potion.Description!;
                    description = StringManager.Instance.PadRightForMixedText(description!, 38);
               }

               Console.Write($"{name} | ");
               Console.Write($"{description} | ");
               // 보유 중인 아이템 체크
               if (player.Inventory.CheckItemExist(item) != null)
               {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[보유중]");
                    Console.ForegroundColor = ConsoleColor.White;
               }
               else
                    Console.WriteLine($"{item.Price} G");
          }

          private void ShowBuyMenu()
          {
               bool isExit = false;

               while (!isExit)
               {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("상점");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();

                    Console.WriteLine("[보유 골드]");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{player.Gold} G");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();

                    Console.WriteLine("=== 구매 목록 ===");

                    JobClass preClass = JobClass.All;
                    int i = 1;
                    // print item list
                    foreach (ItemManagement item in ShopManager.Instance.GetShopItems())
                    {
                         if (item is EquipItem equipItem)
                         {
                              if (preClass != equipItem.Job)
                              {
                                   preClass = equipItem.Job;
                                   string? classStr = "";
                                   switch (preClass)
                                   {
                                        case JobClass.All:
                                             classStr = "전직업";
                                             break;
                                        case JobClass.Warrior:
                                             classStr = "전사";
                                             break;
                                        case JobClass.Archer:
                                             classStr = "궁수";
                                             break;
                                        case JobClass.Magician:
                                             classStr = "마법사";
                                             break;
                                        case JobClass.Thief:
                                             classStr = "도적";
                                             break;
                                   }
                                   Console.ForegroundColor = ConsoleColor.Cyan;
                                   Console.WriteLine();
                                   Console.WriteLine($"[{classStr}용 아이템]");
                                   Console.ForegroundColor = ConsoleColor.White;
                              }
                         }
                         Console.Write($"{i++}. ");
                         ShowItems(item);
                    }

                    Console.WriteLine();

                    Console.WriteLine("0. 돌아가기\n");
                    Console.WriteLine("구매하실 아이템을 선택해주세요.");
                    Console.Write(">> ");

                    if (byte.TryParse(Console.ReadLine(), out byte input))
                    {
                         if (input != 0)
                         {
                              ShopManager.Instance.BuyItem(player, input - 1);
                              //Console.WriteLine(success ?
                                   //$"{ShopManager.Instance.GetShopItems()[input].Name}을(를) 구매 하셨습니다!" : "Gold가 부족합니다.");
                              Thread.Sleep(2000);
                         }
                         else
                              isExit = true;
                    }

                    Console.Clear();
               }
          }

          private void ShowSellMenu()
          {
               bool isExit = false;

               while (!isExit)
               {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("상점");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();

                    Console.WriteLine("[보유 골드]");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{player.Gold} G");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();

                    Console.WriteLine("=== 판매 목록 ===");

                    for (int i = 0; i < player.Inventory.GetCount(); i++)
                    {
                         var item = player.Inventory.GetItem(i);
                         string name = StringManager.Instance.PadRightForMixedText(item.Name!, 15);
                         string description = "";
                         if (item is Weapon weapon)
                         {
                              string attack = $"공격력: +{weapon.AtkPower}";
                              attack = StringManager.Instance.PadRightForMixedText(attack, 15);
                              string crit = $"치명타 확률: +{weapon.CriPro}";
                              crit = StringManager.Instance.PadRightForMixedText(crit, 20);
                              description = $"{attack} | {crit}";
                         }
                         else if (item is Armor armor)
                         {
                              description = $"방어력: +{armor.DefPower}";
                              description = StringManager.Instance.PadRightForMixedText(description, 38);
                         }
                         else if (item is Potion potion)
                         {
                              description = potion.Description!;
                              description = StringManager.Instance.PadRightForMixedText(description!, 38);
                         }

                         int sellPrice = (int)(item.Price * 0.8f);

                         Console.Write($"{i + 1}. ");
                         Console.Write($"{name} | ");
                         Console.Write($"{description} | ");
                         Console.WriteLine($"{sellPrice} G");
                    }

                    Console.WriteLine("0. 돌아가기\n");
                    Console.WriteLine("판매하실 아이템을 선택해주세요.");
                    Console.Write(">> ");

                    if (byte.TryParse(Console.ReadLine(), out byte input) && input != 0)
                    {
                         ShopManager.Instance.SellItem(player, input - 1);
                    }

                    Console.Clear();
               }
          }
     }
}
