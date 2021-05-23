
using ProjectReferenceValidator.Lib.Helpers;
using ProjectReferenceValidator.Lib.Tests.TestData;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ProjectReferenceValidator.Lib.Tests.Helpers
{
    public class ProjectFileAnalyzerTests
    {
        [Fact]
        public void ExtractProjectReferencesFromProjectFile_ProjectWithoutReferences_ReturnsEmptyList()
        {
            // Arrange
            var candidate = new ProjectFileAnalyzer();

            using var workingDir = new RandomTempDir(MethodBase.GetCurrentMethod().Name);
            var projectFile = Path.Combine(workingDir.FullName, "MyProject.csproj");
            File.WriteAllText(projectFile, ExampleProjectFiles.NoProjectReferences);

            // Act
            var references = candidate.ExtractProjectReferencesFromProjectFile(projectFile);

            // Assert
            Assert.NotNull(references);
            Assert.Empty(references);
        }

        [Fact]
        public void ExtractProjectReferencesFromProjectFile_ProjectWithSingleReference_ReturnsSingleReference()
        {
            // Arrange
            var candidate = new ProjectFileAnalyzer();

            using var workingDir = new RandomTempDir(MethodBase.GetCurrentMethod().Name);
            var projectFile = Path.Combine(workingDir.FullName, "MyProject.csproj");
            File.WriteAllText(projectFile, ExampleProjectFiles.ProjectReferenceAndOtherItems);

            var expectedProjectReferencePath = "..\\Fs.FileSystemObfuscator.Services\\Fs.FileSystemObfuscator.Services.csproj";

            // Act
            var references = candidate.ExtractProjectReferencesFromProjectFile(projectFile);

            // Assert
            Assert.NotNull(references);
            Assert.Single(references);
            Assert.Equal(expectedProjectReferencePath, references.Single());
        }


        [Fact]
        public void ExtractProjectReferencesFromProjectFile_ProjectWithThreeReferencesAndLotsOfNoise_ReturnsThreeReferences()
        {
            // Arrange
            var candidate = new ProjectFileAnalyzer();

            using var workingDir = new RandomTempDir(MethodBase.GetCurrentMethod().Name);
            var projectFile = Path.Combine(workingDir.FullName, "MyProject.csproj");
            File.WriteAllText(projectFile, ExampleProjectFiles.ThreeProjectReferencesAndOtherItems);

            var expectedProjectReferencePath1 = "..\\Fs.InvoiceA3Export.Services\\Fs.InvoiceA3Export.Services.csproj";
            var expectedProjectReferencePath2 = "..\\Fs.InvoiceA3Export.TestEnvironment\\Fs.InvoiceA3Export.TestEnvironment.csproj";
            var expectedProjectReferencePath3 = "..\\Fs.InvoiceA3Export.VaultApplication\\Fs.InvoiceA3Export.VaultApplication.csproj";

            // Act
            var references = candidate.ExtractProjectReferencesFromProjectFile(projectFile);

            // Assert
            Assert.NotNull(references);
            Assert.Equal(3, references.Count);
            Assert.Equal(expectedProjectReferencePath1, references[0]);
            Assert.Equal(expectedProjectReferencePath2, references[1]);
            Assert.Equal(expectedProjectReferencePath3, references[2]);
        }
    }
}
