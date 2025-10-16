using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scenes
{
    internal class ActionResultScene : Scene
    {
        public Character Attacker { get; private set; }
        public List<Character> Defenders { get; private set; }

        public int AttBeforeMp { get; private set; }
        public List<int> DefBeforeHp { get; private set; }

        public Battle CurrentBattle { get; private set; }

        public ActionResultScene(Battle current, Character att, List<Character> defs, int attBeforeMp, List<int> defBeforeHp)
        {
            Attacker = att;
            Defenders = defs;

            AttBeforeMp = attBeforeMp;
            DefBeforeHp = defBeforeHp;

            CurrentBattle = current;
        }

        public override void HandleInput(byte input)
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            Console.Clear();

            Console.WriteLine("Battle!!\n");

            PrintAttackerInfo();

            PrintDefendersInfo();

            Console.WriteLine("계속하려면 아무 키나 누르세요...");
            Console.Write(">> ");
            Console.ReadKey();
        }

        void PrintAttackerInfo()
        {
            switch (CurrentBattle.GetBattleState())
            {
                case BattleState.NormalAttack:
                    if (Attacker is Player)
                        Console.WriteLine($"{Attacker.Name} 의 공격!");
                    else
                        Console.WriteLine($"Lv.{Attacker.Level} {Attacker.Name} 의 공격!");
                    break;
                case BattleState.Skill:
                    // Attacker == Player (무조건)
                    Console.WriteLine($"{Attacker.Name} 의 스킬 {CurrentBattle.userChoice} 번");
                    Console.WriteLine($"MP {AttBeforeMp} -> {((Player)Attacker).NowMp}");
                    break;
                case BattleState.Item:
                    // Attacker == Player (무조건)
                    Console.WriteLine($"{Attacker.Name} 의 {CurrentBattle.userChoice} 번 소비 아이템 사용");
                    break;
                default:
                    break;
            }

            Console.WriteLine();
        }

        void PrintDefendersInfo()
        {
            switch (CurrentBattle.GetBattleState())
            {
                case BattleState.NormalAttack:
                case BattleState.Skill:
                    for (int i = 0; i < Defenders.Count; i++)
                    {
                        if (Defenders[i] is Player)
                        {
                            Console.WriteLine($"{Defenders[i].Name} 을(를) 공격하였습니다. [데미지 : {DefBeforeHp[i] - Defenders[i].NowHp}]");
                            Console.WriteLine($"HP : {DefBeforeHp[i]} -> {Defenders[i].NowHp}");
                        }
                        else
                        {
                            Console.WriteLine($"Lv.{Defenders[i].Level} {Defenders[i].Name} 을(를) 공격하였습니다. [데미지 : {DefBeforeHp[i] - Defenders[i].NowHp}]");
                            Console.WriteLine($"HP : {DefBeforeHp[i]} -> {Defenders[i].NowHp}");
                        }
                        Console.WriteLine();
                    }
                    break;
                default:
                    break;
            }

            Console.WriteLine();
        }
    }
}
