using System;
using System.Globalization;

namespace NPortugol.Runtime.Exceptions
{
    public class MainNotFoundException: Exception
    {
        private const string ptMessage =
            "Função main não encontrada. Certifique-se que o script contenha uma função main definida.";

        private const string enMessage = "Main function not found. Ensure your script has a main function defined.";

        public override string Message
        {
            get { return CultureInfo.CurrentCulture.Name.StartsWith("pt") ? ptMessage : enMessage; }
        }
    }
}