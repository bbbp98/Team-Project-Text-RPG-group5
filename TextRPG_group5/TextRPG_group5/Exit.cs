using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.Managers;

namespace TextRPG_group5
{
     public static class Exit
     {
          public enum ExitAction
          {
               ExitWithoutSave,
               ExitAfterSave,
               ReturnToMainMenu
          }

          public static async Task<ExitAction> Show(GameProgress gp)
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
                              var saved = await TrySave(gp);
                              if (saved)
                              {
                                   Info("저장 완료! 게임을 종료합니다.");
                                   await Pause();
                                   return ExitAction.ExitAfterSave;
                              }
                              else
                              {
                                   Warn("저장에 실패했습니다. 메인 메뉴로 돌아갑니다.");
                                   await Pause();
                                   return ExitAction.ReturnToMainMenu;
                              }

                         case '2':
                              Warn("진행 사항은 저장되지 않아요. 정말 종료할까? (Y/N)");
                              if (Confirm())
                              {
                                   Info("게임을 종료합니다.");
                                   await Pause();
                                   return ExitAction.ExitWithoutSave;
                              }
                              break;

                         case '3':

                              Info("메인 메뉴로 돌아갑니다.");
                              await Pause();
                              return ExitAction.ReturnToMainMenu;

                         default:
                              Warn("올바른 번호를 선택해주시기바랍니다.");
                              await Task.Delay(700);
                              break;
                    }
               }
          }

          private static void DrawHeader()
          {
               Console.WriteLine("=== 종료 화면 ===");
               Console.WriteLine();
          }

          private static void DrawSummary(GameProgress gp)
          {
               Console.WriteLine(gp?.ToString() ?? "진행 상황 없음");
          }

          private static void DrawMenu()
          {
               Console.WriteLine();
               Console.WriteLine("1. 저장하고 종료");
               Console.WriteLine("2. 저장 안 하고 종료");
               Console.WriteLine("3. 메인 메뉴로 돌아가기");
               Console.WriteLine();
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
                    Error($"저장 실패: {ex.Message}");
                    return false;
               }
          }

          private static void Info(string message)
          {
               Console.ForegroundColor = ConsoleColor.Green;
               Console.WriteLine(message);
               Console.ResetColor();
          }

          private static void Warn(string message)
          {
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine(message);
               Console.ResetColor();
          }

          private static void Error(string message)
          {
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine(message);
               Console.ResetColor();
          }

          private static async Task Pause()
          {
               Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
               await Task.Run(() => Console.ReadKey(true));
          }

          private static bool Confirm()
          {
               var key = Console.ReadKey(true).KeyChar;
               return key == 'Y' || key == 'y';
          }
     }

     public class GameProgress
     {
          public static SaveManager SaveLoadManager = new SaveManager();

          public override string ToString()
          {
               return "현재 게임 진행 상황";
          }
     }

     public class SaveManager
     {
          public async Task SaveAsync(GameProgress gp)
          {
               await Task.Delay(500);
               throw new Exception("디스크 쓰기 오류");
          }
     }
}