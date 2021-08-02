using System;

namespace QueryLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
           QueryManager.search("select name from x where id=2 and name='x'");

        }
    }
}
