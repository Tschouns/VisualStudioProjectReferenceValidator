using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ProjectReferenceValidator.Lib.Helpers
{
    public class ProjectFileAnalyzer : IProjectFileAnalyzer
    {
        public IReadOnlyList<string> ExtractProjectReferencesFromProjectFile(string projectFile)
        {
            if (projectFile == null)
            {
                throw new ArgumentNullException(nameof(projectFile));
            }

            // Load the XML DOM.
            var fileStream = new FileStream(projectFile, FileMode.Open);
            var reader = new XmlTextReader(fileStream);
            var xml = new XmlDocument();

            xml.Load(reader);

            fileStream.Close();
            reader.Close();

            // Search for project references.
            var projectReferenceElements = xml.GetElementsByTagName("ProjectReference").Cast<XmlNode>().ToList();
            var projectReferencePaths = projectReferenceElements
                .Select(e => e.Attributes
                    .Cast<XmlAttribute>()
                    .SingleOrDefault(a => a.Name == "Include"))
                .Where(a => a != null)
                .Select(a => a.Value)
                .ToList();

            return projectReferencePaths;
        }
    }
}
