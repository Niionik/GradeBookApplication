using GradeBook.GradeBooks;
using System;

namespace GradeBook.UserInterfaces
{
    public static class StartingUserInterface
    {
        public static bool Quit = false;

        public static void CommandLoop()
        {
            while (!Quit)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine(">> What would you like to do?");
                var command = Console.ReadLine().ToLower();
                CommandRoute(command);
            }
        }

        public static void CommandRoute(string command)
        {
            if (command.StartsWith("create"))
                CreateCommand(command);
            else if (command.StartsWith("load"))
                LoadCommand(command);
            else if (command == "help")
                HelpCommand();
            else if (command == "quit")
                Quit = true;
            else
                Console.WriteLine("{0} was not recognized, please try again.", command);
        }

        public static void CreateCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 4)
            {
                Console.WriteLine("Command not valid, Create requires a name, type of gradebook, if it's weighted (true / false).");
                return;
            }

            var name = parts[1];
            var type = parts[2].ToLower();
            var isWeighted = bool.Parse(parts[3]);

            BaseGradeBook gradeBook;

            if (type == "standard")
            {
                gradeBook = new StandardGradeBook(name, isWeighted);
                Console.WriteLine("Created a standard gradebook named {0}.", name);
            }
            else if (type == "ranked")
            {
                gradeBook = new RankedGradeBook(name, isWeighted);
                Console.WriteLine("Created a ranked gradebook named {0}.", name);
            }
            else
            {
                Console.WriteLine("{0} is not a supported type of gradebook, please try again.", type);
                return;
            }

            GradeBookUserInterface.CommandLoop(gradeBook);
        }

        public static void LoadCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 2)
            {
                Console.WriteLine("Command not valid, Load requires a name.");
                return;
            }
            var name = parts[1];
            var gradeBook = BaseGradeBook.Load(name);

            if (gradeBook == null)
                return;

            GradeBookUserInterface.CommandLoop(gradeBook);
        }

        public static void HelpCommand()
        {
            Console.WriteLine();
            Console.WriteLine("While a gradebook is open you can use the following commands:");
            Console.WriteLine();
            Console.WriteLine("Add 'Name' 'Student Type' 'Enrollment Type' - Adds a new student to the gradebook with the provided name, type of student, and type of enrollment.");
            Console.WriteLine();
            Console.WriteLine("Accepted Student Types:");
            Console.WriteLine("Standard - Student not enrolled in Honors classes or Dual Enrolled.");
            Console.WriteLine("Honors - Students enrolled in Honors classes and not Dual Enrolled.");
            Console.WriteLine("DualEnrolled - Students who are Dual Enrolled.");
            Console.WriteLine();
            Console.WriteLine("Accepted Enrollment Types:");
            Console.WriteLine("Campus - Students who are in the same district as the school.");
            Console.WriteLine("State - Students whose legal residence is outside the school's district, but is in the same state as the school.");
            Console.WriteLine("National - Students whose legal residence is not in the same state as the school, but is in the same country as the school.");
            Console.WriteLine("International - Students whose legal residence is not in the same country as the school.");
            Console.WriteLine();
            Console.WriteLine("List - Lists all students.");
            Console.WriteLine();
            Console.WriteLine("AddGrade 'Name' 'Score' - Adds a new grade to a student with the matching name of the provided score.");
            Console.WriteLine();
            Console.WriteLine("RemoveGrade 'Name' 'Score' - Removes a grade from a student with the matching name and score.");
            Console.WriteLine();
            Console.WriteLine("Remove 'Name' - Removes the student with the provided name.");
            Console.WriteLine();
            Console.WriteLine("Statistics 'Name' - Gets statistics for the specified student.");
            Console.WriteLine();
            Console.WriteLine("Statistics All - Gets general statistics for the entire gradebook.");
            Console.WriteLine();
            Console.WriteLine("Create 'Name' 'Type' 'Weighted' - Creates a new gradebook where 'Name' is the name of the gradebook, 'Type' is what type of grading it should use, and 'Weighted' is whether or not grades should be weighted (true or false).");
            Console.WriteLine();
            Console.WriteLine("Close - Closes the gradebook and takes you back to the starting command options.");
            Console.WriteLine();
            Console.WriteLine("Save - Saves the gradebook to the hard drive for later use.");
        }


    }
}
