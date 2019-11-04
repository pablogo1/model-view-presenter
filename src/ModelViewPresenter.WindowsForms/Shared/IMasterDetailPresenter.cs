namespace ModelViewPresenter.WindowsForms.Shared
{
    public interface IMasterDetailPresenter<in TView> : IPresenter<TView>
    {
        void DisplayDetail();
    }
}
