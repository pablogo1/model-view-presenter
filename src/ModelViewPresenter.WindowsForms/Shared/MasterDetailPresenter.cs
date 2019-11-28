namespace ModelViewPresenter.WindowsForms.Shared
{
    public abstract class MasterDetailPresenter<TView> : IMasterDetailPresenter<TView>
        where TView : class
    {
        protected TView View { get; set; }

        public void SetView(TView view)
        {
            View = view ?? throw new System.ArgumentNullException(nameof(view));
            SetupView();
        }

        protected abstract void Display();
        protected abstract void DisplayDetail();
        protected abstract void SetupView();
    }
}
