using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scenes
{
    internal class BattleScene : Scene
    {
        Battle bt;
        Player player;
        List<Monster> monsters;

        public BattleScene(Player player, List<Monster> monsters)
        {
            this.player = player;
            this.monsters = monsters;

            // 전투 로직 로드
            bt = new Battle(player, monsters);
        }

        // 입력 값 처리 메서드
        public override void HandleInput(byte input)
        {
            if (bt.GetBattleState() == BattleState.None)
            {
                switch (input)
                {
                    case 0:
                        bt.SetBattleState(BattleState.None);
                        break;
                    case 1:
                        bt.SetBattleState(BattleState.NormalAttack);
                        break;
                    case 2:
                        bt.SetBattleState(BattleState.Skill);
                        break;
                    case 3:
                        bt.SetBattleState(BattleState.Item);
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
                    bt.SetBattleState(BattleState.None);
                    bt.userChoice = 0;
                    return;
                }

                if (bt.GetBattleState() == BattleState.NormalAttack)
                {
                    if (input > monsters.Count)
                    {
                        Console.WriteLine("잘못된 입력입니다.\n");
                        return;
                    }
                }
                else if (bt.GetBattleState() == BattleState.Skill)
                {
                    int skillCount = 3; // player.Skills.Count;

                    if (input > skillCount)
                    {
                        Console.WriteLine("잘못된 입력입니다.\n");
                        return;
                    }
                }
                else if (bt.GetBattleState() == BattleState.Item)
                {
                    int itemCount = 3; // player.Inventory.Count;

                    if ( input > itemCount)
                    {
                        Console.WriteLine("잘못된 입력입니다.\n");
                        return;
                    }
                }

                bt.userChoice = input;
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

            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].ShowStatus();
            }
            Console.WriteLine();

            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level}\t{player.Name} ( {player.Job} )");
            Console.WriteLine($"HP {player.NowHp} / {player.MaxHp}");
            Console.WriteLine();

            Console.WriteLine("==============================");
            Console.WriteLine();

            switch ((byte)bt.GetBattleState())
            {
                case 0:
                    Console.WriteLine("1. 일반 공격");
                    Console.WriteLine("2. 스킬 사용");
                    Console.WriteLine("3. 아이템 사용");
                    break;
                case 1:
                    if (bt.userChoice != 0)
                        bt.HitNormalAttack();
                    else
                        bt.SelectTarget();
                    //bt.HitNormalAttack();
                    break;
                case 2:
                    if (bt.userChoice != 0)
                        bt.UseSkill();
                    bt.SelectSkill();
                    break;
                case 3:
                    if (bt.userChoice != 0)
                        bt.UseItem();
                    bt.SelectUsableItem();
                    break;
                default:
                    break;
            }
        }
    }
}
