using System;
using System.Collections.Generic;
using System.Text;

namespace GitObjectDb
{
    /// <summary>
    /// Provides details about special folders.
    /// </summary>
    public static class FileSystemStorage
    {
        /// <summary>
        /// The data file name used to store information in Git.
        /// </summary>
        public const string DataFile = "data.json";

        /// <summary>
        /// The data file name used to store information in Git.
        /// </summary>
        public const string ResourceFolder = "Resources";

        internal static ISet<string> ReservedNames { get; } = new HashSet<string>(
            new[] { DataFile, ResourceFolder },
            StringComparer.OrdinalIgnoreCase);
    }
}
