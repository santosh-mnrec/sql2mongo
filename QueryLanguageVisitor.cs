using System.Collections.Generic;
using Antlr4.Runtime.Misc;

namespace SqlToMongoDB
{


    public class QueryLanguageVisitor : SqlToMongoDBBaseVisitor<string>
    {

        private BuildMongoQuery buildMongoQuery;
        private static Stack<object> elements = new Stack<object>();


        public QueryLanguageVisitor()
        {
            buildMongoQuery = new BuildMongoQuery(elements);
        }


        private string query = "";
        private Stack<string> stack = new Stack<string>();

        public override string VisitQuery(SqlToMongoDBParser.QueryContext context)
        {
           

            Visit(context.select_stmt());
            Visit(context.from_stmt());
            Visit(context.where_stmt());
          

            return buildMongoQuery.BuildQuery();

        }

        public override string VisitSelect_stmt([NotNull] SqlToMongoDBParser.Select_stmtContext context)
        {


            var d = new Dictionary<string, string>();
            for (int i = 1; i < context.children.Count; i++)
            {
                if (context.children[i].GetText() == ",")
                {
                    continue;
                }
                d.Add(context.children[i].GetText(), "1");

            }
            elements.Push(d);
            buildMongoQuery.Parse("select");
            return "";
        }
        public override string VisitField([NotNull] SqlToMongoDBParser.FieldContext context)
        {

            return (context.GetText());
        }
        public override string VisitWhere_stmt([NotNull] SqlToMongoDBParser.Where_stmtContext context)
        {

            foreach (var x in context.search_condition())
            {

                query += Visit(x);

            }

            return query;


        }
        public override string VisitFrom_stmt(SqlToMongoDBParser.From_stmtContext context)
        {


           
            elements.Push(context.children[1].GetText());
            buildMongoQuery.Parse("from");
            return null;
        }

        public override string VisitSearch_condition([NotNull] SqlToMongoDBParser.Search_conditionContext context)
        {

            VisitChildren(context);
            return query; ;
        }


        public override string VisitPredicate([NotNull] SqlToMongoDBParser.PredicateContext context)
        {


           
            if (context.ChildCount == 1)
            {
                this.Visit(context.children[0]);


            }
            else
            {
                var op = this.Visit(context.children[0]);
                this.Visit(context.children[1]); ;
                buildMongoQuery.Parse(op);
            }
            return query;



        }
        public override string VisitComparison_predicate([NotNull] SqlToMongoDBParser.Comparison_predicateContext context)
        {

           


            var field = Visit(context.children[0]);
            var op = Visit(context.children[1]);
            var value = Visit(context.children[2]);
            var l1 = new Dictionary<string, string>(){

                {field,value},
            };
            elements.Push(l1);
            return query;

        }
      
        public override string VisitAnd([NotNull] SqlToMongoDBParser.AndContext context)
        {
            

            return "$and";

        }


        public override string VisitOr([NotNull] SqlToMongoDBParser.OrContext context)
        {
           
            return "$or";
        }
        public override string VisitEquals([NotNull] SqlToMongoDBParser.EqualsContext context)
        {
            

            return "$eq";

        }
        public override string VisitTerm([NotNull] SqlToMongoDBParser.TermContext context)
        {
           

            return context.GetText();
        }
        public override string VisitNumber([NotNull] SqlToMongoDBParser.NumberContext context)
        {

            return context.GetText();
        }

       

    }
}