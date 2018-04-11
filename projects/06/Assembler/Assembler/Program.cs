using System;

namespace Assembler
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new SymbolTable();
            var p = new Parser(args[0]);
            while (p.HasMoreCommands())
            {
                if(p.CommandType == Command.L_COMMAND)
                {
                    s.AddLabel(p.Symbol, p.OutputLine);
                }
                p.Advance();
            }
            p.Reset();

            while(p.HasMoreCommands())
            {

                p.Advance();
            }
        }
    }
}
