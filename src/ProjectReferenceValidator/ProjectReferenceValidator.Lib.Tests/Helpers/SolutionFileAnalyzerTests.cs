using ProjectReferenceValidator.Lib.Helpers;
using ProjectReferenceValidator.Lib.Tests.TestData;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ProjectReferenceValidator.Lib.Tests.Helpers
{
    public class SolutionFileAnalyzerTests
    {
        [Fact]
        public void ExtractProjectReferencesFromSolutionFile_SolutionWithoutProjects_ReturnsEmptyList()
        {
            // Arrange
            var candidate = new SolutionFileAnalyzer();

            using var workingDir = new RandomTempDir(MethodBase.GetCurrentMethod().Name);
            var solutionFile = Path.Combine(workingDir.FullName, "MySolution.sln");
            File.WriteAllText(solutionFile, ExampleSolutionFiles.NoProjects);

            // Act
            var references = candidate.ExtractProjectPathsFromSolutionFile(solutionFile);

            // Assert
            Assert.NotNull(references);
            Assert.Empty(references);
        }

        [Fact]
        public void ExtractProjectReferencesFromSolutionFile_SolutionWitSinlgeProject_ReturnsSingleProjectFilePath()
        {
            // Arrange
            var candidate = new SolutionFileAnalyzer();

            using var workingDir = new RandomTempDir(MethodBase.GetCurrentMethod().Name);
            var solutionFile = Path.Combine(workingDir.FullName, "MySolution.sln");
            File.WriteAllText(solutionFile, ExampleSolutionFiles.SingleProject);

            var expectedProjectPath = Path.Combine(workingDir.FullName, "ConsoleApp2\\ConsoleApp2.csproj");

            // Act
            var projects = candidate.ExtractProjectPathsFromSolutionFile(solutionFile);

            // Assert
            Assert.NotNull(projects);
            Assert.Single(projects);
            Assert.Equal(expectedProjectPath, projects.Single().FullName);
        }
    }
}
