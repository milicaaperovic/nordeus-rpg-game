using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPGGame
{
     class Program
    {
        

        static void Main(string[] args)
        {

            // In a real full-stack implementation:
            // Monsters and moves would be fetched from a backend API (GET /config)
            // Monster actions would be calculated server-side (GET /next-move)
            // This allows game designers to tweak balance without changing the client

            Console.WriteLine("==== RPG GAME ====");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Exit");

            string input = Console.ReadLine();

            if (input != "1")
            {
                return;
            }

            Character hero = new Character
            {
                Name = "Hero",
                HP = 100,
                Attack = 20,
                Defense = 10,
                Magic = 10
            };

            var goblin = new Character
            {
                Name = "Goblin",
                HP = 80,
                Moves = new List<Move>
                {
                    new Move { Name = "Bite", Value = 15, Effect = "Damage" }
                }
            };

            var witch = new Character
            {
                Name = "Witch",
                HP = 90,
                Moves = new List<Move>
                {
                    new Move { Name = "Shadow Bolt", Value = 30, Type = "Magic", Effect = "Damage" },
                    new Move { Name = "Drain Life", Value = 15, Type = "Magic", Effect = "Drain" },
                    new Move { Name = "Curse", Value = 5, Type = "Magic", Effect = "DebuffAttack", Duration = 2 },
                    new Move { Name = "Dark Pact", Value = 10, Type = "Magic", Effect = "BuffMagic", Duration = 2 }
                }
            };

            var spider = new Character
            {
                Name = "Giant Spider",
                HP = 100,
                Moves = new List<Move>
                {
                    new Move { Name = "Bite", Value = 20, Type = "Physical", Effect = "Damage" },
                    new Move { Name = "Web Throw", Value = 10, Type = "Physical", Effect = "DebuffAttack", Duration = 2 },
                    new Move { Name = "Pounce", Value = 30, Type = "Physical", Effect = "Damage" },
                    new Move { Name = "Skitter", Value = 10, Type = "Physical", Effect = "BuffDefense", Duration = 2 }
                }
            };

            var dragon = new Character
            {
                Name = "Dragon",
                HP = 150,
                Moves = new List<Move>
                {
                    new Move { Name = "Flame Breath", Value = 30, Type = "Magic", Effect = "Damage" },
                    new Move { Name = "Claw Swipe", Value = 20, Type = "Physical", Effect = "Damage" },
                    new Move { Name = "Intimidate", Value = 5, Type = "Physical", Effect = "DebuffAttack", Duration = 2 },
                    new Move { Name = "Dragon Scales", Value = 10, Type = "Physical", Effect = "BuffDefense", Duration = 2 }
                }
            };

            var goblinWarrior = new Character
            {
                Name = "Goblin Warrior",
                HP = 110,
                Moves = new List<Move>
                {
                    new Move { Name = "Rusty Blade", Value = 20, Effect = "Damage" },
                    new Move { Name = "Dirty Kick", Value = 10, Effect = "DebuffDefense", Duration = 2 },
                    new Move { Name = "Frenzy", Value = 10, Effect = "BuffAttack", Duration = 2 },
                    new Move { Name = "Headbutt", Value = 30, Effect = "Damage" }
                }
            };

            var goblinMage = new Character
            {
                Name = "Goblin Mage",
                HP = 90,
                Moves = new List<Move>
                {
                    new Move { Name = "Firebolt", Value = 20, Effect = "Damage" },
                    new Move { Name = "Arcane Surge", Value = 10, Effect = "BuffMagic", Duration = 2 },
                    new Move { Name = "Mana Drain", Value = 10, Effect = "DebuffMagic", Duration = 2 },
                    new Move { Name = "Hex Shield", Value = 10, Effect = "BuffDefense", Duration = 2 }
                }
            };


            List<Move> heroMoves = new List<Move>
            {
                new Move { Name = "Slash", Value = 20, Type = "Physical", Effect = "Damage" },
                new Move { Name = "Second wind", Value = 15, Type = "Magic", Effect = "Heal" }, // heal = negativan damage
                new Move { Name = "Shield Up", Value = 10, Effect = "BuffDefense", Duration = 2 },
                new Move { Name = "Battle Cry", Value = 10, Effect = "BuffAttack", Duration = 2 }
            };
            hero.Moves = heroMoves;

            

            Random rand = new Random();

            var monsters = new List<Character>
            {
                goblin,
                witch,
                spider,
                dragon,
                goblinWarrior,
                goblinMage
            };

            Console.WriteLine("\nUpcoming battles:");

            foreach (var m in monsters)
            {
                Console.WriteLine($"- {m.Name}");
            }

            Console.WriteLine("\nPress any key to start...");
            Console.ReadKey();

            foreach (var monster in monsters)
            {
                Console.WriteLine("=================================");
                Console.WriteLine($"A wild {monster.Name} appeared!");
                Console.WriteLine("=================================");

                int scale = hero.Level;

                monster.HP += scale * 10;
                monster.Attack += scale * 2;

                Console.WriteLine($"{monster.Name} scaled to your level! (LEVEL {hero.Level})");
                Console.WriteLine($"{monster.Name} HP: {monster.HP} | Attack: {monster.Attack}");

                if (monster.Name == "Dragon")
                {
                    monster.HP += 50;
                    monster.Attack += 10;

                    Console.WriteLine("!!! BOSS FIGHT !!! The Dragon is enraged!");
                }

                while (hero.HP > 0 && monster.HP > 0)
                {
                    
                    Console.Clear();
                    Console.WriteLine("\n==============================");

                    Console.WriteLine($"\nHero LVL: {hero.Level} | HP: {hero.HP} | ATK: {hero.Attack} | DEF: {hero.Defense} | MAG: {hero.Magic}");
                    Console.WriteLine($"Enemy: {monster.Name} | HP: {monster.HP}");
                    string status = "Normal";

                    if (hero.AttackBuffTurns > 0)
                        status = "Attack Buff";
                    else if (hero.DefenseBuffTurns > 0)
                        status = "Defense Buff";

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Status: {status}");
                    Console.ResetColor();

                    Console.WriteLine("Choose move:");
                    for (int i = 0; i < heroMoves.Count; i++)
                    {
                        var move = heroMoves[i];

                        string desc;

                        if (move.Effect == "Damage")
                        {
                            if (move.Type == "Physical")
                                desc = "Deals physical damage based on your Attack, reduced by enemy Defense";
                            else
                                desc = "Deals magic damage based on your Magic stat";
                        }
                        else if (move.Effect == "Heal")
                        {
                            desc = "Restores your health based on your Magic stat";
                        }
                        else if (move.Effect == "BuffAttack")
                        {
                            desc = "Increases your Attack for 2 turns";
                        }
                        else if (move.Effect == "BuffDefense")
                        {
                            desc = "Increases your Defense for 2 turns";
                        }
                        else
                        {
                            desc = move.Effect;
                        }

                        Console.WriteLine($"{i + 1}. {move.Name} - {desc}");
                    }


                    int choice;
                    while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > heroMoves.Count)
                    {
                        Console.WriteLine("Invalid input, try again:");
                    }
                    choice -= 1;

                    Move selectedMove = heroMoves[choice];

                    if (selectedMove.Effect == "Damage")
                    {
                        int baseDamage;

                        if (selectedMove.Type == "Physical")
                        {
                            baseDamage = (hero.Attack + selectedMove.Value) - monster.Defense;
                        }
                        else // magic
                        {
                            baseDamage = hero.Magic + selectedMove.Value;
                        }

                        // random varijacija
                        int damage = baseDamage + rand.Next(-5, 6);

                        // da ne bude negativan
                        damage = Math.Max(damage, 0);

                        // critical
                        bool isCritical = rand.Next(100) < 20;
                        if (isCritical)
                        {
                            damage *= 2;
                        }

                        monster.HP = Math.Max(monster.HP - damage, 0);

                        if (isCritical)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("CRITICAL HIT!");
                            Console.ResetColor();

                            Console.WriteLine($"You strike the {monster.Name} with {selectedMove.Name} and deal {damage} damage!");
                        }
                        else
                        {
                            string log = $"You used {selectedMove.Name} and dealt {damage} damage to {monster.Name}.";
                            
                            Console.WriteLine(log);
                        }

                    }
                    else if (selectedMove.Effect == "Heal")
                    {
                        int baseHeal = hero.Magic + selectedMove.Value;
                        int heal = baseHeal + rand.Next(0, 6);
                        hero.HP += heal;

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"You heal yourself for {heal} HP.");
                        Console.ResetColor();
                    }
                     else if (selectedMove.Effect == "BuffAttack")
                    {
                        hero.Attack += selectedMove.Value;
                        hero.AttackBuffTurns = selectedMove.Duration;
                        Console.WriteLine("Your attack increased!");
                    }
                    else if (selectedMove.Effect == "BuffDefense")
                    {
                        hero.Defense += selectedMove.Value;
                        hero.DefenseBuffTurns = selectedMove.Duration;
                        Console.WriteLine("Your defense increased!");
                    }

                    

                    hero.HP = Math.Max(hero.HP, 0);
                    monster.HP = Math.Max(monster.HP, 0);

                    if (monster.HP > 0)
                    {
                        Move monsterMove;

                        if (hero.HP < 30)
                        {
                            // monster pokusava da zavrsi fight
                            monsterMove = monster.Moves.OrderByDescending(m => m.Value).First();
                        }
                        else if (monster.HP < 30)
                        {
                            // ako je slab, pokusava heal ili buff
                            monsterMove = monster.Moves.FirstOrDefault(m => m.Effect == "Heal" || m.Effect.Contains("Buff"));

                            if (monsterMove == null)
                                monsterMove = monster.Moves[rand.Next(monster.Moves.Count)];
                        }
                        else
                        {
                            // inace random attack
                            monsterMove = monster.Moves[rand.Next(monster.Moves.Count)];
                        }

                        Console.Write($"{monster.Name} is preparing an attack");
                        Thread.Sleep(300);
                        Console.Write(".");
                        Thread.Sleep(300);
                        Console.Write(".");
                        Thread.Sleep(300);
                        Console.WriteLine(".");


                        // potezi
                        if (monsterMove.Effect == "Damage")
                        {
                            int damage = monsterMove.Value + rand.Next(-5, 6);

                            bool isCritical = rand.Next(100) < 15; // monster malo slabiji crit
                            if (isCritical)
                            {
                                damage *= 2;
                            }

                            hero.HP = Math.Max(hero.HP - damage, 0);

                            if (isCritical)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n!!! CRITICAL HIT !!!");
                                Console.ResetColor();

                                Console.WriteLine($"{monster.Name} uses {monsterMove.Name} and deals {damage} damage to you!");
                            }
                            else
                            {
                                string log = $"{monster.Name} used {monsterMove.Name} and dealt {damage} damage.";
                                
                                Console.WriteLine(log);
                            }
                            
                        }
                        else if(monsterMove.Effect == "Drain")
                        {
                            hero.HP -= monsterMove.Value;
                            monster.HP += monsterMove.Value;
                            Console.WriteLine($"{monster.Name} used {monsterMove.Name} and healed!");
                        }
                        else if (monsterMove.Effect.Contains("Buff"))
                        {
                            Console.WriteLine($"{monster.Name} used {monsterMove.Name} (buff)");
                        }
                        
                        hero.HP = Math.Max(hero.HP, 0);
                        WaitForNext();
                    }

                    // Attack buff countdown
                    if (hero.AttackBuffTurns > 0)
                    {
                        hero.AttackBuffTurns--;

                        if (hero.AttackBuffTurns == 0)
                        {
                            hero.Attack -= 10;
                            Console.WriteLine("Attack buff has worn off.");
                        }
                    }

                    // Defense buff countdown
                    if (hero.DefenseBuffTurns > 0)
                    {
                        hero.DefenseBuffTurns--;

                        if (hero.DefenseBuffTurns == 0)
                        {
                            hero.Defense -= 10;
                            Console.WriteLine("Defense buff has worn off.");
                        }
                    }

                    

                }

                if(hero.HP <= 0)
                {
                    Console.WriteLine($"{monster.Name} defeated you!");
                    break;
                }

                Console.WriteLine($"\nYou defeated the {monster.Name}!");



                GainXP(hero, 30);

                Move learnedMove = monster.Moves[rand.Next(monster.Moves.Count)];
                if (!heroMoves.Any(m => m.Name == learnedMove.Name))
                {
                    if (heroMoves.Count < 4)
                    {
                        heroMoves.Add(learnedMove);
                        Console.WriteLine($"You learned {learnedMove.Name}!");
                    }
                    else
                    {
                        Console.WriteLine($"You learned {learnedMove.Name}, but your move list is full!");

                        Console.WriteLine("Choose a move to replace:");
                        for (int i = 0; i < heroMoves.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {heroMoves[i].Name}");
                        }

                        int replaceChoice;
                        while (!int.TryParse(Console.ReadLine(), out replaceChoice) || replaceChoice < 1 || replaceChoice > heroMoves.Count)
                        {
                            Console.WriteLine("Invalid choice, try again:");
                        }

                        replaceChoice -= 1;

                        Console.WriteLine($"{heroMoves[replaceChoice].Name} was replaced with {learnedMove.Name}!");

                        heroMoves[replaceChoice] = learnedMove;
                    }
                }
                else
                {
                    Console.WriteLine($"You already know {learnedMove.Name}!");
                }



            }
     
           
            
            Console.WriteLine(hero.HP > 0 ? "You win!" : "You lose!");
            Console.WriteLine("Game over. Press any key to exit...");
            Console.ReadKey();
        
        }

        static void GainXP(Character hero, int amount)
        {
            hero.XP += amount;
            Console.WriteLine($"You gained {amount} XP!");

            while (hero.XP >= hero.XPToNextLevel)
            {
                hero.XP -= hero.XPToNextLevel;
                hero.Level++;
                hero.XPToNextLevel += 20;

                hero.HP += 20;
                hero.Attack += 5;

                Console.WriteLine($"LEVEL UP! You are now level {hero.Level}!");
                Console.WriteLine("Stats increased!");
            }
        }

        static void WaitForNext()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    }
}
