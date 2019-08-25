using System;
using System.IO;
using System.Reflection;

namespace EwidOperaty.Tools
{
    public static class VbResource
    {
        public static string GetVbText(string name)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("EwidOperaty.Tools.VbResource." + name) ?? throw new InvalidOperationException());
            string data = sr.ReadToEnd();
            sr.Close();

            return data;

        }
    }
}
