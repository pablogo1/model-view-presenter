namespace ModelViewPresenter.WindowsForms.Shared
{
    public interface IPresenter<in TView> where TView : class
    {
        void SetView(TView view);
    }
}
