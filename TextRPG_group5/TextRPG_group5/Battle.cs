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

        private BattleState state;
        public byte userChoice;

        public bool isPlayerTurn;

        public Battle(Player player, List<Monster> monsters)
        {
            Player = player;
            Monsters = monsters;

            // Player 선공
            isPlayerTurn = true;

            state = BattleState.None;
    }

        public void SetBattleState(BattleState currentState) => state = currentState;
        public BattleState GetBattleState() => state;

        public void HitNormalAttack()
        {
            // Console.WriteLine("일반 공격 사용!");

            Character attacker;
            Character defender;

            int attackerBeforeHp;
            int defenderBeforeHp;

            if (isPlayerTurn)   // Player 턴
            {
                attacker = Player;
                defender = Monsters[userChoice - 1];

                attackerBeforeHp = Player.NowHp;
                defenderBeforeHp = Monsters[userChoice - 1].NowHp;
            }
            else    // Monster 턴
            {
                // Monster 무리 중 Player 를 공격할 Monster 를 랜덤으로 하나 선택
                attacker = Monsters[new Random().Next(0, Monsters.Count)];
                defender = Player;

                attackerBeforeHp = attacker.NowHp;
                defenderBeforeHp = Player.NowHp;
            }

            defender.TakeDamage(attacker.Attack, attacker.Critical);
            isPlayerTurn = !isPlayerTurn;

            ActionResultScene result = new ActionResultScene(attacker, defender, attackerBeforeHp, defenderBeforeHp);
            result.Show();
        }

        public void UseSkill()
        {
            Console.WriteLine("스킬 사용!");
        }

        public void UseItem()
        {
            Console.WriteLine("아이템 사용!");
        }
     }
}
