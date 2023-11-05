using System;
using System.Linq;
using System.Collections.Generic;
using Uppgift10;
using Uppgift10.Models;
using Uppgift10.Data;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("Northwind Database");
            Console.WriteLine("1. Hämta alla kunder (stigande företagsnamn)");
            Console.WriteLine("2. Hämta alla kunder (fallande företagsnamn)");
            Console.WriteLine("3. Visa detaljer om en kund och deras ordrar");
            Console.WriteLine("4. Lägg till en ny kund");
            Console.WriteLine("5. Avsluta programmet");
            Console.Write("Välj en funktion (1/2/3/4/5): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListCustomers();
                    break;
                case "2":
                    ListCustomers();
                    break;
                case "3":
                    ShowCustomerDetails();
                    break;
                case "4":
                    AddCustomer();
                    break;
                case "5":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }
        }
    }

    static void ListCustomers()
    {
        using (var context = new NorthContext())
        {
            Console.Clear();
            Console.WriteLine("Northwind Database");
            Console.WriteLine("1. Hämta alla kunder (stigande företagsnamn)");
            Console.WriteLine("2. Hämta alla kunder (fallande företagsnamn)");
            Console.WriteLine("Välj en funktion (1/2): ");
            string choice = Console.ReadLine();

            var customers = context.Customers.AsQueryable();

            if (choice == "1")
            {
                customers = customers.OrderBy(c => c.CompanyName);
            }
            else if (choice == "2")
            {
                customers = customers.OrderByDescending(c => c.CompanyName);
            }
            else
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
                return;
            }

            // Använd Include för att hämta orderdata tillsammans med kunddata
            customers = customers.Include(c => c.Orders);

            var customersList = customers.ToList();

            Console.Clear();
            Console.WriteLine("Lista över kunder:");

            foreach (var customer in customersList)
            {
                Console.WriteLine($"Företagsnamn: {customer.CompanyName}");
                Console.WriteLine($"Land: {customer.Country}");
                Console.WriteLine($"Region: {customer.Region ?? "Ej tillgänglig"}");
                Console.WriteLine($"Telefonnummer: {customer.Phone}");
                Console.WriteLine($"Antal ordrar: {customer.Orders.Count}");
                Console.WriteLine();
            }

            Console.WriteLine("Tryck på valfri tangent för att återgå till huvudmenyn.");
            Console.ReadKey();
        }
    }

    static void ShowCustomerDetails()
    {
        using (var context = new NorthContext())
        {
            Console.Clear();
            Console.Write("Ange kund-ID: ");
            string customerId = Console.ReadLine();

            var customer = context.Customers
                .Where(c => c.CustomerId == customerId)
                .FirstOrDefault();

            if (customer != null)
            {
                Console.WriteLine($"Kundinformation för {customer.CompanyName}:");
                Console.WriteLine($"Land: {customer.Country}");
                Console.WriteLine($"Region: {customer.Region ?? "Ej tillgänglig"}");
                Console.WriteLine($"Telefonnummer: {customer.Phone}");
                Console.WriteLine($"Antal ordrar: {customer.Orders.Count}");

                Console.WriteLine("Ordrar:");
                foreach (var order in context.Orders.Where(o => o.CustomerId == customerId))
                {
                    Console.WriteLine($"OrderID: {order.OrderId}, Orderdatum: {order.OrderDate}");
                }
            }
            else
            {
                Console.WriteLine("Kunden kunde inte hittas.");
            }

            Console.WriteLine("Tryck på valfri tangent för att återgå.");
            Console.ReadKey();
        }
    }

    static void AddCustomer()
    {
        using (var context = new NorthContext())
        {
            Console.Clear();
            Console.Write("Ange ett nytt kund-ID: ");
            string customerId = Console.ReadLine();
            Console.Write("Ange ett företagsnamn: ");
            string companyName = Console.ReadLine();
            Console.Write("Ange land: ");
            string country = Console.ReadLine();
            Console.Write("Ange region (tryck Enter om ingen): ");
            string region = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(region))
            {
                region = null;
            }
            Console.Write("Ange telefonnummer: ");
            string phone = Console.ReadLine();

            var newCustomer = new Customer
            {
                CustomerId = customerId,
                CompanyName = companyName,
                Country = country,
                Region = region,
                Phone = phone
            };

            context.Customers.Add(newCustomer);
            context.SaveChanges();

            Console.WriteLine("Kunden har lagts till i databasen.");
            Console.WriteLine("Tryck på valfri tangent för att återgå.");
            Console.ReadKey();
        }
       

    }
}