using System;
using System.Collections.Generic;
using System.Text;

namespace Assembler
{
    class Code
    {
        private static Dictionary<string, string> comp = new Dictionary<string, string>{
            {  "0", "101010" }, {  "1", "111111" }, { "-1", "111010" },
            {  "d", "001100" }, {  "a", "110000" }, { "!d", "001101" },
            { "!a", "110001" }, { "-d", "001111" }, { "-a", "110011" },
            {"d+1", "011111" }, {"a+1", "110111" }, {"d-1", "001110" },
            {"a-1", "110010" }, {"d+a", "000010" }, {"d-a", "010011" },
            {"a-d", "000111" }, {"d&a", "000000" }, {"d|a", "010101" }
        };

    }
}
