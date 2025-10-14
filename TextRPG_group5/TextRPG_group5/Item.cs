using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{

    public class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int Price { get; protected set; }

        public bool IsEquip { get; protected set; }
    }
}
