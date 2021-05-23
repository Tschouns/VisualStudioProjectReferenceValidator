
using System.Collections.Generic;
using System.IO;

namespace ProjectReferenceValidator.Lib.Helpers
{
    /// <summary>
    /// Provides methods to analyze Visual Studio solution files.
    /// </summary>
    public interface ISolutionFileAnalyzer
    {
        /// <summary>
        /// Extracts the referenced project files from the specified solution file.
        /// </summary>
        /// <param name="solutionFile">
        /// The solution file to extract the references from
        /// </param>
        /// <returns>
        /// The eferenced project files
        /// </returns>
        IReadOnlyList<FileInfo> ExtractProjectPathsFromSolutionFile(string solutionFile);
    }
}
