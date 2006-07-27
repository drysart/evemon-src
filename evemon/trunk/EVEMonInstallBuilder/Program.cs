using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace EVEMonInstallBuilder
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
#if !DEBUG
                string config = "Release";
#endif
#if DEBUG
                string config = "Debug";
                return 0;
#endif
                string projectDir = String.Join(" ", args);
                string desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                Assembly exeAsm = Assembly.LoadFrom("../../../bin/x86/"+config+"/EVEMon.exe");
                string ver = exeAsm.GetName().Version.ToString();

                ProcessInstallScripts(projectDir);

                string param =
                    "/DVERSION=" + ver + " " +
                    "\"/DOUTDIR=" + desktopDir + "\" " +
                    "/PAUSE "+
                    "\""+projectDir+"bin\\x86\\Release\\EVEMon Installer Script.nsi\"";
                //System.Windows.Forms.MessageBox.Show(param);
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(
                    "C:/Program Files/NSIS/makensis.exe", param);
                psi.WorkingDirectory = projectDir;
                System.Diagnostics.Process makensisProcess = System.Diagnostics.Process.Start(psi);
                makensisProcess.WaitForExit();
                int exitCode = makensisProcess.ExitCode;
                makensisProcess.Dispose();

                return exitCode;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return 1;
            }
        }

        private static void ProcessInstallScripts(string projectDir)
        {
            List<string> filesToProcess = new List<string>();
            foreach (string s in Directory.GetFiles(projectDir+"/bin/Release", "*.nsi"))
            {
                filesToProcess.Add(s);
            }
            foreach (string s in Directory.GetFiles(projectDir + "/bin/Release", "*.nsh"))
            {
                filesToProcess.Add(s);
            }

            foreach (string fn in filesToProcess)
            {
                using (MemoryStream ms = new MemoryStream())
                using (StreamWriter sw = new StreamWriter(ms))
                {
                    using (StreamReader sr = new StreamReader(fn))
                    {
                        while (!sr.EndOfStream)
                        {
                            string tLine = sr.ReadLine();
                            if (tLine == "## INSTALLBUILDER: INSERT FILES HERE ##")
                                InsertFiles(sw, projectDir, true);
                            else if (tLine == "## INSTALLBUILDER: INSERT DELETES HERE ##")
                                InsertFiles(sw, projectDir, false);
                            else if (tLine.Contains("INSTALLBUILDER"))
                                throw new ApplicationException("unknown installbuilder command: " + tLine);
                            else
                            {
                                sw.WriteLine(tLine);
                            }
                        }
                    }
                    sw.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    using (StreamReader sr = new StreamReader(ms))
                    using (FileStream fs = new FileStream(fn, FileMode.Create))
                    using (StreamWriter fsw = new StreamWriter(fs))
                    {
                        while (!sr.EndOfStream)
                        {
                            fsw.WriteLine(sr.ReadLine());
                        }
                    }
                }
            }
        }

        private static void InsertFiles(StreamWriter sw, string projectDir, bool adding)
        {
            foreach (string fn in Directory.GetFiles(projectDir + "..\\bin\\Release", "*.*"))
            {
                if (adding)
                {
                    sw.Write(" File \"");
                    sw.Write(fn);
                    sw.WriteLine("\"");
                }
                else
                {
                    sw.Write(" Delete \"$INSTDIR\\");
                    sw.Write(Path.GetFileName(fn));
                    sw.WriteLine("\"");
                }
            }
        }
    }
}
