using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scenes
{
     internal class DungeonResultScene : Scene
     {
          private bool isClearStage;

          public DungeonResultScene(bool isClearStage)
          {
               this.isClearStage = isClearStage;
          }

          public override void HandleInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         // go to MainScene
                         //Program.SetScene(new MainScene());
                         if (isClearStage)
                              Program.currentScene = new DungeonEntranceScene();
                         else
                              //Program.SetScene(new MainScene());
                              Console.WriteLine("마을로 이동");
                         break;
                    default:
                         Console.WriteLine("잘못된 입력입니다.\n");
                         break;
               }
          }

          public override void Show()
          {
               Console.WriteLine("던전 결과\n");

               // 던전 클리어
               if (isClearStage)
               {
                    Console.WriteLine("Victory\n");

                    Console.WriteLine("적이 쓰러지자 탑의 문이 천천히 열린다.");
                    Console.WriteLine("새로운 층이 모습을 드러냈다");
                    Console.WriteLine("당신의 용기는 탑의 정점을 향해 한 걸음 더 나아간다.");
                    Console.WriteLine();
               }
               else
               {
                    Console.WriteLine("이번엔 패배했지만, 모험은 끝나지 않았다.");
                    Console.WriteLine("던전은 당신의 귀환을 기다린다.");
                    Console.WriteLine("누군가는 포기하겠지만, 진정한 모험가는 다시 돌아온다.");
                    Console.WriteLine("실패는 또 다른 시작일 뿐이다. 준비를 마치고 다시 도전하라.");
                    Console.WriteLine();
               }

               Result();

               Console.WriteLine("0. 돌아가기");
          }

          private void Result()
          {
               //GamePlayer.Player player = Program.player ?? new GamePlayer.Player("test", "전사");
               //GamePlayer.Player beforePlayer = new GamePlayer.Player
               //{ 
               //     NowHp = player.NowHp,
               //     Attack = player.Attack,
               //     Defence = player.Defence,
               //};
               //player.NowHp -= 30;

               //if (isClearStage)
               //{
               //     Console.WriteLine("[캐릭터 정보]");
               //     //player.Gold += 보상.Gold;
               //     //player.Exp += 보상.Exp;
               //     string levelStr = beforePlayer.Level.ToString();
               //     string atkStr = beforePlayer.Attack.ToString();
               //     string defStr = beforePlayer.Defence.ToString();
               //     if (beforePlayer.Level != player.Level)
               //     {
               //          levelStr = $"{levelStr} -> {player.Level}";
               //          atkStr = $"{atkStr} -> {player.Attack}";
               //          defStr = $"{defStr} -> {player.Defence}";
               //     }
               //     Console.WriteLine($"Lv: {levelStr} {player.Name}");
               //     Console.WriteLine($"HP: {beforePlayer.NowHp} -> {player.NowHp}");
               //     //Console.WriteLine($"MP: {beforePlayer.NowMp} -> {player.NowMp}");
               //     Console.WriteLine($"공격력: {atkStr}");
               //     Console.WriteLine($"방어력: {defStr}");

               //     Console.WriteLine("\n[획득 아이템]");
               //     //Console.WriteLine($"Gold: {beforePlayer.Gold} -> {player.Gold}");
               //     //Console.WriteLine($"EXP: {beforePlayer.EXP} -> {player.EXP}");

               //}
               //else
               //{
               //     Console.WriteLine("[캐릭터 정보]");
               //     Console.WriteLine($"Lv: {player.Level} {player.Name}");
               //     Console.WriteLine($"HP: {beforePlayer.NowHp} -> {player.NowHp}");
               //}
          }
     }
}
