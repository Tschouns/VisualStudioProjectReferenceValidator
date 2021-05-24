using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectReferenceValidator.Lib.Models
{
    /// <summary>
    /// Represents the result of a validation.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="ruleViolations">
        /// The rule voilations.
        /// </param>
        public ValidationResult(IReadOnlyList<RuleViolation> ruleViolations)
        {
            if (ruleViolations == null)
            {
                throw new ArgumentNullException(nameof(ruleViolations));
            }

            this.RuleViolations = ruleViolations;
            this.Valid = !this.RuleViolations.Any();
        }

        /// <summary>
        /// Gets a value indicating whether the project references validation was successful.
        /// </summary>
        public bool Valid { get; }

        /// <summary>
        /// Gets a list of rule voilations.
        /// </summary>
        public IReadOnlyList<RuleViolation> RuleViolations { get; }
    }
}
