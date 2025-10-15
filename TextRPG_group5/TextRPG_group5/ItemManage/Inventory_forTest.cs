// 아이템 출력 예시

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TextRPG_group5
//{
//    public class Inventory
//    {
//        Item item;
//        List<string> ItemList = new List<string>();

//        public void ShowInventory()
//        {
//            ItemList.Add("목검");
//            ItemList.Add("가죽갑옷");
            
//            for(int i = 0; i < ItemList.Count(); i++)
//            {
//                item = ItemInfo.GetItem(ItemList[i]);
//                Console.Write($"{item.Name} : {item.Description}   ");
//                if(item.IsEquip)
//                {
//                    Console.WriteLine("[E]");
//                }
//                else
//                {
//                    Console.WriteLine();
//                }
//            }
//        }
//    }
//}
