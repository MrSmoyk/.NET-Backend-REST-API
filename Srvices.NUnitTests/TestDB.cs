using DAL;
using Microsoft.EntityFrameworkCore;

namespace Srvices.NUnitTests
{
    public class TestDB
    {
        public static ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().EnableSensitiveDataLogging()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeletedAsync();
            _context.Database.EnsureCreatedAsync();
            return _context;
        }
    }
}
