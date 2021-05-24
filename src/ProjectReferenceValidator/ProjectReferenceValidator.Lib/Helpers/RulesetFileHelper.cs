using ProjectReferenceValidator.Lib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ProjectReferenceValidator.Lib.Helpers
{
    public class RulesetFileHelper : IRulesetFileHelper
    {
        public IReadOnlyList<Rule> LoadRuleset(string rulesetFile)
        {
            if (string.IsNullOrWhiteSpace(rulesetFile))
            {
                throw new ArgumentNullException(nameof(rulesetFile));
            }

            var fileText = File.ReadAllText(rulesetFile);
            var serializer = new XmlSerializer(typeof(Ruleset));
            var ruleset = (Ruleset)serializer.Deserialize(new StringReader(fileText));

            return ruleset.Rules;
        }

        public void SaveRuleset(string rulesetFile, IReadOnlyList<Rule> ruleset)
        {
            if (string.IsNullOrWhiteSpace(rulesetFile))
            {
                throw new ArgumentNullException(nameof(rulesetFile));
            }

            if (ruleset == null)
            {
                throw new ArgumentNullException(nameof(ruleset));
            }

            var rulesetObj = new Ruleset
            {
                Rules = ruleset.ToList(),
            };

            var serializer = new XmlSerializer(typeof(Ruleset));

            var writer = new StreamWriter(rulesetFile);
            serializer.Serialize(writer, rulesetObj);
            writer.Close();
        }
    }
}
