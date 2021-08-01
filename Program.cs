using System;

namespace QueryLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
           QueryManager.search("select id,salary from employee where age = 35 and  id=2 or x=2 and y=4");

        }
    }
}
