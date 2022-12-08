using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace code.Day3;

public class Code
{
    [Test]
    public void Step1()
    {
        Console.WriteLine(DoIt());
        Console.WriteLine(Part2());
    }
    
    int DoIt()
    {
        var sr = new StreamReader("Day3/data.txt");

        var dups = new List<char>();

        var priority = GetPriorityList();

        while (true)
        {
            if (sr.EndOfStream)
                break;
                
            var raw = sr.ReadLine();
            var middle = raw.Length / 2;
            var left = raw.Substring(0, middle);
            var right = raw.Substring(middle, middle);

            dups.Add(left.ToCharArray().First(c => right.Any(r => r == c)));
        }

        return dups.Select(d => priority[d]).Sum();
    }

    int Part2()
    {
        var sr = new StreamReader("Day3/data.txt");

        var dups = new List<char>();

        var priority = GetPriorityList();

        var group = new List<string>();
        
        while (true)
        {
            if (sr.EndOfStream)
                break;

            group.Add(sr.ReadLine());
            
            if (group.Count == 3)
            {
                var all = group.Select((g, i) => new {Index = i, Things = g.ToCharArray()}).ToList();

                var found = false;
                foreach (var item in all)
                {
                    var others = all.Where(x => x.Index != item.Index).ToList();

                    foreach (var c in item.Things)
                    {
                        var existInAll = others.All(o => o.Things.Contains(c));
                        if (existInAll)
                        {
                            found = true;
                            dups.Add(c);
                            break;
                        }
                    }

                    if (found)
                        break;
                }
                
                group.Clear();
            }
        }

        var total = dups.Select(d => priority[d]).Sum();
        
        return total;
    }
    
    static Dictionary<char, int> GetPriorityList()
    {
        var priority = new Dictionary<char, int>();
        var j = 0;
        for (var i = 97; i <= 122; i++)
        {
            priority.Add((char)i, ++j);
        }

        for (var i = 65; i <= 90; i++)
        {
            priority.Add((char)i, ++j);
        }

        return priority;
    }
}