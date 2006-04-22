using System;
using System.Collections.Generic;
using System.Text;
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

                Assembly exeAsm = Assembly.LoadFrom("../../../bin/"+config+"/EVEMon.exe");
                string ver = String.Empty;
                foreach (Attribute a in exeAsm.GetCustomAttributes(false))
                {
                    if (a is AssemblyFileVersionAttribute)
                    {
                        AssemblyFileVersionAttribute ava = a as AssemblyFileVersionAttribute;
                        ver = ava.Version;
                    }
                }
                if (String.IsNullOrEmpty(ver))
                    throw new ApplicationException("no version");

                string param =
                    "/DVERSION=" + ver + " " +
                    "\"/DOUTDIR=" + desktopDir + "\" " +
                    "\"EVEMon Installer Script.nsi\"";
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
    }
}
