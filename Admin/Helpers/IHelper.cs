namespace Admin;

public interface IHelper
{
    Task<string> RenderPartialToStringAsync<TModel>(string partialName, TModel model);
}