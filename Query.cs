using System;
using System.Collections.Generic;
using System.Text;

namespace QueryLanguage
{

    public abstract class ASTNode
    {

    }


    public class TermNode : ASTNode
    {

        public TermNode(string name)
        {
            this.name = name;
        }
        public string name;

        public override string ToString()
        {
            return name;
        }

    }




    public class Predicate : ASTNode
    {
     
        private const int indentSize = 2;
        public ASTNode field { get; set; }
        public ASTNode value { get; set; }
        public string parent { get; set; }
        public string @operator { get; set; }
     

        public List<Predicate> predicates { get; set; } = new List<Predicate>();
        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string('{', indentSize * indent);
            sb.Append($"{i}{@operator}\n");
            if (!string.IsNullOrWhiteSpace(value.ToString()))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.Append(field);
                sb.Append("\n");
            }

            foreach (var e in predicates)
                sb.Append(e.ToStringImpl(indent + 1));

            sb.Append($"{'}'}</{field}>\n");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }
}