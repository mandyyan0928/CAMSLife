using System.Threading.Tasks;

namespace CaliphWeb.Services.Helper
{
    public interface ICaliphAPIHelper
    {
        Task<TReturn> PostAsync<T, TReturn>(T req, string post) where T : class;

        Task<TReturn> PostAsync< TReturn>(string post) where TReturn : class;
        Task<TReturn> PostAsyncPayLater<T, TReturn>(T req, string post) where T : class;

    }
}