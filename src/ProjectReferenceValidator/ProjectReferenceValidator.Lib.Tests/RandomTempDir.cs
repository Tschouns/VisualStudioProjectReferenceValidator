using System;
using System.IO;

namespace ProjectReferenceValidator.Lib.Tests
{
    /// <summary>
    /// Creates a random name temporary directory for testing purposes.
    /// </summary>
    class RandomTempDir : IDisposable
    {
        public RandomTempDir(string basePath)
        {
            var randomName = new Random().Next().ToString();
            var dir = Directory.CreateDirectory(Path.Combine(basePath, randomName));
            this.FullName = dir.FullName;
        }

        /// <summary>
        /// Gets the directory path.
        /// </summary>
        public string FullName { get; }

        public void Dispose()
        {
            Directory.Delete(this.FullName, true);
        }
    }
}
