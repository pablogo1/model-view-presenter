namespace ModelViewPresenter.WindowsForms.Shared
{
    public interface IPresenter<in TView>
    {
        void Display();
        void SetView(TView view);
    }
}
