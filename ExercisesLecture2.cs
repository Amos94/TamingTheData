using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TamingTheData.Domain;
using Xunit;
using Xunit.Abstractions;

namespace TamingTheData.Test
{
    public class ExercisesLecture2
    {
        private static readonly IList<Person> Persons = DomainFactory.Persons;
        private static readonly ICollection<SalaryStatistics> SalaryStatistics = DomainFactory.SalryStatistics;
        private readonly ITestOutputHelper output;

        public ExercisesLecture2(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        //Determine how many persons live in Switzerland
        public void Exercise1()
        {
            var personsInSwitzerland = from p in Persons
                                       where p.Geography.DisplayName == "Switzerland"
                                       select p;

            output.WriteLine("" + personsInSwitzerland.ToArray().Length);
            
        }
        [Fact]
        //Determine how many persons live in which country
        public void Exercise2()
        {
            Dictionary<string, Person> dict = new Dictionary<string, Person>();
            var grouped = Persons
                .GroupBy(x => x.Geography)
                .Select(g => new
                {
                    
                    Name = g.Key,
                    Country = g.Count()
                })
                .OrderBy(g => g.Country)
                .ToArray();

            foreach(var ct in grouped)
            {
                output.WriteLine($"Country: {ct.Name.DisplayName}, Number:{ct.Country}");
            }

        }
        [Fact]
        //Determine how many persons work in which industry
        public void Exercise3()
        {

            var groupedByIndustry = Persons
                .GroupBy(x => x.Employment.Industry.DisplayName)
                .Select(g => new
                {
                    Emp = g.Key,
                    Count = g.Count()
                })
                .OrderBy(g => g.Count)
                .ToArray();

            foreach(var ind in groupedByIndustry)
            {
                output.WriteLine($"Industry: {ind.Emp}, Number: {ind.Count}");
            }
            

        }
        [Fact]
        //Determine the average salary by country in our staff list
        public void Exercise4()
        {
            var groupByCountry = Persons
                .GroupBy(x => x.Geography)
                .Select(g => new
                {
                    Country = g.Key,
                    Avg = g.Average(p => p.Employment.Salary)
                })
                .OrderBy(g => g.Avg)
                .ToArray();

            foreach(var cav in groupByCountry)
            {
                output.WriteLine($"Country: {cav.Country.DisplayName} ; Average Salary: {cav.Avg}");
            }
            

        }
        [Fact]
        //Determine how many persons are below average and how many above average salary: 
        /*
            1. In the total portfolio
            2. By country
            3. By industry
            4. By industry and country
         */
        public void Exercise5()
        {
            //Calculate the average salary
            var avgSalaryArray = Persons.Select(x => x.Employment.Salary).ToArray();
            var avgSalary = avgSalaryArray.Average();
            output.WriteLine($"The salary average is: {avgSalary}");

            //Counting people for 1
            var belowAverageTotal = Persons.Where(x => x.Employment.Salary <= avgSalary).Count();
            output.WriteLine($"Number of people with salary below or equal to average: {belowAverageTotal}");
            var aboveAverageTotal = Persons.Where(x => x.Employment.Salary > avgSalary).Count();
            output.WriteLine($"Number of people with salary higher than the average: {aboveAverageTotal}");

            //Counting people for 2
            var belowAverageByCountryArray = Persons
                .GroupBy(p => p.Geography)
                .Select(x => new
                {
                    Key = x.Key,
                    NumPeople = x.Count(p => p.Employment.Salary < avgSalary)
                })
                .OrderBy(g => g.NumPeople)
                .ToArray();
            foreach(var country in belowAverageByCountryArray)
            {
                output.WriteLine($"Country: {country.Key.DisplayName}, Number of people with the salary < average salary: {country.NumPeople}");
            }

            var aboveAverageByCountryArray = Persons
                .GroupBy(p => p.Geography)
                .Select(x => new
                {
                    Key = x.Key,
                    NumPeople = x.Count(p => p.Employment.Salary > avgSalary)
                })
                .OrderBy(g => g.NumPeople)
                .ToArray();
            foreach (var country in aboveAverageByCountryArray)
            {
                output.WriteLine($"Country: {country.Key.DisplayName}, Number of people with the salary > average salary: {country.NumPeople}");
            }

            //Counting people for 3
            var belowAverageByIndustryArray = Persons
                .GroupBy(p => p.Employment.Industry)
                .Select(x => new
                {
                    Key = x.Key,
                    NumPeople = x.Count(p => p.Employment.Salary < avgSalary)
                })
                .OrderBy(g => g.NumPeople)
                .ToArray();
            foreach (var industry in belowAverageByIndustryArray)
            {
                output.WriteLine($"Industry: {industry.Key.DisplayName}, Number of people with the salary < average salary: {industry.NumPeople}");
            }
            var aboveAverageByIndustryArray = Persons
                .GroupBy(p => p.Employment.Industry)
                .Select(x => new
                {
                    Key = x.Key,
                    NumPeople = x.Count(p => p.Employment.Salary > avgSalary)
                })
                .OrderBy(g => g.NumPeople)
                .ToArray();
            foreach (var industry in aboveAverageByIndustryArray)
            {
                output.WriteLine($"Industry: {industry.Key.DisplayName}, Number of people with the salary > average salary: {industry.NumPeople}");
            }

            //Counting people for 4
            //Joining TAKS
            var belowAverageByIndustryAndCountry = 0;
            var aboveAverageByIndustryAndCountry = 0;


        }
        [Fact]
        //For which countries and industries do the persons are the persons in our portfolio below the average statistics?
        public void Exercise6()
        {
            Console.WriteLine("Write code here");

        }

    }
}
