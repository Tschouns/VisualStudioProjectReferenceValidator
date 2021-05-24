using System.Collections.Generic;

namespace ProjectReferenceValidator.Lib.Models
{
    /// <summary>
    /// Represents a set of rules which can be applied to a project reference graph.
    /// </summary>
    public class Ruleset
    {
        /// <summary>
        /// Gets or sets the rules in the ruleset.
        /// </summary>
        public List<Rule> Rules { get; set; }
    }
}
