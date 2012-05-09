using System;
using GrammarIDE.Core;
using GrammarIDE.Views;

namespace GrammarIDE.Presenters
{
    public interface IMainPresenter
    {
        IMainView MainView { get; set; }

        void Error(Exception ex);
        void Error(string text);
    }

    public class MainPresenter : IMainPresenter
    {
        public IMainView MainView { get; set; }

        public void Error(Exception ex)
        {
            MainView.ClearOutput();
            MainView.WriteOutput(ex.Message);
        }

        public void Error(string text)
        {
            MainView.ClearOutput();
            MainView.WriteOutput(text);
        }
    }
}