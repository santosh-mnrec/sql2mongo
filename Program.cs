using System;
using Antlr4.Runtime;

namespace QueryLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            var sqlQuery = "select valid,santosh from flight where verified='true' or valid='false' or valid==12";

            var input = new AntlrInputStream(sqlQuery);

            var lexer = new SqlToMongoDBLexer(input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);

            var parser = new SqlToMongoDBParser(tokens);
            var tree = parser.query();

            System.Console.WriteLine(tree.ToStringTree(parser));

            var visitor = new QueryLanguageVisitor();
            var jpqlQuery = visitor.Visit(tree);
            System.Console.WriteLine(jpqlQuery.ToString());

        }
    }
}
