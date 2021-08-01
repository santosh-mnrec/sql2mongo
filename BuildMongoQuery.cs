using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace QueryLanguage
{
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
                    new List<object> {
                        left,
                        right
                    }}};
        }
        public void Parse(string op)
        {
            if (op == "$and")
            {
                var x = build_binary(op, _elements.Pop().ToString(), _elements.Pop().ToString());
                _elements.Push(JsonConvert.SerializeObject(x));
                Console.WriteLine(JsonConvert.SerializeObject(x));
            }
            if (op == "$or")
            {
                var y = build_binary(op, _elements.Pop().ToString(), _elements.Pop().ToString());
                Console.WriteLine(JsonConvert.SerializeObject(y));
                _elements.Push(JsonConvert.SerializeObject(y));
            }
            if (op == "$eq")
            {
                var y = build_binary(op, _elements.Pop().ToString(), _elements.Pop().ToString());
                Console.WriteLine(JsonConvert.SerializeObject(y));
                _elements.Push(JsonConvert.SerializeObject(y));
            }
        }
    }
}