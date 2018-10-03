using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarDll
{
    public static class GrammarParser
    {
        public static string Parse(string arg)
        {
            // возвращаемое значение
            StringBuilder retval = new StringBuilder();
            // перебираем символы входной строки
            for (int i = 0; i < arg.Length; i++)
            {
                if (arg[i] == '[' || arg[i] == ']')
                {
                    // ошибка в грамматике, строка начинается с [
                    throw new FormatException("строка не соответствует уловию, проверьте правильнность входной строки");
                }
                else if (char.IsDigit(arg[i]))
                {
                    // нам надо понять, где заканчивается число, поэтому продвигаемся вперед
                    int rep = -1;
                    for (int j = i + 1; j < arg.Length; j++)
                    {
                        if (char.IsDigit(arg[j]))
                        {
                            //если следующий символ число
                            continue;
                        }
                        else if (arg[j] == '[')
                        {
                            // число закончилось, получаем значение этого числа
                            rep = int.Parse(arg.Substring(i, j - i));
                            i = j;
                            break;
                        }
                        else
                        {
                            // встретился неожиданный символ
                            throw new FormatException("недопстимый формат строки");
                        }
                    }

                    //далее мы разбираем элемнт, который нам надо повторить rep раз и определить где заканчивается скобка
                    int counter = 1; //количество открытых скобок
                    // продвигаемся вперед, пока не обнулится скобка
                    for (int j = i + 1; j < arg.Length; j++)
                    {
                        if (arg[j] == '[')
                        {
                            // вложеная скобка, увеличиваем количество скобок на один
                            counter++;
                        }
                        else if (arg[j] == ']')
                        {
                            // закрытая скобка, уменьшаем счетчик
                            counter--;
                            if (counter == 0)
                            {
                                //когда счетчик обнулился, мы нашл именно нашу скобку закрытя, вычисляем внутренюю строку
                                if (arg[j - 1] == '[' && arg[j] == ']') { throw new FormatException("не верный формат строки, скобки имеют пустое значение"); } // Обрабатываем случай пустых скобок типа 10[]xy

                                //запускаем обработку подстроки
                                string fragment = Parse(arg.Substring(i + 1, j - i - 1));

                                // накапливаем этот фрагмент в возвращаемом значении указанное число раз
                                while (rep-- > 0) { retval.Append(fragment); }
                                // переопределяем индексы, чтобы адекватно продолжит разбор строки
                                i = j; // Именно j, а не j + 1, потому что переменная нарастится в рамках самого исходного цикла for
                                break; //выходм их цила, в котором считали подстроку
                            }
                        }
                    }

                    if (counter > 0) { throw new FormatException("неожиданный обрыв строки"); }
                }
                else if (char.IsLetter(arg[i]))
                {
                    // если у нас простой символ, записываем его
                    retval.Append(arg[i]);
                }
                else
                {
                    // Ошибка, неизвестный символ
                    throw new FormatException("незвестный формат символа");
                }
            }
            // возвращаем значение
            return retval.ToString();
        }
    }
}
