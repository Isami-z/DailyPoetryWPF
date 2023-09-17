using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DailyPoetryWPF.Helpers
{
    class ResourceHelper
    {
        public static void ExtractResourceToFile(string resourceName, string outputPath)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string fullResourceName = $"{assembly.GetName().Name}.Assets.{resourceName}";

            using (Stream resourceStream = assembly.GetManifestResourceStream(fullResourceName))
            {
                if (resourceStream == null)
                {
                    throw new ArgumentException($"资源 '{fullResourceName}' 不存在.");
                }

                using (FileStream outputStream = new FileStream(outputPath, FileMode.Create))
                {
                    resourceStream.CopyTo(outputStream);
                }
            }
        }
    }
}
