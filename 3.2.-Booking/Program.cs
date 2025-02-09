using _3._2._Booking;
using System;
using System.Collections.Generic;

public class Program
{
    private List<Table> tables = new List<Table>();
    private List<Book> bookings = new List<Book>();

    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить стол");
            Console.WriteLine("2. Добавить бронирование");
            Console.WriteLine("3. Редактировать стол по ID");
            Console.WriteLine("4. Показать информацию о столе по ID");
            Console.WriteLine("5. Показать все бронирования");
            Console.WriteLine("6. Найти бронирование по имени клиента и последним 4 цифрам телефона");
            Console.WriteLine("7. Доступные столы по фильтру");
            Console.WriteLine("0. Выход");

            Console.Write("\nВыберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTable();
                    break;
                case "2":
                    AddBooking();
                    break;
                case "3":
                    EditTable();
                    break;
                case "4":
                    ShowTableInfo();
                    break;
                case "5":
                    ShowAllBookings();
                    break;
                case "6":
                    SearchBooking();
                    break;
                case "7":
                    FilltrTables();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    public void AddTable()
    {
        Console.Write("Введите ID стола: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите расположение стола (например, 'у окна'): ");
        string location = Console.ReadLine();

        Console.Write("Введите количество мест: ");
        int seats = Convert.ToInt32(Console.ReadLine());

        tables.Add(new Table(id, location, seats));
        Console.WriteLine("Стол добавлен.");
    }

    public void AddBooking()
    {
        Console.Write("Введите ID клиента: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите имя клиента: ");
        string name = Console.ReadLine();

        Console.Write("Введите телефон клиента: ");
        string phone = Console.ReadLine();

        Console.Write("Введите время начала брони (например, 12): ");
        int startHour = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите время окончания брони (например, 13): ");
        int endHour = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите комментарий: ");
        string comment = Console.ReadLine();

        Console.Write("Введите ID стола для бронирования: ");
        int tableId = Convert.ToInt32(Console.ReadLine());

        Table table = tables.Find(t => t.Id == tableId);


        bool check = true;
        for (int i = startHour; i < endHour; i++)
        {
            if (table.Schedule[i] != null)
            {
                check = false; break;
            }
        }
        if (table == null)
        {
            Console.WriteLine("Стол с таким ID не найден.");
            return;
        }
        else if(check) 
        {
            bookings.Add(new Book(id, name, phone, startHour, endHour, comment, table));
            Console.WriteLine("Бронирование добавлено.");
        }
        else
        {
            Console.WriteLine("Стол занят в это время");
        }
    }

    public void EditTable()
    {
        Console.Write("Введите ID стола для редактирования: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Table table = tables.Find(t => t.Id == id);
        if (table != null)
        {
            if (table.Schedule.Any(entry => entry.Value != null)) 
            {
                Console.WriteLine("Этот стол забронирован, редактирование невозможно.");
                return;
            }

            Console.Write("Введите новое количество мест у стола: ");
            int newSeats = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите новое расположение стола: ");
            string newLocation = Console.ReadLine();

            table.changeOfInfoAboveTale(newLocation, newSeats);
            Console.WriteLine("Информация о столе обновлена.");
        }
        else
        {
            Console.WriteLine("Стол с таким ID не найден.");
        }
    }


    public void ShowTableInfo()
    {
        Console.Write("Введите ID стола для вывода информации: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Table table = tables.Find(t => t.Id == id);

        if (table != null)
        {
            table.printInfoAboveTable();
        }
        else
        {
            Console.WriteLine("Стол с таким ID не найден.");
        }
    }

    public void ShowAllBookings()
    {
        Console.WriteLine("\nВсе бронирования:");
        foreach (var booking in bookings)
        {
            Console.WriteLine($"ID Клиента: {booking.id}, Телефон: {booking.telephone}, Имя клиента: {booking.name}, ID Столика: {booking.table.Id}");
        }
    }

    public void SearchBooking()
    {
        Console.Write("Введите имя клиента: ");
        string clientName = Console.ReadLine();

        Console.Write("Введите последние 4 цифры телефона клиента: ");
        string last4DigitsPhone = Console.ReadLine();

        var booking = bookings.Find(b =>
            b.name.Equals(clientName, StringComparison.OrdinalIgnoreCase) &&
            b.telephone.EndsWith(last4DigitsPhone));

        if (booking != null)
        {
            Console.WriteLine($"Бронь найдена: ID Клиента: {booking.id}, Телефон: {booking.telephone}, Имя клиента: {booking.name}, ID Столика: {booking.table.Id}");
        }
        else
        {
            Console.WriteLine("Информация о бронировании не найдена.");
        }
    }
    public void FilltrTables()
    {
        Console.Write("Введите минимальное количество мест: ");
        int countOfSeats = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите начало бронирования (в формате часа, например 10): ");
        int startOfBook = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите конец бронирования (в формате часа, например 12): ");
        int endOfBook = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите предпочитаемое расположение (оставьте пустым, если неважно): ");
        string place = Console.ReadLine();

        // Флаг для отслеживания, найдены ли столы
        bool tableFound = false;

        foreach (var table in tables)
        {
            // Проверяем количество мест и расположение
            bool matchesSeats = table.Seats >= countOfSeats;
            bool matchesLocation = string.IsNullOrEmpty(place) || table.Location.Equals(place, StringComparison.OrdinalIgnoreCase);

            if (matchesSeats && matchesLocation)
            {
                // Проверяем доступность стола в указанное время
                bool isAvailable = true;
                for (int hour = startOfBook; hour < endOfBook; hour++)
                {
                    if (table.Schedule.ContainsKey(hour) && table.Schedule[hour] != null)
                    {
                        isAvailable = false;
                        break;
                    }
                }

                if (isAvailable)
                {
                    table.printInfoAboveTable();
                    tableFound = true;
                }
            }
        }

        if (!tableFound)
        {
            Console.WriteLine("Нет доступных столов, соответствующих заданным параметрам.");
        }
    }


}
