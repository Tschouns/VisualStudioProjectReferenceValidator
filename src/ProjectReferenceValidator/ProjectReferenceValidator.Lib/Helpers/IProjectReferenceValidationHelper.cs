using ProjectReferenceValidator.Lib.Models;
using System.Collections.Generic;

namespace ProjectReferenceValidator.Lib.Helpers
{
    /// <summary>
    /// Provides a method to validate project references based on rules.
    /// </summary>
    public interface IProjectReferenceValidationHelper
    {
        /// <summary>
        /// Validates the specified projects and their references against the specified rules.
        /// </summary>
        /// <param name="projects">
        /// The projects and their references to validate
        /// </param>
        /// <param name="rules">
        /// The rules to validate the project references against
        /// </param>
        /// <returns>
        /// A validation result
        /// </returns>
        ValidationResult ValidateProjectReferences(IEnumerable<Project> projects, IEnumerable<Rule> rules);
    }
}
