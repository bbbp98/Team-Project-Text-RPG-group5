using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
     public static class LoadScreen
     {
          public enum LoadResult
          {
               Cancel,      // 사용자 취소 또는 뒤로가기
               Success,     // 정상 로드: GameProgress 반환
               NewGameMade  // 저장이 없어서 새 게임 생성
          }

          public static async Task<(LoadResult result, GameProgress? data)> Show()
          {
               while (true)
               {
                    Console.Clear();
                    Console.WriteLine("=== Load ===");
                    Console.WriteLine("저장 데이터를 불러올까요?");
                    Console.WriteLine("1. 불러오기");
                    Console.WriteLine("0. 뒤로가기");
                    Console.Write("선택: ");

                    var key = Console.ReadKey(true).KeyChar;
                    switch (key)
                    {
                         case '1':
                              Console.WriteLine("\n불러오는 중...");
                              var gp = await TryLoad();
                              if (gp != null)
                              {
                                   Console.WriteLine("✅ 불러오기 성공!");
                                   await Task.Delay(700);
                                   return (LoadResult.Success, gp);
                              }
                              else
                              {
                                   Console.WriteLine("⚠ 저장 데이터가 없거나 손상되었어요.");
                                   Console.WriteLine("1. 새 게임으로 시작");
                                   Console.WriteLine("0. 뒤로가기");
                                   Console.Write("선택: ");
                                   var k2 = Console.ReadKey(true).KeyChar;
                                   if (k2 == '1')
                                   {
                                        var newGp = new GameProgress();
                                        Console.WriteLine("\n새 게임을 생성했어요.");
                                        await Task.Delay(600);
                                        return (LoadResult.NewGameMade, newGp);
                                   }
                                   else
                                   {
                                        Console.WriteLine("\n뒤로 돌아갑니다.");
                                        await Task.Delay(600);
                                        return (LoadResult.Cancel, null);
                                   }
                              }

                         case '0':
                              Console.WriteLine("\n뒤로 돌아갑니다.");
                              await Task.Delay(600);
                              return (LoadResult.Cancel, null);

                         default:
                              Console.WriteLine("\n올바른 번호를 선택해줘.");
                              await Task.Delay(700);
                              break;
                    }
               }
          }

          private static async Task<GameProgress> TryLoad()
          {
               try
               {
                    //var gp = await GameProgress.SaveLoadManager.LoadAsync();
                    //return IsValid(gp) ? gp : null;
                    return null;
               }
               catch
               {
                    return null;
               }
          }

          private static bool IsValid(GameProgress gp)
          {
               //if (gp == null) return false;
               //if (gp.Player == null) return false;
               //if (string.IsNullOrWhiteSpace(gp.Player.Name)) return false;
               //if (gp.Player.Level < 1 || gp.Player.Level > 999) return false;
               //if (gp.SaveVersion != 1) return false;
               return true;
          }
     }
}
