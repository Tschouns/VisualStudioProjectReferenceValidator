
namespace ProjectReferenceValidator.Lib.Models
{
    /// <summary>
    /// Specifies a rule with respect to two projects; project A and project B.
    /// </summary>
    public class Rule
    {
        /// <summary>
        /// Gets or sets project A, i.e. a regular expression to match a project or a set of projects.
        /// </summary>
        public string ProjectA { get; set; }

        /// <summary>
        /// Gets or sets project B, i.e. a regular expression to match a project or a set of projects.
        /// </summary>
        public string ProjectB { get; set; }

        /// <summary>
        /// Gets or sets the rule type which describes the relationship between any given project A and B (e.g. "A must not reference B").
        /// </summary>
        public RuleType Type { get; set; }
    }
}
