using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Battle(Player player, List<Monster> monsters, byte currentStage)
        {
            Player = player;
            Monsters = monsters;
            CurrentStage = currentStage;

            // Player 선공
            isPlayerTurn = true;

            state = BattleState.None;
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
                // Monster 무리 중 Player 를 공격할 Monster 를 랜덤으로 하나 선택
                attacker = Monsters[new Random().Next(0, Monsters.Count)];
                defenders.Add(Player);

                // attackerBeforeMp = attacker.NowMp;
                defendersBeforeHp.Add(Player.NowHp);
            }

            defenders[0].TakeDamage(attacker.Attack, attacker.Critical);
            isPlayerTurn = !isPlayerTurn;

            ActionResultScene result = new ActionResultScene(this, attacker, defenders, attackerBeforeMp, defendersBeforeHp);
            result.Show();
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
     }

        public void EndBattle(bool isClear)
        {
            Program.SetScene(new DungeonResultScene(Player, CurrentStage, isClear));
        }
    }
}
