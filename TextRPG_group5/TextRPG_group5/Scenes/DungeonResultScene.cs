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
          private int stage;
          private Player player;
          private Player preBattlePlayer;

          private class Reward
          {
               public int Gold;
               public int Exp;

               public Reward(int stage)
               {
                    Gold = 200 * stage;
                    Exp = 15 * stage;
               }
          }

          public DungeonResultScene(Player player, int stage, bool isClearStage)
          {
               this.player = player;
               this.preBattlePlayer = new Player
               {
                    NowHp = player.NowHp,
                    Attack = player.Attack,
                    Defence = player.Defence,
                    Level = player.Level,
               };
               this.stage = stage;
               this.isClearStage = isClearStage;
          }

          public DungeonResultScene(Player player, Player preBattlePlayer, int stage, bool isClearStage)
          {
               this.player = player;
               this.preBattlePlayer = preBattlePlayer;
               this.stage = stage;
               this.isClearStage = isClearStage;
          }

          public override void HandleInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         if (isClearStage)
                              Program.SetScene(new DungeonEntranceScene(player));
                         else
                              Program.SetScene(new MainScene(player));
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
               Console.WriteLine("던전 결과\n");
               Console.ForegroundColor = ConsoleColor.White;

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
                    Console.WriteLine("누군가는 포기하겠지만, 진정한 모험가는 다시 돌아온다.");
                    Console.WriteLine("실패는 또 다른 시작일 뿐이다. 준비를 마치고 다시 도전하라.");
                    Console.WriteLine();
               }

               Result();

               Console.WriteLine("0. 돌아가기");
          }

          private void Result()
          {
               //     Player beforePlayer = new Player
               //     {
               //          NowHp = player.NowHp,
               //          Attack = player.Attack,
               //          Defence = player.Defence,
               //          Level = player.Level,
               //     };
               //     int beforeMp = player.NowMp;
               //     int beforeGold = player.Gold;
               //     int beforeExp = player.Exp;

               if (isClearStage)
               {
                    Reward reward = new Reward(stage);

                    player.Gold += reward.Gold; // Player에서 Exp처럼 Gold를 더할 수 있는 메서드 필요
                    player.GainExp(reward.Exp);

                    Console.WriteLine("[캐릭터 정보]");
                    string levelStr = preBattlePlayer.Level.ToString();
                    string atkStr = preBattlePlayer.Attack.ToString();
                    string defStr = preBattlePlayer.Defence.ToString();

                    if (preBattlePlayer.Level != player.Level)
                    {
                         levelStr = $"{levelStr} -> {player.Level}";
                         atkStr = $"{atkStr} -> {player.Attack}";
                         defStr = $"{defStr} -> {player.Defence}";
                    }

                    Console.WriteLine($"Lv: {levelStr} {player.Name}");
                    Console.WriteLine($"HP: {preBattlePlayer.NowHp} -> {player.NowHp}");
                    Console.WriteLine($"MP: {preBattlePlayer.NowMp} -> {player.NowMp}");
                    //Console.WriteLine($"MP: {beforePlayer.NowMp} -> {player.NowMp}");
                    Console.WriteLine($"공격력: {atkStr}");
                    Console.WriteLine($"방어력: {defStr}");

                    Console.WriteLine("\n[획득 아이템]");
                    Console.WriteLine($"Gold: {preBattlePlayer.Gold} -> {player.Gold}");
                    //Console.WriteLine($"Gold: {beforeGold} -> {player.Gold}");
                    Console.WriteLine($"EXP: {preBattlePlayer.Exp} -> {player.Exp}");
                    //Console.WriteLine($"EXP: {beforeExp} -> {player.Exp}");

                    if (stage == player.ReachedStage) // update player stage
                    {
                         Console.WriteLine("\n최고 층이 갱신되었습니다.");
                         if (player.ReachedStage < Program.maxStage)
                              player.ReachedStage++;
                    }
               }
               else
               {
                    Console.WriteLine("[캐릭터 정보]");
                    Console.WriteLine($"Lv: {player.Level} {player.Name}");
                    Console.WriteLine($"HP: {preBattlePlayer.NowHp} -> {player.NowHp}");
               }
               Console.WriteLine();
          }
     }
}
