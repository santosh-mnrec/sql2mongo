using System;

namespace QueryLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
           QueryManager.arch("select valid from flight where verified='true' and valid='true'");

        }
    }
}
