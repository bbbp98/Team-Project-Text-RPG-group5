using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
     public static class SaveScreen
     {
          public enum SaveResult
          {
               Success,
               Failed,
               Canceled
          }

          public static async Task<SaveResult> Show(GameProgress gp)
          {
               while (true)
               {
                    Console.Clear();
                    DrawHeader();
                    DrawSummary(gp);
                    DrawMenu();

                    var key = Console.ReadKey(true).KeyChar;
                    switch (key)
                    {
                         case '1':
                              var ok = await TrySave(gp);
                              if (ok)
                              {
                                   Info("저장에 성공했어요!");
                                   await Pause();
                                   return SaveResult.Success;
                              }
                              else
                              {
                                   Error("저장에 실패했어요. 저장 없이 메인으로 돌아갈게.");
                                   await Pause();
                                   return SaveResult.Failed;
                              }

                         case '0':
                              Info("메인 화면으로 돌아갑니다.");
                              await Pause();
                              return SaveResult.Canceled;

                         default:
                              Warn("올바른 번호를 선택해줘.");
                              await Task.Delay(700);
                              break;
                    }
               }
          }

          private static void DrawHeader()
          {
               Console.WriteLine("=== 게임 저장 ===");
               Console.WriteLine("현재 진행 상황을 저장할 수 있어.");
               Console.WriteLine();
          }

          private static void DrawSummary(GameProgress gp)
          {
               Console.WriteLine(gp?.ToString() ?? "(진행 정보 없음)");
          }

          private static void DrawMenu()
          {
               Console.WriteLine("1. 저장하기");
               Console.WriteLine("0. 뒤로가기");
               Console.Write("선택: ");
          }

          private static async Task<bool> TrySave(GameProgress gp)
          {
               try
               {
                    Info("저장 중...");
                    await GameProgress.SaveLoadManager.SaveAsync(gp);
                    return true;
               }
               catch (Exception ex)
               {
                    Error($"예외 발생: {ex.Message}");
                    return false;
               }
          }

          private static Task Pause(int ms = 800) => Task.Delay(ms);

          private static void Info(string msg)
          {
               Console.WriteLine();
               Console.WriteLine($"[안내] {msg}");
          }

          private static void Warn(string msg)
          {
               Console.WriteLine();
               Console.WriteLine($"[주의] {msg}");
          }

          private static void Error(string msg)
          {
               Console.WriteLine();
               Console.WriteLine($"[오류] {msg}");
          }
     }
}
