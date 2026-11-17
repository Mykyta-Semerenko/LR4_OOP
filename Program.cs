
using System;
using System.Diagnostics;

class Program
{

    static void InsertionSort(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            int key = array[i];
            int j = i - 1;

            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }

            array[j + 1] = key;
        }
    }

    static int BinarySearch(int[] array, int key)
    {
        int left = 0;
        int right = array.Length - 1;

        while (left <= right)
        {
            int middle = left + (right - left) / 2;

            if (array[middle] == key)
                return middle;

            if (array[middle] < key)
                left = middle + 1;
            else
                right = middle - 1;
        }

        return -1;
    }
    static int CountOccurrences(int[] array, int key)
    {
        int index = BinarySearch(array, key);

        if (index == -1)
            return 0;

        int count = 0;

        int i = index;

        while (i >= 0 && array[i] == key)
        {
            count++;
            i--;
        }

        i = index + 1;

        while (i < array.Length && array[i] == key)
        {
            count++;
            i++;
        }

        return count;
    }
    static int FindFirst(int[] array, int key)
    {
        int left = 0;
        int right = array.Length - 1;
        int result = -1;

        while (left <= right)
        {
            int middle = left + (right - left) / 2;

            if (array[middle] >= key)
            {
                if (array[middle] == key)
                    result = middle;

                right = middle - 1;
            }
            else
            {
                left = middle + 1;
            }
        }

        return result;
    }

    static int FindLast(int[] array, int key)
    {
        int left = 0;
        int right = array.Length - 1;
        int result = -1;

        while (left <= right)
        {
            int middle = left + (right - left) / 2;

            if (array[middle] <= key)
            {
                if (array[middle] == key)
                    result = middle;

                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }

        return result;
    }
    static int CountOccurrencesOptimized(int[] array, int key)
    {
        int first = FindFirst(array, key);

        if (first == -1)
            return 0;

        int last = FindLast(array, key);

        return last - first + 1;
    }
    static double MeasureSortingTime(int[] source, int repetitions)
    {
        int[][] testArrays = new int[repetitions][];

        for (int i = 0; i < repetitions; i++)
        {
            testArrays[i] = (int[])source.Clone();
        }

        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();

        for (int i = 0; i < repetitions; i++)
        {
            InsertionSort(testArrays[i]);
        }

        stopwatch.Stop();

        return stopwatch.Elapsed.TotalMilliseconds / repetitions;
    }
    static double MeasureSearchTime(int[] array, int key, int repetitions)
    {
        int result = -1;

        for (int i = 0; i < 1000; i++)
        {
            result = BinarySearch(array, key);
        }

        Stopwatch stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < repetitions; i++)
        {
            result = BinarySearch(array, key);
        }

        stopwatch.Stop();

        // Використовуємо результат пошуку
        if (result == int.MinValue)
            Console.WriteLine(result);

        return stopwatch.Elapsed.TotalMilliseconds / repetitions;
    }
    static double MeasureCountTime(
        int[] array,
        int key,
        int repetitions,
        bool optimized)
    {
        int result = 0;

        for (int i = 0; i < 1000; i++)
        {
            result = optimized
                ? CountOccurrencesOptimized(array, key)
                : CountOccurrences(array, key);
        }

        Stopwatch stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < repetitions; i++)
        {
            result = optimized
                ? CountOccurrencesOptimized(array, key)
                : CountOccurrences(array, key);
        }

        stopwatch.Stop();

        if (result < 0)
            Console.WriteLine(result);

        return stopwatch.Elapsed.TotalMilliseconds / repetitions;
    }

    static void RunPerformanceTests()
    {
        int[] sizes = { 32, 1000, 5000, 10000 };

        Random random = new Random(21);

        Console.WriteLine("\n======================================");
        Console.WriteLine("     ВИМІРЮВАННЯ ЧАСУ ВИКОНАННЯ");
        Console.WriteLine("======================================");

        foreach (int size in sizes)
        {
            int[] randomArray = new int[size];

            for (int i = 0; i < size; i++)
            {
                randomArray[i] = random.Next(10, 100001);
            }

            int[] sortedArray = (int[])randomArray.Clone();
            Array.Sort(sortedArray);

            int[] reversedArray = (int[])sortedArray.Clone();
            Array.Reverse(reversedArray);

            int repetitions = size <= 32 ? 10000 :
                              size <= 1000 ? 1000 :
                              size <= 5000 ? 50 : 20;

            Console.WriteLine("\n--------------------------------------");
            Console.WriteLine($"Розмір масиву: {size}");
            Console.WriteLine("--------------------------------------");

            double randomTime =
                MeasureSortingTime(randomArray, repetitions);

            double sortedTime =
                MeasureSortingTime(sortedArray, repetitions);

            double reversedTime =
                MeasureSortingTime(reversedArray, repetitions);

            Console.WriteLine(
                $"Випадковий масив: {randomTime:F6} мс");

            Console.WriteLine(
                $"Відсортований масив: {sortedTime:F6} мс");

            Console.WriteLine(
                $"Зворотний порядок: {reversedTime:F6} мс");

            int searchKey = sortedArray[size / 2];

            double searchTime =
                MeasureSearchTime(sortedArray, searchKey, 100000);

            Console.WriteLine(
                $"Бінарний пошук: {searchTime:F8} мс");

            double normalCountTime =
                MeasureCountTime(sortedArray, searchKey, 10000, false);

            double optimizedCountTime =
                MeasureCountTime(sortedArray, searchKey, 10000, true);

            Console.WriteLine(
                $"Звичайний підрахунок: {normalCountTime:F8} мс");

            Console.WriteLine(
                $"Оптимізований підрахунок: {optimizedCountTime:F8} мс");
        }

        Console.WriteLine("\n======================================");
        Console.WriteLine("       ТЕСТУВАННЯ ЗАВЕРШЕНО");
        Console.WriteLine("======================================");
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        int K = 21;

        int N = (int)(20 + 0.6 * K);

        int[] array = new int[N];

        Random random = new Random();
        for (int i = 0; i < N; i++)
        {
            array[i] = random.Next(10, 101);
        }
        
        Console.WriteLine($"\nРозмір масиву N = {N}");
        Console.WriteLine("\nПочатковий масив:");

        Console.WriteLine(string.Join(" ", array));
        InsertionSort(array);
        Console.WriteLine("\nВідсортований масив:");

        Console.WriteLine(string.Join(" ", array));
        Console.Write("\nВведіть число для пошуку: ");

        if (!int.TryParse(Console.ReadLine(), out int key))
        {
            Console.WriteLine("Помилка! Введіть ціле число.");
            return;
        }
        int result = BinarySearch(array, key);
        int count = CountOccurrences(array, key);
        int optimizedCount = CountOccurrencesOptimized(array, key);

        Console.WriteLine("\n======================================");
        Console.WriteLine("             РЕЗУЛЬТАТ");
        Console.WriteLine("======================================");

        if (result != -1)
        {
            Console.WriteLine($"Число {key} знайдено!");

            Console.WriteLine(
                $"Індекс одного з входжень: {result}");

            Console.WriteLine(
                $"Кількість входжень: {count}");

            Console.WriteLine(
                $"Оптимізований підрахунок: {optimizedCount}");
        }
        else
        {
            Console.WriteLine($"Число {key} не знайдено.");

            Console.WriteLine("Кількість входжень: 0");
        }
        if (count == optimizedCount)
        {
            Console.WriteLine(
                "\nПеревірка оптимізації: успішна.");
        }
        else
        {
            Console.WriteLine(
                "\nПомилка під час оптимізованого підрахунку.");
        }
        Console.WriteLine("\nБажаєте провести тестування продуктивності?");
        Console.WriteLine("1 - Так");
        Console.WriteLine("0 - Ні");

        Console.Write("Ваш вибір: ");

        string choice = Console.ReadLine() ?? "";

        if (choice == "1")
        {
            RunPerformanceTests();
        }

        Console.WriteLine("\nПрограму завершено.");
        Console.ReadLine();
    }
}
