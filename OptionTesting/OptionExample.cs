using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;

namespace FunctionalPractice.OptionTesting;

public static class OptionExample
{
    public static void RunOptionExample()
    {
        Option<string> _ = Option<string>.None;

        Option<string> John = Option<string>.Some("John");

        Console.WriteLine(Greet(_));
        Console.WriteLine(Greet(John));


    }

    public static string Greet(Option<string> greetee)
        => greetee.Match(
            None: () => "Sorry, Who?",
            Some: (name) => $"Hello, {name}");
}
