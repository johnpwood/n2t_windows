using System;
using System.Collections.Generic;
using System.Text;

namespace Assembler
{
    class Code
    {
        private static Dictionary<string, string> comp = new Dictionary<string, string>{
            {  "0", "101010" }, {  "1", "111111" }, { "-1", "111010" },
            {  "D", "001100" }, {  "A", "110000" }, { "!D", "001101" },
            { "!A", "110001" }, { "-D", "001111" }, { "-A", "110011" },
            {"D+1", "011111" }, {"A+1", "110111" }, {"D-1", "001110" },
            {"A-1", "110010" }, {"D+A", "000010" }, {"D-A", "010011" },
            {"A-D", "000111" }, {"D&A", "000000" }, {"D|A", "010101" }
        };
        // the binary representation of the index gives the correct jump portion of a command:
        private static Array jump = new[] { "", "JGT", "JEQ", "JGE", "JLT", "JNE", "JLE", "JMP" };

        public string Dest(string d)
        {
            int dest = 0; // a bit is set for each destination:
            if (d.Contains("M")) { dest += 1; }
            if (d.Contains("D")) { dest += 2; }
            if (d.Contains("A")) { dest += 4; }
            return Convert.ToString(dest, 2).PadLeft(3, '0');
        }

        public string Comp(string c)
        {
            string a = c.Contains("M")?"1":"0"; // see page 109;
            c = c.Replace("M", "A");
            if (comp.ContainsKey(c))
            {
                return a + comp[c];
            }
            else
            {
                throw new Exception();
            }

        }

        public string Jump(string j)
        {
            return Convert.ToString(Array.IndexOf(jump, j), 2).PadLeft(3, '0');
        }
    }
}
