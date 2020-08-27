using System.Collections.Generic;


namespace airtekassignment
{
    public interface IDataProvider<T>
    {
        List<T> GetData();
    }
}
