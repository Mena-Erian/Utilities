using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace HelperUtilities
{
    //test
    [Flags]
    public enum AlertType
    {
        None = 1, Massege = 2, throwException = 4, boolean = 8
    }
    public static class Helper
    {

        public static TEnum GetFromUserByType<TEnum>(string? MsgToUser, bool isMainMsg = true) where TEnum : Enum
            => (TEnum)Enum.ToObject(typeof(TEnum), GetIntFromUser(MsgToUser, isMainMsg));

        public static IFormatProvider DefaultformatProvider = CultureInfo.CurrentCulture;
        public static List<T> GetArrFormUser<T>(string dataName, int arrSize,
                    IFormatProvider? formatProvider, char[]? seperators = null) where T : IParsable<T>
        {

            List<T> arr = new List<T>(arrSize);
            Console.Write($"Please Enter {dataName}: ");

            bool isValidInput = false;
            string userinput;
            string[] splitTheInputUser;
            do
            {
                userinput = Console.ReadLine() ?? "";//"3 3 "
                splitTheInputUser = userinput.Split(seperators ?? [' '], StringSplitOptions.RemoveEmptyEntries);
                if (splitTheInputUser.Length != arrSize) Console.Write("Count of Array is not true Please Try Again: ");
                else isValidInput = true;
            }
            while (!isValidInput);


            for (int i = 0; i < splitTheInputUser.Length; i++)
            {
                isValidInput = T.TryParse(splitTheInputUser[i], formatProvider, out T value);
                if (!isValidInput || value is null) throw new Exception("YOUR INPUT is NOT VALID");
                arr.Add(value);
            }

            return arr;
        }
        public static List<T> GetArrFormUser<T>(int arrSize,
            IFormatProvider? formatProvider) where T : IParsable<T>
        {

            List<T> arr = new List<T>(arrSize);

            bool isValidInput = false;
            string userinput;
            string[] splitTheInputUser;
            do
            {
                userinput = Console.ReadLine() ?? "";//"3 3 "
                splitTheInputUser = userinput.Split(" ");
                if (splitTheInputUser.Length != arrSize) Console.WriteLine("Your Input is Not Valid");
                else isValidInput = true;
            }
            while (!isValidInput);


            for (int i = 0; i < splitTheInputUser.Length; i++)
            {
                isValidInput = T.TryParse(splitTheInputUser[i], formatProvider, out T value);
                if (!isValidInput || value is null) throw new Exception("YOUR INPUT is NOT VALID");
                arr.Add(value);
            }

            return arr;
        }
        public static List<T> GetArrFormUser<T>(int arrSize, AlertType @alertType,
            IFormatProvider? formatProvider) where T : IParsable<T>
        {
            List<T> arr = new List<T>(arrSize);

            bool isValidInput = false;
            string userinput;
            string[] splitTheInputUser;
            switch (alertType)
            {
                /// I Should Handle another cases but i don't have time,
                /// i will make it later (becouse this not requrment task) Thanks:)
                case (AlertType)6:
                    //Massege and Throw Exception
                    do
                    {
                        userinput = Console.ReadLine() ?? "";//"3 3 "
                        splitTheInputUser = userinput.Split(" ");
                        if (splitTheInputUser.Length != arrSize) Console.WriteLine("Your Input is Not Valid");
                        else isValidInput = true;
                    }
                    while (!isValidInput);
                    for (int i = 0; i < splitTheInputUser.Length; i++)
                    {
                        isValidInput = T.TryParse(splitTheInputUser[i], formatProvider, out T value);
                        if (!isValidInput || value is null) throw new Exception("YOUR INPUT is NOT VALID");
                        else arr.Add(value);
                    }
                    break;
                case AlertType.boolean:
                    do
                    {
                        userinput = Console.ReadLine() ?? "";//"3 3 "
                        splitTheInputUser = userinput.Split(" ");
                        if (splitTheInputUser.Length != arrSize) Console.WriteLine("NO");
                        else isValidInput = true;
                    }
                    while (!isValidInput);
                    for (int i = 0; i < splitTheInputUser.Length; i++)
                    {
                        isValidInput = T.TryParse(splitTheInputUser[i], formatProvider, out T? value);
                        if (!isValidInput || value is null)
                        {
                            Console.WriteLine("NO");
                            break;
                        }
                        else arr.Add(value);
                    }
                    if (isValidInput && splitTheInputUser.Length == arrSize) Console.Write("YES");
                    break;
                default:
                    goto case (AlertType)6;
            }
            return arr;
        }

        public static int GetIntFromUser(string? massageToUser, bool isMainMsg = true)
        {
            int number = 0;
            if (massageToUser != null)
            {
                switch (isMainMsg)
                {
                    case true:
                        do
                        {
                            Console.Write(massageToUser);
                        }
                        while (!int.TryParse(Console.ReadLine(), out number));
                        break;
                    case false:
                        do
                        {
                            Console.Write($"Please Enter {massageToUser}: ");
                        }
                        while (!int.TryParse(Console.ReadLine(), out number));
                        break;
                }
            }
            else
            {
                do
                {

                }
                while (!int.TryParse(Console.ReadLine(), out number));
            }

            return number;
        }
        public static decimal GetDecimalFromUser(string massageToUser, bool isMainMsg = true)
        {
            decimal number = 0;
            switch (isMainMsg)
            {
                case true:
                    do
                    {
                        Console.Write(massageToUser);
                    }
                    while (!decimal.TryParse(Console.ReadLine(), out number));
                    break;
                case false:
                    do
                    {
                        Console.Write($"Please Enter the {massageToUser}: ");
                    }
                    while (!decimal.TryParse(Console.ReadLine(), out number));
                    break;
            }

            return number;
        }
        public static string GetStringFromUser(string dataName, bool withWrap = false)
        {
            string str = string.Empty;
            if (withWrap)
            {
                do
                {
                    Console.WriteLine($"Please Enter {dataName}: ");
                    str = Console.ReadLine() ?? string.Empty;
                }
                while (str == string.Empty || int.TryParse(str, out _));
            }
            else
            {
                do
                {
                    Console.Write($"Please Enter {dataName}: ");
                    str = Console.ReadLine() ?? string.Empty;
                }
                while (str == string.Empty || int.TryParse(str, out _));
            }

            return str;
        }
        public static char GetCharFromUser(string msg, bool isMainMsg = false)
        {
            char character = default(char);
            bool isParsed = false;
            if (isMainMsg)
            {
                do
                {
                    Console.Write(msg);
                    isParsed = char.TryParse(Console.ReadLine(), out character);
                }
                while (!(isParsed && !int.TryParse(character.ToString(), out _)));
            }
            else
            {
                do
                {
                    Console.Write($"Please Enter {msg}: ");
                    isParsed = char.TryParse(Console.ReadLine(), out character);
                }
                while (!(isParsed && !int.TryParse(character.ToString(), out _)));
            }

            return character;
        }
        public static bool GetBoolFromUserByChar(string MsgToUser, bool isMainMsg = false)
        {
            char chr;
            do
            {
                chr = Helper.GetCharFromUser(MsgToUser,
                      isMainMsg);
            }
            while (!(chr.ToString().ToLower() == "y" ||
                   chr.ToString().ToLower() == "n"));
            if (chr.ToString().ToLower() == "y") return true;
            else return false;
        }

        public static void Print<T>(this T value, bool withNewLine = true)
        {
            if (withNewLine) Console.WriteLine(value);
            else Console.Write(value);
        }
        public static void Print<T>(this T value, string addToTheEnd) => Console.WriteLine(value + addToTheEnd);
        public static void Print<T>(this IEnumerable<T> values) where T : IEnumerable
        {
            foreach (T item in values) Console.WriteLine(item);
        }
        public static void PrintAll<T>(this ICollection<T> values, string? addToTheEnd = null, bool withNewLine = true)
        {
            if (withNewLine)
            {
                foreach (T item in values) Console.WriteLine(item);
                if (addToTheEnd is not null) Console.WriteLine(addToTheEnd);
            }
            else
            {
                foreach (T item in values) Console.Write($"{item} ");
                if (addToTheEnd is not null) Console.Write(addToTheEnd);
            }
        }
        public static void PrintAll<T>(this ICollection values)
        {
            foreach (T item in values) Console.WriteLine(item);
        }
        public static void PrintAllArray<T>(this ICollection<T> values)
        {
            Console.Write("[");
            foreach (T item in values) Console.Write($"{item}, ");
            Console.Write("]");
        }
        public static void PrintAll<T>(this IEnumerable<T> values)
        {
            foreach (T item in values) Console.WriteLine(item);
        }
        public static void PrintAll<T>(this IEnumerable<ICollection<T>> values)
        {
            foreach (var chunk in values)
            {
                foreach (var item in chunk) Console.WriteLine(item);
                Console.WriteLine("-------------------------");
            }
            ;
        }
        public static void PrintAll<TKay, TElement>(this ILookup<TKay, TElement> values)
        {
            foreach (IGrouping<TKay, TElement> item in values)
            {

                Console.Write("Key: "); item.Key.Print();
                Console.WriteLine("Elements: "); item.PrintAll();
                Console.WriteLine("------------------------------------\n");
            }
        }
        public static void PrintAll<TKay, TElement>(this IEnumerable<IGrouping<TKay, TElement>> values)
        {
            foreach (IGrouping<TKay, TElement> item in values)
            {

                Console.Write("Key: "); item.Key.Print();
                Console.WriteLine("Elements: "); item.PrintAll();
                Console.WriteLine("------------------------------------\n");
            }
        }
        public static void PrintAll(this DataTable dt, bool withRowState = false)
        {
            if (withRowState)
                foreach (DataRow item in dt.Rows)
                {
                    item.ItemArray.PrintAll(withNewLine: false);
                    item.RowState.Print(false);
                    "-------------------------------*".Print();
                }
            foreach (DataRow item in dt.Rows)
            {
                item.ItemArray.PrintAll(addToTheEnd: "\n", withNewLine: false);
                "-------------------------------*".Print();
            }
        }
    }
}
