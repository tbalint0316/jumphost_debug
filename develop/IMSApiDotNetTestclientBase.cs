using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text;


namespace com.itac.mes.imsapi.client.dotnet
{
    class IMSApiDotNetTestclientBase
    {
        public const int FILL_WITH_SPACE = 50;


        public static String fillWithSpace(String value)
        {
            StringBuilder line = new StringBuilder(value);
            while (line.Length < FILL_WITH_SPACE)
            {
                line.Append(' ');
            }
            return line.ToString();
        }


        public static MethodInfo dectectMethod(Type testClientClass, String name)
        {
            MethodInfo result = null;
            MethodInfo[] methods = testClientClass.GetMethods();
            ArrayList hits = new ArrayList();
            for (int m = 0; m < methods.Length; m++)
            {
                if (methods[m].Name.ToLower().StartsWith(("test_" + name).ToLower()))
                {
                    hits.Add(methods[m]);
                }
            }
            if (hits.Count == 0)
            {
            }
            else if (hits.Count == 1)
            {
                result = (MethodInfo)hits[0];
            }
            else
            {
                for (int m = 0; m < hits.Count; m++)
                {
                    Console.Out.WriteLine((m + 1) + ": " + ((MethodInfo)hits[m]).Name.Substring(5));
                }
                Console.Out.WriteLine("Choose index of function <1-" + hits.Count + ">: ");
                try
                {
                    int index = Int32.Parse(getInput());
                    if (index > 0 && index <= hits.Count)
                    {
                        result = (MethodInfo)hits[index - 1];
                    }
                }
                catch (FormatException fe)
                {
                    Console.Out.WriteLine("Invalid input.");
                }
            }
            return result;
        }

        public static String getInput()
        {
            return Console.ReadLine();
        }

        public static String getInputNoEcho()
        {
            StringBuilder line = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.Out.WriteLine();
                    break;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    line.Remove(line.Length - 1, 1);
                }
                else
                {
                    line.Append(info.KeyChar);
                }
            }
            return line.ToString();
        }
    }
}
