using Antlr4.Runtime;

namespace SqlToMongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            //convert to hex


            var sqlQuery = "select name,age from employee where verified='true' or IsContractor='true'";

            var input = new AntlrInputStream(sqlQuery);

            var lexer = new SqlToMongoDBLexer(input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);

            var parser = new SqlToMongoDBParser(tokens);
            var tree = parser.query();

            System.Console.WriteLine(tree.ToStringTree(parser));

            var visitor = new QueryVisitor();
            var mongoQuery = visitor.Visit(tree);
            System.Console.WriteLine(mongoQuery.ToString());

        }
    }
}
