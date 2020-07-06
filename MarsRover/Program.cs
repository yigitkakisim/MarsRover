using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary>
/// YİĞİT KAKIŞIM | Case Study - Mars Rover
/// </summary>

namespace MarsRover
{
    class Program
    {
        public static Point maxPoint = new Point();

        static void Main(string[] args)
        {
            maxPoint = SetTheFarthestPoint();

            while (true)
            {
                var position = new Position();
                var isWithinTheArea = false;

                do
                {
                    position = SetPosition();
                    isWithinTheArea = position.IsWithinTheArea(maxPoint);
                    if (!isWithinTheArea)
                    {
                        Console.WriteLine("!! The position entered is not within the specified area! Please try again with proper input values...\n");
                        continue;
                    }
                }
                while (!isWithinTheArea);

                var instructions = SetInstructions();
                ExecuteInstructions(position, instructions);
            }
        }

        /// <summary>
        /// Sets the farthest point
        /// </summary>
        /// <returns></returns>
        public static Point SetTheFarthestPoint()
        {
            var point = new Point();
            var isPatternMatched = false;
            var hasArithmeticError = false;
            var farthestPointArr = new string[2];

            do
            {
                Console.WriteLine(">> Enter the farthest point coordinates of the area (x<uint32> y<uint32>):");
                var input = Console.ReadLine();

                var regex = new Regex(@"^\d+ \d+$");
                Match match = regex.Match(input);
                isPatternMatched = match.Success;

                if (!isPatternMatched)
                {
                    Console.WriteLine("!! Incorrect input pattern! Please try again with proper input values...\n");
                    continue;
                }

                farthestPointArr = input.Split(' ');


                try
                {
                    point.X = farthestPointArr[0].ToUInt32();
                    point.Y = farthestPointArr[1].ToUInt32();
                }
                catch (ArithmeticException ae)
                {
                    hasArithmeticError = true;
                    Console.WriteLine($"!! {ae.Message}");
                    continue;
                }
            }
            while (!isPatternMatched || hasArithmeticError);
            return point;
        }

        /// <summary>
        /// Sets the position of the rover
        /// </summary>
        /// <returns></returns>
        public static Position SetPosition()
        {
            var position = new Position();
            var isPatternMatched = false;
            var hasArithmeticError = false;

            var positionArr = new string[3];

            do
            {
                Console.WriteLine("\n>> Enter the position of the rover (x<uint32> y<uint32> direction<N|E|S|W>):");
                var input = Console.ReadLine();

                var regex = new Regex(@"^\d+ \d+ (?:N|E|S|W)$");
                Match match = regex.Match(input.ToUpper());
                isPatternMatched = match.Success;

                if (!isPatternMatched)
                {
                    Console.WriteLine("!! Incorrect input pattern! Please try again with proper input values...\n");
                    continue;
                }

                positionArr = input.Split(' ');

                try
                {
                    position.X = positionArr[0].ToUInt32();
                    position.Y = positionArr[1].ToUInt32();
                }
                catch (ArithmeticException ae)
                {
                    hasArithmeticError = true;
                    Console.WriteLine($"!! {ae.Message}");
                    continue;
                }
                Enum.TryParse(positionArr[2].ToUpper(), out position.Direction);
            }
            while (!isPatternMatched || hasArithmeticError);
            return position;
        }

        /// <summary>
        /// Sets the instructions
        /// </summary>
        /// <returns></returns>
        public static InstructionInput SetInstructions()
        {
            var isPatternMatched = false;
            var instructionSet = new List<char>();

            do
            {
                Console.WriteLine("\n>> Enter instructions (L|R|M)+:");
                var input = Console.ReadLine().ToUpper();

                var regex = new Regex(@"^[L|R|M]+$");
                Match match = regex.Match(input);
                isPatternMatched = match.Success;

                if (!isPatternMatched)
                {
                    Console.WriteLine("!! Incorrect input pattern! Please try again with proper input values...\n");
                    continue;
                }

                input.ToCharArray().ToList().ForEach(x => instructionSet.Add(x));
            }
            while (!isPatternMatched);

            var instructions = new InstructionInput() { InstructionSet = instructionSet };
            return instructions;
        }

        /// <summary>
        /// Executes instruction set for the specified rover
        /// </summary>
        /// <param name="position"></param>
        /// <param name="instructionInput"></param>
        public static void ExecuteInstructions(Position position, InstructionInput instructionInput)
        {
            foreach (var instruction in instructionInput.InstructionSet)
            {
                switch (instruction)
                {
                    case 'M':
                        Move(position);
                        break;
                    default:
                        Rotate(position, instruction);
                        break;
                }
            }

            Console.WriteLine($"\n>> Rover's current position: {position.X} {position.Y} {position.Direction}");
            Console.WriteLine("\n\n----------PRESS ANY KEY FOR ANOTHER INSTRUCTION----------");
            Console.ReadLine();
        }

        /// <summary>
        /// Execute left/right rotation instructions
        /// </summary>
        /// <param name="position"></param>
        /// <param name="instruction"></param>
        private static void Rotate(Position position, char instruction)
        {
            switch (instruction)
            {
                case 'R':
                    position.Direction = (Position.Directions)((((int)position.Direction) + 1) % 4);
                    break;
                case 'L':
                    position.Direction = (Position.Directions)((((int)position.Direction) + 3) % 4);
                    break;
            }
        }

        /// <summary>
        /// Executes movement within area limit
        /// </summary>
        /// <param name="position"></param>
        private static void Move(Position position)
        {
            switch (position.Direction)
            {
                case Position.Directions.N:
                    if (position.Y != maxPoint.Y)
                        position.Y++;
                    break;
                case Position.Directions.E:
                    if (position.X != maxPoint.X)
                        position.X++;
                    break;
                case Position.Directions.S:
                    if (position.Y != 0)
                        position.Y--;
                    break;
                case Position.Directions.W:
                    if (position.X != 0)
                        position.X--;
                    break;
            }
        }
    }
}
