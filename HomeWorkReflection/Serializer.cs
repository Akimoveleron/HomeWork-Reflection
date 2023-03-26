using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkReflection
{
    internal class Serializer
    {
        public static void Serialize<T>(T serialziObject) where T : class
        {
            var typeObject = serialziObject.GetType();
          
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
           
            var fields = typeObject.GetFields(bindingFlags);
           

            var properties = typeObject.GetProperties(bindingFlags);

          
            FileCSV.RecordCSV("Save.csv", fields, properties, serialziObject);
            FileCSV.ReadCSV("Save.csv");

        }
        public static T Deserialize<T>() where T : class, new()
        {
            List<List<(string, string)>> fileCsv = FileCSV.ReadCSV("Save.csv");
            List<(string, string)> fieldsSave = fileCsv[0];
            List<(string, string)> propertiesSave = fileCsv[1];
           
            var newObject = new T();
            var type = newObject.GetType();
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

            foreach ( var field in fieldsSave )
            {
                var fieldName = field.Item1;
                var fieldValue = field.Item2;

                var fieldInfo = type.GetField(fieldName, bindingFlags);
                var fieldType = fieldInfo?.FieldType;

                fieldInfo?.SetValue(newObject, Convert.ChangeType( fieldValue, fieldType));
            }
            foreach ( var property in propertiesSave)
            {
                var propertyName = property.Item1;
                var propertyValue = property.Item2;

                var propertyInfo = type.GetField(propertyName, bindingFlags);


                var propertyType = propertyInfo?.FieldType;

                propertyInfo?.SetValue(newObject, Convert.ChangeType( propertyValue, propertyType));
            }
            return newObject;
        }

    }
}
