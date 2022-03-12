using System.Collections;
using System.Collections.Generic;

namespace BitShifter.Cryptography.Extensions.Tests
{
    internal class AppsettingsValues : IEnumerable<object[]>
    {
        private readonly IEnumerable<object[]> _data = new List<object[]>()
        {
            new object[] { "DbConnectionString", "Data Source=localhost;Initial Catalog=database_name;Integrated Security=False;User Id=sa;Password=<SECRET+*#~|!§$%&/()=?ß-_.SECRET>;MultipleActiveResultSets=True;App=ApplicationName" },
            new object[] { "NestedObject:DbConnectionString", "Data Source=localhost;Initial Catalog=database_name;Integrated Security=False;User Id=sa;Password=<SECRET+*#~|!§$%&/()=?ß-_.SECRET>;MultipleActiveResultSets=True;App=ApplicationName" },
            new object[] { "NestedNestedObject:NestedObject:DbConnectionString", "Data Source=localhost;Initial Catalog=database_name;Integrated Security=False;User Id=sa;Password=<SECRET+*#~|!§$%&/()=?ß-_.SECRET>;MultipleActiveResultSets=True;App=ApplicationName" }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
