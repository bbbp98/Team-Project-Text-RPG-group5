using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Stage
{
    internal class Stage1Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Slime(stage + 4),
                    new Slime(stage + 4),
                    new Slime(stage + 4),
               };
        }
    }

    internal class Stage2Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Slime(stage + 5),
                    new Slime(stage + 4),
                    new Goblin(stage + 3),
                    new Goblin(stage + 4),
               };
        }
    }

    internal class Stage3Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new HobGoblin(stage + 7),
                    new HobGoblin(stage + 4),
                    new Goblin(stage + 4),
               };
        }
    }

    internal class Stage4Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Skeleton(stage + 6),
                    new Skeleton(stage + 6),
                    new Skeleton(stage + 6),
               };
        }
    }

    internal class Stage5Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Orc(stage + 7),
               };
        }
    }

    internal class Stage6Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Goblin(stage + 6),
                    new HobGoblin(stage + 9),
                    new HobGoblin(stage + 9),
                    new HobGoblin(stage + 12),
               };
        }
    }

    internal class Stage7Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Golem(stage + 18),
                    new Golem(stage + 18),
               };
        }
    }

    internal class Stage8Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new ShadowAssassin(stage + 16),
                    new ShadowAssassin(stage + 18),
               };
        }
    }

    internal class Stage9Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Orc(stage + 16),
                    new HobGoblin(stage + 16),
                    new HobGoblin(stage + 16),
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
                    new Golem(stage + 20)
               };
        }
    }

    internal class Stage11Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Goblin(stage + 29),
                    new Goblin(stage + 34),
                    new HobGoblin(stage + 34),
               };
        }
    }

    internal class Stage12Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Orc(stage + 43),
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
                    new Jester(stage + 47),
                    new Jester(stage + 42),
                    new ShadowAssassin(stage + 37),
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
                    new ShadowAssassin(stage + 41),
                    new Jester(stage + 51),
                    new HobGoblin(stage + 41)
               };
        }
    }

    internal class Stage15Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Dragon(stage + 85),
               };
        }
    }
}
