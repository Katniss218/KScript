using KScript.Language.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KScript.Language.InputLanguage.Compilation
{
    /// <summary>
    /// A high level class intended to be used by the end user.
    /// </summary>
    public class Compiler
    {
        const string KSCRIPT_FILE_EXTENSION = ".ks";
        /// <summary>
        /// Directory with the source (.ks) files.
        /// </summary>
        public string InputDirectory { get; set; }

        /// <summary>
        /// Directory for the output files.
        /// </summary>
        public string OutputDirectory { get; set; }

        private Lexer _lexer;
        private Parser _parser;

        public Compiler( string inputDirectory, string outputDirectory )
        {
            this.InputDirectory = inputDirectory;
            this.OutputDirectory = outputDirectory;
        }

        public void CompileAll()
        {
            // TODO - clear output.

            string[] sourceFiles = Directory.GetFiles( InputDirectory, $"*{KSCRIPT_FILE_EXTENSION}" );

            foreach( var sourcePath in sourceFiles )
            {
                // TODO - exception handling

                string source = File.ReadAllText( sourcePath, Encoding.UTF8 );

                string compiled = Compile( source );

                string relativePath = sourcePath.Replace( InputDirectory, "" );

                string outputPath = Path.Combine( OutputDirectory, relativePath );

                File.WriteAllText( outputPath, compiled, Encoding.UTF8 );
            }
        }

        // TODO - How do we handle compiler warnings/errors? (can't be exceptions to the end user).

        private string Compile( string source )
        {
            // Performs the actual compilation of the source file.

            throw new NotImplementedException();
        }
    }
}
