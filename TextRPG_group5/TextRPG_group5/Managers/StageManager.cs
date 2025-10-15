using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Managers
{
     internal class StageManager
     {
          public List<Monster> CreateMonsters(int stage)
          {
               List<Monster> monsters = new List<Monster>();

               // 이런 방식으로 생각 중입니다. or switch(더 촘촘하게 하려면)
               if (stage <= 2)
               {
                    // 슬라임1, 고블린1
                    monsters.Add(new Slime(stage));
                    monsters.Add(new Goblin(stage));
               }
               else if (stage <= 4)
               {
                    // 슬라임2, 고블린2
                    monsters.Add(new Slime(stage));
               }
               else
               {

               }

               return monsters;
          }
     }
}
