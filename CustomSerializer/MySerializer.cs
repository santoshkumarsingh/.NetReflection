using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomSerializer
{
    public class MySerializer
    {
        Type _targetType;
        public MySerializer(Type targetTpe)
        {
            this._targetType = targetTpe;
            //if (!_targetType.IsDefined(typeof(MyDataContractAttribute), false))
            //    throw new Exception("No soup for you");

        }
        public void WriteObject(Stream stream, object graph)
        {
            if (graph.GetType().IsGenericType)
            {
                // Determine whether the object is a list  
                string TypeName = graph.GetType().GetGenericTypeDefinition().Name;
                if (TypeName == "List`1")
                {
                    // If it is, get the number of elements in the list  
                    int n = (int)graph.GetType().GetProperty("Count").GetValue(graph, null);
                    var writer = new StreamWriter(stream);
                    writer.Write("<" + _targetType.Name + ">");
                    // Process each element in the list  
                    for (int i = 0; i < n; i++)
                    {
                        // Get the list element as type object  
                        object[] index = { i };
                        object myObject = graph.GetType().GetProperty("Item").GetValue(graph, index);

                        // Get the object properties  
                        PropertyInfo[] objectProperties = myObject.GetType().GetProperties();

                        // Process each property  
                        foreach (PropertyInfo currentProperty in objectProperties)
                        {
                            string propertyValue = currentProperty.GetValue(myObject, null).ToString();
                           // Console.WriteLine(propertyValue);
                            writer.Write("\t<" + currentProperty.Name + ">" + propertyValue + "</" + currentProperty.Name + ">");
                        }

                        // Skip a line between objects  
                        Console.WriteLine();
                    }
                    writer.Write("</" + _targetType.Name + ">");
                    writer.Flush();
                }
            }
        }
    }
}
