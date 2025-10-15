using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
    internal class Battle
    {
        public void StartBattle()
        {
            Console.WriteLine("전투 시작!\n");
        }

        public void HitNormalAttack()
        {
            Console.WriteLine("일반 공격 사용!");
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
