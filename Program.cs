using System.Text;


string[] arr = new string[2];
string[] arrResult = null;

int n = 3;
string splitter = ",";

int counter = 0; // количество добавленных элементов в массив



string c = "";
bool b = true;



//Меню программы
void PrintMainMenu()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine($"В 1 массиве содержится {counter} элеметов");
        Console.WriteLine(@$"Выберете необходимое действие :
                1 – Заполнить 1 массив строкой с использованрием разделителя (Старые значения не удаляются, а дополняются)
                2 – Изменить разделитель для первого пункта заполнения  (сейчас - '{splitter}')
                3 – Заполнить 1 массив по 1 элементу (Старые значения не удаляются, а дополняются)
                4 – Изменить значение n - длины строк заполняющих второй массив (сейчас {n})
                5 – Вывод массивов на экран
                6 - Очистка массива
                0 - Выход из программы");;
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




void CleanArray()
{
    arr = new string[2];
    counter = 0;
}



string[] buff = new string[2];
Console.WriteLine();
int count = 0;

while (b)
{
    if (buff != null && buff[0] != null)
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(i+1 + "/" + count + " || " + buff[i]);
        }
    }
    Console.WriteLine("------------------------------------");
    Console.WriteLine("Введите число");
    b = GetOrEscape(out c);
    Console.Clear();
    AddToArray(ref buff, c, count);

    count++;
    Console.Clear();
}



Console.ReadKey();