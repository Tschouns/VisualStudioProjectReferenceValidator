using ProjectReferenceValidator.Lib.Models;
using System.Collections.Generic;

namespace ProjectReferenceValidator.Lib.Helpers
{
    /// <summary>
    /// Helps with saving and loading ruleset files.
    /// </summary>
    public interface IRulesetFileHelper
    {
        /// <summary>
        /// Loads the ruleset from the specified file.
        /// </summary>
        /// <param name="rulesetFile">
        /// The ruleset
        /// </param>
        /// <returns>
        /// The ruleset file
        /// </returns>
        IReadOnlyList<Rule> LoadRuleset(string rulesetFile);

        /// <summary>
        /// Saves the specified ruleset in the specified ruleset file.
        /// </summary>
        /// <param name="rulesetFile">
        /// The file where the ruleset is saved
        /// </param>
        /// <param name="ruleset">
        /// The ruleset to save
        /// </param>
        void SaveRuleset(string rulesetFile, IReadOnlyList<Rule> ruleset);
    }
}
