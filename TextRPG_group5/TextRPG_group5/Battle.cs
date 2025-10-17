using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.ItemManage;
using TextRPG_group5.Scenes;

namespace TextRPG_group5
{
    enum BattleState
    {
        None,
        NormalAttack,
        Skill,
        Item,
        ActionResult
    }

    internal class Battle
    {
        public Player Player { get; private set; }
        public List<Monster> Monsters { get; private set; }
        public byte CurrentStage { get; private set; }

        private BattleState state;
        public byte userChoice;

        public bool isPlayerTurn;

        private Player preBattlePlayer;

        public List<UsableItem> UsableItemOnly { get; private set; }

        public Battle(Player player, List<Monster> monsters, byte currentStage)
        {
            Player = player;
            Monsters = monsters;
            CurrentStage = currentStage;

            // Player 선공
            isPlayerTurn = true;

            state = BattleState.None;

            preBattlePlayer = Player.Clone();

            UsableItemOnly = GetUsableItemList(Player.Inventory);
        }

        public void SetBattleState(BattleState currentState) => state = currentState;
        public BattleState GetBattleState() => state;

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
                /* LJH 로부터 요청받은 로직 : 살아있고, 기절하지 않은 몬스터들만 공격 가능 */
                /* TODO : IsStun 구현 후, 주석 해제 */
                List<Monster> aliveMons = Monsters.Where(m => !m.IsDead /*&& !m.IsStun*/).ToList();

                if (aliveMons.Count == 0)
                {
                    /* TODO : 모든 몬스터를 처치하였는지, 행동불능 상태 몬스터가 남아있는지 확인하는 로직 추가 */
                    EndBattle(IsStageClear());
                    return;
                }

                // Monster 무리 중 Player 를 공격할 Monster 를 랜덤으로 하나 선택
                attacker = aliveMons[new Random().Next(0, aliveMons.Count)];
                defenders.Add(Player);

                // attackerBeforeMp = attacker.NowMp;
                defendersBeforeHp.Add(Player.NowHp);
            }

            /* LJH 로부터 요청받은 로직 */
            /* TODO : Character 클래스에 GetFinal~() 구현 후, 주석 해제 */
            /* defenders[0].TakeDamage(attacker.GetFinalAttack(), attacker.GetFinalCritical());*/

            defenders[0].TakeDamage(attacker.Attack, attacker.Critical);

            ActionResultScene result = new ActionResultScene(this, attacker, defenders, attackerBeforeMp, defendersBeforeHp);
            result.Show();
            Program.SetScene(result);
        }

        /* LJH 로부터 요청받은 로직 : 플레이어와 모든 몬스터의 효과를 업데이트 */
        public bool ProcessStartOfTurnEffects()
        {
            Console.WriteLine("--- 턴 시작 ---");

            /* TODO : Character 클래스 업데이트 후 주석 해제
            Player.UpdateEffect();
            foreach (var monster in Monsters.Where(m => !m.IsDead))
            {
                monster.UpdateEffect();
            }*/
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

            foreach (Monster mon in Monsters)
            {
                defenders.Add(mon);
                defendersBeforeHp.Add(mon.NowHp);
            }

            Console.WriteLine("스킬 사용!");
        }

        public void UseItem()
        {
            // Attacker == ItemUser == Player (무조건)
            Character attacker = Player;
            List<Character> defenders = new List<Character>();

            // TODO : 일단 HP, MP 회복 두 개만, 나중에 상태이상 회복도 추가
            int attackerBeforeHp = Player.NowHp;
            int attackerBeforeMp = Player.NowMp;

            List<UsableItem> usableItems = GetUsableItemList(Player.Inventory);
            
            /* Inventory 클래스에 구현 예정 (범근님 작업)
            UsableItem selectedItem = usableItems[userChoice - 1];
            Player.Inventory.UseItem(selectedItem);*/

            UsableItemOnly[userChoice - 1].UseItem(Player);    // UsableItem 클래스 내 함수 (민근님 작업)

            ActionResultScene result = new ActionResultScene(this, attackerBeforeHp, attackerBeforeMp);
            result.Show();
            Program.SetScene(result);
        }

        public List<UsableItem> GetUsableItemList(Inventory playerInventory)
        {
            List<UsableItem> usableItems = new List<UsableItem>();

            for (int i = 0; i < playerInventory.GetCount(); i++)
            {
                if (playerInventory.GetItem(i) is UsableItem)
                    usableItems.Add((UsableItem)playerInventory.GetItem(i));
            }

            return usableItems;
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
            //Program.SetScene(new DungeonResultScene(Player, CurrentStage, isClear));
            Program.SetScene(new DungeonResultScene(Player, preBattlePlayer, CurrentStage, isClear));
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
