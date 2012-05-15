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

        private string templateName = "Programa {0}.p";

        public void CreateWindow(string filepath)
        {
            string name = string.Format(templateName, ++WindowCount);

            if (!string.IsNullOrEmpty(filepath))
            {
                var parts = filepath.Split('\\');
                name = parts[parts.Length - 1];
            }

            var view = new ExecForm(filepath, name);

            var presenter = new ExecPresenter(this) { ExecView = view, ProgramName = view.ProgramName, FilePath = view.FilePath};

            view.ExecPresenter = presenter;

            MainView.AddTab(presenter);
        }

        public void CloseWindow(string key)
        {
            MainView.CloseTab(key);
        }
    }
}