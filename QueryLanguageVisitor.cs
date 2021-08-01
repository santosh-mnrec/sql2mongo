using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime.Misc;
using Newtonsoft.Json;

namespace QueryLanguage
{
    public class QueryLanguageVisitor : QueryBaseVisitor<ASTNode>
    {


        private Predicate query = new Predicate();

        public override ASTNode VisitQuery(QueryParser.QueryContext context)
        {

            Visit(context.select_stmt());
            Visit(context.from_stmt());
            Visit(context.where_stmt());


            var r = JsonConvert.SerializeObject(query);
            System.Console.WriteLine(r);
            return null;


        }


        public override ASTNode VisitField([NotNull] QueryParser.FieldContext context)
        {

            return new TermNode(context.GetText());
        }
        public override ASTNode VisitWhere_stmt([NotNull] QueryParser.Where_stmtContext context)
        {
            System.Console.WriteLine("Visiting Where");


            foreach (var x in context.search_condition())
            {
                System.Console.WriteLine(x.GetText());


                Visit(x);
            }
            return null;


        }
        public override ASTNode VisitFrom_stmt(QueryParser.From_stmtContext context)
        {


            System.Console.WriteLine("Visiting From");
            System.Console.Write(context.GetText());



            return null;
        }

        public override ASTNode VisitSearch_condition([NotNull] QueryParser.Search_conditionContext context)
        {

            return VisitChildren(context);
        }


        public override ASTNode VisitFunction_predicate([NotNull] QueryParser.Function_predicateContext context)

        {
            return VisitChildren(context);
        }
        public override ASTNode VisitPredicate([NotNull] QueryParser.PredicateContext context)
        {
            System.Console.WriteLine("predicate");
            if (context.ChildCount == 1)
            {
                var binaryNode = this.Visit(context.children[0]) as Predicate;
                query.predicates.Add(binaryNode);
            }
            else
            {

                var predicate = new Predicate();
                var op = this.Visit(context.children[0]);


                var binaryNode = this.Visit(context.children[1]);;
                
                
                predicate=(Predicate)binaryNode;
                predicate.parent = op.ToString();
                query.predicates.Add(predicate);

            }
            return query;



        }
        public override ASTNode VisitComparison_predicate([NotNull] QueryParser.Comparison_predicateContext context)
        {

            System.Console.WriteLine("comp Predicate");
            var temp = new Predicate();

            var left = Visit(context.children[1]);
            var right = Visit(context.children[2]);
            var field = Visit(context.children[0]);

            temp.field = field;
            temp.parent = left.ToString();
            temp.value = right;


            return temp;



        }
        public override ASTNode VisitRange_op([NotNull] QueryParser.Range_opContext context)
        {
            return base.VisitRange_op(context);
        }
        public override ASTNode VisitAnd([NotNull] QueryParser.AndContext context)
        {
            System.Console.WriteLine("Visiting AND");

            return new TermNode("$and");

        }

        public override ASTNode VisitOr([NotNull] QueryParser.OrContext context)
        {
            System.Console.WriteLine("Visiting OR");


            return new TermNode("$or");
        }
        public override ASTNode VisitEquals([NotNull] QueryParser.EqualsContext context)
        {
            System.Console.WriteLine("Visiting Eq");




            return new TermNode("$eq");

        }
        public override ASTNode VisitTerm([NotNull] QueryParser.TermContext context)
        {
            System.Console.WriteLine("Visiting term");
            return new TermNode(context.GetText());
        }
        public override ASTNode VisitNumber([NotNull] QueryParser.NumberContext context)
        {
            return new TermNode(context.GetText());
        }

    }
}