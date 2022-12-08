using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace code.Day2;

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
        var sr = new StreamReader("Day2/data.txt");

        var part1Total = new List<int>();
        var part2Total = new List<int>();
        
        while (true)
        {
            if (sr.EndOfStream)
                break;
                
            var play = sr.ReadLine()?.Split();

            var computer = Play.Create(play[0]);
            var me = Play.Create(play[1]);
            var expectedResult = Create(play[1]);
            
            part1Total.Add(me.Fight(computer));
            part2Total.Add(computer.InvertFight(expectedResult));
            
        }

        return new (part1Total.Sum(), part2Total.Sum());
    }

    

    static ExpectedResult Create(string raw)
    {
        return raw switch
        {
            "A" or "X" => ExpectedResult.Lose,
            "B" or "Y" => ExpectedResult.Draw,
            _ => ExpectedResult.Win
        };
    }
    public enum ExpectedResult
    {
        Win,
        Draw,
        Lose
    };
    public abstract class Play
    {
        public static Play Create(string draw)
        {
            return draw switch
            {
                "A" or "X" => new Rock(),
                "B" or "Y" => new Paper(),
                _ => new Scissor()
            };
        }
        protected abstract Play CanBeat { get; }
        protected abstract Play CanLose { get; }
        protected abstract int Point { get; }
        public virtual int Fight(Play other)
        {
            if (CanBeat.GetType() == other.GetType())
                return Point + 6;

            if (this.GetType() == other.GetType())
                return Point + 3;

            return Point;
        }

        public virtual int InvertFight(ExpectedResult expectedResult)
        {
            if (expectedResult == ExpectedResult.Draw)
                return Point + 3;

            if (expectedResult == ExpectedResult.Win)
                return CanLose.Point + 6;

            return CanBeat.Point;
        }
    }

    class Rock : Play
    {
        protected override Play CanBeat=> new Scissor();
        protected override Play CanLose => new Paper();
        protected override int Point => 1;

    }
    
    class Paper : Play
    {
        protected override Play CanBeat => new Rock();
        protected override Play CanLose => new Scissor();
        protected override int Point => 2;
    }

    class Scissor : Play
    {
        protected override Play CanBeat => new Paper();
        protected override Play CanLose => new Rock();
        protected override int Point => 3;
    }


    
   
}