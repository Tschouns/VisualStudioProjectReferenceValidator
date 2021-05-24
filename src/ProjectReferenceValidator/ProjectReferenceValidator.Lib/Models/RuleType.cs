
namespace ProjectReferenceValidator.Lib.Models
{
    /// <summary>
    /// Specifies the existing types of rules.
    /// </summary>
    public enum RuleType
    {
        /// <summary>
        /// No rule specified.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// "Project A must not reference project B."
        /// </summary>
        MustNotReference = 1,

        /// <summary>
        /// "Project A can reference project B."
        /// </summary>
        CanReference = 2,
    }
}
