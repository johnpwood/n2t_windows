using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace vm1
{
    enum Command {C_ARITHMETIC, C_PUSH, C_POP, C_LABEL, C_GOTO, C_IF, C_FUNCTION, C_RETURN, C_CALL}
    class parser
    {
        private string code;

        public parser(string f)
        {
            try
            {
                using (var sr = new StreamReader(f))
                {
                    code = sr.ReadAllLines();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

    }
}
