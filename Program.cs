using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FolderParserChild
{
    class Program
    {
        static void Main(string[] args)
        {
            bool has_child = false;
            string path = "";
            String arguments = "";
            String path_exe = "";

            // print params
            Console.Write("Params:");
            foreach (string arg in args)
            {
                Console.Write(" " + arg);

                if (arg == "-child")
                    has_child = true;
      
                if (arg.Contains(@":\") && arg.Contains(@".exe"))
                    path = arg;

                if (arg.Contains(@".exe"))
                    path_exe = arg;

                if ( arg != "-child" && !arg.Contains(@":\") && !arg.Contains(@".exe"))
                    arguments += " " + arg;
            }
            Console.WriteLine();
            var curr_path = Directory.GetCurrentDirectory();

            //scan the directory
            try
            {
                string[] file_list;
                string[] dir_list;

                file_list = Directory.GetFiles(curr_path);
                dir_list = Directory.GetDirectories(curr_path);
                StreamWriter sw = new StreamWriter("log_parse_folder.txt");
                foreach (string dir_to_read in dir_list)
                {
                    sw.WriteLine(dir_to_read);
                    sw.Flush();
                    Console.WriteLine(dir_to_read);
                }
                foreach (string file_to_read in file_list)
                {
                    sw.WriteLine(file_to_read);
                    sw.Flush();
                    Console.WriteLine(file_to_read);
                }
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // call child process
            if (has_child)
            {
                string file = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                string command = curr_path + @"\" + file + ".exe";
                ExecuteCommand(command, arguments);
            }
            else
            if (path_exe != "")
            {
                string command = curr_path + @"\" + path_exe;
                ExecuteCommand(command, arguments);
            }
            else
            if (path !="")
            {
               ExecuteCommand(path, arguments);
            }
            Console.WriteLine("\nEnter to close");
            Console.ReadLine();
        }

    
        static void ExecuteCommand(string command, String arguments)
        {
            ProcessStartInfo processInfo;
            Process process;

            Console.WriteLine("/C " + command + arguments);        
            processInfo = new ProcessStartInfo("cmd.exe", "/C " + command + arguments);

            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = true;

            process = Process.Start(processInfo);
           // process.WaitForExit();
           // process.Close();
        }

    }
}
