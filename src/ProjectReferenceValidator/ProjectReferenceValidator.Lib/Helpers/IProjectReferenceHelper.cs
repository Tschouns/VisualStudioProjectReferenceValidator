using ProjectReferenceValidator.Lib.Models;
using System.Collections.Generic;

namespace ProjectReferenceValidator.Lib.Helpers
{
    /// <summary>
    /// Provides helper methods to analyze the project structure.
    /// </summary>
    public interface IProjectReferenceHelper
    {
        /// <summary>
        /// Analyzes the specified project files and prepares the projects as an object graph including their references.
        /// </summary>
        /// <param name="projectFiles">
        /// A list of file paths of the project files to analyze
        /// </param>
        /// <returns>
        /// The projects as an object graph
        /// </returns>
        IReadOnlyList<Project> PrepareProjectReferenceGraph(IEnumerable<string> projectFiles);
    }
}
