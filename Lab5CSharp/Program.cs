using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Виберіть завдання:");
        Console.WriteLine("1 - Завдання 1 та 2");
        Console.WriteLine("3 - Завдання 3");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                // Завдання 1 та 2
                RunTask1And2();
                break;

            case 2:
                // Завдання 1 та 2
                RunTask1And2();
                break;

            case 3:
                // Завдання 3
                RunTask3();
                break;

            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }

    // Завдання 1 та 2
    static void RunTask1And2()
    {
        Console.WriteLine("Завдання 1 та 2");

        // Завдання 1: Ієрархія класів
        // Замінити створення PrintedEdition на конкретний клас (наприклад, Journal)
        PrintedEdition printedEdition = new Journal("Журнал наука", 2025, "Іван Іванов");
        printedEdition.Show();
        Console.WriteLine();

        Journal journal = new Journal("Технічний журнал", 2023, "Іван Іванов");
        journal.Show();
        Console.WriteLine();

        Book book = new Book("Математика для школярів", 2021, "Олексій Петренко");
        book.Show();
        Console.WriteLine();

        Textbook textbook = new Textbook("Основи фізики", 2020, "Михайло Шевченко", "Фізика");
        textbook.Show();
        Console.WriteLine();

        // Завдання 2: Конструктори і деструктори
        Console.WriteLine("Виклик деструкторів:");
        GC.Collect();  // Змушує збирач сміття спрацювати та викликати деструктори
    }

    // Завдання 3
    static void RunTask3()
    {
        Console.WriteLine("Завдання 3");

        List<Product> products = new List<Product>();

        // Створення товарів
        Product product1 = new ProductItem("Молоко", 20.5m, new DateTime(2025, 3, 1), new DateTime(2025, 4, 10));
        Product product2 = new Batch("Пакет цукру", 15.0m, new DateTime(2025, 2, 15), new DateTime(2025, 5, 10), 100);
        Product product3 = new Set("Набір для чаю", 50.0m, new DateTime(2025, 1, 20), new DateTime(2025, 6, 1), new List<Product> { product1, product2 });

        // Додавання товарів в масив
        products.Add(product1);
        products.Add(product2);
        products.Add(product3);

        DateTime currentDate = DateTime.Now;

        Console.WriteLine("Всі товари:");
        foreach (var product in products)
        {
            product.Show();
            Console.WriteLine($"Прострочений: {product.IsExpired(currentDate)}");
            Console.WriteLine();
        }

        // Пошук прострочених товарів
        Console.WriteLine("Прострочені товари:");
        foreach (var product in products)
        {
            if (product.IsExpired(currentDate))
            {
                product.Show();
            }
        }
    }
}

abstract class PrintedEdition
{
    public string Title { get; set; }
    public int Year { get; set; }

    public PrintedEdition() { }
    public PrintedEdition(string title, int year)
    {
        Title = title;
        Year = year;
        Console.WriteLine("Створено PrintedEdition");
    }

    public virtual void Show()
    {
        Console.WriteLine($"Назва: {Title}, Рік видання: {Year}");
    }

    ~PrintedEdition()
    {
        Console.WriteLine("Знищено PrintedEdition");
    }
}

class Journal : PrintedEdition
{
    public string Editor { get; set; }

    public Journal() : base() { }
    public Journal(string title, int year, string editor) : base(title, year)
    {
        Editor = editor;
        Console.WriteLine("Створено Journal");
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"Редактор: {Editor}");
    }

    ~Journal()
    {
        Console.WriteLine("Знищено Journal");
    }
}

class Book : PrintedEdition
{
    public string Author { get; set; }

    public Book() : base() { }
    public Book(string title, int year, string author) : base(title, year)
    {
        Author = author;
        Console.WriteLine("Створено Book");
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"Автор: {Author}");
    }

    ~Book()
    {
        Console.WriteLine("Знищено Book");
    }
}

class Textbook : Book
{
    public string Subject { get; set; }

    public Textbook() : base() { }
    public Textbook(string title, int year, string author, string subject) : base(title, year, author)
    {
        Subject = subject;
        Console.WriteLine("Створено Textbook");
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"Предмет: {Subject}");
    }

    ~Textbook()
    {
        Console.WriteLine("Знищено Textbook");
    }
}

abstract class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpiryDate { get; set; }

    public abstract void Show();
    public abstract bool IsExpired();

    // Метод для перевірки терміну придатності
    public bool IsExpired(DateTime currentDate)
    {
        return currentDate > ExpiryDate;
    }
}

class ProductItem : Product
{
    public ProductItem(string name, decimal price, DateTime productionDate, DateTime expiryDate)
    {
        Name = name;
        Price = price;
        ProductionDate = productionDate;
        ExpiryDate = expiryDate;
    }

    public override void Show()
    {
        Console.WriteLine($"Продукт: {Name}, Ціна: {Price} грн, Дата виробництва: {ProductionDate.ToShortDateString()}, Термін придатності: {ExpiryDate.ToShortDateString()}");
    }

    public override bool IsExpired()
    {
        return DateTime.Now > ExpiryDate;
    }
}

class Batch : Product
{
    public int Quantity { get; set; }

    public Batch(string name, decimal price, DateTime productionDate, DateTime expiryDate, int quantity)
    {
        Name = name;
        Price = price;
        ProductionDate = productionDate;
        ExpiryDate = expiryDate;
        Quantity = quantity;
    }

    public override void Show()
    {
        Console.WriteLine($"Партія: {Name}, Ціна: {Price} грн, Кількість: {Quantity} шт, Дата виробництва: {ProductionDate.ToShortDateString()}, Термін придатності: {ExpiryDate.ToShortDateString()}");
    }

    public override bool IsExpired()
    {
        return DateTime.Now > ExpiryDate;
    }
}

class Set : Product
{
    public List<Product> Products { get; set; }

    public Set(string name, decimal price, DateTime productionDate, DateTime expiryDate, List<Product> products)
    {
        Name = name;
        Price = price;
        ProductionDate = productionDate;
        ExpiryDate = expiryDate;
        Products = products;
    }

    public override void Show()
    {
        Console.WriteLine($"Комплект: {Name}, Ціна: {Price} грн, Дата виробництва: {ProductionDate.ToShortDateString()}, Термін придатності: {ExpiryDate.ToShortDateString()}");
        Console.WriteLine("Склад комплекту:");
        foreach (var product in Products)
        {
            product.Show();
        }
    }

    public override bool IsExpired()
    {
        return DateTime.Now > ExpiryDate;
    }
}
