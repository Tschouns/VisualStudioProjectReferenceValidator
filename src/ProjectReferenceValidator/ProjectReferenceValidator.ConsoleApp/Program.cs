using ProjectReferenceValidator.Lib.Helpers;
using ProjectReferenceValidator.Lib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectReferenceValidator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments.
            if (args.Length != 2)
            {
                DisplayError("Invalid number of arguments. The following arguments are expected:" +
                    "\n- Ruleset File" +
                    "\n- Solution File");
                
                return;
            }

            var ruleset = args[0];
            var solution = args[1];

            Console.WriteLine($"Ruleset: {ruleset}");
            Console.WriteLine($"Solution: {solution}");
            Console.WriteLine();

            if (!File.Exists(ruleset))
            {
                DisplayError($"The Ruleset File \"{ruleset}\" was not found.");
                return;
            }

            if (!File.Exists(solution))
            {
                DisplayError($"The Solution File \"{solution}\" was not found.");
                return;
            }

            try
            {
                // Get projects.
                var analyzer = new SolutionFileAnalyzer();
                var projectFiles = analyzer.ExtractProjectPathsFromSolutionFile(solution);
                var projectFilesOrdered = projectFiles.OrderBy(f => f.FullName).ToList();

                Console.WriteLine($"{projectFilesOrdered.Count} projects were found:");
                foreach (var proj in projectFilesOrdered)
                {
                    Console.WriteLine($"- {proj.FullName}");
                }
                Console.WriteLine();

                // Prepare dependency graph.
                var helper = new ProjectReferenceHelper(new ProjectFileAnalyzer());
                var projects = helper.PrepareProjectReferenceGraph(projectFilesOrdered.Select(f => f.FullName));

                Console.WriteLine("Projects an their references: ");
                DisplayProjectGraph(projects, 0);
                Console.WriteLine();

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                DisplayError($"An error has occurred: {ex.Message}");
                return;
            }
        }

        private static void DisplayProjectGraph(IEnumerable<Project> projects, int indent)
        {
            if (indent > 100)
            {
                return;
            }

            var indentString = "";
            for (var i = 0; i < indent; i++) { indentString += " "; }
            foreach (var project in projects)
            {
                Console.WriteLine($"{indentString}- {project.Name}");
                DisplayProjectGraph(project.ProjectReferences, indent + 4);
            }
        }

        private static void DisplayError(string message)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message);
            Console.WriteLine();

            Console.ForegroundColor = originalColor;

            Console.ReadKey();
        }
    }
}

