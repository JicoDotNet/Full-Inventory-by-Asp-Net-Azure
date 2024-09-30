namespace DataAccess.Sql.Entity
{
    public interface INameValuePair
    {
        string getName { get; }
        object getValue { get; }
    }
}
