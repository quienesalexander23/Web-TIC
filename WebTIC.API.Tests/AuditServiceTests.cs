using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTIC.API.Data;
using WebTIC.API.Services;
using Xunit;

namespace WebTIC.API.Tests
{
    public class AuditServiceTests
    {
        private DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            // Creamos una base de datos en memoria nueva para cada prueba
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task LogEventAsync_ShouldInsertLogIntoDatabase()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var actionType = "TEST_ACTION";
            var userId = "12345";
            var ipAddress = "192.168.1.1";
            var details = "Test details";

            // Act
            using (var context = new AppDbContext(options))
            {
                var auditService = new AuditService(context);
                await auditService.LogEventAsync(actionType, userId, ipAddress, details);
            }

            // Assert
            using (var context = new AppDbContext(options))
            {
                var logs = await context.LogAuditoria.ToListAsync();
                Assert.Single(logs);
                
                var insertedLog = logs.First();
                Assert.Equal(actionType, insertedLog.ActionType);
                Assert.Equal(userId, insertedLog.UserId);
                Assert.Equal(ipAddress, insertedLog.IpAddress);
                Assert.Equal(details, insertedLog.Details);
                
                // La estampa de tiempo debe haberse asignado aproximadamente en este momento
                Assert.True(insertedLog.Timestamp <= DateTime.UtcNow);
                Assert.True(insertedLog.Timestamp >= DateTime.UtcNow.AddMinutes(-1));
            }
        }
    }
}
