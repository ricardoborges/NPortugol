using System;
using System.Globalization;

namespace NPortugol.Runtime.Exceptions
{
    public class FunctionNotFoundException: Exception
    {
        private readonly string function;

        public FunctionNotFoundException(string name)
        {
            function = name;
        }

        private const string ptMessage = "Função {0} não encontrada. Certifique-se que o script contenha esta função definida.";

        private const string enMessage = "{0} function not found. Ensure your script has that function defined.";

        public override string Message
        {
            get { return string.Format(CultureInfo.CurrentCulture.Name.StartsWith("pt") ? ptMessage : enMessage, function); }
        }
    }
}