using System;
using System.Collections.Generic;

namespace ProjectReferenceValidator.Lib.Models
{
    /// <summary>
    /// Represents a C# project which may reference a number of other projects.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="name">
        /// The project name
        /// </param>
        /// <param name="file">
        /// The project file path
        /// </param>
        /// <param name="projectReferences">
        /// The projects referenced by this project
        /// </param>
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

        /// <summary>
        /// Gets the project name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the project file path.
        /// </summary>
        public string File { get; }

        /// <summary>
        /// Gets the projects referenced by this project.
        /// </summary>
        public IReadOnlyList<Project> ProjectReferences { get; }
    }
}
