using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace code.Day1
{
    public class code
    {
        public code()
        {
            
            
        }

        public List<Elf> GetTopThree()
        {
            return DoIt().OrderByDescending(e => e.TotalCalories).Take(3).ToList();
        }

        public long GetMost()
        {
            return DoIt().Max(e => e.TotalCalories);
        }

        static IEnumerable<Elf> DoIt()
        {
            var sr = new StreamReader("Day1/data.txt");
            
            var index = 0;
            
            var elf = new Elf
            {
                Index = ++index
            };

            var elfs = new List<Elf>();
            
            while (true)
            {
                if (sr.EndOfStream)
                    break;
                
                var item = sr.ReadLine();

                if (string.IsNullOrWhiteSpace(item))
                {
                    elfs.Add(elf);
                    
                    elf = new Elf{ Index = ++index};
                }
                
                if (long.TryParse(item, out var num))
                {
                    elf.Calories.Add(num);
                }
            }

            return elfs;
        }

        public class Elf
        {
            public Elf()
            {
                
            }
            
            public int Index { get; set; }
            public List<long> Calories { get; set; } = new List<long>();

            public long TotalCalories => Calories.Sum();
        }
    }
}