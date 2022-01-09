using Xunit;
using FluentAssertions;
using PixelDance.Cryptography.Common;

namespace PixelDance.Cryptography.Tests
{
    public class IsBase64Test
    {
        [Fact]
        public void IsValidBase64String()
        {
            string connectionString = "jJHikO/KFe47fnLDN+5ct6A2BqDT7w4SI+e2qaTsZSSsXoCmYeOEZZiU++HzbuDN";
            //string connectionString = "Data Source=localhost;Initial Catalog=database_name;Integrated Security=False;User Id=sa;Password=<SECRET+*#~|!§$%&/()=?ß-_.SECRET>;MultipleActiveResultSets=True;App=ApplicationName";

            connectionString.IsBase64String()
                .Should()
                .BeTrue();
        }

    }
}
