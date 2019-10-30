using System.Windows.Forms;

namespace ModelViewPresenter.WindowsForms.Shared
{
    public class MessageBoxAlertService : IAlertService
    {
        public void ShowError(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowWarning(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
