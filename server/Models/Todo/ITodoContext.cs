namespace Atento.FileManager.Web.Api.Models
{
    using MongoDB.Driver;

    public interface ITodoContext
    {
        IMongoCollection<Todo> Todos { get; }
    }
}