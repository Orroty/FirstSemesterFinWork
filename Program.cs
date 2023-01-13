using System.Text;
using System.Xml.Serialization;

string[] arr = new string[2];
string[] arrResult = new string[1];

int n = 3; // количество символов, максимальных для внесение во второй массив
string splitter = ","; // разделитель при внесении данных в массив arr одной строкой

int counter = 0; // количество добавленных элементов в массив arr
int counterResult = 0; // количество добавленных элементов в массив arrResult


// Меню программы
void PrintMainMenu()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine($"В 1 массиве содержится {counter} элеметов");
        Console.WriteLine(new string('_', Console.WindowWidth));
        Console.WriteLine();
        Console.WriteLine(@$"Выберете необходимое действие :
             1 – Заполнить 1 массив строкой с использованрием разделителя (Старые значения не удаляются, а дополняются)
             2 – Изменить разделитель для первого пункта заполнения  (сейчас - '{splitter}')
             3 – Заполнить 1 массив по 1 элементу (Старые значения не удаляются, а дополняются)
             4 – Изменить значение n - длины строк заполняющих второй массив (сейчас {n}). При изменении 1 и второй массив будут отчищены.
             5 – Вывод массивов на экран
             6 - Очистка массива
             0 - Выход из программы");
        Console.WriteLine(new string('_', Console.WindowWidth));
        switch (char.ToLower(Console.ReadKey(true).KeyChar))
        {
            case '1': { AddMultiDataInOneLineToArray(); break; }
            case '2': { ChangeSplitter(); break; }
            case '3': { AddOneDataToArray(); break; }
            case '4': { ChangeNValue(); break; }
            case '5': { PrintAllArrays(); break; }
            case '6': { CleanArray(); break; }
            case '0': return;
        }
    }
}

// ввод строки 
// атрибут result - вводимая строка. Передается с модификатором out
// возвращает bool - если в конце нажат Enter - вернет true, если Esc - вернет false
bool GetOrEscape(out string result)
{
    result = "";
    StringBuilder c = new StringBuilder("");

    while (true) //ввод строки посимвольно до момента нажатия Enter или Esc
    {
        var buff = Console.ReadKey(true);
        if (buff.Key == ConsoleKey.Escape)  //отменить ввод
        {
            return false;
        }
        else if (buff.Key == ConsoleKey.Enter) //подтвердить ввод слова
        {
            result = c.ToString();
            Console.WriteLine("");
            return true;

        }
        else if (buff.Key == ConsoleKey.Backspace) //удалить последний символ
        {
            if (Console.CursorLeft > 0)
            {
                var cli = --Console.CursorLeft;
                c.Remove(cli, 1); //удаляем последний символ
                Console.CursorLeft = cli; // на 1 символ назад
                Console.Write(" ");//затираем лишний символ
                Console.CursorLeft = cli; // на 1 символ назад
            }
        }
        else // любой другой символ добавляется в строку
        {
            c.Append(buff.KeyChar);
            Console.Write(buff.KeyChar);
        }
    }
}

// Заполнение массива строк. Если размер массива недостаточен увеличивает размер массива на 1 
// атрибуты
// arr - измеяемый массив. Передается с модификатором ref
// value - строка, добавляемая в массив.
// counter - число - индекс на который добавляется строка в массиве
void AddToArray(ref string[] arr, string value, int counter)
{
    int size = arr.Length;
    if (counter >= size)
    {
        Array.Resize(ref arr, size + 1);
    }
    arr[counter] = value;
}

// Добавляет строку data в массив arr и удовлетворяющих условию длина строки <= n в массив arrResult
// атрибут data - строка для добавления в массивы
void AddToBothArrays(string data)
{
    AddToArray(ref arr, data, counter);

    if (arr[counter].Length <= n)
    {
        AddToArray(ref arrResult, arr[counter], counterResult);
        counterResult++;
    }
    counter++;
}

// Разбиение входящей строки по splitter
// атрибут data - строка для разделения
// возвращает массив строк - результат разделения
string[] SplitData(string data)
{
    return data.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
}

// Ввод строки, разбиение по сплитеру  непустой входной строки, добавление результатов разделения в массив arr  и удовлетворяющих условию длина строки <=n в массив arrResult. Выход осуществляется по нажатию клавиши Esc.
void AddMultiDataInOneLineToArray()
{
    string temp;
    bool b = true;
    while (b)
    {
        Console.Clear();
        Console.WriteLine($"Введите строку, которая будет разбита по разделителю (сейчас разделитель - '{splitter}') и добавлена в первый массив \n ВНИМАНИЕ: Учтите, что пробел это тоже сивол, поэтому ',' и ', ' 'это разные разделители. \n Для отмены ввода нажмите клавишу Esc");

        b = GetOrEscape(out temp);
        if (b)
        {
            if (temp != null && !temp.Equals(""))
            {
                var buff = SplitData(temp);

                foreach (string i in buff)
                {
                    AddToBothArrays(i);
                }
            }
            else
            {
                Console.WriteLine("Строка не должна быть пустой. \n Для продолжение нажмите любую клавишу.");
                Console.ReadKey();
            }
        }
    }
}

// Изменение значения переменной splitter. splitter - делитель, по которому будет делиться входная строка в первом пункте меню. 
void ChangeSplitter()
{
    Console.Clear();

    Console.WriteLine($"Введитеновый разделитель который будет применяться в 1 пункте меню (сейчас '{splitter}')  \n ВНИМАНИЕ: Учтите, что пробел это тоже сивол, поэтому ',' и ', ' 'это разные разделители. \n Для отмены ввода нажмите клавишу Esc");

    if (GetOrEscape(out string temp))
    {
        if (temp != null && !temp.Equals("")) //если ввел не пустую строку
        {
            splitter = temp;
        }
        else
        {
            Console.WriteLine("Разделитель не должен быть пустым \n Для возврата в меню нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}

// Ввод и добавление непустой входной строки в массив arr и удовлетворяющих условию длина строки <= n в массив arrResult. Выход осуществляется по нажатию клавиши Esc.
void AddOneDataToArray()
{
    string temp;
    bool b = true;
    while (b)
    {
        Console.Clear();
        Console.WriteLine($"Введите строку, которая будет добавлена в первый массив  \n Для отмены ввода нажмите клавишу Esc");

        b = GetOrEscape(out temp);
        if (b)
        {
            if (temp != null && !temp.Equals(""))
            {
                AddToBothArrays(temp);
            }
            else
            {
                Console.WriteLine("Строка не должна быть пустой. \n Для продолжение нажмите любую клавишу.");
                Console.ReadKey();
            }
        }
    }
}

// Изменение значения переменной n. n - максимальная длина строк, добавляемых во массив arrResult.
void ChangeNValue()
{
    Console.Clear();

    Console.WriteLine($"Введите значение n - максимальная длина строки добавляемая во второй массив (сейчас n = {n}) \n При изменении 1 и второй массив будут отчищены. \n Для отмены ввода нажмите клавишу Esc");

    if (GetOrEscape(out string temp))
    {
        int temp2 = 0;
        if (!Int32.TryParse(temp, out temp2)) //если ввел буквы, а не числа
        {
            Console.WriteLine("Ввод не является числом. Для возврата в меню нажмите любую клавишу");
            Console.ReadKey();
        }
        else
        {
            n = temp2;
        }
    }
}

// Выводит на печать массив строк
// атрибуты
// arr - массив для вывода на печать
// counter - до какого элемента выводить на печать
void PrintStringArray(string[] arr, int counter)
{
    for (int i = 0; i < counter; i++)
    {
        Console.Write((i + 1 + "/" + counter).PadRight(10));
        Console.WriteLine(" || " + arr[i]);
    }
}


// Вывод обоих массивов в консоль
void PrintAllArrays()
{
    Console.Clear();
    Console.WriteLine(new string('_', Console.WindowWidth));
    Console.WriteLine("     Первый массив:");
    PrintStringArray(arr, counter);
    Console.WriteLine(new string('_', Console.WindowWidth));
    Console.WriteLine("     Второй (итоговый) массив:");
    PrintStringArray(arrResult, counterResult);
    Console.WriteLine(new string('_', Console.WindowWidth));
    Console.WriteLine("\n Для возврата в меню нажмите любую клавишу");
    Console.ReadKey();
}

// очистка обоих массивов и счетчиков заполненных элементов
void CleanArray()
{
    arr = new string[2];
    arrResult = new string[1];
    counterResult = counter = 0;
}

PrintMainMenu();
