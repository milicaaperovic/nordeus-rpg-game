using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPGGame
{
    public class Move
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Type { get; set; } // "Physical" ili "Magic"
        public string Effect { get; set; } // "Damage", "Heal", "Buff"

        public int Duration { get; set; } // za buff/debuff

    }
}
