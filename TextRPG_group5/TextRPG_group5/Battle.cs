using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.ItemManage;
using TextRPG_group5.Scenes;
using TextRPG_group5.QuestManagement;

namespace TextRPG_group5
{
    enum BattleState
    {
        None,
        NormalAttack,
        Skill,
        Item,
        Escape
    }

    internal class Battle
    {
        public Player Player { get; private set; }
        public List<Monster> Monsters { get; private set; }
        public byte CurrentStage { get; private set; }

        public BattleState CurrentState { get; private set; }

        // 일반 공격, 아이템 사용 시, 사용자 입력 저장용 변수
        public byte userChoice;

        // 스킬 사용 시, 사용자 입력 저장용 변수
        public byte userSkillChoice;
        public byte userTargetChoice;

        public bool isPlayerTurn;

        public Player PreBattlePlayer { get; private set; }

        public Battle(Player player, List<Monster> monsters, byte currentStage)
        {
            Player = player;
            Monsters = monsters;
            CurrentStage = currentStage;    // 사용자가 선택한 스테이지

            // Player 선공
            isPlayerTurn = true;

            // 초기 상태 설정
            CurrentState = BattleState.None;

            // 던전 클리어 시, Player 능력치 전후 비교를 위한 복제본 생성
            PreBattlePlayer = Player.Clone();
        }

        public void SetBattleState(BattleState currentState) => CurrentState = currentState;

        public void HitNormalAttack()
        {
            Character attacker;
            List<Character> defenders = new List<Character>();

            int attackerBeforeMp = 0;
            List<int> defendersBeforeHp = new List<int>();

            if (isPlayerTurn)   // Player 턴
            {
                attacker = Player;
                defenders.Add(Monsters[userChoice - 1]);

                attackerBeforeMp = Player.NowMp;
                defendersBeforeHp.Add(defenders[0].NowHp);
            }
            else    // Monster 턴
            {
                /* 살아있고, 기절하지 않은 몬스터들만 공격 가능 */
                List<Monster> aliveMons = Monsters.Where(m => !m.IsDead && !m.IsStun).ToList();

                if (aliveMons.Count == 0)
                {
                    EndBattle(IsStageClear());
                    return;
                }

                // Monster 무리 중 Player 를 공격할 Monster 를 랜덤으로 하나 선택
                attacker = aliveMons[new Random().Next(0, aliveMons.Count)];
                defenders.Add(Player);

                // attackerBeforeMp = attacker.NowMp;
                defendersBeforeHp.Add(Player.NowHp);
            }

            defenders[0].TakeDamage(attacker.GetFinalAttack(), attacker.GetFinalCritical());

            // 공격한 몬스터가 죽으면, 퀘스트 진행 상황 업데이트 및 몬스터 처치 경험치 획득
            if (defenders[0] is Monster && defenders[0].IsDead)
            {
                QuestManager.Instance.UpdateProgress(defenders[0].Name);
                Player.GainExp(((Monster)defenders[0]).Exp);
            }

            ActionResultScene result = new ActionResultScene(this, attacker, defenders, attackerBeforeMp, defendersBeforeHp);
            result.Show();
            Program.SetScene(result);
        }

        /* LJH 로부터 요청받은 로직 : 플레이어와 모든 몬스터의 효과를 업데이트 */
        public bool ProcessStartOfTurnEffects()
        {
            Console.WriteLine("--- 턴 시작 ---");

            Player.UpdateEffect();
            foreach (var monster in Monsters.Where(m => !m.IsDead))
            {
                monster.UpdateEffect();
            }
            Thread.Sleep(1000); // 1초 대기

            // 효과 처리 후 전투 종료 조건 확인
            if (Player.IsDead)
            {
                Console.WriteLine($"\n{Player.Name}이(가) 지속 효과로 쓰러졌습니다...");
                Thread.Sleep(1500);
                EndBattle(false); // 플레이어가 죽으면 무조건 패배
                return false; // 전투 종료
            }

            if (IsAllEnemyDead()) // 플레이어 생존 시 처리
            {
                Console.WriteLine("\n모든 몬스터가 지속 효과로 쓰러졌습니다!");
                Thread.Sleep(1500);
                EndBattle(true); // 몬스터만 모두 죽었으면 승리
                return false; // 전투 종료
            }

            return true; // 전투 계속
        }

        public void UseSkill()
        {
            // Attacker == Player (무조건)
            Character attacker = Player;
            List<Character> defenders = new List<Character>();

            int attackerBeforeMp = Player.NowMp;
            List<int> defendersBeforeHp = new List<int>();

            defenders.Add(Monsters[userTargetChoice - 1]);
            defendersBeforeHp.Add(defenders[0].NowHp);

            //var selectedSkill = Player.Skill.skillBook[userSkillChoice - 1];
            //PlayerSkill skills = Player.Skill;
            //skills.UseSkill(userSkillChoice - 1, defenders[0]);

            // 공격한 몬스터가 죽으면, 퀘스트 진행 상황 업데이트 및 몬스터 처치 경험치 획득
            if (defenders[0].IsDead)
            {
                QuestManager.Instance.UpdateProgress(defenders[0].Name);
                Player.GainExp(((Monster)defenders[0]).Exp);
            }

            ActionResultScene result = new ActionResultScene(this, attacker, defenders, attackerBeforeMp, defendersBeforeHp);
            result.Show();
            Program.SetScene(result);
        }

        public void UseItem()
        {
            // Attacker == ItemUser == Player (무조건)
            Character attacker = Player;
            List<Character> defenders = new List<Character>();

            int attackerBeforeHp = Player.NowHp;
            int attackerBeforeMp = Player.NowMp;

            List<UsableItem> usableItems = Player.Inventory.GetUsableItems();
            
            UsableItem selectedItem = usableItems[userChoice - 1];
            Player.Inventory.UseItem(selectedItem);

            ActionResultScene result = new ActionResultScene(this, attackerBeforeHp, attackerBeforeMp, selectedItem);
            result.Show();
            Program.SetScene(result);
        }

        public bool IsAllEnemyDead()
        {
            foreach (Monster mon in Monsters)
            {
                // 한 마리라도 살아있다면
                if (!mon.IsDead)
                    return false;
            }
            return true;
        }

        public bool IsStageClear()
        {
            if (IsAllEnemyDead())
                return true;
            return false;
        }

        public void EndBattle(bool isClear)
        {
            Program.SetScene(new DungeonResultScene(Player, PreBattlePlayer, CurrentStage, isClear));
        }

        /* LJH 로부터 요청받은 로직 : 상태 이상으로 인한 전투 종료 로직 */
        public void EndBattleAsDefeat()
        {
            // TODO : 나중에 필요 없으면 삭제해도 OK
            Console.WriteLine("\n전투 상황이 종료되었습니다.");
            Thread.Sleep(1500);
            EndBattle(false); // 효과로 인해 전투가 끝나면 무조건 패배(클리어 실패) 처리
        }
    }
}
