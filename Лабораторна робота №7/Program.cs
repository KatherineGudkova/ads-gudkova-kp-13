namespace Лабораторна7
{
    public class Program
    {
        static Random random = new Random();
        const double GOLDEN = 0.618;
        //місце входу в програму
        static void Main(string[] args)
        {
            //запуск програми 
            Console.WriteLine("Натиснiть Enter, щоб iнiцiалiзувати таблицю");
            Console.ReadLine();

            HashTable table = InitHashTable();
            InitializeStartPanel(table);
        }

        //ініціалізація нової геш-таблиці
        static private HashTable InitHashTable()
        {
            HashTable table = new HashTable();
            table.Size = 5;
            table.Table = new Entry[5];

            return table;
        }

        //ініціалізація консольної панелі управління
        static private void InitializeStartPanel(HashTable table)
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Головне меню");
            Console.WriteLine("Введiть одне з наступних ключових слiв:");
            Console.WriteLine("Insert - додати новий елмент в таблицю");
            Console.WriteLine("Find - знайти один з елементiв");
            Console.WriteLine("Remove - видалити один з елементiв");
            Console.WriteLine("Update - оновити дисциплiни");
            Console.WriteLine("End - завершити виконання програми");
            Console.Write("\nВиконати команду ");

            string command = Console.ReadLine();
            switch (command)
            {
                case "Insert":
                    table = InsertEntryEvent(table);
                    break;

                case "Find":
                    FindEntryEvent(table);
                    break;

                case "Remove":
                    table = RemoveEntryEvent(table);
                    break;

                case "Update":
                    table = UpdateDisEvent(table);
                    break;

                case "End":
                    return;

                default:
                    Console.WriteLine("\n---Помилка команди---");
                    InitializeStartPanel(table);
                    break;
            }
            Console.WriteLine();
            InitializeStartPanel(table);
        }

        //івент додавання нового Entry
        static private HashTable InsertEntryEvent(HashTable table)
        {
            //створення Key
            Key key = ReadKey();

            //створення Value
            Value value = new Value();

            char id1 = (char)random.Next('A', 'Z');
            char id2 = (char)random.Next('A', 'Z');
            int id3 = random.Next(10000, 100000);
            value.StudentID = Convert.ToString(id1) + Convert.ToString(id2) + Convert.ToString(id3);

            Console.WriteLine("\nУведiть день народження:");
            int day = int.Parse(Console.ReadLine());
            Console.WriteLine("Уведiть мiсяць народження:");
            int month = int.Parse(Console.ReadLine());
            Console.WriteLine("Уведiть рiк народження:");
            int year = int.Parse(Console.ReadLine());

            value.Birth = new Date(year, month, day);

            Console.WriteLine("Уведiть рiк вступу:");
            value.YearOfEntry = int.Parse(Console.ReadLine());

            Console.WriteLine("Уведiть адресу:");
            value.Address = Console.ReadLine();

            Console.WriteLine("\nДисциплiни");
            value = ReadDisciplines(value);

            int gradeSum = 0;
            foreach (KeyValuePair<string, int> discipline in value.Disciplines)
            {
                gradeSum += discipline.Value;
            }
            value.AverageGrade = gradeSum * 1.0 / value.Disciplines.Count;

            //створення Entry
            Entry entry = new Entry();
            entry.Key = key;
            entry.Value = value;

            InsertEntry(entry, table);
            if (table.Loadness * 1.0 / table.Size > GOLDEN)
            {
                table = Rehashing(table);
            }

            Console.WriteLine("---Студента успiшно додано до таблицi---");
            return table;
        }

        //зчитування дисциплін
        static private Value ReadDisciplines(Value value)
        {
            try
            {
                Console.WriteLine("Уведiть кiлькiсть дисциплiн, що хочете додати (вiд 1 до 10 включно): ");
                int disciplinesAmount = int.Parse(Console.ReadLine());
                if (disciplinesAmount <= 0 || disciplinesAmount > 10)
                {
                    Console.WriteLine("Обране число лежить поза межами дiапазону");
                    value = ReadDisciplines(value);
                }
                else
                {
                    for (int i = 0; i < disciplinesAmount; i++)
                    {
                        Console.WriteLine($"Уведiть назву дисциплiни пiд номером {i + 1}: ");
                        string disName = Console.ReadLine();

                        Console.WriteLine($"Уведiть бал з дисциплiни пiд номером {i + 1} (вiд 1 до 100): ");
                        int disGrade = int.Parse(Console.ReadLine());

                        value.Disciplines.Add(disName, disGrade);
                    }
                }
            }
            catch
            {
                Console.WriteLine("\n---Помилка вводу---");
                value = ReadDisciplines(value);
            }

            return value;
        }

        //додавання Entry до геш-таблиці
        static private HashTable InsertEntry(Entry entry, HashTable table)
        {
            long index = GetHash(entry.Key, table.Size);

            Entry checkEntry = table.Table[index];
            if (checkEntry == null)
            {
                table.Table[index] = entry;
            }
            else
            {
                Entry tempEntry = checkEntry.NextEntry;
                while (tempEntry != null)
                {
                    checkEntry = tempEntry;
                    tempEntry = tempEntry.NextEntry;
                }
                checkEntry.NextEntry = entry;
            }

            table.Loadness++;
            return table;
        }

        //рехешування геш-таблиці
        static private HashTable Rehashing(HashTable table)
        {
            int newSize = CalculatePrimeNumber(table.Size);
            HashTable tempTable = new HashTable();
            tempTable.Size = newSize;
            tempTable.Table = new Entry[newSize];

            for (int i = 0; i < table.Size; i++)
            {
                Entry tempEntry = table.Table[i];
                while (tempEntry != null)
                {
                    while (tempEntry.NextEntry != null)
                    {
                        tempEntry = tempEntry.NextEntry;
                    }
                    table = RemoveEntry(tempEntry.Key, table);
                    tempTable = InsertEntry(tempEntry, tempTable);
                    tempEntry = table.Table[i];
                }
            }

            table = tempTable;
            return table;
        }

        //івент пошуку Entry
        static private void FindEntryEvent(HashTable table)
        {
            Key key = ReadKey();

            Entry entry = FindEntry(key, table);

            //виведення Value у консоль
            if (entry != null)
            {
                Value value = entry.Value;

                Console.WriteLine($"\nID студента {value.StudentID}");
                Console.WriteLine($"Дата народження студента: {value.Birth.Day}-{value.Birth.Month}-{value.Birth.Year}");
                Console.WriteLine($"Адреса студента: {value.Address}");
                Console.WriteLine($"Рiк вступу студента: {value.YearOfEntry}");
                Console.WriteLine("\nДисциплiни:");
                foreach (KeyValuePair<string, int> pair in value.Disciplines)
                {
                    Console.WriteLine($"Назва: {pair.Key}");
                    Console.WriteLine($"Бал: {pair.Value}");
                }
                Console.WriteLine($"\nСередня оцiнка: {Math.Round(value.AverageGrade, 1)}");
            }
            else
            {
                Console.WriteLine("---Студента не знайдено---");
            }

        }

        //знаходження Entry за значенням Key
        static private Entry FindEntry(Key key, HashTable table)
        {
            long index = GetHash(key, table.Size);
            Entry tempEntry = table.Table[index];

            while (tempEntry != null)
            {
                if (tempEntry.Key.FirstName == key.FirstName && tempEntry.Key.LastName == key.LastName)
                {
                    return tempEntry;
                }
                tempEntry = tempEntry.NextEntry;
            }
            return tempEntry;
        }

        //івент видалення Entry
        static private HashTable RemoveEntryEvent(HashTable table)
        {
            int initialLoadness = table.Loadness;

            //створення Key
            Key key = ReadKey();

            table = RemoveEntry(key, table);
            if (initialLoadness == table.Loadness)
            {
                Console.WriteLine("---Пару не знайдено---");
            }
            else
            {
                Console.WriteLine("---Успiшно видалено---");
            }
            return table;
        }

        //видалення Entry з геш-таблиці за Key
        static private HashTable RemoveEntry(Key key, HashTable table)
        {
            long index = GetHash(key, table.Size);
            Entry tempEntry = table.Table[index];

            if (tempEntry != null)
            {
                if (tempEntry.Key.FirstName == key.FirstName && tempEntry.Key.LastName == key.LastName)
                {
                    table.Table[index] = tempEntry.NextEntry;
                    table.Loadness--;
                }
                else
                {
                    Entry checkEntry = tempEntry;
                    while (true)
                    {
                        tempEntry = tempEntry.NextEntry;
                        if (tempEntry.Key.FirstName == key.FirstName && tempEntry.Key.LastName == key.LastName)
                        {
                            checkEntry.NextEntry = tempEntry.NextEntry;
                            table.Loadness--;
                            break;
                        }
                        else if (tempEntry == null)
                        {
                            break;
                        }
                        checkEntry = tempEntry;
                    }
                }
            }

            return table;
        }

        //івент оновлення словника дисциплін
        static private HashTable UpdateDisEvent(HashTable table)
        {
            try
            {
                Key key = ReadKey();
                Value value = FindEntry(key, table).Value;

                //виведення поточного списку дисциплін
                Console.WriteLine("Поточний список дисциплiн");
                foreach (KeyValuePair<string, int> pair in value.Disciplines)
                {
                    Console.WriteLine($"Назва: {pair.Key}");
                    Console.WriteLine($"Бал: {pair.Value}");
                }

                UpdateDisPanel(value, table);
                return table;
            }
            catch
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Помилка команди");
                InitializeStartPanel(table);
                return table;
            }
        }

        //панель взаємодії з дисциплінами
        static private void UpdateDisPanel(Value value, HashTable table)
        {
            Console.WriteLine("-----------------\nОберіть одну із запропонованих команд:");
            Console.WriteLine("Update - змінити бал однієї з дисциплін");
            Console.WriteLine("Delete - видалити дисципліну");
            Console.WriteLine("Insert - додати нову дисципліну");
            Console.WriteLine("Menu - повернутись до меню");

            string command = Console.ReadLine();
            switch (command)
            {
                case "Update":
                    value = UpdateDiscipline(value, table);
                    break;

                case "Delete":
                    value = DeleteDiscipline(value, table);
                    break;

                case "Insert":
                    value = InsertDiscipline(value);
                    break;

                case "Menu":
                    InitializeStartPanel(table);
                    break;

                default:
                    Console.WriteLine("\n---Помилка команди---");
                    UpdateDisPanel(value, table);
                    break;
            }
            Console.WriteLine();
            UpdateDisPanel(value, table);
        }

        //оновлення дисципліни
        static private Value UpdateDiscipline(Value value, HashTable table)
        {
            Console.WriteLine("Уведiть назву дисциплiни, бал з якої хочете оновити");
            string discipline = Console.ReadLine();

            bool disciplineExists = false;
            foreach (KeyValuePair<string, int> pair in value.Disciplines)
            {
                if (pair.Key == discipline)
                {
                    disciplineExists = true;
                    value.Disciplines.Remove(discipline);

                    Console.WriteLine("Уведiть новий бал (цiле число вiд 0 до 100):");
                    int grade = int.Parse(Console.ReadLine());
                    value.Disciplines.Add(pair.Key, grade);

                    int gradeSum = 0;
                    foreach (KeyValuePair<string, int> _discipline in value.Disciplines)
                    {
                        gradeSum += _discipline.Value;
                    }
                    value.AverageGrade = gradeSum * 1.0 / value.Disciplines.Count;

                    break;
                }
            }

            if (!disciplineExists)
            {
                Console.WriteLine("Дисциплiну не знайдено");
                UpdateDisPanel(value, table);
            }

            return value;
        }

        //видалення дисципліни
        static private Value DeleteDiscipline(Value value, HashTable table)
        {
            Console.WriteLine("Уведiть назву дисциплiни, що хочете видалити");
            string discipline = Console.ReadLine();

            bool disciplineExists = false;
            foreach (KeyValuePair<string, int> pair in value.Disciplines)
            {
                if (pair.Key == discipline)
                {
                    disciplineExists = true;
                    value.Disciplines.Remove(discipline);

                    int gradeSum = 0;
                    foreach (KeyValuePair<string, int> _discipline in value.Disciplines)
                    {
                        gradeSum += _discipline.Value;
                    }
                    value.AverageGrade = gradeSum * 1.0 / value.Disciplines.Count;

                    break;
                }
            }

            if (!disciplineExists)
            {
                Console.WriteLine("Дисциплiну не знайдено");
                UpdateDisPanel(value, table);
            }

            return value;
        }

        //додавання дисципліни
        static private Value InsertDiscipline(Value value)
        {
            Console.WriteLine($"Уведiть назву нової дисциплiни: ");
            string disName = Console.ReadLine();

            Console.WriteLine($"Уведiть бал з цiєї дисциплiни(вiд 1 до 100): ");
            int disGrade = int.Parse(Console.ReadLine());

            value.Disciplines.Add(disName, disGrade);

            int gradeSum = 0;
            foreach (KeyValuePair<string, int> _discipline in value.Disciplines)
            {
                gradeSum += _discipline.Value;
            }
            value.AverageGrade = gradeSum * 1.0 / value.Disciplines.Count;

            return value;
        }

        //зчитування ключа
        static private Key ReadKey()
        {
            Key key = new Key();

            Console.WriteLine("--------------------------------------------------------\nУведiть FirstName (латинськi лiтери):");
            key.FirstName = Console.ReadLine();
            Console.WriteLine("Уведiть LastName (латинськi лiтери):");
            key.LastName = Console.ReadLine();

            return key;
        }

        //знаходить найближче просте число до подвійного добутка почтакового
        static private int CalculatePrimeNumber(int initialNumber)
        {
            bool isPrime = false;
            int newNumber = initialNumber * 2;

            while (!isPrime)
            {
                newNumber++;
                isPrime = true;

                for (int i = 0; i < Math.Sqrt(newNumber) + 1; i++)
                {
                    if (newNumber % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
            }

            return newNumber;
        }

        //знаходження числового вираження ключа
        static private long HashCode(Key key)
        {
            char[] alphabet = new char[]
               {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
                'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};

            string textKey = key.FirstName + key.LastName;
            long numberKey = 0;

            for (int i = 0; i < textKey.Length; i++)
            {
                int alphabetIndex = -1;
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (alphabet[j] == char.ToLower(textKey[i]))
                    {
                        alphabetIndex = j;
                        break;
                    }
                }
                numberKey += (alphabetIndex + 1) * (long)Math.Pow(alphabet.Length, textKey.Length - 1 - i);
            }

            return numberKey;
        }

        //знаходження індексу для ключа у геш-таблиці
        static private long GetHash(Key key, int tableSize)
        {
            long numberKey = HashCode(key);

            //обчислення індексу за модульною арифметикою
            long index = numberKey % tableSize;
            return index;
        }
    }
}