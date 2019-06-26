using System;
using System.IO;
using System.Threading.Tasks;

namespace NewFeatures
{
    class Program
    {
        public static async Task Main()
        {
            /* C# 7 */

            // Out Variables
            {
                if (int.TryParse("0", out var outResult))
                {
                    Log($"Out Variables", $"{ outResult }");
                }
            }

            // Tuples
            {
                (var TupleFirst, var TupleLast) = ("John", "Doe");
                Log($"Tuples", $"{ TupleFirst }, { TupleLast }");
            }

            // Discards
            {
                (_, _, var DiscardState) = ("224 Vernon St", "Roseville", "CA");
                Log($"Discards", $"{ DiscardState }");
            }

            // Pattern Matching
            {
                if (0 is int number)
                {
                    Log($"Pattern Matching", $"{ number }");
                }
            }

            // Local Functions and Expression Bodies
            {
                Log($"Local Function", $"{LocalFunction()}");
                string LocalFunction() => "Hello from inside Main()";
            }

            /* C# 7.1 */

            // Async Main
            {
                await Task.Run(() => Log($"Async Main", $"Async Work"));
            }

            // Inferred Tuple Names
            {
                var inferredStreet = "224 Vernon St";
                var inferredCity = "Roseville";
                var inferredTuple = (inferredStreet, inferredCity);
                Log($"Inferred Tuple", $"{ inferredTuple.inferredCity }");
            }

            /* C# 7.2 -- Nothing very newsworthy */

            /* C# 7.3 */

            // Tuple Equality
            {
                Log($"Tuple Equality", $"{ ("x", "y") == ("x", "y") }");
            }

            // ----------------------------------------------------------------- //

            /* C# 8.0 */

            // Switch Expressions!
            {
                var pickNumber = 2;

                // Old
                switch (pickNumber)
                {
                    case 1:
                        Log($"Switch", $"1");
                        break;
                    case 2:
                        Log($"Switch", $"2");
                        break;
                    case 3:
                        Log($"Switch", $"3");
                        break;
                    default:
                        Log($"Switch Old", $"Unknown");
                        break;
                }

                // New
                var switchPicked = pickNumber switch
                {
                    1 => "1",
                    2 => "2",
                    3 => "3",
                    _ => "Unknown"
                };
                Log($"Switch New", $"{ switchPicked }");
            }

            // Property Patterns -- OMG, now it's getting interesting...
            {
                var tupleInput = (Address: "224 Vernon Street", City: "Roseville");
                var zipCode = tupleInput switch
                {
                    { City: "Rocklin" } => "95650",
                    { City: "Roseville" } => "95678",
                    { City: "Lincoln" } => "95648",
                    _ => "00000"
                };
                Log($"Property Patterns", $"{zipCode}");
            }

            // When Patterns -- injecting logic into switch!
            {
                var tupleInput = (Address: "224 Vernon Street", City: "Roseville");
                var zipCode = tupleInput switch
                {
                    var (_, City) when City.ToLower() == "rocklin" => "95650",
                    var (_, City) when City.ToLower() == "roseville" => "95678",
                    _ => "00000"
                };
                Log($"When Patterns", $"{ zipCode }");
            }

            // Type Patterns
            {
                Pet pet = new Cat();
                var petType = pet switch
                {
                    Cat c => "Cat",
                    Dog d => "Dog",
                    _ => "Unknown"
                };
                Log($"Type Patterns", $"{ petType }");
            }

            // Using Declaraions
            {
                var fileName = @".\temp.txt";
                {
                    using var usingFile = File.Open(fileName, FileMode.OpenOrCreate);
                    usingFile.Write(new byte[] { 0x2a, 0x2a, 0x2a, 0x2a }, 0, 4);
                }

                // Below cannot go above because it's still open until here

                {
                    Log($"Using Declarations", $"{ File.ReadAllText(fileName) }");
                    File.Delete(fileName);
                }
            }

            // Static Local Functions
            {
                Log($"Local Function", $"{LocalFunction()}");
                static string LocalFunction() => "Hello from inside Main()";
            }

            // Ranges!
            {
                var alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                var test1 = new string(alpha[..3]);
                var test2 = new string(alpha[..^23]);
                var test3 = new string(alpha[13..15]);
                var test4 = new string(alpha[^3..^0]);
                Log("Ranges",$"{ test1 } - { test3 } - { test2 } - { test4 }");
            }

        }

        // Nullable Reference Types
        public string WarningProperty { get; set; }
        public string SafeProperty { get; set; } = "";

        static void Log(string label, string value) => Console.WriteLine($"{label.PadLeft(30)} : {value}");
    }

    class Pet { }
    class Cat : Pet { }
    class Dog : Pet { }

    /* 
     * Compiler Fun 
     * 
     * Ready to Run
     * dotnet publish -r win-x64 -c Release
     * 
     * Docker Linux
     * dotnet publish -r linux-x64 -c Release
     * 
     * Self Contained Apps
     * Tree Shaking
     * 
     */
}
