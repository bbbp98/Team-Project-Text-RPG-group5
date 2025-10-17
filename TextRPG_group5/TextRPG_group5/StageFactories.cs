using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
     internal class Stage1Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new Slime(stage),
                    new Slime(stage),
                    new Slime(stage),
               };
          }
     }

     internal class Stage2Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new Slime(stage),
                    new Slime(stage),
                    new Goblin(stage),
                    new Goblin(stage),
               };
          }
     }

     internal class Stage3Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new HobGoblin(stage),
                    new HobGoblin(stage),
               };
          }
     }

     internal class Stage4Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new Skeleton(stage),
                    new Skeleton(stage),
                    new Skeleton(stage),
                    new Skeleton(stage),
               };
          }
     }

     internal class Stage5Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new Orc(stage),
               };
          }
     }

     internal class Stage6Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new HobGoblin(stage),
                    new HobGoblin(stage),
                    new HobGoblin(stage),
               };
          }
     }

     internal class Stage7Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new Golem(stage),
               };
          }
     }

     internal class Stage8Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new ShadowAssassin(stage),
                    new ShadowAssassin(stage),
               };
          }
     }

     internal class Stage9Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new Orc(stage),
                    new Goblin(stage),
                    new Goblin(stage),
               };
          }
     }

     internal class Stage10Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new Dople(player),
               };
          }
     }

     internal class Stage11Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new Goblin(stage),
                    new Goblin(stage),
                    new HobGoblin(stage),
                    new HobGoblin(stage),
               };
          }
     }

     internal class Stage12Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new Orc(stage),
                    new Dople(player),
                    new Dople(player),
               };
          }
     }

     internal class Stage13Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new HobGoblin(stage),
                    new HobGoblin(stage),
                    new HobGoblin(stage),
                    new HobGoblin(stage),
               };
          }
     }

     internal class Stage14Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new Dople(player),
                    new Dople(player),
                    new Dople(player),
               };
          }
     }

     internal class Stage15Factory : IStageFactory
     {
          public List<Monster> Create(Player player, int stage)
          {
               return new List<Monster>
               {
                    new Dragon(stage),
               };
          }
     }
}
