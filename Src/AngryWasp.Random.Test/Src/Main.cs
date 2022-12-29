using System;

namespace AngryWasp.Random.Test
{
    internal class MainClass
    {
        private static void Main(string[] rawArgs)
        {
            {
                var rng = new XoShiRo128PlusPlus();
                Console.WriteLine("Testing XoShiRo128++");
                for (uint i = 0; i < 10; i++)
                {
                    rng.Reseed(i);
                    Console.WriteLine($"Seeding with {i}. Generating 10 numbers");
                    for (uint j = 0; j < 10; j++)
                        Console.WriteLine(rng.Next());
                }
            }

            {
                var rng = new XoShiRo128StarStar();
                Console.WriteLine("Testing XoShiRo128**");
                for (uint i = 0; i < 10; i++)
                {
                    rng.Reseed(i);
                    Console.WriteLine($"Seeding with {i}. Generating 10 numbers");
                    for (uint j = 0; j < 10; j++)
                        Console.WriteLine(rng.Next());
                }
            }

            {
                var rng = new XoShiRo256PlusPlus();
                Console.WriteLine("Testing XoShiRo256++");
                for (uint i = 0; i < 10; i++)
                {
                    rng.Reseed(i);
                    Console.WriteLine($"Seeding with {i}. Generating 10 numbers");
                    for (uint j = 0; j < 10; j++)
                        Console.WriteLine(rng.Next());
                }
            }

            {
                var rng = new XoShiRo256StarStar();
                Console.WriteLine("Testing XoShiRo256**");
                for (uint i = 0; i < 10; i++)
                {
                    rng.Reseed(i);
                    Console.WriteLine($"Seeding with {i}. Generating 10 numbers");
                    for (uint j = 0; j < 10; j++)
                        Console.WriteLine(rng.Next());
                }
            }
        }
    }
}