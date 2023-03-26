using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkReflection
{
    internal class FileCSV
    {
        public static void RecordCSV<T>(string path, FieldInfo [] fields, PropertyInfo[ ] properties, T serializeObject)
        {


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@path, false))
            {
                foreach (var field in fields)
                    file.WriteLine(field.Name + ";" + field.GetValue(serializeObject) );
                file.WriteLine('>');
                foreach (var property in properties) 
                    file.WriteLine(property.Name + ";" + property.GetValue(serializeObject));
            }

        }

        public static List<List<(string,string)>> ReadCSV(string path)
        {
            List<List<(string,string)>> result = new List<List<(string, string)>> ();
            using (StreamReader reader = new StreamReader(path))
            {
                result = reader.ReadToEnd().
                    Split('>').
                    Select(line=>line.
                         Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).
                         Select(line => line.Split(';')).
                         Select(line => (line[0], line[1])).
                         ToList()).
                     ToList() ;
            }
            return result;
        }
    }
}
