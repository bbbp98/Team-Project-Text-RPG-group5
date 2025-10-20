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
                    new Slime(stage + 1),
                    new Slime(stage + 1),
                    new Slime(stage + 1),
               };
        }
    }

    internal class Stage2Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Slime(stage + 2),
                    new Slime(stage + 2),
                    new Goblin(stage + 1),
                    new Goblin(stage + 1),
               };
        }
    }

    internal class Stage3Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new HobGoblin(stage + 2),
                    new HobGoblin(stage + 2),
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
                    new Skeleton(stage + 1),
                    new Skeleton(stage + 1),
                    new Skeleton(stage + 2),
               };
        }
    }

    internal class Stage5Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Orc(stage + 3),
               };
        }
    }

    internal class Stage6Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new HobGoblin(stage + 4),
                    new HobGoblin(stage + 4),
                    new HobGoblin(stage + 4),
               };
        }
    }

    internal class Stage7Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Golem(stage + 8),
               };
        }
    }

    internal class Stage8Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new ShadowAssassin(stage + 8),
                    new ShadowAssassin(stage + 8),
               };
        }
    }

    internal class Stage9Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Orc(stage + 8),
                    new Goblin(stage + 6),
                    new Goblin(stage + 7),
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
                    new Goblin(stage + 9),
                    new Goblin(stage + 9),
                    new HobGoblin(stage + 9),
                    new HobGoblin(stage + 14),
               };
        }
    }

    internal class Stage12Factory : IStageFactory
    {
        public List<Monster> Create(Player player, int stage)
        {
            return new List<Monster>
               {
                    new Orc(stage + 13),
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
                    new HobGoblin(stage + 17),
                    new HobGoblin(stage + 17),
                    new HobGoblin(stage + 17),
                    new HobGoblin(stage + 17),
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
