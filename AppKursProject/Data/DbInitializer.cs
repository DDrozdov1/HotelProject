using Microsoft.EntityFrameworkCore;
using System;
using AppKursProject.Data;
using AppKursProject.Models;


namespace AppKursProject.Data
{
    //public static class DbInitializer
    //{
    //    private static Random random = new Random();

    //    public static void Initialize(ApplicationDbContext db)
    //    {
    //        db.Database.EnsureCreated();

    //        if (db.Customers.Any() || db.Employees.Any() || db.HotelServices.Any() || db.ProvidedServices.Any() ||
    //            db.Rooms.Any() || db.RoomPrices.Any() || db.RoomServices.Any() || db.Stays.Any())
    //        {
    //            return;s
    //        }

    //        for (int customerId = 1; customerId <= 500; customerId++)
    //        {
    //            db.Customers.Add(new Customer { CustomerName = Faker.Name.First(), CustomerSurname = Faker.Name.Last() });
    //        }

    //        db.SaveChanges();

    //        for (int employeeId = 1; employeeId <= 10000; employeeId++)
    //        {
    //            db.Employees.Add(new Employee
    //            {
    //                EmployeeName = Faker.Name.First(),
    //                EmployeeMiddlename = Faker.Name.Last(),
    //                EmployeeSurname = Faker.Name.Last()
    //            });
    //        }

    //        db.SaveChanges();

    //        for (int hotelServiceId = 1; hotelServiceId <= 500; hotelServiceId++)
    //        {
    //            db.HotelServices.Add(new HotelService { ServiceName = Faker.Company.Name(), ServicePrice = Faker.RandomNumber.Next(10, 100) });
    //        }

    //        db.SaveChanges();

    //        for (int providedServiceId = 1; providedServiceId <= 20000; providedServiceId++)
    //        {
    //            db.ProvidedServices.Add(new ProvidedService
    //            {
    //                ServiceId = Faker.RandomNumber.Next(1, 500),
    //                StayId = Faker.RandomNumber.Next(1, 500),
    //                Quantity = Faker.RandomNumber.Next(1, 5)
    //            });
    //        }

    //        db.SaveChanges();

    //        for (int roomId = 1; roomId <= 500; roomId++)
    //        {
    //            db.Rooms.Add(new Room { RoomNumber = Faker.RandomNumber.Next(100, 1000), Capacity = Faker.RandomNumber.Next(1, 5) });
    //        }

    //        db.SaveChanges();

    //        for (int roomPriceId = 1; roomPriceId <= 500; roomPriceId++)
    //        {
    //            db.RoomPrices.Add(new RoomPrice { RoomId = Faker.RandomNumber.Next(1, 500), Price = Faker.RandomNumber.Next(50, 200) });
    //        }

    //        db.SaveChanges();

    //        for (int roomServiceId = 1; roomServiceId <= 20000; roomServiceId++)
    //        {
    //            db.RoomServices.Add(new RoomService
    //            {
    //                ServiceId = Faker.RandomNumber.Next(1, 500),
    //                RoomId = Faker.RandomNumber.Next(1, 500),
    //                Quantity = Faker.RandomNumber.Next(1, 5)
    //            });
    //        }

    //        db.SaveChanges();

    //        DateTime checkInDate, checkOutDate;

    //        DateTime startDateGen = new DateTime(2020, 1, 1);

    //        for (int stayId = 1; stayId <= 500; stayId++)
    //        {
    //            checkInDate = GenerateRandomDate(startDateGen, DateTime.Now);
    //            checkOutDate = GenerateRandomDate(checkInDate, DateTime.Now);

    //            db.Stays.Add(new Stay
    //            {
    //                CustomerId = Faker.RandomNumber.Next(1, 500),
    //                RoomId = Faker.RandomNumber.Next(1, 500),
    //                CheckInDate = checkInDate,
    //                CheckOutDate = checkOutDate
    //            });
    //        }

    //        db.SaveChanges();
    //    }

    //    static DateTime GenerateRandomDate(DateTime startDate, DateTime endDate)
    //    {s
    //        int range = (endDate - startDate).Days;
    //        int randomDays = random.Next(range + 1);

    //        return startDate.AddDays(randomDays);
    //    }
    //}
}
