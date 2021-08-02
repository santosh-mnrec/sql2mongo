using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace QueryLanguage
{
    public static class De{

        public static string Serialize(this object query){
            return JsonConvert.SerializeObject(query);


        }
    }
    public class BuildMongoQuery
    {
        private Stack<object> _elements;

        public BuildMongoQuery(Stack<object> elements)
        {
            _elements = elements;

        }
        public object build_binary(object op, object left, object right)
        {

            return new Dictionary<object, object> {
                {
                    op,
                    new List<object> {left,right }
                    }
                };
        }
         public object build_From(object op)
        {
                return "db."+op;
           
        }
        public void Parse(string op)
        {
            
            if (op == "$and")
            {
                var x = build_binary(op, _elements.Pop(), _elements.Pop());
                _elements.Push(x);
               
            }
            if (op == "$or")
            {
                var y = build_binary(op, _elements.Pop(), _elements.Pop());
               
                _elements.Push(y);
            }
            if (op == "$eq")
            {
                var y = build_binary(op, _elements.Pop(), _elements.Pop());
              
                _elements.Push(y);
            }
            if (op == "from")
            {
                var y = build_From( _elements.Pop());
              
                _elements.Push(y);
            }
        }
    }
}