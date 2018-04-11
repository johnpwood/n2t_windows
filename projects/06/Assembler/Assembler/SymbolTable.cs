using System;
using System.Collections.Generic;
using System.Text;

namespace Assembler
{
    class SymbolTable
    {
        Dictionary<string, int> _symbols;
        int n;

        public SymbolTable()
        {
            n = 16; // number of registers
            _symbols = new Dictionary<string, int>{
                { "SP", 0 }, {"LCL", 1}, {"ARG", 2}, {"THIS", 3}, {"THAT", 4}, {"SCREEN", 16384}, {"KBD", 24576}
            };
            for(int i = 0; i < n; i++)
            {
                _symbols.Add($"R{ i }", i);
            }
        }

        public void AddLabel(string symbol, int i)
        {
            _symbols.Add(symbol, i);
        }

        public void AddVariable(string variable)
        {
            _symbols.Add(variable, n++);
        }

        public bool Contains(string symbol) => _symbols.ContainsKey(symbol);

        public int GetAddress(string symbol) => _symbols[symbol];
    }
}
