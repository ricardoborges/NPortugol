using System;
using System.Globalization;

namespace NPortugol.Runtime.Exceptions
{
    public class MainNotFoundException: Exception
    {
        private const string ptMessage =
            "Fun��o main n�o encontrada. Certifique-se que o script contenha uma fun��o main definida.";

        private const string enMessage = "Main function not found. Ensure your script has a main function defined.";

        public override string Message
        {
            get { return CultureInfo.CurrentCulture.Name.StartsWith("pt") ? ptMessage : enMessage; }
        }
    }
}