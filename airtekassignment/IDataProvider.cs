using System.Collections.Generic;


namespace airtekassignment
{
    internal interface IDataProvider<T>
    {
        List<T> GetData();
    }
}
