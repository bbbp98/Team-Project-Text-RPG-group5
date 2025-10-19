using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TextRPG_group5.EffectManagement;
using TextRPG_group5.ItemManage;
using TextRPG_group5.Managers;

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

          // 소비, 기타 아이템용
          public int Quanitity { get; set; } = 1;
          public List<ItemManagement> Items { get; set; }
          private Player Owner { get; set; }

          public Inventory()
          {
               Items = new List<ItemManagement>();
          }

          public Inventory(Player owner)
          {
               Items = new List<ItemManagement>();
               this.Owner = owner;
          }

          public void SetOwner(Player owner)
          { this.Owner = owner; }

          /// <summary>
          /// 인벤토리에 해당 아이템이 존재하는지 확인
          /// </summary>
          /// <param name="item"></param>
          /// <returns>존재하면 item을 return, null: 존재X</returns>
          public ItemManagement? CheckItemExist(ItemManagement item) => Items.FirstOrDefault(i => i.Name == item.Name);


          /// <summary>
          /// Inventory의 아이템 종류 개수 받아오기
          /// </summary>
          public int GetCount() => Items.Count;

          /// <summary>
          /// Inventory에 장비 아이템 추가
          /// </summary>
          public void AddItem(ItemManagement item)
          {
               ItemManagement? existing = CheckItemExist(item);
               if (existing == null)
               {
                    Items.Add(item);
               }
               else
               {
                    Console.WriteLine($"{item.Name}은(는) 이미 보유 중입니다.");
               }

               //Console.WriteLine($"{item.Name}을(를) 인벤토리에 추가했습니다.");
          }

          /// <summary>
          /// 소비, 기타 아이템 같이 개수가 여러 개인 아이템 추가
          /// </summary>
          public void AddItem(ItemManagement item, int quantity)
          {
               ItemManagement? existing = CheckItemExist(item);

               // 인벤토리에 아이템이 존재하지 않으면 인벤토리에 추가
               if (existing == null)
               {
                    item.ItemCounts = quantity;
                    Items.Add(item);
               }
               // 인벤토리에 아이템이 존재한다면 개수만 추가
               else
               {
                    existing.ItemCounts += quantity;
               }
          }

          /// <summary>
          /// 소비 아이템 사용
          /// </summary>
          public void UseItem(UsableItem item)
          {
               ItemManagement? existing = CheckItemExist(item);

               if (existing == null)
               {
                    Console.WriteLine($"{item.Name}이 존재하지 않습니다.");
                    return;
               }

               if (item is Potion potion)
               {
                    potion.UseItem(Owner, potion);
                    DecreaseItem(potion);
               }
          }

          public void DecreaseItem(UsableItem item)
          {
               item.ItemCounts--;
               if (item.ItemCounts <= 0)
                    RemoveItem(item);
          }

          /// <summary>
          /// Inventory의 아이템 삭제
          /// </summary>
          public void RemoveItem(ItemManagement item)
          {
               if (Items.Remove(item))
               {
                    Console.WriteLine($"{item.Name}이(가) 삭제되었습니다.");
                    Thread.Sleep(500);
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
               Items.OrderBy(x => x.Name).ToList();

               Console.WriteLine();
               if (Items.Count == 0)
               {
                    Console.WriteLine("인벤토리가 비어있습니다.");
                    return;
               }

               for (int i = 0; i < Items.Count; i++)
               {
                    var item = Items[i];
                    Console.Write($"- {i + 1}. ");
                    string equipMark = Items[i].IsEquip ? "[E]" : "";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{equipMark}");
                    Console.ForegroundColor = ConsoleColor.White;
                    string name = StringManager.Instance.PadRightForMixedText(Items[i].Name!, Items[i].IsEquip ? 12 : 15);
                    //string name = StringManager.Instance.PadRightForMixedText(item.Name!, 15);
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
                         string quantityStr = $" | 개수 : {potion.ItemCounts}";
                         description = StringManager.Instance.PadRightForMixedText(description!, 25);
                         quantityStr = StringManager.Instance.PadRightForMixedText(quantityStr, 13);
                         description += quantityStr;
                    }

                    Console.Write($"{name} | ");
                    Console.WriteLine($"{description}");
               }
          }

          /// <summary>
          /// 아이템 장착/해제
          /// </summary>
          public void Equip(int index)
          {
               index--;
               if (index < 0 && index > Items.Count)
                    return;

               if (Items[index] == null)
               {
                    Console.WriteLine("장착할 수 있는 아이템이 없습니다.");
                    return;
               }

               //if (items[index].IsEquip)
               //{
               //     Console.WriteLine($"{items[index].Name}은(는) 이미 장착 중입니다.");
               //     return;
               //}

               if (Items[index] is EquipItem equipItem)
               {
                    if (Items[index].IsEquip)
                         Owner.Equipment.UnEquipItem(equipItem);
                    else
                         Owner.Equipment.EquipItem(equipItem);
               }
               else
               {
                    // 소비, 기타
                    Console.WriteLine($"{Items[index].Name}은(는) 장착할 수 없습니다.\n");
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
                         Items = Items.OrderBy(p => p.Name).ToList();
                         break;
                    case 2: // 장착순(무기 -> 방어구)
                         Items = Items.OrderByDescending(p => p.IsEquip).ThenBy(p => order[p.GetType()]).ToList();
                         break;
                    case 3: // 장비 아이템순
                         Items = Items.OrderBy(p => order[p.GetType()]).ToList();
                         break;
                    case 4: // 소비 아이템순
                         Items = Items.OrderByDescending(p => order[p.GetType()]).ToList();
                         break;
               }
          }

          /// <summary>
          /// 특정 인덱스의 아이템 가져오기
          /// </summary>
          public ItemManagement GetItem(int index)
          {
               return Items[index];
          }

          /// <returns>인벤토리 안의 모든 UsableItem 반환</returns>
          public List<UsableItem> GetUsableItems()
          {
               List<UsableItem> usableItems = new List<UsableItem>();

               foreach (var item in Items)
               {
                    if (item is UsableItem usableItem)
                    {
                         usableItems.Add(usableItem);
                    }
               }

               return usableItems;
          }
     }
}
