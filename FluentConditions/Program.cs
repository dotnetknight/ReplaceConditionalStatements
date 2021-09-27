using System;

namespace FluentConditions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your age");
            int age = Convert.ToInt32(Console.ReadLine().ToString());

            var result = FluentCondition
                .When(IsUnderTheAgeLimit(age), Age.Child)
                .Otherwise(Age.Adult)
                .GetResult();

            Console.WriteLine(result.ToString());
            Console.ReadLine();
        }

        public static bool IsUnderTheAgeLimit(int age)
        {
            return age < 18;
        }
    }

    public enum Age
    {
        Adult,
        Child,
    }
}
