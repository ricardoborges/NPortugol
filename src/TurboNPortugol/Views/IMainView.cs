using TurboNPortugol.Presenters;

namespace TurboNPortugol.Views
{
    public interface IMainView
    {
        void AddTab(ExecPresenter presenter);

        void CloseTab(string name);
    }
}