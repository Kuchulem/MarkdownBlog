using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core.Tokens;

namespace Kuchulem.MarkdownBlog.Services.MarkdownExtensions
{
    /// <summary>
    /// Base classe for blog's technical blocks parsing
    /// </summary>
    public abstract class TechnicalBlockParser : BlockParser
    {
        private enum ParseState { None, Opening, Tag, Params, Closing, Complete }
        private const int MaxBrackets = 2;
        private const char ParamSeparator = ' ';
        private readonly string tag;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tag"></param>
        public TechnicalBlockParser(string tag)
            : base()
        {
            OpeningCharacters = new char[] { '[', '{' };
            this.tag = tag;
        }

        /// <summary>
        /// see <see cref="BlockParser.TryOpen(BlockProcessor)"/>
        /// </summary>
        /// <param name="processor"></param>
        /// <returns></returns>
        public override BlockState TryOpen(BlockProcessor processor)
        {
            // if in code tag stop
            if (processor.IsCodeIndent)
                return BlockState.None;

            // col and sourcePosition will be used to place the final block
            var col = processor.Column;
            var line = processor.Line;
            var sourcePosition = processor.Start;

            // Current char is the matched opening car (either [ ou {)
            var openingChar = processor.Line.CurrentChar;

            // Get the closing char matching the opening char
            var closingChar = openingChar switch
            {
                '[' => ']',
                '{' => '}',
                _ => '\0'
            };

            // The blocks may use params, lets initialize the list

            // we start by cheking that the opening is ok before going further
            if (!TryReadMatch(line, new string(openingChar, MaxBrackets), out line))
                return BlockState.None; // if not, we do not match the block

            // check if we find the tag right after the opening
            if (!TryReadMatch(line, tag, out line))
                return BlockState.None;

            // Gets the params if any
            var blockParams = ReadParams(line, ParamSeparator, closingChar, out line);

            // Check if closure is ok
            if (!TryReadMatch(line, new string(closingChar, MaxBrackets), out _))
                return BlockState.None;

            // Let the child parser class create the leaf block
            var block = MakeBlock(blockParams);

            if(block != null)
            {
                // This is where the block will be inserted
                block.Column = col;
                block.Span = new SourceSpan { Start = sourcePosition };

                // We add the block to the processor
                processor.NewBlocks.Push(block);

                // We stop the parsing for the line
                return BlockState.Break;
            }

            // If no block returnes, the parsing will go on with another parser
            return BlockState.Continue;
        }

        /// <summary>
        /// Creates a new instance of a LeafBlock to be rendered
        /// </summary>
        /// <param name="blockParams"></param>
        /// <returns></returns>
        protected abstract LeafBlock MakeBlock(IEnumerable<string> blockParams);

        private bool TryReadMatch(StringSlice slice, string match, out StringSlice updatedSlice)
        {
            // let's try to mach a string with the following chars in the line
            int charIndex = 0;
            updatedSlice = slice;
            do
            {
                var c = slice.CurrentChar;

                // oops one of the chars did not match, we stop there and return false
                if (match[charIndex] != c)
                    return false;

                slice.NextChar();
                charIndex++;
            } while (charIndex < match.Length);

            updatedSlice = slice;

            // got the string, lets return true
            return true;
        }

        private IEnumerable<string> ReadParams(StringSlice slice, char separator, char closingCharacter, out StringSlice updatedSlice)
        {
            var result = new List<string>();
            var closures = new[] { separator, closingCharacter };
            // if we do not find the separator, we are no more in params
            while (slice.CurrentChar == separator)
            {
                var param = "";
                while (slice.NextChar() != '\0')
                {
                    // lets stop if we find the separator or closing char
                    if (closures.Contains(slice.CurrentChar))
                        break;

                    // add the caracter to the param value
                    param += slice.CurrentChar;
                }

                // lets add the param (if not empty)
                if (!string.IsNullOrEmpty(param))
                    result.Add(param);
            }

            updatedSlice = slice;

            // We do not want the params list to be modified so lets return an IEnumerable
            return result.AsEnumerable();
        }
    }
}
