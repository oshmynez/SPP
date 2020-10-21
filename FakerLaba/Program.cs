using Bogus.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestClass;

namespace FakerLaba
{
    class Program
    {
        static string path = "file:///W:\\C#\\TestClass\\bin\\Debug\\TestClass.dll";
        static void Main(string[] args)
        {
            
            Assembly asm = Assembly.LoadFrom(path);            
            Console.WriteLine(asm.Location);
            Faker faker = new Faker(asm);
           
            Console.ReadLine();
        }
       
    }


}

