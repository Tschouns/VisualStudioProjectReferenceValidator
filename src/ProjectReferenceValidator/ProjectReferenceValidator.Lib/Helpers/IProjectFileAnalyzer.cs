using System.Collections.Generic;

namespace ProjectReferenceValidator.Lib.Helpers
{
    /// <summary>
    /// Provides helper methods to analyze C# project files.
    /// </summary>
    public interface IProjectFileAnalyzer
    {
        /// <summary>
        /// Extracts the project references, as project file paths, from the specified project file.
        /// </summary>
        /// <param name="projectFile">
        /// The project file to extract the references from
        /// </param>
        /// <returns>
        /// The project references as project file paths
        /// </returns>
        IReadOnlyList<string> ExtractProjectReferencesFromProjectFile(string projectFile);
    }
}
