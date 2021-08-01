using System;

namespace QueryLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
           QueryManager.search("Select last_name from '/path/to/index/' where first_name='Foo' and age = 30 and x=1");

        }
    }
}
