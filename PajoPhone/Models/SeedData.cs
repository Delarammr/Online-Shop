using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using PajoPhone.Data;
using System;
using System.Linq;

namespace PajoPhone.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new PajoPhoneContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<PajoPhoneContext>>()))
        {
            if (context.Phone.Any())
            {
                return;   
            }

            context.Phone.AddRange(
                new Phone
                {
                    Name = "Asa 1",
                    Color = "Black",
                    Price = 28.9M,
                    Description = "weight: 200gr"
                },
                new Phone
                {
                    Name = "Asa 2",
                    Color = "Blue",
                    Price = 55.99M,
                    Description = "Ram: 6GB"
                }
            ) ;
            context.SaveChanges();
        }
    }
}