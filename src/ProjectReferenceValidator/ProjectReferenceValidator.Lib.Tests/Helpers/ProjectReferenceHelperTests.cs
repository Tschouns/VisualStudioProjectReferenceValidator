using Moq;
using ProjectReferenceValidator.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ProjectReferenceValidator.Lib.Tests
{
    public class ProjectReferenceHelperTests
    {
        [Fact]
        public void PrepareProjectReferenceGraph_EmptyList_ReturnsEmptyGraph()
        {
            // Arrange
            var mockProjectFileAnalyzer = new Mock<IProjectFileAnalyzer>(MockBehavior.Strict);
            var candidate = new ProjectReferenceHelper(mockProjectFileAnalyzer.Object);

            // Act
            var projects = candidate.PrepareProjectReferenceGraph(projectFiles: new string[0]);

            // Assert
            Assert.NotNull(projects);
            Assert.Empty(projects);
        }

        [Fact]
        public void PrepareProjectReferenceGraph_ProjectsWithoutReferences_ReturnsProjectsWithCorrectName()
        {
            // Arrange
            var projectFiles = new[]
            {
                "MyProd.Gui\\MyProd.Gui.csproj",
                "MyProd.Services\\MyProd.Services.csproj",
            };

            var mockProjectFileAnalyzer = new Mock<IProjectFileAnalyzer>(MockBehavior.Strict);
            mockProjectFileAnalyzer
                .Setup(m => m.ExtractProjectReferencesFromProjectFile(It.IsAny<string>()))
                .Returns(new List<string>());

            var candidate = new ProjectReferenceHelper(mockProjectFileAnalyzer.Object);

            var expectedProjectName1 = Path.GetFileNameWithoutExtension(projectFiles[0]);
            var expectedProjectName2 = Path.GetFileNameWithoutExtension(projectFiles[1]);

            // Act
            var projects = candidate.PrepareProjectReferenceGraph(projectFiles);

            // Assert
            Assert.NotNull(projects);
            Assert.Equal(projectFiles.Count(), projects.Count);

            Assert.Equal(expectedProjectName1, projects[0].Name);
            Assert.Equal(projectFiles[0], projects[0].File);
            Assert.Empty(projects[0].ProjectReferences);

            Assert.Equal(expectedProjectName2, projects[1].Name);
            Assert.Equal(projectFiles[1], projects[1].File);
            Assert.Empty(projects[1].ProjectReferences);
        }

        [Fact]
        public void PrepareProjectReferenceGraph_ProjectsReferenceEachOther_ReturnsProjectsWithCorrectReferences()
        {
            // Arrange
            var projectFiles = new[]
            {
                "MyProd.Gui\\MyProd.Gui.csproj",
                "MyProd.Services\\MyProd.Services.csproj",
                "MyProd.Access\\MyProd.Access.csproj",
            };

            var mockProjectFileAnalyzer = new Mock<IProjectFileAnalyzer>(MockBehavior.Strict);
            mockProjectFileAnalyzer
                .Setup(m => m.ExtractProjectReferencesFromProjectFile(It.Is<string>(f => f == projectFiles[0])))
                .Returns(new List<string> { "..\\MyProd.Services\\MyProd.Services.csproj" });
            mockProjectFileAnalyzer
                .Setup(m => m.ExtractProjectReferencesFromProjectFile(It.Is<string>(f => f == projectFiles[1])))
                .Returns(new List<string> { "..\\MyProd.Access\\MyProd.Access.csproj" });
            mockProjectFileAnalyzer
                .Setup(m => m.ExtractProjectReferencesFromProjectFile(It.Is<string>(f => f == projectFiles[2])))
                .Returns(new List<string>());

            var candidate = new ProjectReferenceHelper(mockProjectFileAnalyzer.Object);

            // Act
            var projects = candidate.PrepareProjectReferenceGraph(projectFiles);

            // Assert
            Assert.NotNull(projects);
            Assert.Equal(projectFiles.Count(), projects.Count);

            Assert.Equal(projectFiles[0], projects[0].File);
            Assert.Single(projects[0].ProjectReferences);
            Assert.Equal(projects[1], projects[0].ProjectReferences.Single());

            Assert.Equal(projectFiles[1], projects[1].File);
            Assert.Single(projects[1].ProjectReferences);
            Assert.Equal(projects[2], projects[1].ProjectReferences.Single());

            Assert.Equal(projectFiles[2], projects[2].File);
            Assert.Empty(projects[2].ProjectReferences);
        }

        [Fact]
        public void PrepareProjectReferenceGraph_ProjectsReferenceMultipleOthers_ReturnsProjectsWithCorrectReferences()
        {
            // Arrange
            var projectFiles = new[]
            {
                "MyProd.Gui\\MyProd.Gui.csproj",
                "MyProd.Services\\MyProd.Services.csproj",
                "MyProd.Services.Tests\\MyProd.Services.Tests.csproj",
                "MyProd.Access\\MyProd.Access.csproj",
                "MyProd.Access.Tests\\MyProd.Access.Tests.csproj",
                "MyProd.Base\\MyProd.Base.csproj",
                "MyProd.Base\\MyProd.Base.Tests.csproj",
            };

            var mockProjectFileAnalyzer = new Mock<IProjectFileAnalyzer>(MockBehavior.Strict);
            mockProjectFileAnalyzer
                .Setup(m => m.ExtractProjectReferencesFromProjectFile(It.Is<string>(f => f == projectFiles[0])))
                .Returns(new List<string>
                    {
                        "..\\MyProd.Base\\MyProd.Base.csproj",
                        "..\\MyProd.Services\\MyProd.Services.csproj",
                    });
            mockProjectFileAnalyzer
                .Setup(m => m.ExtractProjectReferencesFromProjectFile(It.Is<string>(f => f == projectFiles[1])))
                .Returns(new List<string>
                    {
                        "..\\MyProd.Access\\MyProd.Access.csproj",
                        "..\\MyProd.Base\\MyProd.Base.csproj",
                    });
            mockProjectFileAnalyzer
                .Setup(m => m.ExtractProjectReferencesFromProjectFile(It.Is<string>(f => f == projectFiles[2])))
                .Returns(new List<string>
                    {
                        "..\\MyProd.Access\\MyProd.Access.csproj",
                        "..\\MyProd.Base\\MyProd.Base.csproj",
                        "..\\MyProd.Services\\MyProd.Services.csproj",
                    });
            mockProjectFileAnalyzer
                .Setup(m => m.ExtractProjectReferencesFromProjectFile(It.Is<string>(f => f == projectFiles[3])))
                .Returns(new List<string>
                    {
                        "..\\MyProd.Base\\MyProd.Base.csproj",
                    });
            mockProjectFileAnalyzer
                .Setup(m => m.ExtractProjectReferencesFromProjectFile(It.Is<string>(f => f == projectFiles[4])))
                .Returns(new List<string>
                    {
                        "..\\MyProd.Access\\MyProd.Access.csproj",
                        "..\\MyProd.Base\\MyProd.Base.csproj",
                    });
            mockProjectFileAnalyzer
                .Setup(m => m.ExtractProjectReferencesFromProjectFile(It.Is<string>(f => f == projectFiles[5])))
                .Returns(new List<string>
                    {
                    });
            mockProjectFileAnalyzer
                .Setup(m => m.ExtractProjectReferencesFromProjectFile(It.Is<string>(f => f == projectFiles[6])))
                .Returns(new List<string>
                    {
                        "..\\MyProd.Base\\MyProd.Base.csproj",
                    });

            var candidate = new ProjectReferenceHelper(mockProjectFileAnalyzer.Object);

            // Act
            var projects = candidate.PrepareProjectReferenceGraph(projectFiles);

            // Assert
            Assert.NotNull(projects);
            Assert.Equal(projectFiles.Count(), projects.Count);

            Assert.Equal(projectFiles[0], projects[0].File);
            Assert.Equal(2, projects[0].ProjectReferences.Count);
            Assert.Equal(projects[5], projects[0].ProjectReferences[0]);
            Assert.Equal(projects[1], projects[0].ProjectReferences[1]);

            Assert.Equal(projectFiles[1], projects[1].File);
            Assert.Equal(2, projects[1].ProjectReferences.Count);
            Assert.Equal(projects[3], projects[1].ProjectReferences[0]);
            Assert.Equal(projects[5], projects[1].ProjectReferences[1]);

            Assert.Equal(projectFiles[2], projects[2].File);
            Assert.Equal(3, projects[2].ProjectReferences.Count);
            Assert.Equal(projects[3], projects[2].ProjectReferences[0]);
            Assert.Equal(projects[5], projects[2].ProjectReferences[1]);
            Assert.Equal(projects[1], projects[2].ProjectReferences[2]);

            Assert.Equal(projectFiles[3], projects[3].File);
            Assert.Equal(1, projects[3].ProjectReferences.Count);
            Assert.Equal(projects[5], projects[3].ProjectReferences[0]);

            Assert.Equal(projectFiles[4], projects[4].File);
            Assert.Equal(2, projects[4].ProjectReferences.Count);
            Assert.Equal(projects[3], projects[4].ProjectReferences[0]);
            Assert.Equal(projects[5], projects[4].ProjectReferences[1]);

            Assert.Equal(projectFiles[5], projects[5].File);
            Assert.Equal(0, projects[5].ProjectReferences.Count);

            Assert.Equal(projectFiles[6], projects[6].File);
            Assert.Equal(1, projects[6].ProjectReferences.Count);
            Assert.Equal(projects[5], projects[6].ProjectReferences[0]);
        }

        [Fact]
        public void PrepareProjectReferenceGraph_ProjectsWithExternalProjectReferences_ExternalReferencesAreNotIncluded()
        {
            // Arrange
            var projectFiles = new[]
            {
                "MyProd.Gui\\MyProd.Gui.csproj",
                "MyProd.Services\\MyProd.Services.csproj",
                "MyProd.Tools\\MyProd.Tools.csproj",
            };

            var mockProjectFileAnalyzer = new Mock<IProjectFileAnalyzer>(MockBehavior.Strict);
            mockProjectFileAnalyzer
                .Setup(m => m.ExtractProjectReferencesFromProjectFile(It.Is<string>(f => f == projectFiles[0])))
                .Returns(new List<string>
                    {
                        "..\\MyProd.Services\\MyProd.Services.csproj",
                        "..\\ExternalProjectNotInTheList\\ExternalProjectNotInTheList.csproj"
                    });
            mockProjectFileAnalyzer
                .Setup(m => m.ExtractProjectReferencesFromProjectFile(It.Is<string>(f => f == projectFiles[1])))
                .Returns(new List<string>
                    {
                        "..\\MyProd.Tools\\MyProd.Tools.csproj",
                        "..\\ExternalProjectNotInTheList\\ExternalProjectNotInTheList.csproj"
                    });
            mockProjectFileAnalyzer
                .Setup(m => m.ExtractProjectReferencesFromProjectFile(It.Is<string>(f => f == projectFiles[2])))
                .Returns(new List<string>
                    {
                        "..\\ExternalProjectNotInTheList\\ExternalProjectNotInTheList.csproj"
                    });

            var candidate = new ProjectReferenceHelper(mockProjectFileAnalyzer.Object);

            // Act
            var projects = candidate.PrepareProjectReferenceGraph(projectFiles);

            // Assert
            Assert.NotNull(projects);
            Assert.Equal(projectFiles.Count(), projects.Count);

            Assert.Equal(1, projects[0].ProjectReferences.Count);
            Assert.Equal(1, projects[1].ProjectReferences.Count);
            Assert.Equal(0, projects[2].ProjectReferences.Count);

            Assert.Contains(projects[0].ProjectReferences.Single(), projects);
            Assert.Contains(projects[1].ProjectReferences.Single(), projects);
        }
    }
}
