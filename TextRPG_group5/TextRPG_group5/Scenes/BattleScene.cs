using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.ItemManage;

namespace TextRPG_group5.Scenes
{
    internal class BattleScene : Scene
    {
        public Battle CurrentBattle { get; private set; }

        public Player Player { get { return CurrentBattle.Player; } }
        public List<Monster> Monsters { get { return CurrentBattle.Monsters; } }

        /* LJH 로부터 요청받은 프로퍼티 : 효과가 처리되었는지 여부 확인용 */
        public bool effectsProcessed = false;

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
                    if (input > Player.Skill.skillBook.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("잘못된 입력입니다.\n");
                        Console.ResetColor();
                        return;
                    }
                }
                else if (CurrentBattle.GetBattleState() == BattleState.Item)
                {
                    if (input > CurrentBattle.UsableItemOnly.Count)
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
            /* LJH 로부터 요청받은 로직 : 턴 시작 시 단 한 번만 모든 캐릭터(플레이어/몬스터)에게 걸려있는 효과를 처리 */
            /* TODO : Battle.ProcessStart~() 내에서 바로 결과창 로드되기 때문에 정상 작동 확인 필요 */
            if (!effectsProcessed)
            {
                bool battleContinues = CurrentBattle.ProcessStartOfTurnEffects();
                effectsProcessed = true; // 효과 처리 끝
                if (!battleContinues)
                {
                    return; // 효과 처리로 전투가 종료되었으면 여기서 중단
                }
            }

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

            /* LJH 로부터 요청받은 로직 */
            if (Player.IsStun)
            {
                Console.WriteLine($"{Player.Name}은(는) 움직일 수 없습니다!");
                Console.WriteLine("\n아무 키나 눌러 턴을 넘깁니다...");
                Console.ReadKey(true);
                CurrentBattle.isPlayerTurn = false;
                Program.SetScene(new BattleScene(CurrentBattle)); // 새 씬을 만들어 효과 처리 플래그 초기화
                return;
            }

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
            List<SkillData> skills = Player.Skill.skillBook;

            for (int i = 0; i < skills.Count; i++)
            {
                string skillName = skills[i].Name;
                Console.WriteLine($"[{i + 1}] {skillName}");
            }

            Console.WriteLine();

            Console.WriteLine("0. 취소");
        }

        void PrintUsableItemList()
        {
            List<UsableItem> usableItems = CurrentBattle.UsableItemOnly;

            for (int i = 0; i < usableItems.Count; i++)
            {
                // TODO : 일단 포션만, 나중에 버프/디버프 소비 아이템도 추가
                Potion potion = (Potion)usableItems[i];
                PotionType type = potion.Type;
                string typeStr = (type == PotionType.HealthPotion) ? "HP" : "MP";

                // 소비 아이템만 출력
                Console.WriteLine($"[{i + 1}] {potion.Name} ({typeStr} +{potion.Amount})");
            }
            Console.WriteLine();

            Console.WriteLine("0. 취소");
        }
    }
}
