using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            List<Person> people = new List<Person>()
            {
                new Person{Name="santosh",Age=18},
                new Person{Name="AA",Age=21}

            };
            MemoryStream stream = new MemoryStream();
            MySerializer s = new MySerializer(people.GetType());
            stream.Seek(0, SeekOrigin.Begin);
            s.WriteObject(stream, people);
            Console.WriteLine(Encoding.UTF8.GetString(stream.GetBuffer()));
            NUnit n = new NUnit();
            n.Test();
        }
    }
}
