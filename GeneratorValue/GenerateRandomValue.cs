using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorValue
{
    public class GenerateRandomValue
    {
        private readonly string allowedStringSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private readonly bool[] boolMassiv = { true, false };
        
        private readonly int[] intMassiv = { 1,2,3,4,5,6,7,8,9,0};
        
        private readonly char[] charMassiv = {')','(','_', ';', ':', '\'', '/', '?' };

        private readonly Random rndBool = new Random();

        private readonly Random rnd = new Random();

        public object[] GeneratemassivObjects()
        {
            object[] masssivObjects = new object[rnd.Next(2,10)];
            dynamic someValue;
            for (int i = 0; i < masssivObjects.Length; i++)
            {
                switch (rnd.Next(1, 4))
                {
                    case 1:
                        someValue = boolMassiv[rnd.Next(0, 1)];
                        break;
                    case 2:
                        someValue = intMassiv[rnd.Next(0, intMassiv.Length)];
                        break;
                    case 3:
                        someValue = charMassiv[rnd.Next(0, charMassiv.Length)];
                        break;
                    case 4:
                        someValue = GenerateRandomString();
                        break;
                    default: someValue = 0;
                        break;
                }
                masssivObjects[i] = someValue;
            }
            return masssivObjects;
        }
        public int GenerateRandomInt()
        {
            return rnd.Next(1, 2147483647);
        }

        public int[] GenerateRandomIntM()
        {
            int[] m = new int[rnd.Next(1,10)];
            for(int i = 0; i < m.Length; i++)
            {
                m[i] = rnd.Next(0,100);
            }
            
            return m;
        }

        public char GenerateRandomChar()
        {
            return (char)rnd.Next(127);
        }

        public bool GenerateRandomBool()
        {
            int i = rndBool.Next();
            bool rndb = i == 1;
            return rndb;
        }

        public float GenerateRandomFloat()
        {
            return Convert.ToSingle(rnd.NextDouble());
        }

        public double GenerateRandomDouble()
        {
            return rnd.NextDouble();
        }

        public string GenerateRandomString()
        {
            var length = rnd.Next(1,10);
            var randomStr = new string(Enumerable.Repeat(allowedStringSymbols, length)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
            return randomStr;
        }

        public Byte[] GenerateRandomByteM()
        {
            Byte[] b = new Byte[5];
            rnd.NextBytes(b);
            return b;
        }

        public DateTime GenerateRandomDateTime()
        {
            return DateTime.Now;
        }

        public byte GenerateRandomByte()
        {
            return (byte)rnd.Next(0, 255);
        }

        public sbyte GenerateRandomSByte()
        {
            return (sbyte)rnd.Next(-128, 127);
        }

        public short GenerateRandomShort()
        {
            return (short)rnd.Next(-32768, 32767);
        }

        public ushort GenerateRandomUShort()
        {
            return (ushort)rnd.Next(0, 65535);
        }

        public IList GenerateRandomList(Type t)
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(t);
            var instance = Activator.CreateInstance(constructedListType);
            
            var list = (IList)instance;

            for (int i = 0; i <= 5; i++)
            {
                var vr = GenerateValue(t);
                list.Add(vr);

            }

            return list;
        }

        public dynamic GenerateValue(Type type)
        {
            
            if (type == typeof(int))
                return GenerateRandomInt();
            else if (type == typeof(Double))
                return GenerateRandomDouble();
            else if (type == typeof(float))
                return GenerateRandomFloat();
            else if (type == typeof(Byte[]))
                return GenerateRandomByteM();
            else if (type == typeof(byte))
                return GenerateRandomByte();
            else if (type == typeof(int[]))
                return GenerateRandomIntM();
            else if (type == typeof(object[]))
                return GeneratemassivObjects();
            else if (type == typeof(DateTime))
                return GenerateRandomDateTime();
            else if (type == typeof(char))
                return GenerateRandomChar();
            else if (type == typeof(bool))
                return GenerateRandomBool();
            else if (type == typeof(string))
                return GenerateRandomString();
            else if (type == typeof(short))
                return GenerateRandomShort();
            else if (type == typeof(ushort))
                return GenerateRandomUShort();
            else if (type == typeof(sbyte))
                return GenerateRandomSByte();
            else
            {
                return default;
            }
        }
    }
}
