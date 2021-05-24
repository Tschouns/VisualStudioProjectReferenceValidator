using System.Collections.Generic;

namespace ProjectReferenceValidator.Lib.Models
{
    /// <summary>
    /// Represents a list of rules which can be applied to a project reference graph.
    /// </summary>
    public class Ruleset
    {
        /// <summary>
        /// Gets or sets the list of rules in the ruleset. Rules lower in the list take precedent.
        /// </summary>
        public List<Rule> Rules { get; set; }
    }
}
