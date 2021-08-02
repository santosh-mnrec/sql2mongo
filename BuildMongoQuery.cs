using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace QueryLanguage
{
    public class BuildMongoQuery2
    {

        //build query from stack

        public static string BuildMongoQuery(Stack<string> stack)
        {
            string query = "";

            string current = stack.Pop();
            var where = elements.Pop();
            var find = elements.Pop();
            var select = elements.Pop();

            return find + ".find(" + JsonConvert.SerializeObject(where) + "," + JsonConvert.SerializeObject(select) + ")";


        }
    }
    public class BuildMongoQuery
    {
        private Stack<object> _elements;

        public BuildMongoQuery(Stack<object> elements)
        {
            _elements = elements;

        }
        public object BuildBinary(object op, object left, object right)
        {

            return new Dictionary<object, object> {
                {
                    op,
                    new List<object> {left,right }
                    }
                };
        }
        public object BuildSelect(object field)
        {


            return field;

        }
        public object BuildFrom(object op)
        {
            return "db." + op;

        }
        public void Parse(string op)
        {

            if (op == "$and")
            {
                _elements.Push(BuildBinary(op, _elements.Pop(), _elements.Pop()));


            }
            if (op == "$or")
            {
                _elements.Push(BuildBinary(op, _elements.Pop(), _elements.Pop()));


            }
            if (op == "$eq")
            {
                _elements.Push(BuildBinary(op, _elements.Pop(), _elements.Pop()));


            }
            if (op == "from")
            {
                _elements.Push(BuildFrom(_elements.Pop()));


            }
            if (op == "select")
            {
                _elements.Push(BuildSelect(_elements.Pop()));


            }
        }
    }
}