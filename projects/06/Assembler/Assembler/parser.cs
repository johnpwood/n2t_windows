using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace Assembler
{
    enum Command { A_COMMAND, C_COMMAND, L_COMMAND }
    class Parser
    {
        private string[] _asm;
        private string _fileName;

        //all variables from here on will change during each advance of a line:
        public bool HasMoreCommands { get; private set; }
        public int SourceLine { get; private set; }
        public int OutputLine { get; private set; }
        public Command CommandType { get; private set; }

        //the current line being parsed will be available at a higher level for more informative error messages:
        public string Source { get; private set; }

        //this will hold symbols of @ or label commands:
        public string Symbol;

        //these will hold the three fields of a c command:
        public string Dest { get; private set; }
        public string Comp { get; private set; }
        public string Jump { get; private set; }


        public Parser(string f)
        {
            _fileName = f;
            try
            {
                _asm = File.ReadAllLines(f);
            }
            catch (Exception e)
            {
                Console.WriteLine("The asm file could not be read:");
                Console.WriteLine(e.Message);
            }
            // remove comments and extra space:
            var r = new Regex("//.*");
            _asm = _asm.Select(l => l = r.Replace(l, "")).ToArray();
            _asm = _asm.Select(l => l.Trim()).ToArray();
            SourceLine = -1;
            OutputLine = 0;
            HasMoreCommands = false;
            for(int i = 0; i < _asm.Length; i++)
            {
                if (_asm[i] != "" && "@ADM-1".Contains(_asm[i][0]))
                {
                    HasMoreCommands = true;
                }
            }
        }

        public void Reset()
        {
            SourceLine = -1;
            OutputLine = 0;
            for (int i = 0; i < _asm.Length; i++)
            {
                if (_asm[i] != "" && "@ADM-1".Contains(_asm[i][0]))
                {
                    HasMoreCommands = true;
                }
            }
        }

        public void Advance()
        {
            SourceLine++;

            while (SourceLine < _asm.Length && _asm[SourceLine] == "") { SourceLine += 1; }

            Source = _asm[SourceLine];

            HasMoreCommands = false;
            for (int i = SourceLine + 1; i < _asm.Length; i++)
            {
                if (_asm[i] != "") { HasMoreCommands = true;  }
            }

            switch (Source[0])
            {
                case '@':
                    CommandType = Command.A_COMMAND;
                    Symbol = ParseA(Source);
                    break;
                case '(':
                    CommandType = Command.L_COMMAND;
                    Symbol = ParseL(Source);
                    break;
                default:
                    CommandType = Command.C_COMMAND;
                    var g = ParseC(Source).ToArray();
                    Dest = g[1];
                    Comp = g[2];
                    Jump = g[3];
                    break;
            }

        }

        private string ParseA(string s)
        {
            var m = Regex.Match(s, @"^@([a-zA-Z._$:][\w._$:]*)");
            if (m.Success)
            {
                OutputLine++;
                return m.Groups[1].Value;
            }
            else
            {
                m = Regex.Match(s, @"^@(\d+)$");
                if (m.Success)
                {
                    OutputLine++;
                    return m.Groups[1].Value;
                }
                else
                {
                    throw new Exception($"Illegal symbol in line { SourceLine } from { _fileName }:\n{ s }");
                }

            }
        }

        private string ParseL(string s)
        {
            var m = Regex.Match(s, @"^\(([a-zA-Z._$:][\w._$:]*)\)$");
            if (m.Success)
            {
                return m.Groups[1].Value;
            }
            else
            {
                throw new Exception($"Illegal symbol in line { SourceLine } from { _fileName }:\n{ s }");
            }
        }

        private IEnumerable<string> ParseC(string s)
        {
            var m = Regex.Match(s, @"(\w*)=?([^=;]+);?(\w*)");
            if(m.Success)
            {
                OutputLine++;
                return m.Groups.Select(g => g.Value);
            }
            else
            {
            throw new Exception($"Could not read command in line { SourceLine } from { _fileName }:\n{ s }");
            }
        }
    }
}
