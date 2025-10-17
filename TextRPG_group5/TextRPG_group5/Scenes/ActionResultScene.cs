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

        public int AttBeforeHp { get; private set; }
        public int AttBeforeMp { get; private set; }
        public List<int> DefBeforeHp { get; private set; }

        public Battle CurrentBattle { get; private set; }

        public ActionResultScene(Battle current, Character att, List<Character> defs, int attBeforeMp, List<int> defBeforeHp)
        {
            // 일반 공격, 스킬 공격용 ActionResultScene 생성자
            Attacker = att;
            Defenders = defs;

            AttBeforeMp = attBeforeMp;
            DefBeforeHp = defBeforeHp;

            CurrentBattle = current;
        }

        public ActionResultScene(Battle current, int playerBeforeHp, int playerBeforeMp)
        {
            // 아이템 사용용 ActionResultScene 생성자
            CurrentBattle = current;
            Attacker = CurrentBattle.Player;
            AttBeforeHp = playerBeforeHp;
            AttBeforeMp = playerBeforeMp;
        }

        public override void HandleInput(byte input)
        {
            switch(input)
            {
                case 0:
                    if (CurrentBattle.IsAllEnemyDead() || CurrentBattle.Player.IsDead)
                    {
                        CurrentBattle.EndBattle(CurrentBattle.IsStageClear());
                        return;
                    }

                    CurrentBattle.SetBattleState(BattleState.None);
                    CurrentBattle.userChoice = 0;
                    CurrentBattle.userTargetChoice = 0;
                    CurrentBattle.userSkillChoice = 0;
                    CurrentBattle.isPlayerTurn = !CurrentBattle.isPlayerTurn; // 턴 교체

                    Program.SetScene(new BattleScene(CurrentBattle));
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.ResetColor();
                    break;
            }
        }

        public override void Show()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Battle!!");
            Console.ResetColor();

            PrintAttackerInfo();

            PrintDefendersInfo();

            Console.WriteLine("0. 다음");
       }

        void PrintAttackerInfo()
        {
            switch (CurrentBattle.GetBattleState())
            {
                case BattleState.NormalAttack:
                    if (Attacker is Player)
                        Console.WriteLine($"{Attacker.Name} 의 공격!");
                    else
                    {
                        Console.WriteLine($"Lv.{Attacker.Level} {Attacker.Name} 의 공격!");
                        Console.WriteLine(((Monster)Attacker).Msg);
                    }
                        
                    break;
                case BattleState.Skill:
                    // Attacker == Player (무조건)
                    string selectedSkillName = ((Player)Attacker).Skill.skillBook[CurrentBattle.userSkillChoice - 1].Name;
                    Console.WriteLine($"{Attacker.Name} 의 {selectedSkillName} 사용!");
                    Console.WriteLine($"MP {AttBeforeMp} -> {((Player)Attacker).NowMp}");
                    break;
                case BattleState.Item:
                    // Attacker == Player (무조건)
                    Console.WriteLine($"{Attacker.Name} 의 {CurrentBattle.UsableItemOnly[CurrentBattle.userChoice - 1].Name} 사용");

                    // TODO : 일단은 HP 만, 나중에 MP, 상태이상 등도 추가
                    Console.WriteLine($"HP : {AttBeforeHp} -> {Attacker.NowHp}");
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
                        int damage = DefBeforeHp[i] - Defenders[i].NowHp;
                        
                        if (Defenders[i] is Player)
                        {
                            if (damage == 0)
                                Console.WriteLine($"{Defenders[i].Name} 이(가) 공격을 회피하였습니다. [데미지 : {damage}]");
                            
                            else
                                Console.WriteLine($"{Defenders[i].Name} 을(를) 공격하였습니다. [데미지 : {damage}]");

                            Console.WriteLine($"HP : {DefBeforeHp[i]} -> {Defenders[i].NowHp}");
                        }
                        else
                        {
                            if (damage == 0)
                                Console.WriteLine($"Lv.{Defenders[i].Level} {Defenders[i].Name} 이(가) 공격을 회피하였습니다. [데미지 : {damage}]");
                            else

                                Console.WriteLine($"Lv.{Defenders[i].Level} {Defenders[i].Name} 을(를) 공격하였습니다. [데미지 : {damage}]");

                            Console.WriteLine($"HP : {DefBeforeHp[i]} -> {Defenders[i].NowHp}");
                        }
                        Console.WriteLine();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
