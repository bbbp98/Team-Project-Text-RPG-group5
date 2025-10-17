using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
     internal interface IStageFactory
     {
          List<Monster> Create(Player player, int stage);
     }
}
