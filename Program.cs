using System;
using System.Text;
using System.Collections.Generic;

namespace LabWork
{
    // --- Product ---
    public class Hero
    {
        private readonly Dictionary<string, string> _parts = new Dictionary<string, string>();
        private readonly string _heroType;

        public Hero(string heroType)
        {
            _heroType = heroType;
        }

        public void Add(string key, string value)
        {
            _parts[key] = value;
        }

        public string Display()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"--- {_heroType} ---");
            foreach (var part in _parts)
            {
                sb.AppendLine($"- {part.Key}: {part.Value}");
            }
            return sb.ToString();
        }
    }

    // --- Builder Interface ---
    public interface IHeroBuilder
    {
        void Reset();
        void SetHealth(int health);
        void SetStrength(int strength);
        void SetMagic(int magic);
        void SetAbilities(string abilities);
        void SetAppearance(string appearance);
        Hero GetResult();
    }

    // --- Concrete Builder ---
    public class WarriorBuilder : IHeroBuilder
    {
        private Hero _hero;

        public WarriorBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            _hero = new Hero("Warrior");
        }

        public void SetHealth(int health)
        {
            _hero.Add("Health (HP)", health.ToString());
        }

        public void SetStrength(int strength)
        {
            _hero.Add("Strength (STR)", strength.ToString());
        }

        public void SetMagic(int magic)
        {
            _hero.Add("Magic (MAG)", magic.ToString());
        }

        public void SetAbilities(string abilities)
        {
            _hero.Add("Abilities", abilities);
        }

        public void SetAppearance(string appearance)
        {
            _hero.Add("Appearance", appearance);
        }

        public Hero GetResult()
        {
            Hero result = _hero;
            this.Reset();
            return result;
        }
    }

    // --- Director ---
    public class Director
    {
        private IHeroBuilder _builder;

        public IHeroBuilder Builder
        {
            set { _builder = value; }
        }

        public void BuildTankHero()
        {
            _builder.Reset();
            _builder.SetHealth(500);
            _builder.SetStrength(80);
            _builder.SetMagic(10);
            _builder.SetAbilities("Defense, Aggression");
            _builder.SetAppearance("Heavy Armor");
        }

        public void BuildAttackerHero()
        {
            _builder.Reset();
            _builder.SetHealth(300);
            _builder.SetStrength(150);
            _builder.SetMagic(20);
            _builder.SetAbilities("Critical Strike");
            _builder.SetAppearance("Light Armor");
        }
        
        public void BuildAssassinHero()
        {
            _builder.Reset();
            _builder.SetHealth(100);
            _builder.SetStrength(300);
            _builder.SetMagic(25);
            _builder.SetAbilities("Invisible");
            _builder.SetAppearance("Light Armor");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var director = new Director();
            var warriorBuilder = new WarriorBuilder();
            director.Builder = warriorBuilder;

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("--- Builder Design Pattern Example ---");
            Console.WriteLine();

            // 2. Create Hero using Director (Tank)
            Console.WriteLine("Creating a hero using Director (Tank):");
            director.BuildTankHero();
            var tank = warriorBuilder.GetResult();
            Console.WriteLine(tank.Display());

            // 3. Create another Hero using Director (Attacker)
            Console.WriteLine("Creating another hero using Director (Attacker):");
            director.BuildAttackerHero();
            var attacker = warriorBuilder.GetResult();
            Console.WriteLine(attacker.Display());
            
            // 4. Create another Hero using Director (Attacker)
            Console.WriteLine("Creating another hero using Director (Assassin):");
            director.BuildAssassinHero();
            var assassin = warriorBuilder.GetResult();
            Console.WriteLine(assassin.Display());

            // 5. Create Hero directly via Builder (Custom)
            Console.WriteLine("Creating a hero manually via Builder (Custom):");
            warriorBuilder.Reset();
            warriorBuilder.SetHealth(400);
            warriorBuilder.SetStrength(120);
            warriorBuilder.SetAppearance("Exotic Gear");
            Hero custom = warriorBuilder.GetResult();
            Console.WriteLine(custom.Display());
        }
    }
}