using System;

namespace ProjectReferenceValidator.Lib.Models
{
    /// <summary>
    /// Represents a concret voilation of a rule by a source project referencing a target project.
    /// </summary>
    public class RuleViolation
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="projectA">
        /// The source project which references the other
        /// </param>
        /// <param name="projectB">
        /// The target project which is referenced
        /// </param>
        public RuleViolation(
            Project sourceProject,
            Project targetProject)
        {
            if (sourceProject == null)
            {
                throw new ArgumentNullException(nameof(sourceProject));
            }

            if (targetProject == null)
            {
                throw new ArgumentNullException(nameof(targetProject));
            }

            this.SourceProject = sourceProject;
            this.TargetProject = targetProject;
        }

        /// <summary>
        /// Gets the source project referencing the other.
        /// </summary>
        public Project SourceProject { get; }

        /// <summary>
        /// Gets the referenced target project
        /// </summary>
        public Project TargetProject { get; }
    }
}
