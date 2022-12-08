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
        var (part1Total, part2Total) = DoIt();
        
        Console.WriteLine(part1Total);
        Console.WriteLine(part2Total);
    }
    (int Part1Total, int Part2Total) DoIt()
    {
        var sr = new StreamReader("Day3/data.txt");

        var dups = new List<char>();

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

        var total = dups.Select(d => priority[d]).Sum();
        
        return new (total ,0);
    }
}