using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TextRPG_group5.ItemManage;

namespace TextRPG_group5
{
     internal class Inventory
     {
          private Dictionary<Type, int> order = new Dictionary<Type, int>
          {
               { typeof(Weapon), 1 },
               { typeof(Armor), 2 },
               { typeof(Potion), 3 },
               { typeof(EtcItem), 4 },
          };

          private List<ItemManagement> items = new List<ItemManagement>();
          private Player owner;

          public Inventory(Player owner)
          {
               this.owner = owner;
          }

          /// <summary>
          /// Inventory의 아이템 종류 개수 받아오기
          /// </summary>
          public int GetCount()
          { return items.Count; }

          /// <summary>
          /// Inventory에 아이템 추가
          /// </summary>
          public void AddItem(ItemManagement item)
          {
               items.Add(item);
               //Console.WriteLine($"{item.Name}을(를) 인벤토리에 추가했습니다.");
          }

          /// <summary>
          /// Inventory의 아이템 삭제
          /// </summary>
          public void RemoveItem(ItemManagement item)
          {
               if (items.Remove(item))
               {
                    Console.WriteLine($"{item.Name}이(가) 삭제되었습니다.");
               }
               else
               {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{item.Name}을(를) 가지고 있지 않습니다.");
                    Console.ForegroundColor = ConsoleColor.White;
               }
          }

          public void Show()
          {
               items.OrderBy(x => x.Name).ToList();

               //Console.WriteLine("인벤토리");
               Console.WriteLine();
               if (items.Count == 0)
               {
                    Console.WriteLine("인벤토리가 비어있습니다.");
                    return;
               }

               for (int i = 0; i < items.Count; i++)
               {
                    string equipMark = items[i].IsEquip ? "[E]" : "";
                    Console.Write($"- {i + 1}. ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{equipMark}");
                    Console.ForegroundColor = ConsoleColor.White;

                    //if (items[i] is Weapon weapon)
                    //{
                    //     if (owner.Job != weapon.Job.ToString())
                    //          Console.ForegroundColor = ConsoleColor.Blue;
                    //}

                    //if (items[i] is Armor armor)
                    //{
                    //     if (owner.Job != armor.Job.ToString())
                    //          Console.ForegroundColor = ConsoleColor.Blue;
                    //}

                    Console.Write($"{items[i].Name!.PadRight(10)} | ");
                    Console.WriteLine(items[i].Description);
                    Console.ForegroundColor = ConsoleColor.White;     
               }
          }

          /// <summary>
          /// 아이템 장착/해제
          /// </summary>
          public void Equip(int index)
          {
               index--;
               if (index < 0 && index > items.Count)
                    return;

               if (items[index] == null)
               {
                    Console.WriteLine("장착할 수 있는 아이템이 없습니다.");
                    return;
               }

               //if (items[index].IsEquip)
               //{
               //     Console.WriteLine($"{items[index].Name}은(는) 이미 장착 중입니다.");
               //     return;
               //}

               if (items[index] is EquipItem equipItem)
               {
                    if (items[index].IsEquip)
                         owner.Equipment.UnEquipItem(equipItem);
                    else
                         owner.Equipment.EquipItem(equipItem);
               }
               else
               {
                    // 소비, 기타
                    Console.WriteLine($"{items[index].Name}은(는) 장착할 수 없습니다.\n");
               }
          }

          /// <summary>
          /// 1: 이름순, 2: 장착순(무기 -> 방어구), 3: 장비 아이템순, 4: 소비 아이템순
          /// </summary>
          public void Sort(byte input)
          {
               switch (input)
               {
                    case 1: // 이름순
                         items = items.OrderBy(p => p.Name).ToList();
                         break;
                    case 2: // 장착순(무기 -> 방어구)
                         items = items.OrderByDescending(p => p.IsEquip).ThenBy(p => order[p.GetType()]).ToList();
                         break;
                    case 3: // 장비 아이템순
                         items = items.OrderBy(p => order[p.GetType()]).ToList();
                         break;
                    case 4: // 소비 아이템순
                         items = items.OrderByDescending(p => order[p.GetType()]).ToList();
                         break;
               }
          }

          /// <summary>
          /// 특정 인덱스의 아이템 가져오기
          /// </summary>
          public ItemManagement GetItem(int index)
          {
               return items[index];
          }
     }
}
