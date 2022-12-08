using System;
using System.Linq;
using NUnit.Framework;

namespace code.Day1;

public class Day1Tests
{
        
    [Test]
    public void Step1()
    {
        var code = new Code();
        var result = code.GetMost();
        Console.WriteLine(result);
    }
        
    [Test]
    public void Step2()
    {
        var code = new Code();
        var result = Code.GetTopThree();
        Console.WriteLine(result.Sum(e => e.TotalCalories));
    }
}