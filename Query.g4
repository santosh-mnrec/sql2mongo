grammar Query;

query :  select_stmt? from_stmt? where_stmt limit_stmt?
      ;

select_stmt : SELECT (STAR | (FIELD (COMMA FIELD)*));

from_stmt : FROM PATH
          ;

where_stmt : WHERE search_condition+
           ;

limit_stmt : LIMIT NUMBER
           ;

search_condition : predicate (predicate | nested_predicate) *
                 ;

predicate : boolean_op? (comparison_predicate | function_predicate)
          ;

comparison_predicate : field (comparison_op value | range_op )
                     ;

function_predicate : regexp  | like | in
                   ;

field : FIELD
      ;

comparison_op        :  EQ       # Equals
                     |  NE       # NotEqual
                     ;

range_op     : greater_than
             | greater_than_equals
             | less_than
             | less_than_equals
             ;


greater_than   :   GT NUMBER    #greaterThanNumber
               |   GT TERM      #greaterThanTerm
 ;

greater_than_equals : GTE NUMBER   #greaterThanEqNumber
                    | GTE TERM     #greaterThanEqTerm
                ;
less_than : LT NUMBER    #lessThanNumber
          | LT TERM      #lessThanTerm
         ;
less_than_equals : LTE NUMBER    #lessThanEqNumber
                 | LTE TERM      #lessThanEqTerm
             ;


boolean_op         :  AND         # And
                   |  OR          # Or
                 ;

nested_predicate : boolean_op? LPAREN predicate (predicate | nested_predicate)* RPAREN
                 ;

value      : NUMBER               # Number
           | TERM                 # Term
          
           ;

regexp  : field MATCHES LPAREN WILD_CARD RPAREN
        ;


like : field LIKE WILD_CARD
     ;

in : field NOT? IN value_list
   ;

value_list : LPAREN (number_list  | term_list  ) RPAREN
           ;

number_list : NUMBER (COMMA NUMBER)*
            ;

      

term_list : TERM (COMMA TERM)*
          ;


           
//Parser Rules End

//Lexer Rules Start
SELECT : [Ss][Ee][Ll][Ee][Cc][Tt] ;
FROM : [Ff][Rr][Oo][Mm] ;
WHERE : [Ww][Hh][Ee][Rr][Ee] ;
AND : [Aa][Nn][Dd] ;
OR : [Oo][Rr] ;
NOT : [Nn][Oo][Tt] ;
DESCRIBE : [Dd][Ee][Ss][Cc][Rr][Ii][Bb][Ee] ;
MATCHES : [Mm][Aa][Tt][Cc][Hh][Ee][Ss] ;

LIKE : [Ll][Ii][Kk][Ee] ;
LIMIT : [Ll][Ii][Mm][Ii][Tt] ;
EQ : '=' ;
NE : '!=';
GT : '>' ;
LT : '<' ;
GTE : '>=' ;
LTE : '<=' ;
IN : [Ii][Nn];
STAR : '*';
fragment
DIGIT : [0-9] ;
NUMBER : DIGIT(DIGIT*) ;
fragment

FIELD : [A-Za-z]+((':'|'_')[0-9]*[A-Za-z]*)* ;
PATH : '\''([A-Za-z]':')?('/')?((~[/])+'/')+ '\'';
TERM : '\''(~[' *?])*'\'' ;

WILD_CARD :'\''(~[' ])*'\'' ;


COMMA : ',' ;
LPAREN : '(' ;
RPAREN : ')' ;
WS : [ \t\r\n]+ -> skip;