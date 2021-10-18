grammar SqlToMongoDB;

query :  select_stmt? from_stmt? where_stmt 
      ;

select_stmt : SELECT (STAR | (FIELD (COMMA FIELD)*));

from_stmt : FROM FIELD
          ;

where_stmt : WHERE search_condition+
           ;

search_condition : predicate (predicate ) *
                 ;
predicate : boolean_op? (comparison_predicate )
          ;
comparison_predicate : field (comparison_op value  )
                     ;

field : FIELD
      ;

comparison_op        :  EQ       # Equals
                     |  NE       # NotEqual
                     ;


boolean_op         :  AND         # And
                   |  OR          # Or
                 ;


value      : NUMBER               # Number
           | TERM                 # Term
           | PHRASE               # Phrase
           | DATE                 # Date
           | MULTI_PHRASE         # MULTI_PHRASE
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
DATE_SEP : [-/] ;
DATE : (NUMBER+(DATE_SEP)?)+;
FIELD : [A-Za-z]+((':'|'_')[0-9]*[A-Za-z]*)* ;

TERM : '\''(~[' *?])*'\'' ;
PHRASE : '\''(~['*?])*'\'' ;
WILD_CARD :'\''(~[' ])*'\'' ;
MULTI_PHRASE : '\''(~[' *?])+(~['])+'\'';
DB_QUOTE_STRING_LIT : ('"'(~["])*'"');
//STRING_LIT : ('\''(~['])*'\'') ;

COMMA : ',' ;
LPAREN : '(' ;
RPAREN : ')' ;
WS : [ \t\r\n]+ -> skip;