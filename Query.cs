using System;
using System.Collections.Generic;
using System.Text;

namespace QueryLanguage
{

    public abstract class ASTNode
    {
        
    }

    public class Terminal : ASTNode
    {
        public string value { get; set; }

        public Terminal(string value)
        {
            this.value = value;
        }
    }


    public class Expression : ASTNode
    {
        public string operation { get; set; }
        public ASTNode left { get; set; }
        public ASTNode right { get; set; }
    }






}