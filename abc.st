
1 <selectparameters>
2    $Prefixes,
3     ParamNames,
4     SourceNames,
5     SourceIds,
6     Types:
7     {pre, pn, sn, sid, t | <asp:$pre$Parameter 
    $if(sn)$$sn$="$sid$"$endif$ Name="$pn$" Type="$t$" />};separator="\n"$ 
8    $
9 </selectparameters>