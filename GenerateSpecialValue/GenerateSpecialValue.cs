using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateSpecialValue
{
    public class GenerateSpecialValue
    {
        private Random rndBool = new Random();

        private Random rnd = new Random();

        enum Name
        {
            Dima, Maks, Vasya, Volodya, Nik, Denis, Artem, Vika, Ella, Don, Andrey, Nastya, Vadim, Alex, Lesha, Anya, Fedor, Zhenya, Kostya, Algerd, Misha, Larisa, Lera, Violetta, Olga
        }

        enum Country
        {
            Belarus, Russia, Denmark, England, Estonia, Finland, Iceland, Ireland, Latvia, Lithuania, Norway, Scotland, Sweden, Wales, Austria, Belgium, France, Germany, Netherlands, Switzerland, Albania, Croatia, Greece, Italy, Malta, Portugal, Serbia, Slovenia, Spain, Armenia, Bulgaria, Georgia, Hungary, Romania, Slovakia
        }

        enum Profession
        {
            Accountant, actor, Actress, Artist, Astronaunt, Auditor, Baker, Banker, Beautician, Biologist, Bricklayer, Broker, BusDriver, Butcher, Chemist, Coach, Collector, Cook, Dentist, Doctor, Programmer, Explorer, farmer, Florist, Garder, Guide, Glazier, Hunter, Nurse
        }

        enum Company
        {
            SaudiAramco, Microsoft, Apple, Amazon, Alphabet, Facebook, Alibaba, Tencet, IBM, Nokia, Toyota, Intel, CocaCola, Disney, Samsung, HewlettPackard, MercedesBenz, Marlboro, GeneralElectric, McDonalds, Verizon, UPS, SAP, PayPal
        }
        enum LastName
        {
            Formago, Buklis, Jdanovich, Ryscho, Babich, Lomov, Pavlov, Petrov
        }
        enum Patronymic
        {
            Michalovich, Sergeevich, Viktorovich, 
        }
        enum DaysOfWeek
        {
            Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
        }

        public string GenerateRandomDayOFweek()
        {
            return ((DaysOfWeek)rnd.Next(0, Enum.GetNames(typeof(DaysOfWeek)).Length)).ToString();
        }

        public string GenerateRandomName()
        {
            return ((Name)rnd.Next(0, Enum.GetNames(typeof(Name)).Length)).ToString();
        }
        
        public string GenerateRandomLastName()
        {
            return ((LastName)rnd.Next(0, Enum.GetNames(typeof(LastName)).Length)).ToString();
        }
        public string GenerateRandomPatronymic()
        {
            return ((Patronymic)rnd.Next(0, Enum.GetNames(typeof(Patronymic)).Length)).ToString();
        }
        public string GenerateRandomFIO()
        {
            return ((Name)rnd.Next(0, Enum.GetNames(typeof(Name)).Length)).ToString()
                + ((LastName)rnd.Next(0, Enum.GetNames(typeof(LastName)).Length)).ToString()
                + ((Patronymic)rnd.Next(0, Enum.GetNames(typeof(Patronymic)).Length)).ToString();
        }
        public string GenerateRandomCountry()
        {
            return ((Country)rnd.Next(0, Enum.GetNames(typeof(Country)).Length)).ToString();
        }

        public string GenerateRandomProfession()
        {
            return ((Profession)rnd.Next(0, Enum.GetNames(typeof(Profession)).Length)).ToString();
        }

        public string GenerateRandomCompany()
        {
            return ((Company)rnd.Next(0, Enum.GetNames(typeof(Company)).Length)).ToString();
        }
        
        public string GenerateRandomPhoneNumber()
        {
            string[] codNUmber = { "29","33","44" };
            string[] codeCountry = { "+1", "+3", "+375", "+673", "+880",
                                    "+591", "+973", "+54", "+684", "+355", 
                                    "+93","+833","+813" };

            return codeCountry.ElementAt(rnd.Next(0, codeCountry.Length)) 
                    + codNUmber.ElementAt(rnd.Next(0, codNUmber.Length))
                    + rnd.Next(1000000, 9999999).ToString();
        }
    }
}
