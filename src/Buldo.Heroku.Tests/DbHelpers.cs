namespace Buldo.Heroku.Tests
{
    using Xunit;

    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".

    public class DbHelpers
    {
        [Fact]
        public void PostgreConvertTest()
        {
            var databaseUrl =
                "postgres://username:password@host.amazonaws.com:5432/databaseName";
            var connectionString = Heroku.DbHelpers.DatabaseUrlToPostgreConnectionString(databaseUrl);
            Assert.Equal("Host=host.amazonaws.com;Port=5432;Database=databaseName;Username=username;Password=password;", connectionString);
        }
    }
}
