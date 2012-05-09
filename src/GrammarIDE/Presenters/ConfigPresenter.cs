using GrammarIDE.Core;

namespace GrammarIDE.Presenters
{
    public interface IConfigPresenter
    {
        void Save(string path);

        Config GetConfig();
    }

    public class ConfigPresenter : IConfigPresenter
    {
        private readonly IConfigRepo configRepo;
        private readonly IMainPresenter mainPresenter;

        public ConfigPresenter(IConfigRepo configRepo, IMainPresenter presenter)
        {
            this.configRepo = configRepo;
            mainPresenter = presenter;
        }

        public void Save(string path)
        {
            configRepo.SavePath(path);

            mainPresenter.MainView.WriteOutput("Configuração salva.");
        }

        public Config GetConfig()
        {
            return configRepo.GetConfig();
        }
    }
}