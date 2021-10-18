using System.Collections.Generic;
using Newtonsoft.Json;

namespace SqlToMongoDB
{
    public class BuildMongoQuery
    {
        private Stack<object> _elements;
        public string BuildQuery()
        {

            var where = _elements.Pop();
            var find = _elements!.Pop();
            var select = _elements.Pop();

            return find + ".find(" + JsonConvert.SerializeObject(where) + "," + JsonConvert.SerializeObject(select) + ")";


        }
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