using System;
using System.IO;
using System.Reflection;

namespace EwidOperaty.Oracle
{
    public static class SqlResource
    {
        public static string GetSqlText(string name)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("EwidOperaty.Oracle.SqlResource." + name) ?? throw new InvalidOperationException());
            string data = sr.ReadToEnd();
            sr.Close();

            return data;

        }
    }
}
