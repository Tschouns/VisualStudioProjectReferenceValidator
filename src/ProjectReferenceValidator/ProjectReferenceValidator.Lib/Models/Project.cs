using System;
using System.Collections.Generic;

namespace ProjectReferenceValidator.Lib.Models
{
    public class Project
    {
        public Project(
            string name,
            string file,
            IReadOnlyList<Project> projectReferences)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (projectReferences == null)
            {
                throw new ArgumentNullException(nameof(projectReferences));
            }

            this.Name = name;
            this.File = file;
            this.ProjectReferences = projectReferences;
        }

        public string Name { get; }
        public string File { get; }
        public IReadOnlyList<Project> ProjectReferences { get; }
    }
}
