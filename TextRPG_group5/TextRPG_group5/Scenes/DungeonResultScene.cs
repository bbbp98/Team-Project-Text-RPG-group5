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
               if (!isClearStage)
                    return;

               switch (input)
               {
                    case 0:
                         if (isClearStage)
                              Program.SetScene(new DungeonEntranceScene(player));
                         //else
                         //     Program.SetScene(new MainScene(player));
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
                    Console.WriteLine("===========================================");
                    Console.WriteLine("           던전 클리어 성공!!!");
                    Console.WriteLine("===========================================\n");
                    Thread.Sleep(800);
                    if (stage == Program.maxStage)
                    {
                         Console.WriteLine("마침내, 던전의 끝에 닿았다.\n");
                         Thread.Sleep(800);
                         Console.WriteLine("오래된 전설처럼… 네 소원이 이뤄질 시간이 다가온다.\n");
                         Thread.Sleep(800);
                         Console.WriteLine("네가 걸어온 길의 끝, 그곳에 ‘진실’이 있었다.\n");
                         Thread.Sleep(1200);
                         Console.WriteLine("당신은 여태까지의 모험을 통해서 소원을 스스로 이룰 힘을 얻었다.\n");
                         Thread.Sleep(800);
                         
                    }
                    else
                    {
                         Console.WriteLine("적은 쓰러지고, 고요가 찾아왔다. 한 걸음 더, 소원에 가까워졌다.\n");
                         Thread.Sleep(800);
                         Console.WriteLine("새로운 길이 열렸다.\n");
                         Thread.Sleep(800);
                         Console.WriteLine("다음 스테이지가 기다린다.\n");
                         Thread.Sleep(800);
                    }
                    Console.WriteLine();
               }
               else
               {
                    Console.WriteLine("===========================================");
                    Console.WriteLine("               던전 클리어 실패...");
                    Console.WriteLine("===========================================\n");
                    Thread.Sleep(800);
                    Console.WriteLine("무릎 꿇은 자리, 숨이 가쁘다.\n");
                    Thread.Sleep(1200);
                    Console.WriteLine("이번에는 패배했지만, 끝은 아니다.\n");
                    Thread.Sleep(800);
                    Program.SetScene(new MainScene(player));
               }

               Result();

               if (isClearStage)
                    Console.WriteLine("0. 돌아가기");
          }

          private void Result()
          {
               if (isClearStage)
               {
                    Reward reward = new Reward(stage);

                    player.Gold += reward.Gold; // Player에서 Exp처럼 Gold를 더할 수 있는 메서드 필요
                    player.GainExp(reward.Exp);

                    Thread.Sleep(1000);
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
                    Console.WriteLine($"공격력: {atkStr}");
                    Console.WriteLine($"방어력: {defStr}");

                    Console.WriteLine("\n[획득 아이템]");
                    Console.WriteLine($"Gold: {preBattlePlayer.Gold} -> {player.Gold}");
                    Console.WriteLine($"EXP: {preBattlePlayer.Exp} -> {player.Exp}");

                    if (stage == player.ReachedStage) // update player stage
                    {
                         Thread.Sleep(1000);
                         Console.WriteLine("\n최고 층이 갱신되었습니다.");
                         if (player.ReachedStage < Program.maxStage)
                              player.ReachedStage++;
                    }
                    Console.WriteLine();
               }
               else
               {
                    player.NowHp = player.MaxHp;
                    Program.SetSkipInput(true);
                    Console.Clear();
               }
          }
     }
}
