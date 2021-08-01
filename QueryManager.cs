using System.Collections.Generic;
using Antlr4.Runtime;

namespace QueryLanguage
{
    public class QueryManager
    {
        public  static List<Device> search(string query)
        {



            var input = new AntlrInputStream(query);

            QueryLexer lexer = new QueryLexer(input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);

            QueryParser parser = new QueryParser(tokens);
            var tree = parser.query();

 System.Console.WriteLine(tree.ToStringTree(parser));

            var visitor = new QueryLanguageVisitor();
            var jpqlQuery = visitor.Visit(tree);
           
            System.Console.WriteLine(jpqlQuery);

            // var queryObject = entityManager.createQuery(jpqlQuery);
            // foreach (var entry int visitor.getParameters().entrySet()) {
            //     queryObject.setParameter(entry.getKey(), entry.getValue());
            // }
            return null;
        }

        public class Device
        {
        }
    }
}