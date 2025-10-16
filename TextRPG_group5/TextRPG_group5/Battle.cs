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

        public Battle(Player player, List<Monster> monsters, byte currentStage)
        {
            Player = player;
            Monsters = monsters;
            CurrentStage = currentStage;

            // Player 선공
            isPlayerTurn = true;

            state = BattleState.None;

            preBattlePlayer = Player.Clone();
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
                // 살아있는 몬스터들만 공격 가능
                List<Monster> aliveMons = Monsters.Where(m => !m.IsDead).ToList();

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

            defenders[0].TakeDamage(attacker.Attack, attacker.Critical);

            ActionResultScene result = new ActionResultScene(this, attacker, defenders, attackerBeforeMp, defendersBeforeHp);
            result.Show();
            Program.SetScene(result);
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
            // Attacker == Player (무조건)
            Character attacker = Player;
            List<Character> defenders = new List<Character>();

            int attackerBeforeMp = Player.NowMp;
            List<int> defendersBeforeHp = new List<int>();

            Console.WriteLine("아이템 사용!");
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
    }
}
