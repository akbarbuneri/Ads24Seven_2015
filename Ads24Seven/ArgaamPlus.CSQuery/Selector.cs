namespace ArgaamPlus.CSQuery
{
    using System.Collections.Generic;

     
    public delegate IEnumerable<TElement> Selector<TElement>(IEnumerable<TElement> elements);
}