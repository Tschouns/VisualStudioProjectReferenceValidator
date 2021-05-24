using ProjectReferenceValidator.Lib.Helpers;
using ProjectReferenceValidator.Lib.Models;
using ProjectReferenceValidator.Lib.Tests.TestData;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;

namespace ProjectReferenceValidator.Lib.Tests.Helpers
{
    public class RulesetFileHelperTests
    {
        [Fact]
        public void LoadRuleset_ExampleFile_ReturnsExpectedRules()
        {
            // Arrange
            var candidate = new RulesetFileHelper();

            using var workingDir = new RandomTempDir(MethodBase.GetCurrentMethod().Name);
            var rulesetFile = Path.Combine(workingDir.FullName, "Rules.ruleset");
            File.WriteAllText(rulesetFile, ExampleRulesetFiles.RulesetWithMultipleRules);

            // Act
            var rules = candidate.LoadRuleset(rulesetFile);

            // Assert
            Assert.NotNull(rules);
            Assert.Equal(3, rules.Count);

            Assert.Equal("*Impl.csproj", rules[0].ProjectA);
            Assert.Equal("*Tests.csproj", rules[0].ProjectB);
            Assert.Equal(RuleType.MustNotReference, rules[0].Type);

            Assert.Equal("*Impl.csproj", rules[1].ProjectA);
            Assert.Equal("*Impl.csproj", rules[1].ProjectB);
            Assert.Equal(RuleType.MustNotReference, rules[1].Type);

            Assert.Equal("Special.Impl.csproj", rules[2].ProjectA);
            Assert.Equal("ExtraSpecial.Impl.csproj", rules[2].ProjectB);
            Assert.Equal(RuleType.CanReference, rules[2].Type);
        }

        [Fact]
        public void SaveRuleset_MultipleRules_SavesExpectedRulesetFile()
        {
            // Arrange
            var candidate = new RulesetFileHelper();

            using var workingDir = new RandomTempDir(MethodBase.GetCurrentMethod().Name);
            var rulesetFile = Path.Combine(workingDir.FullName, "Rules.ruleset");
            var rules = new List<Rule>
            {
                new Rule
                {
                    ProjectA = "*Impl.csproj",
                    ProjectB = "*Tests.csproj",
                    Type = RuleType.MustNotReference,
                },
                new Rule
                {
                    ProjectA = "*Impl.csproj",
                    ProjectB = "*Impl.csproj",
                    Type = RuleType.MustNotReference,
                },
                new Rule
                {
                    ProjectA = "Special.Impl.csproj",
                    ProjectB = "ExtraSpecial.Impl.csproj",
                    Type = RuleType.CanReference,
                },
            };

            // Act
            candidate.SaveRuleset(rulesetFile, rules);

            // Assert
            var fileText = File.ReadAllText(rulesetFile);
            Assert.Equal(ExampleRulesetFiles.RulesetWithMultipleRules, fileText);
        }
    }
}
