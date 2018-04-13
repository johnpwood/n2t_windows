using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Assembler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new Exception("Please provide the name of the Hack assembly language file you would like to assemble.");
            }
            var s = new SymbolTable();
            var p = new Parser(args[0]);
            var c = new Code();
            var commands = new List<string>();
            

            while (p.HasMoreCommands)
            {
                p.Advance();
                if(p.CommandType == Command.L_COMMAND)
                {
                    s.AddLabel(p.Symbol, p.OutputLine);
                }
            }
            p.Reset();

            while(p.HasMoreCommands)
            {
                p.Advance();
                switch (p.CommandType)
                {
                    case Command.A_COMMAND:
                        int a = 0;  // address could be a number or symbol:
                        if ( ! Int32.TryParse(p.Symbol, out a) )
                        {
                            a = s.GetAddress(p.Symbol);
                        }
                        commands.Add(Convert.ToString(a, 2).PadLeft(16, '0'));
                        break;
                    case Command.C_COMMAND:
                        var command = "111";
                        try
                        {
                            command += c.Comp(p.Comp);
                        }
                        catch
                        {
                            throw new Exception($"Command { p.Comp } on line { p.SourceLine } not recognized:\n{ p.Source }");
                        }

                        string d = c.Dest(p.Dest);
                        var reg = new Regex("A?M?D?");
                        if (!reg.Match(d).Success)
                        {
                            throw new Exception($"Error reading destination code { p.Dest } from line { p.SourceLine }:\n{ p.Source }");
                        }
                        command += d;

                        string j = c.Jump(p.Jump);
                        if (j == "-1")
                        {
                            throw new Exception($"Error reading jump code { p.Jump } on line { p.SourceLine }:\n{ p.Source}");
                        }
                        command += j;

                        commands.Add(command);
                        break;
                    case Command.L_COMMAND:
                        break;
                    default:
                        throw new Exception($"error determining command type on line { p.SourceLine }:\n");
                }
            }
            foreach ( var line in commands )
            {
                Console.WriteLine(line);
            }
            Console.ReadKey();
        }
    }
}
