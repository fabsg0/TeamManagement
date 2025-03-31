namespace Web.TeamManagement.Blazor.ToastMessage;

public class ToastService
{
    public event Action<string, string, int>? OnShow;
    public event Action? OnHide;

    private void ShowToast(string message, string type = "info", int dismissAfter = 5)
    {
        OnShow?.Invoke(message, type, dismissAfter);
    }

    public void HideToast()
    {
        OnHide?.Invoke();
    }

    public void ShowSuccess(string message, int dismissAfter = 5) => ShowToast(message, "success", dismissAfter);
    public void ShowError(string message, int dismissAfter = 5) => ShowToast(message, "failure", dismissAfter);
    public void ShowWarning(string message, int dismissAfter = 5) => ShowToast(message, "warning", dismissAfter);
    public void ShowSInfo(string message, int dismissAfter = 5) => ShowToast(message, "info", dismissAfter);
}