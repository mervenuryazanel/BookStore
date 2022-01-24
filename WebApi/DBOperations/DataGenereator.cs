using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                // Look for any book.
                if (context.Books.Any())
                {
                    return;   // Data was already seeded
                }

                context.Books.AddRange(
                  new Book
                  {
                      //Id = 1,
                      Title = "Lean Startup",
                      GenreId = 1, //personal growth et cetera
                      PageCount = 200,
                      PublishDate = new DateTime(2001, 06, 12)
                  },

                  new Book
                  {
                      //Id = 2,
                      Title = "Herland", //science fiction
                      GenreId = 2,
                      PageCount = 202,
                      PublishDate = new DateTime(2003, 02, 12)
                  },
                    new Book
                    {
                        //Id = 3,
                        Title = "Dune",
                        GenreId = 2, //science fiction
                        PageCount = 437,
                        PublishDate = new DateTime(2008, 12, 12)
                    }


                   );

                context.SaveChanges();
            }
        }

    }
}
