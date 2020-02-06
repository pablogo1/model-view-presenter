using ModelViewPresenter.WindowsForms.MasterDetailWithDirectMethodCalls.Model;
using ModelViewPresenter.WindowsForms.MasterDetailWithDirectMethodCalls.View;
using ModelViewPresenter.WindowsForms.Shared;
using System.Threading.Tasks;

namespace ModelViewPresenter.WindowsForms.MasterDetailWithDirectMethodCalls.Presenter
{
    public interface IContactsPresenter : IMasterDetailPresenter<IContactsView>
    {
        void LoadView();
        void ChangeSelectedContact(Contact selectedContact);
        void SaveContact();
        void RemoveContact();
        void NewContact();
    }
}
