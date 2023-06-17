using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dal;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();

        // Применяем все миграции, которых еще нет в базе данных
        context.Database.Migrate();
        
        FillDatabase(context);
    }

    private static void FillDatabase(AppDbContext dbContext)
    {
        if (dbContext.Users.Any()) return;
        // Создание регионов
        var regions = new List<Region>
        {
            new Region { Name = "Region 1" },
            new Region { Name = "Region 2" },
            // Добавьте другие регионы по вашему усмотрению
        };
        dbContext.Regions.AddRange(regions);
        dbContext.SaveChanges();

        // Создание районов
        var districts = new List<District>
        {
            new District { Name = "District 1", RegionId = regions[0].Id },
            new District { Name = "District 2", RegionId = regions[1].Id },
            // Добавьте другие районы по вашему усмотрению
        };
        dbContext.Districts.AddRange(districts);
        dbContext.SaveChanges();

        // Создание городов
        var cities = new List<City>
        {
            new City { Name = "City 1", DistrictId = districts[0].Id, RegionId = regions[0].Id },
            new City { Name = "City 2", DistrictId = districts[1].Id, RegionId = regions[1].Id },
            // Добавьте другие города по вашему усмотрению
        };
        dbContext.Cities.AddRange(cities);
        dbContext.SaveChanges();
        // Создание пользователей
        var users = new List<User>
        {
            new User { PhoneNumber = "111-1111", Password = "password1" },
            new User { PhoneNumber = "222-2222", Password = "password2" },
            // Добавьте других пользователей по вашему усмотрению
        };
        dbContext.Users.AddRange(users);
        dbContext.SaveChanges();

        // Создание жалоб
        var complaints = new List<Complaint>
        {
            new Complaint
            {
                Name = "Complaint 1", AuthorId = users[0].Id, Description = "Description 1", CountLike = 2,
                CountDislike = 1, Date = DateTime.Now, Status = ComplaintStatus.Review
            },
            new Complaint
            {
                Name = "Complaint 2", AuthorId = users[1].Id, Description = "Description 2", CountLike = 0,
                CountDislike = 3, Date = DateTime.Now, Status = ComplaintStatus.Completed
            },
            // Добавьте другие жалобы по вашему усмотрению
        };
        dbContext.Complaints.AddRange(complaints);
        dbContext.SaveChanges();

        // Создание координат
        var coordinates = new List<Coordinate>
        {
            new Coordinate
            {
                ComplaintId = complaints[0].Id, RegionId = regions[0].Id, DistrictId = districts[0].Id, CityId = cities[0].Id, Latitude = 123.456,
                Longitude = 654.321
            },
            new Coordinate
            {
                ComplaintId = complaints[1].Id, RegionId = regions[1].Id, DistrictId = districts[1].Id, CityId = cities[1].Id, Latitude = 987.654,
                Longitude = 456.789
            },
            // Добавьте другие координаты по вашему усмотрению
        };
        dbContext.Coordinates.AddRange(coordinates);
        dbContext.SaveChanges();

        // Создание пользовательских жалоб
        var userComplaints = new List<UserComplaint>
        {
            new UserComplaint
                { UserId = users[0].Id, ComplaintId = complaints[0].Id, Importance = ComplaintImportance.Like },
            new UserComplaint
                { UserId = users[1].Id, ComplaintId = complaints[0].Id, Importance = ComplaintImportance.Dislike },
            // Добавьте другие пользовательские жалобы по вашему усмотрению
        };
        dbContext.UserComplaints.AddRange(userComplaints);
        dbContext.SaveChanges();
    }
}