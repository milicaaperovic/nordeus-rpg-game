using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    public class Character
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public List<Move> Moves { get; set; }
        public int Level { get; set; } = 1;
        public int XP { get; set; } = 0;
        public int XPToNextLevel { get; set; } = 50;
        public int Defense { get; set; }
        public int Magic { get; set; }
        public int AttackBuffTurns = 0;
        public int DefenseBuffTurns = 0;

    }
}
