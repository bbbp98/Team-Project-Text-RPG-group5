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
                    new Golem(stage + 13),
                    new Golem(stage + 13),
               };
        }
    }

    internal class Stage8Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new ShadowAssassin(stage + 12),
                    new ShadowAssassin(stage + 16),
               };
        }
    }

    internal class Stage9Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Orc(stage + 11),
                    new HobGoblin(stage + 11),
                    new HobGoblin(stage + 11),
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
                    new Golem(stage + 15)
               };
        }
    }

    internal class Stage11Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Goblin(stage + 24),
                    new Goblin(stage + 34),
                    new HobGoblin(stage + 29),
               };
        }
    }

    internal class Stage12Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Orc(stage + 28),
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
                    new Jester(stage + 27),
                    new Jester(stage + 27),
                    new ShadowAssassin(stage + 22),
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
                    new ShadowAssassin(stage + 26),
                    new Jester(stage + 31)
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
