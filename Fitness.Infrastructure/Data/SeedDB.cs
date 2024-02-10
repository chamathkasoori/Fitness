//using Fitness.Core.Entities;

using Fitness.Core.Entities;
using Fitness.Core.Enums;
using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Fitness.Infrastructure.Data
{
    public static class SeedDB
    {
        public static void Seed(ModelBuilder builder)
        {
            //List<Country> country = new();
            //country.Add(new Country { Id = 1, Name = "Saudi Arabia", NameAR = "المملكة العربية السعودية", Code = "+966", CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false });
            //country.Add(new Country { Id = 2, Name = "United Arab Emirates", NameAR = "الإمارات العربية المتحدة", Code = "+971", CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false });
            //country.Add(new Country { Id = 3, Name = "Qatar", NameAR = "دولة قطر", Code = "+974", CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false });

            //List<City> city = new();
            //city.Add(new City { Id = 1, Name = "Riyadh", NameAR = "الرياض", CountryId = 1, CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false });
            //city.Add(new City { Id = 2, Name = "Jeddah", NameAR = "جدة", CountryId = 1, CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false });
            //city.Add(new City { Id = 3, Name = "Dammam", NameAR = "الدمام", CountryId = 1, CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false });
            //city.Add(new City { Id = 4, Name = "Dubai", NameAR = "دبي", CountryId = 2, CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false });
            //city.Add(new City { Id = 5, Name = "Abu Dhabi", NameAR = "أبو ظبي", CountryId = 2, CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false });
            //city.Add(new City { Id = 6, Name = "Al Ain", NameAR = "العين", CountryId = 2, CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false });
            //city.Add(new City { Id = 7, Name = "Doha", NameAR = "الدوحة", CountryId = 3, CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false });
            //city.Add(new City { Id = 8, Name = "Al Rayyan Municipality", NameAR = "بلدية الريان", CountryId = 3, CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false });
            //city.Add(new City { Id = 9, Name = "Al Wakrah", NameAR = "الوكرة", CountryId = 3, CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false });

            //var modules = new List<Module>
            //{
            //    new() {Id = 1, Name = "Company", CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false, Hierarchy = "1"},
            //    new() {Id = 2, Name = "City", CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false, Hierarchy = "2"},
            //    new() {Id = 3, Name = "Club", CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false, Hierarchy = "3"},
            //    new() {Id = 4, Name = "Country", CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false, Hierarchy = "4"},
            //    new() {Id = 5, Name = "Department", CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false, Hierarchy = "5"},
            //    new() {Id = 6, Name = "Module Operation", CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false, Hierarchy = "6"},
            //    new() {Id = 7, Name = "Operation", CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false, Hierarchy = "7"},
            //    new() {Id = 8, Name = "Position", CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false, Hierarchy = "8"},
            //    new() {Id = 9, Name = "Region", CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false, Hierarchy = "9"},
            //    new() {Id = 10, Name = "Role Module", CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false, Hierarchy = "10"},
            //    new() {Id = 11, Name = "Role", CreatedOn = DateTime.MinValue, ModifiedOn = DateTime.MinValue, IsDelete = false, Hierarchy = "11"},
            //};

            //builder.Entity<Country>().HasData(country);
            //builder.Entity<City>().HasData(city);
            //builder.Entity<Module>().HasData(modules);
        }
    }
}
