using System;

namespace QueryLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
           QueryManager.search("select valid from flight where verified='true' and valid='true'");

        }
    }
}
