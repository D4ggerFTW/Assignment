using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

// TODO: replace these with the processor input and output types.
using TInput = System.String;         //input
using TOutput = Level_Library.MyLevels;   //output

namespace Level_extension_Lib
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "Level_Processor1")]
    public class ContentProcessor1 : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            string[] lines = input.Split(new char[] { '\n' });
            int rows = Convert.ToInt32(lines[0]);
            int columns = Convert.ToInt32(lines[1]);

            int[,] levelData = new int[rows, columns];
            for (int row = 0; row < rows; row++)
            {
                string[] values = lines[row + 2].Split(new char[] { ' ' });
                for (int column = 0; column < columns; column++)
                {
                    levelData[row, column] = Convert.ToInt32(values[column]);
                }
            }

            return new Level_Library.MyLevels(levelData);
        }
    }
}