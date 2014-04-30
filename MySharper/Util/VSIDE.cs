using System.IO;

namespace MySharper.Util
{
    class VSIDE
    {
        private static string IDEFileFormat = @"{0}:\Program Files (x86)\Microsoft Visual Studio {1}\Common7\IDE\devenv.exe";
        private static string IDEFile;

        public static void EditFile(string file)
        {
            if (IDEFile == null)
            {
                GetIDEFile();
            }

            if (File.Exists(file) == false) return;

            var p = AppProcessor.StartNewProcess(IDEFile, " /edit " + file);
            p.WaitForExit();
            p.Close();
        }

        private static void GetIDEFile()
        {
            string[] disks = { "C", "D", "E" };
            string[] versions = { "11.0", "10.0" };
            for (int i = 0; i < disks.Length; i++)
            {
                for (int j = 0; j < versions.Length; j++)
                {
                    string ideFile = string.Format(IDEFileFormat, disks[i], versions[j]);
                    if (File.Exists(ideFile))
                    {
                        IDEFile = ideFile;
                        break;
                    }
                }
                if (IDEFile != null) break;
            }
            IDEFile = IDEFile ?? string.Empty;
        }
    }
}
