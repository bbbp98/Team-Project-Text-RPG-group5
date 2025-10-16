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
     //private Dictionary<Type, int> 
     internal class Inventory
     {
          private List<ItemManagement> items = new List<ItemManagement>();
          private Player owner;

          public Inventory(Player owner)
          {
               this.owner = owner;
          }

          public int GetCount()
          { return items.Count; }

          public void AddItem(ItemManagement item)
          {
               items.Add(item);
               //Console.WriteLine($"{item.Name}을(를) 인벤토리에 추가했습니다.");
          }

          public void RemoveItem(ItemManagement item)
          {
               if (items.Remove(item))
               {
                    Console.WriteLine($"{item.Name}이(가) 삭제되었습니다.");
               }
               else
               {
                    Console.WriteLine($"{item.Name}을(를) 가지고 있지 않습니다.");
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
                    Console.Write($"- {i + 1}. {equipMark}{items[i].Name!.PadRight(10)} | ");
                    Console.WriteLine(items[i].Description);
               }
          }

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
                    owner.Equipment.EquipItem(equipItem);
               }
               else
               {
                    // 소비, 기타
                    Console.WriteLine($"{items[index].Name}은(는) 장착할 수 없습니다.");
               }
          }

          public void Sort(byte input)
          {
               switch (input)
               {
                    case 1: // 이름순
                         items.OrderBy(p => p.Name).ToList();
                         break;
                    case 2: // 장착순
                         items.OrderBy(p => p.IsEquip).ToList(); 
                         break;
                    case 3: // 공격력
                         //items.OrderBy();
                         break;
                    case 4:
                         //items.OrderBy();
                         break;
               }
          }
     }
}
