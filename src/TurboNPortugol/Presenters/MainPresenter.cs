using System;
using TurboNPortugol.Views;
using TurboNPortugol.Views.Exec;

namespace TurboNPortugol.Presenters
{
    public interface IMainPresenter
    {
        IMainView MainView { get; set; }

        void CreateWindow(string filepath);
        void CloseWindow(string key);
    }

    public class MainPresenter : IMainPresenter
    {
        public IMainView MainView { get; set; }

        public int WindowCount { get; set; }

        private string name = "Programa {0}";

        public void CreateWindow(string filepath)
        {
            var view = new ExecForm(filepath, string.Format(name, ++WindowCount));

            var presenter = new ExecPresenter(this) { ExecView = view };

            view.ExecPresenter = presenter;

            MainView.AddTab(presenter);
        }

        public void CloseWindow(string key)
        {
            MainView.CloseTab(key);
        }
    }
}