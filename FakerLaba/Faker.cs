using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GeneratorValue;
using GenerateSpecialValue;
using System.Collections;
using TestClass;

namespace FakerLaba
{
    public class Faker
    {
        
        private static readonly String[] specialTypes = { "Company", "Name", "Profession", "Country", "NumberPhone", "LastName", "Patronymic", "FIO", "DayOfWeek"};

        private static GenerateSpecialValue.GenerateSpecialValue generateSpecialValue = new GenerateSpecialValue.GenerateSpecialValue();

        private static GenerateRandomValue generateRandomValue = new GeneratorValue.GenerateRandomValue();    
        
        private static List<dynamic> listObjects;
        
        private static Type[] types;
        
        private static Type typeD;

        private static bool stateCreateListObjects = false;

        public Faker(Assembly assembly)
        {
            listObjects = new List<dynamic>();
            types = assembly.GetTypes();
            
            foreach (Type t in types)
            {
                Console.WriteLine(t.Name);
                var test1 = DtoGenerator<int>();
                var test2 = DtoGenerator<TestClass1>();
                var test3 = DtoGenerator<TestClass3>();
                var test4 = DtoGenerator<TestClass4>();
                var test5 = DtoGenerator<TestClass5>();
                var test6 = DtoGenerator<Person>();
                var test7 = DtoGenerator<A>();
                stateCreateListObjects = false;
                listObjects.Clear();
                var test8 =  DtoGenerator<TestClass6>();
                                         
            }            
        }

        private object DtoGenerator<T>()
        {
            return CreateDtoObject(typeof(T));
        }

        public static dynamic CreateDtoObject(Type type)
        {
            if ((type == typeof(byte)) || (type == typeof(short)) || (type == typeof(string))
                || (type == typeof(int)) || (type == typeof(byte[])) || (type == typeof(bool))
                || (type == typeof(float)) || (type == typeof(double)) || (type == typeof(char))
                || (type == typeof(DateTime)) || (type == typeof(object[])) || (type == typeof(sbyte)))
            {                          
                Console.WriteLine("Простое значение типа {0} = {1}", type.Name, GenerateValue(type));
                return null;
            }

            var ctor = type.GetConstructors()?.FirstOrDefault();
            if (ctor == null)
            {
                try
                {
                    var resultClass = Activator.CreateInstance(type);
                    CreateFieldsProps(ref resultClass);
                    return resultClass;
                }
                catch
                {
                    return null;
                }
                
            }
            else
            {
                var constructorParams = ctor.GetParameters();
                var generatedParams = new List<dynamic>();

                foreach (ParameterInfo param in constructorParams)
                {
                    if (param.ParameterType.IsGenericType)
                    {
                        generatedParams.Add(generateRandomValue.GenerateRandomList(param.ParameterType.GetGenericArguments()[0]));
                    }
                    else generatedParams.Add(GenerateValue(param.ParameterType));
                }

                var resultClass = Activator.CreateInstance(type, generatedParams.ToArray());


                if (listObjects.Contains(type) == false)
                {
                    listObjects.Add(type);
                }
                else
                {
                    if (!stateCreateListObjects)
                        return resultClass;
                }
                CreateFieldsProps(ref resultClass);
                return resultClass;
            }
           

        }

        public static void CreateFieldsProps(ref dynamic resultClass)
        {
            var props = resultClass.GetType().GetProperties();
            var fields = resultClass.GetType().GetFields();

            foreach (var field in fields)
                field.SetValue(resultClass, GenerateValue(field.FieldType));

            foreach (var pInfo in props)
            {
                if (!(pInfo?.CanWrite ?? false))
                    continue;
                    
                if (pInfo.GetAccessors(true)[1].IsPublic)
                {
                    if (pInfo.PropertyType.IsGenericType)
                    {
                        if (CheckGenericType(pInfo.PropertyType.GetGenericArguments()[0]))
                        {
                            pInfo.SetValue(resultClass, null);
                        }
                        else pInfo.SetValue(resultClass, generateRandomValue.GenerateRandomList(pInfo.PropertyType.GetGenericArguments()[0]));
                    }
                    else
                    {
                        if (Array.IndexOf(specialTypes, pInfo.Name) == -1)
                        {
                            pInfo.SetValue(resultClass, GenerateValue(pInfo.PropertyType));
                        }
                        else pInfo.SetValue(resultClass, GenerateSpecialValue(pInfo));
                    }
                }                
            }            
        }
        public static bool CheckGenericType(Type genericType)
        {            
            foreach(Type t in types)
            {
                if (t.Name == genericType.Name)
                {
                    typeD = t; 
                    return true;
                }
                else continue;
            }
            return false;
        }

        public static IList CreateListObjects(Type type)
        {
            var listObjects = typeof(List<>);
            var constructedListType = listObjects.MakeGenericType(type);
            var instance = Activator.CreateInstance(constructedListType);
            
            var list = (IList)instance;

            stateCreateListObjects = true;
            for (int i = 0; i < 2; i++)                            
                list.Add(CreateDtoObject(type));            

            stateCreateListObjects = false;
            return list;
        }

        private static dynamic GenerateValue(Type type)
        {
            if (type == typeof(int))
                return generateRandomValue.GenerateRandomInt();
            else if (type == typeof(Double))
                return generateRandomValue.GenerateRandomDouble();
            else if (type == typeof(float))
                return generateRandomValue.GenerateRandomFloat();
            else if (type == typeof(DateTime))
                return generateRandomValue.GenerateRandomDateTime();
            else if (type == typeof(string))
                return generateRandomValue.GenerateRandomString();
            else if (type == typeof(bool))
                return generateRandomValue.GenerateRandomBool();
            else if (type == typeof(char))
                return generateRandomValue.GenerateRandomChar();
            else if (type == typeof(int[]))
                return generateRandomValue.GenerateRandomIntM();
            else if (type == typeof(short))
                return generateRandomValue.GenerateRandomShort();
            else if (type == typeof(byte))
                return generateRandomValue.GenerateRandomByte();
            else if (type == typeof(byte[]))
                return generateRandomValue.GenerateRandomByteM();
            else if (type == typeof(object[]))
                return generateRandomValue.GeneratemassivObjects();
            else
                return CreateDtoObject(type);
        }

        static dynamic GenerateSpecialValue(PropertyInfo propertyInfo)
        {
            if (propertyInfo.Name == "Name")
                return generateSpecialValue.GenerateRandomName();
            if (propertyInfo.Name == "Company")
                return generateSpecialValue.GenerateRandomCompany();
            if (propertyInfo.Name == "Profession")
                return generateSpecialValue.GenerateRandomProfession();
            if (propertyInfo.Name == "Country")
                return generateSpecialValue.GenerateRandomCountry();
            if (propertyInfo.Name == "NumberPhone")
                return generateSpecialValue.GenerateRandomPhoneNumber();
            if (propertyInfo.Name == "DayOfWeek")
                return generateSpecialValue.GenerateRandomDayOFweek();
            if (propertyInfo.Name == "LastName")
                return generateSpecialValue.GenerateRandomLastName();
            if (propertyInfo.Name == "Patronymic")
                return generateSpecialValue.GenerateRandomPatronymic();
            else
                return generateSpecialValue.GenerateRandomFIO();
        }

    }    
}
