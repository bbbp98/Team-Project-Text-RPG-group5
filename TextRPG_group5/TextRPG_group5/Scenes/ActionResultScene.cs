using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.ItemManage;
using TextRPG_group5.QuestManagement;

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

        private Potion selectedItem;

        public ActionResultScene(Battle current, Character att, List<Character> defs, int attBeforeMp, List<int> defBeforeHp)
        {
            // 일반 공격, 스킬 공격용 ActionResultScene 생성자
            Attacker = att;
            Defenders = defs;

            AttBeforeMp = attBeforeMp;
            DefBeforeHp = defBeforeHp;

            CurrentBattle = current;
        }

        public ActionResultScene(Battle current, int playerBeforeHp, int playerBeforeMp, UsableItem selectedItem)
        {
            // 아이템 사용용 ActionResultScene 생성자
            CurrentBattle = current;
            Attacker = CurrentBattle.Player;
            AttBeforeHp = playerBeforeHp;
            AttBeforeMp = playerBeforeMp;

            this.selectedItem = (Potion)selectedItem;
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

            Console.WriteLine("\n0. 다음");
       }

        void PrintAttackerInfo()
        {
            switch (CurrentBattle.CurrentState)
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
                    SkillData selectedSkill = ((Player)Attacker).Skill.skillBook[CurrentBattle.userSkillChoice - 1];

                    if (AttBeforeMp - selectedSkill.MpCost < 0)
                    {
                        Console.WriteLine($"{selectedSkill.Name} 를 사용할 수 없습니다...");
                        Console.WriteLine($"MP {AttBeforeMp} -> {AttBeforeMp}");
                    }
                    else
                    {
                        Console.WriteLine($"{Attacker.Name} 의 {selectedSkill.Name} 사용!");
                        Console.WriteLine($"MP {AttBeforeMp} -> {AttBeforeMp - selectedSkill.MpCost}");
                    }
                    
                    break;
                case BattleState.Item:
                    // Attacker == Player (무조건)
                    Console.WriteLine($"{Attacker.Name} 의 {selectedItem.Name} 사용");

                    PrintUsingItemResult(selectedItem.Type);

                    break;
                default:
                    break;
            }

            Console.WriteLine();
        }

        void PrintDefendersInfo()
        {
            switch (CurrentBattle.CurrentState)
            {
                case BattleState.NormalAttack:
                    for (int i = 0; i < Defenders.Count; i++)
                    {
                        int damage = DefBeforeHp[i] - Defenders[i].NowHp;

                        PrintNormalAttackResult(Defenders[i], DefBeforeHp[i], damage);
                    }
                    break;

            case BattleState.Skill:
                ((Player)Attacker).Skill.UseSkill(CurrentBattle.userSkillChoice - 1, Defenders[0]);
                Console.WriteLine();

                Console.WriteLine($"{Defenders[0].Name} HP : {DefBeforeHp[0]} -> {Defenders[0].NowHp}");
                Console.WriteLine();

                // 공격한 몬스터가 죽으면, 퀘스트 진행 상황 업데이트 및 몬스터 처치 경험치 획득
                if (Defenders[0].IsDead)
                {
                    QuestManager.Instance.UpdateProgress(Defenders[0].Name);
                    ((Player)Attacker).GainExp(((Monster)Defenders[0]).Exp);
                }
                break;
            default:
                break;
            }
        }

        void PrintNormalAttackResult(Character defender, int defBeforeHP, int damage)
        {
            if (defender is Player)
            {
                if (damage == 0)
                    Console.WriteLine($"{defender.Name} 이(가) 공격을 회피하였습니다. [데미지 : {damage}]");

                else
                    Console.WriteLine($"{defender.Name} 을(를) 공격하였습니다. [데미지 : {damage}]");

                Console.WriteLine($"HP : {defBeforeHP} -> {defender.NowHp}");
            }
            else
            {
                if (damage == 0)
                    Console.WriteLine($"Lv.{defender.Level} {defender.Name} 이(가) 공격을 회피하였습니다. [데미지 : {damage}]");
                else

                    Console.WriteLine($"Lv.{defender.Level} {defender.Name} 을(를) 공격하였습니다. [데미지 : {damage}]");

                Console.WriteLine($"HP : {defBeforeHP} -> {defender.NowHp}");
            }
            Console.WriteLine();
        }

        void PrintUsingItemResult(PotionType type)
        {
            switch (type)
            {
                case PotionType.HealthPotion:
                    Console.WriteLine($"HP {AttBeforeHp} -> {Attacker.NowHp} (+{Attacker.NowHp - AttBeforeHp})");
                    break;
                case PotionType.ManaPotion:
                    Console.WriteLine($"MP {AttBeforeMp} -> {((Player)Attacker).NowMp} (+{((Player)Attacker).NowMp - AttBeforeMp})");
                    break;
                default:
                    break;
            }
        }
    }
}
