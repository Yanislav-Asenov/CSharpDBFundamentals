namespace OOPIntro
{
    using System;

    public class Startup
    {
        static void Main()
        {
            // CreatePerson(); 02
            //OldestFamilyMember(); 03
            //Students(); 04
            //PlanckConstant(); 05
            //MathUtilities(); 06
        }

        static void MathUtilities()
        {
            var command = Console.ReadLine();

            while (command != "End")
            {
                var commandArgs = command.Split(' ');
                var methodName = commandArgs[0];
                double firstNumber = double.Parse(commandArgs[1]);
                double secondNumber = double.Parse(commandArgs[2]);

                string result = string.Empty;
                switch (commandArgs[0])
                {
                    case "Sum":
                        result = $"{MathUtil.Sum(firstNumber, secondNumber):F2}";
                        break;
                    case "Multiply":
                        result = $"{MathUtil.Multiply(firstNumber, secondNumber):F2}";
                        break;
                    case "Subtract":
                        result = $"{MathUtil.Subtract(firstNumber, secondNumber):F2}";
                        break;
                    case "Divide":
                        result = $"{MathUtil.Divide(firstNumber, secondNumber):F2}";
                        break;
                    case "Percentage":
                        result = $"{MathUtil.Percentage(firstNumber, secondNumber):F2}";
                        break;
                }

                Console.WriteLine(result);

                command = Console.ReadLine();
            }

        }

        static void PlanckConstant()
        {
            Console.WriteLine(Calculation.GetReducedPlanckConstant());
        }

        static void Students()
        {
            string command = Console.ReadLine();
            while (command != "End")
            {
                var student = new Student(command);
                command = Console.ReadLine();
            }

            Console.WriteLine(Student.NumberOfPersonsCreated);
        }

        static void OldestFamilyMember()
        {
            int n = int.Parse(Console.ReadLine());
            var family = new Family();

            for (int i = 0; i < n; i++)
            {
                var personInfo = Console.ReadLine().Split(' ');
                family.AddMember(
                    new Person
                    {
                        Name = personInfo[0],
                        Age = int.Parse(personInfo[1])
                    });
            }

            var oldestMember = family.GetOldestMember();

            Console.WriteLine(oldestMember);
        }

        static void CreatePerson()
        {
            string[] personInfo = Console.ReadLine().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            Person person;
            int age;

            if (personInfo.Length == 0)
            {
                person = new Person();
            }
            else if (personInfo.Length == 1)
            {
                if (int.TryParse(personInfo[0], out age))
                {
                    person = new Person(age);
                }
                else
                {
                    person = new Person(personInfo[0]);
                }
            }
            else
            {
                person = new Person(personInfo[0], int.Parse(personInfo[1]));
            }

            Console.WriteLine(person);
        }
    }
}
