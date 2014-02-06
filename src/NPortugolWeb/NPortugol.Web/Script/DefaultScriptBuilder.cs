using System.Text;

namespace NPortugol.Web.Script
{
    public interface IDefaultScriptBuilder
    {
        string Parse(string page);
    }

    /// <summary>
    /// Prova de conceito
    /// </summary>
    /// TODO: Criar um parser utilizando ANTLR que transforme o arquivo np em funções NPortugol
    public class DefaultScriptBuilder : IDefaultScriptBuilder
    {
        private string _tab = "    ";

        public string Parse(string page)
        {
            int cmd = 2;

            var sb = new StringBuilder();
            var sf = new StringBuilder();

            sb.AppendLine("funcao page(infra)");

            if (page.IndexOf("<%") < 0)
                sb.AppendLine(ToScript(page).Trim());

            while (page.IndexOf("<%") > 0)
            {
                var start = page.IndexOf("<%") + cmd;
                var end = page.IndexOf("%>");
                var html = ToScript(page.Substring(0, start - cmd));
                var slice = page.Substring(start, end - start).Trim();

                sb.AppendLine(_tab + html.Trim());

                if (!slice.StartsWith("funcao"))
                    sb.AppendLine(_tab + slice.Trim());
                else
                    sf.AppendLine(_tab + slice.Trim());

                page = page.Remove(0, end + cmd);
            }

            sb.AppendLine("fim");

            return sb.ToString() + sf;
        }

        private string ToScript(string text)
        {
            var sb = new StringBuilder();
            var parts = text.Split('\r');

            foreach (var part in parts)
            {
                var line = part.Replace("\n", "").Trim();

                if (string.IsNullOrEmpty(line)) continue;

                sb.AppendLine(_tab + string.Format("escreva(\"{0}\")", line));
            }

            return sb.ToString();
        }
    }
}