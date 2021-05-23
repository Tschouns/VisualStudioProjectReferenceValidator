
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectReferenceValidator.Lib.Helpers
{
    public class SolutionFileAnalyzer : ISolutionFileAnalyzer
    {
        public IReadOnlyList<FileInfo> ExtractProjectPathsFromSolutionFile(string solutionFile)
        {
            if (solutionFile == null)
            {
                throw new ArgumentNullException();
            }

            // Read the relevant lines from the file.
            var fileLines = File.ReadAllLines(solutionFile);
            var projectRefereneLines = fileLines
                .Where(l => l.Trim().StartsWith("Project("))
                .ToList();

            // Extract the path.
            var projectFiles = projectRefereneLines
                .Select(ExtractProjectPathFromLine)
                .ToList();

            // Generate absolute paths.
            var solutionLocation = Path.GetDirectoryName(solutionFile);
            var absoluteProjectFilePaths = projectFiles
                .Select(f => Path.Combine(solutionLocation, f))
                .ToList();

            // Generate file infos.
            var projectFileInfos = absoluteProjectFilePaths
                .Select(f => new FileInfo(f))
                .ToList();

            return projectFileInfos;
        }

        private static string ExtractProjectPathFromLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                throw new ArgumentNullException(nameof(line));
            }

            var splitsByEquals = line.Split('=');
            if (splitsByEquals.Length != 2)
            {
                throw new ArgumentException($"The line \"{line}\" could not be parsed.");
            }

            var splitsByComma = splitsByEquals.Last().Split(',');
            if (splitsByComma.Length != 3)
            {
                throw new ArgumentException($"The line \"{line}\" could not be parsed.");
            }

            var projectPath = splitsByComma[1].Trim().Trim('\"');
            
            return projectPath;
        }
    }
}
