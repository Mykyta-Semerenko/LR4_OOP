
using System;

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
            {
                return middle;
            }
            else if (array[middle] < key)
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }

        return -1;
    }

    static int CountOccurrences(int[] array, int key)
    {
        int index = BinarySearch(array, key);

        if (index == -1)
        {
            return 0;
        }

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
        Console.WriteLine("Розмір масиву N = " + N);

        Console.WriteLine("\nПочатковий масив:");
        Console.WriteLine(string.Join(" ", array));

        InsertionSort(array);

        Console.WriteLine("\nВідсортований масив:");
        Console.WriteLine(string.Join(" ", array));

        Console.Write("\nВведіть число для пошуку: ");

        if (!int.TryParse(Console.ReadLine(), out int key))
        {
            Console.WriteLine("Помилка! Потрібно ввести ціле число.");
            return;
        }
        int result = BinarySearch(array, key);
        int count = CountOccurrences(array, key);
        if (result != -1)
        {
            Console.WriteLine("\nРезультат пошуку:");
            Console.WriteLine("Число " + key + " знайдено.");
            Console.WriteLine("Індекс одного з входжень: " + result);
            Console.WriteLine("Кількість входжень: " + count);
        }
        else
        {
            Console.WriteLine("\nЧисло " + key + " у масиві не знайдено.");
            Console.WriteLine("Кількість входжень: 0");
        }

        Console.WriteLine("\nНатисніть Enter для завершення...");
        Console.ReadLine();
    }
}
