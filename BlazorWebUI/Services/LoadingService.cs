
namespace BlazorWebUI.Services;

public class LoadingService
{
    public event Action<bool> OnLoadingChanged;

    public void Show() => OnLoadingChanged?.Invoke(true);
    public void Hide() => OnLoadingChanged?.Invoke(false);
}