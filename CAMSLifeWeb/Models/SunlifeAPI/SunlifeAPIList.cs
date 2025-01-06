using System.Collections.Generic;

public class SunlifeAPIList<T>
{
    public SunlifeAPIList()
    {
        subList = new List<T>();
    }
   
    public List<T> subList { get; set; }
    public int numberOfPages { get; set; }
    public int numberOfItems { get; set; }
    public int currentPage { get; set; }
}