using System;
using System.Runtime.CompilerServices;
using TextRPG_group5.ItemManage;
using TextRPG_group5.Managers;
using TextRPG_group5.Scenes;

namespace TextRPG_group5
{
     internal class Program
     {
          static private Scene? currentScene = new NewTitleScene();
          static private Player? player;
          static private bool isSkipInput = false;
          public const int maxStage = 10;

          static void Main(string[] args)
          {
               //Initialize();

               while (true)
               {
                    if (currentScene is NewTitleScene
                         || currentScene is CreatePlayerScene)
                    { currentScene!.Show(); }
                    else
                    {
                         currentScene!.Show();
                         if (isSkipInput)
                         {
                              isSkipInput = false;
                              continue;
                         }
                         Console.WriteLine();
                         Console.WriteLine("원하시는 행동을 입력해주세요.");
                         Console.Write(">> ");
                         byte input = byte.TryParse(Console.ReadLine(), out byte val) ? val : byte.MaxValue;
                         Console.Clear();
                         currentScene.HandleInput(input);
                    }
               }
          }

          static public void Initialize(Player newPlayer)
          {
               // player initialize
               player = newPlayer;
               player.ReachedStage = 1;

               // item initialize
               switch (player.Job)
               {
                    case "전사":
                         player.Inventory.AddItem(ItemInfo.GetItem("목검"));
                         player.Inventory.AddItem(ItemInfo.GetItem("가죽갑옷"));
                         break;
                    case "궁수":
                         player.Inventory.AddItem(ItemInfo.GetItem("나무 활"));
                         player.Inventory.AddItem(ItemInfo.GetItem("가죽 보호구"));
                         break;
                    case "도적":
                         player.Inventory.AddItem(ItemInfo.GetItem("녹슨 단검"));
                         player.Inventory.AddItem(ItemInfo.GetItem("암행복"));
                         break;
                    case "마법사":
                         player.Inventory.AddItem(ItemInfo.GetItem("나무스태프"));
                         player.Inventory.AddItem(ItemInfo.GetItem("수련생의 로브"));
                         break;
               }

               player.Inventory.GetItem(0).IsEquip = true;
               player.Inventory.GetItem(1).IsEquip = true;
               player.Equipment.EquipItem((EquipItem)player.Inventory.GetItem(0));
               player.Equipment.EquipItem((EquipItem)player.Inventory.GetItem(1));

               player.Inventory.AddItem(ItemInfo.GetItem("HP소형포션"), 3);
               player.Inventory.AddItem(ItemInfo.GetItem("MP소형포션"), 3);

               // scene initialize
               currentScene = new MainScene(player); // startScene
          }

          static public void SetScene(Scene scene)
          {
               currentScene = scene;
          }

          static public void SetSkipInput(bool isSkip)
          {
               isSkipInput = isSkip;
          }
     }
}
