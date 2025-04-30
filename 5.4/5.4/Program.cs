using System;
using System.Linq;

class Program
{
    static void Main()
    {
        // Запит на вибір способу реалізації
        Console.WriteLine("Як ви хочете реалізувати задачу?");
        Console.WriteLine("1 - Використовувати структури");
        Console.WriteLine("2 - Використовувати кортежі");
        Console.WriteLine("3 - Використовувати записи");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
        {
            Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
        }

        switch (choice)
        {
            case 1:
                // Використовуємо структури
                RunWithStructures();
                break;
            case 2:
                // Використовуємо кортежі
                RunWithTuples();
                break;
            case 3:
                // Використовуємо записи
                RunWithRecords();
                break;
            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }

    // Реалізація через структури
    static void RunWithStructures()
    {
        Console.WriteLine("Вибір: Структури");

        // Введення масиву школярів
        SchoolboyStruct[] schoolboys = new SchoolboyStruct[]
        {
            new SchoolboyStruct("Іван Іванов", "10-А", "123456789", 5, 4, 2, 4),
            new SchoolboyStruct("Марія Петрівна", "10-Б", "987654321", 3, 4, 4, 4),
            new SchoolboyStruct("Олег Олегович", "9-А", "555555555", 2, 3, 5, 5)
        };

        // Видалення школярів з оцінкою 2 хоча б по одному предмету
        schoolboys = schoolboys.Where(s => s.MathGrade != 2 && s.PhysicsGrade != 2 && s.RussianGrade != 2 && s.LiteratureGrade != 2).ToArray();

        // Додавання нового елемента на початок масиву
        Array.Resize(ref schoolboys, schoolboys.Length + 1);
        schoolboys[0] = new SchoolboyStruct("Анатолій Антонов", "11-А", "123123123", 4, 4, 3, 5);

        // Виведення результатів
        Console.WriteLine("Школярі після модифікації:");
        foreach (var schoolboy in schoolboys)
        {
            Console.WriteLine($"Прізвище, ім'я, по батькові: {schoolboy.FullName}, Клас: {schoolboy.Class}, Телефон: {schoolboy.PhoneNumber}");
            Console.WriteLine($"Оцінки: Математика: {schoolboy.MathGrade}, Фізика: {schoolboy.PhysicsGrade}, Російська мова: {schoolboy.RussianGrade}, Література: {schoolboy.LiteratureGrade}");
            Console.WriteLine();
        }
    }

    // Реалізація через кортежі
    static void RunWithTuples()
    {
        Console.WriteLine("Вибір: Кортежі");

        // Введення масиву школярів у вигляді кортежів
        var schoolboys = new (string FullName, string Class, string PhoneNumber, int MathGrade, int PhysicsGrade, int RussianGrade, int LiteratureGrade)[]
        {
            ("Іван Іванов", "10-А", "123456789", 5, 4, 2, 4),
            ("Марія Петрівна", "10-Б", "987654321", 3, 4, 4, 4),
            ("Олег Олегович", "9-А", "555555555", 2, 3, 5, 5)
        };

        // Видалення школярів з оцінкою 2 хоча б по одному предмету
        schoolboys = schoolboys.Where(s => s.MathGrade != 2 && s.PhysicsGrade != 2 && s.RussianGrade != 2 && s.LiteratureGrade != 2).ToArray();

        // Додавання нового елемента на початок масиву
        var newSchoolboy = ("Анатолій Антонов", "11-А", "123123123", 4, 4, 3, 5);
        schoolboys = new[] { newSchoolboy }.Concat(schoolboys).ToArray();

        // Виведення результатів
        Console.WriteLine("Школярі після модифікації:");
        foreach (var schoolboy in schoolboys)
        {
            Console.WriteLine($"Прізвище, ім'я, по батькові: {schoolboy.FullName}, Клас: {schoolboy.Class}, Телефон: {schoolboy.PhoneNumber}");
            Console.WriteLine($"Оцінки: Математика: {schoolboy.MathGrade}, Фізика: {schoolboy.PhysicsGrade}, Російська мова: {schoolboy.RussianGrade}, Література: {schoolboy.LiteratureGrade}");
            Console.WriteLine();
        }
    }

    // Реалізація через записи
    static void RunWithRecords()
    {
        Console.WriteLine("Вибір: Записи");

        // Введення масиву школярів у вигляді записів
        var schoolboys = new[]
        {
            new SchoolboyRecord("Іван Іванов", "10-А", "123456789", 5, 4, 2, 4),
            new SchoolboyRecord("Марія Петрівна", "10-Б", "987654321", 3, 4, 4, 4),
            new SchoolboyRecord("Олег Олегович", "9-А", "555555555", 2, 3, 5, 5)
        };

        // Видалення школярів з оцінкою 2 хоча б по одному предмету
        schoolboys = schoolboys.Where(s => s.MathGrade != 2 && s.PhysicsGrade != 2 && s.RussianGrade != 2 && s.LiteratureGrade != 2).ToArray();

        // Додавання нового елемента на початок масиву
        var newSchoolboy = new SchoolboyRecord("Анатолій Антонов", "11-А", "123123123", 4, 4, 3, 5);
        schoolboys = new[] { newSchoolboy }.Concat(schoolboys).ToArray();

        // Виведення результатів
        Console.WriteLine("Школярі після модифікації:");
        foreach (var schoolboy in schoolboys)
        {
            Console.WriteLine($"Прізвище, ім'я, по батькові: {schoolboy.FullName}, Клас: {schoolboy.Class}, Телефон: {schoolboy.PhoneNumber}");
            Console.WriteLine($"Оцінки: Математика: {schoolboy.MathGrade}, Фізика: {schoolboy.PhysicsGrade}, Російська мова: {schoolboy.RussianGrade}, Література: {schoolboy.LiteratureGrade}");
            Console.WriteLine();
        }
    }
}

// Структура для варіанту з використанням структур
struct SchoolboyStruct
{
    public string FullName;
    public string Class;
    public string PhoneNumber;
    public int MathGrade;
    public int PhysicsGrade;
    public int RussianGrade;
    public int LiteratureGrade;

    public SchoolboyStruct(string fullName, string className, string phoneNumber, int mathGrade, int physicsGrade, int russianGrade, int literatureGrade)
    {
        FullName = fullName;
        Class = className;
        PhoneNumber = phoneNumber;
        MathGrade = mathGrade;
        PhysicsGrade = physicsGrade;
        RussianGrade = russianGrade;
        LiteratureGrade = literatureGrade;
    }
}

// Запис для варіанту з використанням записів
record SchoolboyRecord(string FullName, string Class, string PhoneNumber, int MathGrade, int PhysicsGrade, int RussianGrade, int LiteratureGrade);
