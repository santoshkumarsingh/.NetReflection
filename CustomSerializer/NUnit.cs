using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomSerializer
{
    class TestAttribute : Attribute
    {

    }
    public class TestMethodAttribute : Attribute { }
    [Test]
    public class MyTestSuite
    {
        public void MyHelper()
        {

        }
        [TestMethod]
        public void MyTest1()
        {
            Console.WriteLine("Doing some testing");
        }
        [TestMethod]
        public void MyTest2()
        {
            Console.WriteLine("Doing some testing ....");
        }

    }
    public class NUnit
    {
        public void Test()
        {
            var testSuites =
                from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.GetCustomAttributes(false).Any(a => a is TestAttribute)
                select t;
            foreach (var t in testSuites)
            {
                var testMethods = from m in t.GetMethods()
                                  where m.GetCustomAttributes(false).Any(a => a is TestMethodAttribute)
                                  select m;
                object suiteInstance = Activator.CreateInstance(t);
                foreach (var mInfo in testMethods)
                {
                    mInfo.Invoke(suiteInstance, new object[0]);
                }

                
            }
        }
    }
}
