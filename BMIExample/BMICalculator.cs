using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using LanguageExt;

namespace FunctionalPractice.BMIExample;

public static class BMICalculator
{
    public static void RunBMICalculator()
    {
        GetInputFromUser()
            .CalculateBMI()
            .EvaluateUserBMIResult()
            .PrintToUser();

    }

    public static (double Height, double Weight) GetInputFromUser()
    {
        WriteLine("Enter Your Heigt in Meters");
        var Height = Double.Parse(ReadLine());
        WriteLine("Enter Your Weight in Kgs");
        var Weight = Double.Parse(ReadLine());

        return  (Height, Weight);
    }

    public static double CalculateBMI(this (double height, double weight) tuple)
    {
        return  Math.Round(tuple.weight / (tuple.height * tuple.height), 4);
    }

    public static void PrintToUser(this string output)
        => Console.WriteLine(output);

    public static string EvaluateUserBMIResult(this double BMI)
        =>
        BMI switch
        {
            <= 18.5 => "You Are Underweight",
            >= 25 => "You Are Overweight",
            _ => "You Are Healthy!"
        };
}
