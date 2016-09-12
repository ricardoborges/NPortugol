using System;
using System.Collections.Generic;
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

        public List<Ajuda> AjudaList()
        {
            return new List<Ajuda>
            {
                new Ajuda { Nome = "imprima", Descrição = "Imprime valor no console de saída."},
                new Ajuda { Nome = "leia", Descrição = "Recebe valor de entrada para atribuição de variável."},
                new Ajuda { Nome = "imprimaVetor", Descrição = "Imprime todos os valores de um vetor."},
                new Ajuda { Nome = "vetorizar", Descrição = "Cria um vetor com cada posição de um número ou palavra."},
                new Ajuda { Nome = "data", Descrição = "Cria um valor do tipo Data."},
                new Ajuda { Nome = "hoje", Descrição = "Cria um valor do tipo Data com o dia atual."},
                new Ajuda { Nome = "nomedia", Descrição = "Cria um valor com o nome do dia atual."}
            };
        }
    }

    public class Ajuda
    {
        public string Nome { get; set; }

        public string Descrição { get; set; }
    }
}