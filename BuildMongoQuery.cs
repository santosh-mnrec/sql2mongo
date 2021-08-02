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
        public object BuildBinary(object op, object left, object right)
        {

            return new Dictionary<object, object> {
                {
                    op,
                    new List<object> {left,right }
                    }
                };
        }
        public object BuildSelect(object field){
           
                   
                  return field;
        
        }
         public object BuildFrom(object op)
        {
                return "db."+op;
           
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
               _elements.Push( BuildBinary(op, _elements.Pop(), _elements.Pop()));
              
              
            }
            if (op == "from")
            {
                _elements.Push( BuildFrom( _elements.Pop()));
              
               
            }
             if (op == "select")
            {
                _elements.Push( BuildSelect(_elements.Pop()));
              
              
            }
        }
    }
}