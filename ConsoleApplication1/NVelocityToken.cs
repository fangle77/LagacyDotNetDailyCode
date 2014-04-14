using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class NVelocityToken
    {
        private static void NVelocityTokenImage()
        {
            string[] TokenImage = new string[] { 
            "<EOF>", "\"[\"", "\"]\"", "\",\"", "\"..\"", "\"(\"", "<RPAREN>", "\")\"", "<ESCAPE_DIRECTIVE>", "<SET_DIRECTIVE>", "<DOLLAR>", "<DOLLARBANG>", "\"##\"", "<token of kind 13>", "\"#*\"", "\"#\"", 
            "\"\\\\\\\\\"", "\"\\\\\"", "<TEXT>", "<SINGLE_LINE_COMMENT>", "\"*#\"", "\"*#\"", "<token of kind 22>", "<WHITESPACE>", "<STRING_LITERAL>", "\"true\"", "\"false\"", "<NEWLINE>", "\"-\"", "\"+\"", "\"*\"", "\"/\"", 
            "\"%\"", "\"&&\"", "\"||\"", "\"<\"", "\"<=\"", "\">\"", "\">=\"", "\"==\"", "\"!=\"", "\"!\"", "\"=\"", "<END>", "\"if\"", "\"elseif\"", "<ELSE_DIRECTIVE>", "\"stop\"", 
            "<DIGIT>", "<NUMBER_LITERAL>", "<LETTER>", "<DIRECTIVE_CHAR>", "<WORD>", "<ALPHA_CHAR>", "<ALPHANUM_CHAR>", "<IDENTIFIER_CHAR>", "<IDENTIFIER>", "<DOT>", "\"{\"", "\"}\"", "<REFERENCE_TERMINATOR>", "<DIRECTIVE_TERMINATOR>"
         };

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < TokenImage.Length; i++)
            {
                Console.WriteLine("{0}-{0:X}\t{1}", i, TokenImage[i]);
                sb.AppendLine(string.Format("{0}-{0:X}\t{1}", i, TokenImage[i]));
            }

            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Velocity-Parser.txt", sb.ToString());

            Console.WriteLine("done");
        }
    }
}
