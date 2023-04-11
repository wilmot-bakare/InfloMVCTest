using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyApp.Models;

namespace MyApp.Data
{
    public class DataContext
    {
        // Databas tables
        private List<User> Users { get; set; }


        public DataContext()
        {
            // On startup we want to seed the data in
            Seed();
        }


        /// <summary>
        /// Seed the default data for the app
        /// </summary>
        private void Seed()
        {
            Users = new List<User>
            {
                new User { Id = 1, Forename = "Grant", Surname = "Cooper", Email = "grant.cooper@example.com",  DateOfBirth = new DateTime(2000, 11, 07), IsActive = true },
                new User { Id = 2, Forename = "Tom", Surname = "Gathercole", Email = "tom.gathercole@example.com",DateOfBirth = new DateTime(1990, 12, 24) , IsActive = true},
                new User { Id = 3, Forename = "Mark", Surname = "Edmondson", Email = "mark.edmondson@example.com",DateOfBirth = new DateTime(1976, 05, 13), IsActive = true },
                new User { Id = 4, Forename = "Graham", Surname = "Clark", Email = "graham.clark@example.com",DateOfBirth = new DateTime(1988, 01, 30), IsActive = true },
                new User { Id = 5, Forename = "Will", Surname = "Bakare", Email = "will.bakare@example.com", DateOfBirth = new DateTime(1995, 10, 05), IsActive = false },
                new User { Id = 6, Forename = "Bill", Surname = "Bakare", Email = "Bill.bakare@example.com",DateOfBirth = new DateTime(2012, 09, 22), IsActive = false }
            };
        }



        public List<TEntity> Set<TEntity>() where TEntity : class
        {
            var propertyInfo = PropertyInfos.FirstOrDefault(p => p.PropertyType == typeof(List<TEntity>));


            if (propertyInfo != null)
            {
                // Get the List<T> from 'this' Context instance
                var x = propertyInfo.GetValue(this, null) as List<TEntity>;

                return x;
            }

            throw new Exception("Type collection not found");
        }
        private IEnumerable<PropertyInfo> _propertyInfos;
        private IEnumerable<PropertyInfo> PropertyInfos
        {
            get
            {
                return _propertyInfos ??
                        (_propertyInfos = GetType()
                                            .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                                            .Where(p => p.PropertyType.IsGenericType &&
                                                        p.PropertyType.GetGenericTypeDefinition() == typeof(List<>)));
            }
        }
    }
}