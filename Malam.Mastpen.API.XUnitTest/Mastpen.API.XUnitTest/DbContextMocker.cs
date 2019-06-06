

using Malam.Mastpen.Core.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Malam.Mastpen.API.XUnitTest
{
    public static class DbContextMocker
    {
        public static MastpenBitachonDbContext GetMastpenDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<MastpenBitachonDbContext>()
              ///יש צורך לבדוק למה זה נחוץ כי זה הביא שגיאה
                 .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new MastpenBitachonDbContext(options);

            // Add entities in memory
            dbContext.SeedAsync();

            return dbContext;
        }
    }
}