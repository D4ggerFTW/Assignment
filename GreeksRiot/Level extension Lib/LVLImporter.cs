using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

// TODO: replace this with the type you want to import.
using TImport = System.String;

namespace Level_extension_Lib
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    /// 
    /// This should be part of a Content Pipeline Extension Library project.
    /// 
    /// ContentImporter attribute to specifies the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>
    [ContentImporter(".route", DisplayName = "LVL Importer", DefaultProcessor = "ContentProcessor1")]
    public class LVLImporter : ContentImporter<TImport>
    {
        public override TImport Import(string filename, ContentImporterContext context)
        {
            // Reads the specified file into an instance of the imported type.
            return System.IO.File.ReadAllText(filename);
        }
    }
}
/*#####################################################
 * the task of the importer is to read in the data. 
 * It isn't actually supposed to do any processing. Of course, we aren't just limited to reading in a file here. 
 * There's a lot of things that we can do in the importer, but keep in mind
 * that it is a really good idea to separate the importing from the processing,
 * avoid doing any actual processing here.!!!!!!
 * #################################################
*/