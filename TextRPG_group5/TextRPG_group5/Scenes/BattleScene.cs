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

        public Player Player { get { return CurrentBattle.Player;  } }
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
                        CurrentBattle.SetBattleState(BattleState.Skill);
                        break;
                    case 3:
                        CurrentBattle.SetBattleState(BattleState.Item);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.\n");
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
                    if (input > Monsters.Count)
                    {
                        Console.WriteLine("잘못된 입력입니다.\n");
                        return;
                    }
                }
                else if (CurrentBattle.GetBattleState() == BattleState.Skill)
                {
                    int skillCount = 3; // player.Skills.Count;

                    if (input > skillCount)
                    {
                        Console.WriteLine("잘못된 입력입니다.\n");
                        return;
                    }
                }
                else if (CurrentBattle.GetBattleState() == BattleState.Item)
                {
                    int itemCount = 3; // player.Inventory.Count;

                    if ( input > itemCount)
                    {
                        Console.WriteLine("잘못된 입력입니다.\n");
                        return;
                    }
                }

                CurrentBattle.userChoice = input;
            }
        }

        // 화면에 보여줄 텍스트들(Console.Write관련)
        public override void Show()
        {
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            /*if (bt.GetBattleState() == BattleState.ActionResult)
            {
                //ActionResultScene resultScene = new ActionResultScene(player, monsters, bt.isPlayerTurn, bt.userChoice);
                //resultScene.Show();
                return;
            }*/

            PrintEnemyInfo();

            PrintPlayerInfo();

            switch ((byte)CurrentBattle.GetBattleState())
            {
                case 0:
                    Console.WriteLine("1. 일반 공격");
                    Console.WriteLine("2. 스킬 사용");
                    Console.WriteLine("3. 아이템 사용");
                    break;
                case 1:
                    if (CurrentBattle.userChoice != 0)
                        CurrentBattle.HitNormalAttack();
                    else
                        CurrentBattle.SelectTarget();
                    //bt.HitNormalAttack();
                    break;
                case 2:
                    if (CurrentBattle.userChoice != 0)
                        CurrentBattle.UseSkill();
                    CurrentBattle.SelectSkill();
                    break;
                case 3:
                    if (CurrentBattle.userChoice != 0)
                        CurrentBattle.UseItem();
                    CurrentBattle.SelectUsableItem();
                    break;
                default:
                    break;
            }
        }

        void PrintEnemyInfo()
        {
            for (int i = 0; i < Monsters.Count; i++)
            {
                Monsters[i].ShowStatus();
            }
            Console.WriteLine();
        }

        void PrintPlayerInfo()
        {
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{Player.Level}\t{Player.Name} ( {Player.Job} )");
            Console.WriteLine($"HP {Player.NowHp} / {Player.MaxHp}");
            Console.WriteLine();
        }
    }
}
