namespace ModelViewPresenter.WindowsForms.Shared
{
    public interface IAlertService
    {
        void ShowError(string message, string title);
        void ShowWarning(string message, string title);
    }
}
