using ProjectReferenceValidator.Lib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectReferenceValidator.Lib.Helpers
{
    public class ProjectReferenceHelper : IProjectReferenceHelper
    {
        private readonly IProjectFileAnalyzer projectFileAnalyzer;

        public ProjectReferenceHelper(IProjectFileAnalyzer projectFileAnalyzer)
        {
            if (projectFileAnalyzer == null)
            {
                throw new ArgumentNullException(nameof(projectFileAnalyzer));
            }

            this.projectFileAnalyzer = projectFileAnalyzer;
        }

        public IReadOnlyList<Project> PrepareProjectReferenceGraph(IEnumerable<string> projectFiles)
        {
            if (projectFiles == null)
            {
                throw new ArgumentNullException(nameof(projectFiles));
            }

            // Arrange references by file.
            var referencesPerFile = new Dictionary<string, IReadOnlyList<string>>();
            foreach (var file in projectFiles)
            {
                var references = this.projectFileAnalyzer.ExtractProjectReferencesFromProjectFile(file);
                referencesPerFile.Add(file, references);
            }

            // Prepare project nodes without references.
            var projectReferencesPerProject = new Dictionary<Project, List<Project>>();
            foreach (var file in referencesPerFile.Keys)
            {
                var projectName = Path.GetFileNameWithoutExtension(file);
                var projectReferences = new List<Project>();
                var project = new Project(projectName, file, projectReferences);

                projectReferencesPerProject.Add(project, projectReferences);
            }

            // Connect the grapth, i.e. fill in references.
            foreach (var projectNode in projectReferencesPerProject)
            {
                var referencedFiles = referencesPerFile[projectNode.Key.File];
                foreach (var referencedFile in referencedFiles)
                {
                    var referencedProject = projectReferencesPerProject.Keys.SingleOrDefault(p => p.Name == Path.GetFileNameWithoutExtension(referencedFile));
                    if (referencedProject != null)
                    {
                        projectNode.Value.Add(referencedProject);
                    }
                }
            }

            return projectReferencesPerProject.Keys.ToList();
        }
    }
}
