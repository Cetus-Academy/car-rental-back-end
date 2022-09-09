namespace CarRental.Application.Shared;

public interface IRazorViewRenderer
{
    Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    Task<string> RenderViewToStringAsync(string viewName, Dictionary<string, object> viewData = null);
}
