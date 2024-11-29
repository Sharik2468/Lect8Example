using System;
using System.Collections;
using System.Collections.Generic;

namespace Example
{
    // Класс узла дерева
    public class TreeNode<T>
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Children { get; set; }

        public TreeNode(T value)
        {
            Value = value;
            Children = new List<TreeNode<T>>();
        }

        public void AddChild(TreeNode<T> child)
        {
            Children.Add(child);
        }
    }

    // Класс дерева
    public class Tree<T>
    {
        public TreeNode<T> Root { get; set; }

        public Tree(T rootValue)
        {
            Root = new TreeNode<T>(rootValue);
        }

        // Рекурсивный обход дерева (DFS)
        public void Traverse(TreeNode<T> node, Action<T> action)
        {
            if (node == null) return;

            action(node.Value);

            foreach (var child in node.Children)
            {
                Traverse(child, action);
            }
        }
    }

    // Класс графа
    public class Graph<T>
    {
        private Dictionary<T, List<T>> adjacencyList;
        private bool isDirected;

        public Graph(bool isDirected = false)
        {
            this.isDirected = isDirected;
            adjacencyList = new Dictionary<T, List<T>>();
        }

        // Добавление узла
        public void AddNode(T node)
        {
            if (!adjacencyList.ContainsKey(node))
            {
                adjacencyList[node] = new List<T>();
            }
        }

        // Добавление ребра
        public void AddEdge(T from, T to)
        {
            AddNode(from);
            AddNode(to);
            adjacencyList[from].Add(to);

            if (!isDirected)
            {
                adjacencyList[to].Add(from);
            }
        }

        // Вывод графа
        public void PrintGraph()
        {
            foreach (var node in adjacencyList)
            {
                Console.Write(node.Key + ": ");
                foreach (var neighbor in node.Value)
                {
                    Console.Write(neighbor + " ");
                }
                Console.WriteLine();
            }
        }

        // Обход графа (DFS)
        public void DFS(T start, HashSet<T> visited = null)
        {
            if (visited == null) visited = new HashSet<T>();

            if (visited.Contains(start)) return;

            Console.WriteLine(start);
            visited.Add(start);

            foreach (var neighbor in adjacencyList[start])
            {
                DFS(neighbor, visited);
            }
        }

        // Обход графа (BFS)
        public void BFS(T start)
        {
            var visited = new HashSet<T>();
            var queue = new Queue<T>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                Console.WriteLine(current);

                foreach (var neighbor in adjacencyList[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }
            }
        }
    }

    // Пример использования дерева
    class Program
    {
        // Реализация быстрой сортировки
        static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                // Разделение массива
                int pivotIndex = Partition(array, left, right);

                // Рекурсивная сортировка левой и правой частей
                QuickSort(array, left, pivotIndex - 1);
                QuickSort(array, pivotIndex + 1, right);
            }
        }

        // Метод разделения массива
        static int Partition(int[] array, int left, int right)
        {
            int pivot = array[right]; // Выбираем последний элемент как опорный
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    Swap(array, i, j); // Обмен местами элементов
                }
            }

            // Перемещение опорного элемента на правильное место
            Swap(array, i + 1, right);

            return i + 1;
        }

        // Метод для обмена двух элементов
        static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        static int LinearSearch(int[] array, int target)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target)
                {
                    return i; // Возвращаем индекс найденного элемента
                }
            }
            return -1; // Если элемент не найден
        }

        static int BinarySearch(int[] array, int target)
        {
            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2; // Избегаем переполнения

                if (array[mid] == target)
                {
                    return mid; // Возвращаем индекс найденного элемента
                }
                else if (array[mid] < target)
                {
                    left = mid + 1; // Искомый элемент в правой половине
                }
                else
                {
                    right = mid - 1; // Искомый элемент в левой половине
                }
            }

            return -1; // Если элемент не найден
        }

        // Метод для записи данных в файл
        static void WriteToFile(string filePath, string[] data)
        {
            try
            {
                // Используем StreamWriter для записи текста в файл
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var line in data)
                    {
                        writer.WriteLine(line); // Записываем строку в файл
                    }
                }
                Console.WriteLine($"Данные успешно записаны в файл: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
            }
        }

        // Метод для чтения данных из файла
        static void ReadFromFile(string filePath)
        {
            try
            {
                // Проверяем, существует ли файл
                if (File.Exists(filePath))
                {
                    Console.WriteLine("\nСодержимое файла:");

                    // Используем StreamReader для чтения текста из файла
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); // Печатаем каждую строку на консоль
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Файл не найден: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении из файла: {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
            #region Массивы
            //string[] people = ["Tom", "Sam", "Bob"];

            //Console.WriteLine(people.Length);
            #endregion

            #region Список
            //var people = new List<string>() { "Tom", "Bob", "Sam" };

            //string firstPerson = people[0];
            //Console.WriteLine(firstPerson);
            //people[0] = "Mike";

            //people.Add("Artem");
            //people.AddRange(new[] { "Nikita", "Sasha" });
            //people.Insert(0, "Eugene");

            //people.Remove("Artem");
            //people.RemoveAt(1);
            //people.RemoveAll(person => person.Length == 3);
            //foreach (var person in people)
            //    Console.WriteLine(person);
            #endregion

            #region Стек
            //var peopleList = new List<string>() { "Tom", "Bob", "Sam" };
            //Stack<string> people = new Stack<string>(peopleList);

            //var people = new Stack<string>();
            //people.Push("Tom");
            //people.Push("Sam");
            //people.Push("Bob");

            //string headPerson = people.Peek();
            //Console.WriteLine(headPerson);

            //string person1 = people.Pop();
            //Console.WriteLine(person1);
            //string person2 = people.Pop();
            //Console.WriteLine(person2);
            //string person3 = people.Pop();
            //Console.WriteLine(person3);
            #endregion

            #region Оxередь
            //var peopleList = new List<string>() { "Tom", "Bob", "Sam" };
            //Stack<string> people = new Stack<string>(peopleList);

            //var people = new Queue<string>();
            //people.Enqueue("Tom");
            //people.Enqueue("Sam");
            //people.Enqueue("Bob");

            //string headPerson = people.Peek();
            //Console.WriteLine(headPerson);

            //string person1 = people.Dequeue();
            //Console.WriteLine(person1);
            //string person2 = people.Dequeue();
            //Console.WriteLine(person2);
            //string person3 = people.Dequeue();
            //Console.WriteLine(person3);
            #endregion

            #region Дерево
            //// Создание дерева с корнем "Root"
            //var tree = new Tree<string>("Root");

            //// Добавление дочерних узлов
            //var child1 = new TreeNode<string>("Child1");
            //var child2 = new TreeNode<string>("Child2");

            //tree.Root.AddChild(child1);
            //tree.Root.AddChild(child2);

            //// Добавление дочерних узлов к Child1
            //child1.AddChild(new TreeNode<string>("Grandchild1"));
            //child1.AddChild(new TreeNode<string>("Grandchild2"));

            //// Добавление дочерних узлов к Child2
            //child2.AddChild(new TreeNode<string>("Grandchild3"));

            //// Обход дерева
            //Console.WriteLine("Tree traversal:");
            //tree.Traverse(tree.Root, value => Console.WriteLine(value));
            #endregion

            #region Граф
            //// Создание графа (ориентированный)
            //var graph = new Graph<string>(isDirected: false);

            //// Добавление рёбер
            //graph.AddEdge("A", "B");
            //graph.AddEdge("A", "C");
            //graph.AddEdge("B", "D");
            //graph.AddEdge("C", "D");
            //graph.AddEdge("D", "E");

            //// Вывод графа
            //Console.WriteLine("Graph representation:");
            //graph.PrintGraph();

            //// DFS обход
            //Console.WriteLine("\nDFS Traversal:");
            //graph.DFS("A");

            //// BFS обход
            //Console.WriteLine("\nBFS Traversal:");
            //graph.BFS("A");
            #endregion

            #region Пузырьковая сортировка
            //int[] arr = { 800, 11, 50, 771, 649, 770, 240, 9 };

            //int temp = 0;

            //for (int write = 0; write < arr.Length; write++)
            //{
            //    for (int sort = 0; sort < arr.Length - 1; sort++)
            //    {
            //        if (arr[sort] > arr[sort + 1])
            //        {
            //            temp = arr[sort + 1];
            //            arr[sort + 1] = arr[sort];
            //            arr[sort] = temp;
            //        }
            //    }
            //}

            //for (int i = 0; i < arr.Length; i++)
            //    Console.Write(arr[i] + " ");
            #endregion

            #region Быстрая сортировка
            //int[] array = { 10, 7, 8, 9, 1, 5 };
            //Console.WriteLine("Исходный массив: " + string.Join(", ", array));

            //try
            //{
            //    QuickSort(array, 0, array.Length + 1);
            //}
            //catch (IndexOutOfRangeException ex) 
            //{
            //    Console.WriteLine($"Обработано исключение: {ex.Message}");
            //}

            //Console.WriteLine("Отсортированный массив: " + string.Join(", ", array));
            #endregion

            #region Линейный поиск
            //int[] array = { 4, 2, 7, 1, 9, 3 };
            //int target = 7;

            //int result = LinearSearch(array, target);

            //if (result != -1)
            //{
            //    Console.WriteLine($"Элемент найден на индексе {result}");
            //}
            //else
            //{
            //    Console.WriteLine("Элемент не найден");
            //}
            #endregion

            #region Бинарный поиск
            //int[] sortedArray = { 1, 2, 3, 4, 7, 9 };
            //int target = 7;

            //int result = BinarySearch(sortedArray, target);

            //if (result != -1)
            //{
            //    Console.WriteLine($"Элемент найден на индексе {result}");
            //}
            //else
            //{
            //    Console.WriteLine("Элемент не найден");
            //}
            #endregion

            #region Запись и чтение из файла
            //string filePath = "example.txt"; // Имя файла
            //string[] dataToWrite = {
            //    "Строка 1: Пример записи данных в файл",
            //    "Строка 2: Ещё один пример строки",
            //    "Строка 3: Последняя строка в файле"
            //};

            //// Запись данных в файл
            //WriteToFile(filePath, dataToWrite);

            //// Чтение данных из файла
            //ReadFromFile(filePath);
            #endregion
        }
    }
}
