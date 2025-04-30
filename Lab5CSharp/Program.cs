using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Виберіть завдання:");
        Console.WriteLine("1 - Завдання 1 та 2");
        Console.WriteLine("2 - Завдання 1 та 2");
        Console.WriteLine("3 - Завдання 3");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                RunTask1And2();
                break;
            case 2:
                RunTask1And2();
                break;
            case 3:
                RunTask3();
                break;
            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }

    static void RunTask1And2()
    {
        Console.WriteLine("Завдання 1 та 2");

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

        Console.WriteLine("Виклик деструкторів:");
        GC.Collect();
    }

    static void RunTask3()
    {
        Console.WriteLine("Завдання 3");

        List<Product> products = new List<Product>();

        Product product1 = new ProductItem("Молоко", 20.5m, new DateTime(2025, 3, 1), new DateTime(2025, 4, 10));
        Product product2 = new Batch("Пакет цукру", 15.0m, new DateTime(2025, 2, 15), new DateTime(2025, 5, 10), 100);
        Product product3 = new Set("Набір для чаю", 50.0m, new DateTime(2025, 1, 20), new DateTime(2025, 6, 1), new List<Product> { product1, product2 });

        products.Add(product1);
        products.Add(product2);
        products.Add(product3);

        Console.WriteLine("Всі товари:");
        foreach (var product in products)
        {
            product.Show();
            Console.WriteLine($"Прострочений: {(product.IsExpired() ? "так" : "ні")}");
            Console.WriteLine();
        }

        Console.WriteLine("Прострочені товари:");
        foreach (var product in products)
        {
            if (product.IsExpired())
            {
                product.Show();
                Console.WriteLine();
            }
        }
    }
}

abstract class PrintedEdition
{
    public string Title { get; set; }
    public int Year { get; set; }

    public PrintedEdition()
    {
        Console.WriteLine("Створено PrintedEdition (без параметрів)");
    }

    public PrintedEdition(string title, int year)
    {
        Title = title;
        Year = year;
        Console.WriteLine($"Створено PrintedEdition: Назва: {Title}, Рік: {Year}");
    }

    public PrintedEdition(string title)
    {
        Title = title;
        Year = 2023;
        Console.WriteLine($"Створено PrintedEdition: Назва: {Title}, Рік за замовчуванням: {Year}");
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

    public Journal() : base()
    {
        Console.WriteLine("Створено Journal (без параметрів)");
    }

    public Journal(string title, int year, string editor) : base(title, year)
    {
        Editor = editor;
        Console.WriteLine($"Створено Journal: Редактор : {Editor}");
    }

    public Journal(string title) : base(title)
    {
        Editor = "Невідомий редактор";
        Console.WriteLine($"Створено Journal: Редактор за замовчуванням: {Editor}");
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

    public Book() : base()
    {
        Console.WriteLine("Створено Book (без параметрів)");
    }

    public Book(string title, int year, string author) : base(title, year)
    {
        Author = author;
        Console.WriteLine($"Створено Book: Автор: {Author}");
    }

    public Book(string title) : base(title)
    {
        Author = "Невідомий автор";
        Console.WriteLine($"Створено Book: Автор за замовчуванням: {Author}");
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

    public Textbook() : base()
    {
        Console.WriteLine("Створено Textbook (без параметрів)");
    }

    public Textbook(string title, int year, string author, string subject) : base(title, year, author)
    {
        Subject = subject;
        Console.WriteLine($"Створено Textbook: Предмет - {Subject}");
    }

    public Textbook(string title) : base(title)
    {
        Subject = "Невідомий предмет";
        Console.WriteLine($"Створено Textbook: Предмет за замовчуванням - {Subject}");
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
