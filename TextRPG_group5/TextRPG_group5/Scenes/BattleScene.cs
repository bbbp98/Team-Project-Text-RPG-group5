using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scenes
{
    internal class BattleScene : Scene
    {
        public Battle CurrentBattle { get; private set; }

        public Player Player { get { return CurrentBattle.Player; } }
        public List<Monster> Monsters { get { return CurrentBattle.Monsters; } }

        public BattleScene(Battle currentBattle)
        {
            CurrentBattle = currentBattle;
        }

        // 입력 값 처리 메서드
        public override void HandleInput(byte input)
        {
            if (CurrentBattle.GetBattleState() == BattleState.None)
            {
                switch (input)
                {
                    case 0:
                        CurrentBattle.SetBattleState(BattleState.None);
                        break;
                    case 1:
                        CurrentBattle.SetBattleState(BattleState.NormalAttack);
                        break;
                    case 2:
                        //CurrentBattle.SetBattleState(BattleState.Skill);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("아직은 사용할 수 없습니다.\n");
                        Console.ResetColor();
                        break;
                    case 3:
                        CurrentBattle.SetBattleState(BattleState.Item);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("잘못된 입력입니다.\n");
                        Console.ResetColor();
                        break;
                }
            }

            else
            {
                if (input == 0)
                {
                    CurrentBattle.SetBattleState(BattleState.None);
                    CurrentBattle.userChoice = 0;
                    return;
                }

                if (CurrentBattle.GetBattleState() == BattleState.NormalAttack)
                {
                    if (input > Monsters.Count || Monsters[input - 1].IsDead)
                    {   // 몬스터 번호 범위 밖이거나, 이미 죽은 몬스터 선택
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("잘못된 입력입니다.\n");
                        Console.ResetColor();
                        return;
                    }
                }
                else if (CurrentBattle.GetBattleState() == BattleState.Skill)
                {
                    int skillCount = 3; // player.Skills.Count;

                    if (input > skillCount)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("잘못된 입력입니다.\n");
                        Console.ResetColor();
                        return;
                    }
                }
                else if (CurrentBattle.GetBattleState() == BattleState.Item)
                {
                    int itemCount = 3; // player.Inventory.Count;

                    if (input > itemCount)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("잘못된 입력입니다.\n");
                        Console.ResetColor();
                        return;
                    }
                }

                CurrentBattle.userChoice = input;
            }
        }

        // 화면에 보여줄 텍스트들(Console.Write관련)
        public override void Show()
        {
            if (!CurrentBattle.isPlayerTurn)
            {
                // 몬스터 턴
                CurrentBattle.SetBattleState(BattleState.NormalAttack);
                CurrentBattle.HitNormalAttack(); // 자동 공격 전환 -> 바로 결과창으로
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!!");
            Console.ResetColor();
            Console.WriteLine();

            PrintEnemyInfo();

            PrintPlayerInfo();

            Console.WriteLine("==============================");
            Console.WriteLine();

            switch (CurrentBattle.GetBattleState())
            {
                case BattleState.None:
                    PrintActionList();
                    break;
                case BattleState.NormalAttack:
                    if (CurrentBattle.userChoice == 0)
                        PrintTargetList();
                        
                    else
                        CurrentBattle.HitNormalAttack();

                    break;
                case BattleState.Skill:
                    if (CurrentBattle.userChoice == 0)
                        PrintSkillList();

                    else
                        CurrentBattle.UseSkill();
                    break;
                case BattleState.Item:
                    if (CurrentBattle.userChoice == 0)
                        PrintUsableItemList();

                    else
                        CurrentBattle.UseItem();
                    break;
                default:
                    break;
            }
        }

        void PrintEnemyInfo()
        {
            for (int i = 0; i < Monsters.Count; i++)
            {
                if (Monsters[i].IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Lv.{Monsters[i].Level} {Monsters[i].Name} DEAD");
                    Console.ResetColor();

                    continue;
                }
                
                Monsters[i].ShowStatus();
            }
            Console.WriteLine();
        }

        void PrintPlayerInfo()
        {
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{Player.Level}\t{Player.Name} ( {Player.Job} )");
            Console.WriteLine($"HP {Player.NowHp} / {Player.MaxHp}");
            Console.WriteLine($"MP {Player.NowMp} / {Player.MaxMp}");
            Console.WriteLine();
        }

        void PrintActionList()
        {
            Console.WriteLine("1. 일반 공격");
            Console.WriteLine("2. 스킬 사용");
            Console.WriteLine("3. 아이템 사용");
        }

        void PrintTargetList()
        {
            for (int i = 0; i < Monsters.Count; i++)
            {
                if (Monsters[i].IsDead)
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.WriteLine($"[{i + 1}] {Monsters[i].Name}");
                Console.ResetColor();

            }
            Console.WriteLine();

            Console.WriteLine("0. 취소");
        }

        void PrintSkillList()
        {
            Console.WriteLine("[1] 스킬 1번");
            Console.WriteLine("[2] 스킬 2번");
            Console.WriteLine("[3] 스킬 3번");
            Console.WriteLine();

            Console.WriteLine("0. 취소");
        }

        void PrintUsableItemList()
        {
            Console.WriteLine("[1] 소비 아이템 1번");
            Console.WriteLine("[2] 소비 아이템 2번");
            Console.WriteLine("[3] 소비 아이템 3번");
            Console.WriteLine();

            Console.WriteLine("0. 취소");
        }
    }
}
