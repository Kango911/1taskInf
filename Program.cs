using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniversalCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Calculator";

            Console.WriteLine("Это консольное приложение является калькулятором для систем счисления от 1 до 50 включительно, а также служит калькулятором для перевода чисел в римскую СС.");
            Console.WriteLine("Калькулятор работает только с положительными целыми числами.");
            Console.WriteLine("Выполнил Устьянцев Е.Ю. Группа: ПрИ-102");
            CreateBorder();
            GetHelp();

            Begin:
            try
            {
                
                GetInput();
            }
            catch (Exception ex)
            {
                if(ex.GetType() == typeof(ArgumentException)) Console.WriteLine(ex.Message);
                else Console.WriteLine("Обнаружена ошибка! повторите ввод еще раз, скорее всего данные некорректны!");

                goto Begin;
            }
            
        }

        private static void GetHelp()
        {
            Console.WriteLine("Операция: 1 - перевод числа из любой СС в любую другую СС.");
            Console.WriteLine("Операция: 2 - перевод числа в римскую СС.");
            Console.WriteLine("Операция: 3 - перевод из римской СС");
            Console.WriteLine("Операция: 4 - суммирование в любой СС.");
            Console.WriteLine("Операция: 5 - вычитание в любой СС.");
            Console.WriteLine("Операция: 6 - умножение в любой СС.");
            Console.WriteLine("Операция: 7 - вызвать список команд программы.");
            CreateBorder();

        }

        private static void WannaTryAgain()
        {
            Console.WriteLine("Если хотите продолжить пользоваться программой, то нажмите любую кнопку.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Это консольное приложение является калькулятором для систем счисления от 1 до 50 включительно, а также служит калькулятором для перевода чисел в римскую СС.");
            Console.WriteLine("Выполнил Устьянцев Е.Ю. Группа: ПрИ-102");
            CreateBorder();
            GetHelp();
            Console.WriteLine("Введите команду!");
        }

        public static int GetInt(char c)
        {
            Dictionary<int, char> alphabet = new Dictionary<int, char>();
            for (int i = 0; i < 62; i++)
            {
                if (i >= 0 && i <= 9)
                    alphabet.Add(i, (char)('0' + i));
                if (i >= 10 && i <= 35)
                    alphabet.Add(i, (char)('A' + i - 10));
                if (i >= 36 && i <= 62)
                    alphabet.Add(i, (char)('a' + i - 36));
            }

            for (int i = 0; i < 62; i++)
            {
                if (alphabet[i] == c)
                {
                    return i;
                }
            }
            throw new ArgumentException("Число невозможно получить из остатка. Попробуйте еще раз в следующий раз!");
        }
        private static int GetInput()
        {
            Console.WriteLine("\n Введите число, соответствующее операции, которую вы хотите выполнить:");
            while (true)
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int command) || !(command >= 1 && command <= 7))
                {
                    Console.WriteLine("Введите существующую команду!");
                    GetInput();
                }
                switch (command)
                {
                    case 1:
                        FirstFunction();
                        WannaTryAgain();
                        break;
                    case 2:
                        SecondFunction();
                        WannaTryAgain();
                        break;
                    case 3:
                        ThirdFunction();
                        WannaTryAgain();
                        break;
                    case 4:
                        FourthFunction();
                        WannaTryAgain();
                        break;
                    case 5:
                        FifthFunction();
                        WannaTryAgain();
                        break;
                    case 6:
                        SixthFunction();
                        WannaTryAgain();
                        break;
                    case 7:
                        GetHelp();
                        break;
                    default:
                        Console.WriteLine("К сожалению, что-то пошло не так.");
                        break;

                }
            }
        }

        private static void CreateBorder()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("=");
            }
        }

        private static char ConvertNumberToSymbol(int modul)
        {
            if (modul >= 0 && modul <= 9) return (char)('0' + modul);
            if (modul >= 10 && modul <= 36) return (char)('A' + (modul - 10));
            if (modul >= 37 && modul <= 62) return (char)('a' + (modul - 36));

            throw new ArgumentException("Некорректный остаток от деления!");
        }

        private static int ConvertFromAnyToDec(string number, int numberBase)
        {
            if (numberBase > 50)
                throw new ArgumentException("Основание некорректо! Оно должно быть в пределе от 1 до 50 включительно!");
            int result = 0;
            int digitsCount = number.Length;
            int num;

            Console.WriteLine("Разбиваем число на отдельные символы.");
            var builder = new StringBuilder();
            builder.Append("Символы:");
            foreach (char c in number.ToCharArray())
            {
                builder.Append($" {c}");
            }
            Console.WriteLine(builder.ToString());

            Console.WriteLine("Теперь начинаем перевод в десятичную систему счисления.");
            Console.WriteLine("Изначально результат вычисления 0.");

            if (numberBase == 1)
            {
                Console.WriteLine("Чтобы число из 1-СС перевести в 10-СС нужно просто подсчитать, сколько 1 в этом числе. Полученное число и будет искомым число в 10-СС");
                int res = 0;
                char[] chars = number.ToCharArray();
                for (int i = 0; i < number.Length; i++)
                {
                    char lol = chars[i];
                    res += int.Parse(lol.ToString());
                }
                Console.WriteLine(res);
                if (res != number.Length) throw new ArgumentException("Некорректное число! Попробуйте еще раз");
                return res;
            }
            for (int i = 0; i < digitsCount; i++)
            {
                char symbol = number[i];

                if (symbol >= '0' && symbol <= '9') num = symbol - '0';

                else if (symbol >= 'A' && symbol <= 'Z') num = symbol - 'A' + 10;
                else if (symbol >= 'a' && symbol <= 'z') num = symbol - 'a' + (('Z' - 'A') + 1) + 10;
                else throw new ArgumentException("Некорректное число!");

                if (num >= numberBase) throw new ArgumentException("Исходная строка имеет некорректные символы в обозначении чисел.");

                Console.WriteLine($"Умножаем результат на основание СС: {numberBase}, затем прибавляем число: {num}, соответствующее {i + 1} элементу числа.");

                result *= numberBase;
                result += num;
                Console.WriteLine($"({result / numberBase} * {numberBase}) + {num} = {result}");
                //Console.WriteLine($"({result} * {numberBase}) + {num}");
            }
            Console.WriteLine($"В ходе манипуляций получаем новое число: {result}");
            return result;
        }

        private static string ConvertFromDecToAny(int number, int numberBase)
        {

            if (numberBase > 50)
                throw new ArgumentException("Основание некорректо! Оно должно быть в пределе от 1 до 50 включительно!");
            StringBuilder builder = new StringBuilder();

            Console.WriteLine($"Теперь {number} переведем из 10-СС в {numberBase}");
            do
            {
                Console.WriteLine($"Делим с остатком {number} на {numberBase}. При этом остаток приписываем к числу-результату. ");
                int mod = number % numberBase;
                char symbol = ConvertNumberToSymbol(mod);
                Console.WriteLine($"{builder} + {mod}");
                builder.Append(symbol);
                number /= numberBase;

            } while (number >= numberBase);

            if (number != 0)
            {
                builder.Append(ConvertNumberToSymbol(number));
                Console.WriteLine($"Делим с остатком {number} на 10. При этом остаток приписываем к числу-результату. ");
            }

            Console.WriteLine($"Получаем число {builder.ToString()}. Но это еще не результат. Чтобы получить корректное нужно его записать наоборот: {string.Join("", builder.ToString().Reverse())}");
            string result = string.Join("", builder.ToString().Reverse());

            return result;
        }

        private static int ConvertFromAnyToDecWithoutComments(string number, int numberBase)
        {
            if (numberBase > 50)
                throw new ArgumentException("Основание некорректо! Оно должно быть в пределе от 1 до 50 включительно!");
            int result = 0;
            int digitsCount = number.Length;
            int num;

            var builder = new StringBuilder();
            builder.Append("Символы:");
            foreach (char c in number.ToCharArray())
            {
                builder.Append($" {c}");
            }


            if (numberBase == 1)
            {
                int res = 0;
                for (int i = 0; i < number.Length; i++)
                    res++;
                if (res != number.Length) throw new ArgumentException("Некорректное число! Попробуйте еще раз");
                return res;
            }
            for (int i = 0; i < digitsCount; i++)
            {
                char symbol = number[i];

                if (symbol >= '0' && symbol <= '9') num = symbol - '0';

                else if (symbol >= 'A' && symbol <= 'Z') num = symbol - 'A' + 10;
                else if (symbol >= 'a' && symbol <= 'z') num = symbol - 'a' + (('Z' - 'A') + 1) + 10;
                else throw new ArgumentException("Некорректное число!");

                if (num >= numberBase) throw new ArgumentException("Исходная строка имеет некорректные символы в обозначении чисел.");
                result *= numberBase;
                result += num;
            }

            return result;

        }

        private static string ConvertFromDecToAnyWithoutComments(int number, int numberBase)
        {
            if (numberBase > 50)
                throw new ArgumentException("Основание некорректо! Оно должно быть в пределе от 1 до 50 включительно!");
            StringBuilder builder = new StringBuilder();

            do
            {
                int mod = number % numberBase;
                char symbol = ConvertNumberToSymbol(mod);
               
                builder.Append(symbol);
                number /= numberBase;

            } while (number >= numberBase);

            if (number != 0)
            {
                builder.Append(ConvertNumberToSymbol(number));
            }

            string result = string.Join("", builder.ToString().Reverse());

            return result;
        }

        private static void FirstFunction()
        {
            Console.WriteLine("Введите число:");
            string originNumber = Console.ReadLine();
            Console.WriteLine("Введите систему счисления этого числа");
            int originNumberBase = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите систему счисления, в которую сконвертировать число");
            int toWhatBase = int.Parse(Console.ReadLine());

            int toDec = ConvertFromAnyToDec(originNumber, originNumberBase);
            Console.WriteLine($"Ваше новое число {ConvertFromDecToAny(toDec, toWhatBase)} в системе счисления {toWhatBase}");
            CreateBorder();
        }

        private static void SecondFunction()
        {
            int[] rim = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] arab = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            Console.WriteLine("Введите число в диапазоне от 1 до 5000");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int number) || !(number >= 1 && number <= 5000)) throw new ArgumentException("Некорректное число! Введите число от 1 до 5000");
            int i;
            i = 0;
            string output = "";
            var otigin = number;
            while (number > 0)
            {
                if (rim[i] <= number)
                {
                    Console.WriteLine($"{number} - {rim[i]} = {number - rim[i]}");
                    Console.WriteLine($"Число {arab[i]}, соответствующее {rim[i]} приписываем справа. И так до 0.");
                    number = number - rim[i];
                    output = output + arab[i];
                }
                else i++;

            }
            Console.WriteLine($"Получаем новое число {output} из исходного {otigin}");
            CreateBorder();
        }

        private static void ThirdFunction()
        {
            Console.WriteLine("Введите число в римской СС");
            string input = Console.ReadLine();
            int[] rim = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] arab = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            for (int i = 0; i < input.Length; i++)
            {
                bool isCorrect = false;
                for (int j = 0; j < arab.Length; j++)
                {
                    if (input[i].ToString() == arab[j])
                    {
                        isCorrect = true;
                        break;
                    }
                }

                if (!isCorrect) throw new ArgumentException("Некорректное число!");
            }

            Console.WriteLine($"Разбиваем число {input} на символы: {string.Join(" ", input.Split(""))}");

            int result = 0;
            var RomToArab = new Dictionary<char, int>
            {{ 'I', 1 },{ 'V', 5 },{ 'X', 10 },{ 'L', 50 },{ 'C', 100 },{ 'D', 500 },{ 'M', 1000 } };
            for (short i = 0; i < input.Length - 1; ++i)
            {
                if (RomToArab[input[i]] < RomToArab[input[i + 1]])
                {
                    Console.WriteLine($"Число слева {RomToArab[input[i]]} меньше числа справа {RomToArab[input[i + 1]]} , поэтому вычитаем из результирующега числа левое {RomToArab[input[i]]}");
                    result -= RomToArab[input[i]];
                }
                else if (RomToArab[input[i]] >= RomToArab[input[i + 1]])
                {
                    Console.WriteLine($"Число слева {RomToArab[input[i]]} больше, чем число справа {RomToArab[input[i + 1]]}, то прибавляем к результирующему числу левое {RomToArab[input[i]]}");
                    result += RomToArab[input[i]];
                }
                Console.WriteLine($"Получили текущее {result}");
            }
            result += RomToArab[input[^1]];
            Console.WriteLine($"Получили текущее {result}");
            Console.WriteLine($"Финальное число: {result}!");

            CreateBorder();
        }

        private static void FourthFunction()
        {
            Console.WriteLine("Введите систему счисления: ");
            string ss = Console.ReadLine();
            if (!int.TryParse(ss, out var based) || !(based >= 1 && based <= 50)) throw new ArgumentException("Некорректная система счисления!");

            Console.WriteLine("Введите первое число: ");
            string number1 = Console.ReadLine();

            Console.WriteLine("Введите второе число: ");
            string number2 = Console.ReadLine();

            //типа валидация.
            int n1 = ConvertFromAnyToDecWithoutComments(number1, based);
            int n2 = ConvertFromAnyToDecWithoutComments(number2, based);

            Console.WriteLine($"Начем сложение числе в {based}-СС");

            if(based == 1)
            {
                Console.WriteLine("Так как система счисления 1, то результатом суммы будет общее количество единиц обоих чисел");
                Console.WriteLine($"Тогда результат: {number1+number2}");
                return;
            }

            List<int> num1 = new List<int>();
            List<int> num2 = new List<int>();

            List<int> sumResult = new List<int>();

            int maxLen = Math.Max(number1.Length, number2.Length);

            number1 = number1.PadLeft(maxLen, '0');
            number2 = number2.PadLeft(maxLen, '0');

            

            foreach (var i in number1)
                num1.Add(GetInt(i));
            foreach (var j in number2)
                num2.Add(GetInt(j));

            num1.Reverse();
            num2.Reverse();

            Console.WriteLine($" {number1}");
            Console.WriteLine($"+");
            Console.WriteLine($" {number2}");
            string border = "";
            Console.WriteLine($" {border.PadLeft(maxLen, '-')}");

            Console.WriteLine("Поразрядно складываем числа");

            int isbitok = 0;
            for(int i = 0; i < num1.Count; i++)
            {
                int result = num1[i] + num2[i] + isbitok;
                if (isbitok >= 1) isbitok -= 1;
                Console.WriteLine($"{num1[i]} + {num2[i]} = {result} в [{i + 1}] разряде  ");
                if (result >= based)
                {
                    Console.WriteLine("Возник избыток при сложении. Значит нужно будет прибавить 1 к следующему разряду");
                    Console.WriteLine($"Помимо этого записываем под {i + 1} разрядом {result} - {based} = {result - based}");
                    sumResult.Add(result - based);
                    isbitok += 1;
                }
                else
                {
                    Console.WriteLine($"Записываем под {i+1} разрядом {result}");
                    sumResult.Add(result);
                }
            }

            sumResult.Reverse();

            StringBuilder sb = new StringBuilder();
            foreach(var item in sumResult)
            {
                sb.Append(item.ToString());
            }
            Console.WriteLine($" {number1}");
            Console.WriteLine($"+");
            Console.WriteLine($" {number2}");
            
            Console.WriteLine($" {border.PadLeft(maxLen, '-')}");

            Console.WriteLine($" {sb.ToString()}");

            Console.WriteLine($"Результат: {sb.ToString()}");
        }

        private static void FifthFunction()
        {
            Console.WriteLine("Введите систему счисления для операции над числами:");
            string ss = Console.ReadLine();
            Console.WriteLine("Введите число:");
            string number = Console.ReadLine();
            Console.WriteLine("Введите вычитаемое:");
            string vichit = Console.ReadLine();

            if (!int.TryParse(ss, out var based) || !(based >= 1 && based <= 50)) throw new ArgumentException("Некорректная система счисления!");

            //Валидация своеобразная.
            int numberCorrected = ConvertFromAnyToDecWithoutComments(number, based);
            int vichitCorrected = ConvertFromAnyToDecWithoutComments(vichit, based);

            bool isNumberNegative = ConvertFromAnyToDecWithoutComments(number, based) < ConvertFromAnyToDecWithoutComments(vichit, based);

            List<int> numberList = new List<int>();
            List<int> vichitList = new List<int>();

            int maxLength = Math.Max(number.Length, vichit.Length);

            vichit = vichit.PadLeft(maxLength, '0');
            number = number.PadLeft(maxLength, '0');
            
            if (isNumberNegative)
            {
                Console.WriteLine($"Число {number} меньше {vichit}, поэтому разность будет отрицательной.");
                Console.WriteLine($"Поэтому просто вычтем из {vichit} число {number} и добавим минус спереди.");

                foreach (var i in number)
                {
                    numberList.Add(GetInt(i));
                }
                foreach (var j in vichit)
                {
                    vichitList.Add(GetInt(j));
                }
            }
            else
            {
                foreach (var i in number)
                    numberList.Add(GetInt(i));
                foreach (var j in vichit)
                    vichitList.Add(GetInt(j));

            }

            StringBuilder sb = new StringBuilder();
            if (!isNumberNegative)
            {
                for (int i = maxLength - 1; i >= 0; i--)
                {
                    Console.WriteLine($"Считаем разряд {i + 1}");

                    if (numberList[i] < vichitList[i])
                    {
                        if (numberList[i] >= 0)
                        {
                            Console.WriteLine($"{numberList[i]} меньше {vichitList[i]}, занимаем у левого разряда");
                            Console.WriteLine($" {numberList[i]} + {based} вычитаем {vichitList[i]} и получаем {numberList[i] + based - vichitList[i]}");
                        }
                        else
                        {
                            Console.WriteLine("Так как этот разряд первого числа равен нулю(ранее из него занимали), то занимаем у левого разряда");
                            Console.WriteLine($"{based - 1} - {vichitList[i]} = {based - 1 - vichitList[i]}");
                        }
                        //тут было numberList[i - 1] = numberList[i - 1] - 1;
                        numberList[i - 1] = numberList[i - 1] - 1;
                        numberList[i] += based;
                    }
                    else
                    {
                        Console.WriteLine($"Из {numberList[i]} - {vichitList[i]} = {numberList[i] - vichitList[i]}");
                    }
                    char resSub = ConvertNumberToSymbol(numberList[i] - vichitList[i]);

                    Console.WriteLine(resSub);
                    sb.Append(resSub);
                }
            }
            else
            {
                var temp = number;
                number = vichit;
                vichit = temp;

                var temp2 = numberList;
                numberList = vichitList;
                vichitList = temp2;


                for (int i = maxLength - 1; i >= 0; i--)
                {
                    Console.WriteLine($"Считаем разряд {i + 1}");

                    if (numberList[i] < vichitList[i])
                    {
                        if (numberList[i] >= 0)
                        {
                            Console.WriteLine($"{numberList[i]} меньше {vichitList[i]}, занимаем у левого разряда");
                            Console.WriteLine($" {numberList[i]} + {based} вычитаем {vichitList[i]} и получаем {numberList[i] + based - vichitList[i]}");
                        }
                        else
                        {
                            Console.WriteLine("Так как этот разряд первого числа равен нулю(ранее из него занимали), то занимаем у левого разряда");
                            Console.WriteLine($"{based - 1} - {vichitList[i]} = {based - 1 - vichitList[i]}");
                        }
                        //тут было numberList[i - 1] = numberList[i - 1] - 1;
                        numberList[i - 1] = numberList[i - 1] - 1;
                        numberList[i] += based;
                    }
                    else
                    {
                        Console.WriteLine($"Из {numberList[i]} - {vichitList[i]} = {numberList[i] - vichitList[i]}");
                    }
                    char resSub = ConvertNumberToSymbol(numberList[i] - vichitList[i]);

                    Console.WriteLine(resSub);
                    sb.Append(resSub);

                    CreateBorder();
                }
            }

            CreateBorder();
            if (isNumberNegative)
            {
                Console.WriteLine();
                Console.WriteLine(" " + vichit);
                Console.WriteLine("-");
                Console.WriteLine(" " + number.PadLeft(maxLength, '0'));
            }

            else
            {
                Console.WriteLine();
                Console.WriteLine(" " + number);
                Console.WriteLine("-");
                Console.WriteLine(" " + vichit.PadLeft(maxLength, '0'));
            }

            for (int i = 0; i <= maxLength; i++)
            {
                Console.Write("-");

            }

            string answer;
            if (isNumberNegative)
            {

                answer = "-" + new string(sb.ToString().Reverse().ToArray());
                Console.WriteLine($"\n {answer}");
            }
            else
            {

                answer = new string(sb.ToString().Reverse().ToArray());
                Console.WriteLine($"\n {answer}");

            }

            Console.WriteLine($"Ответ: {answer}") ;
            CreateBorder();
        }

        private static void SixthFunction()
        {
            /*
                34567
               * 8989
               ------
                 ===num1
                 ==num2=
                 =num3==
                 num4===
             
                34
               *10
               ---
                00
               34
               ---
               340
               число сверху умножается на каждый разряд числа снизу, при этом переводясь в СС. и так по колву разрядов, при этом смещаясь на единицу вправо.
             */

            Console.WriteLine("Введите систему счисления:");
            string ss = Console.ReadLine();
            if (!int.TryParse(ss, out int based) || !(based >= 1 && based <= 50)) throw new ArgumentException("Некорректная система счисления! Она должна быть от 1 до 50 включительно!");

            Console.WriteLine("Введите первое число:");
            string number1 = Console.ReadLine();

            Console.WriteLine("Введите второе число:");
            string number2 = Console.ReadLine();

            //Валидация
            int n1 = ConvertFromAnyToDecWithoutComments(number1, based);
            int n2 = ConvertFromAnyToDecWithoutComments(number2, based);

            List<int> num2 = new List<int>();

            List<int> multResultsInDec = new List<int>();
            List<string> multResultsInAny = new List<string>();
            foreach (var i in number2)
                num2.Add(GetInt(i));

            Console.WriteLine("Начинаем умножение");

            num2.Reverse();

           
            for(int i = 0; i < num2.Count; i++)
            {
                int currentRazryad = ConvertFromAnyToDecWithoutComments(number1, based) * num2[i];
                string displayedRazryad = ConvertFromDecToAnyWithoutComments(currentRazryad, based);
                Console.WriteLine($"{number1} * {ConvertNumberToSymbol(num2[i])} = {displayedRazryad}, где умножаем 1 число на число под [{i + 1}] разрядом.");
                multResultsInDec.Add(currentRazryad);
                multResultsInAny.Add(displayedRazryad);
            }
            
            List<string> finalResults = new List<string>();
            finalResults.Add(multResultsInAny[0].PadLeft(multResultsInAny[0].Length + multResultsInAny.Count - 1, '0'));
            for(int i = 1; i < multResultsInAny.Count; i++)
            {

                var result = multResultsInAny[i].PadLeft(finalResults[0].Length - i , '0');
                result = result.PadRight(finalResults[0].Length, '0');

                finalResults.Add(result);

            }
            Console.WriteLine("Получившиеся строки складываем поразрядно. Пример :");
            Console.WriteLine(number1.PadLeft(number1.Length + finalResults.Count, ' '));
            Console.WriteLine("*");
            Console.WriteLine(number2.PadLeft(number2.Length+finalResults.Count, ' '));
            Console.WriteLine(" " + "".PadLeft(finalResults[0].Length, '-'));
            foreach (var i in finalResults)
            {
                Console.WriteLine("+" + i);
            }

            Console.WriteLine(" " + "".PadLeft(finalResults[0].Length, '-'));

            Console.WriteLine(" " + ConvertFromDecToAnyWithoutComments(n1*n2, based));



        }
    }
}