using System.Collections.Generic;
using System.Globalization;

namespace NPortugol.Runtime.Exceptions
{
    public class Ops
    {
        private static readonly Dictionary<string, string> br = new Dictionary<string, string>
                                                                    {
                                                                        {"no_variable", "Parece que voc� n�o declarou a vari�vel '{0}'."},
                                                                        {"no_var_init", "Voc� esqueceu de inicializar a vari�vel '{0}'."},
                                                                        {"is_not_variable", "Tem certeza que '{0}' pode ser usado como vari�vel?"},
                                                                        {"no_array_init", "Se pretende usar '{0}' como um vetor, voc� deve inicializa-lo antes."},
                                                                    };

        private static readonly Dictionary<string, string> en = new Dictionary<string, string>
                                                                    {
                                                                        {"no_variable", "It seems you didn't declare '{0}' variable."},
                                                                        {"no_var_init", "You didn't initialize the variable '{0}'."},
                                                                        {"no_array_init", "If you expect using '{0}' as array, you must initialize it before."},
                                                                        {"is_not_variable", "Are you sure '{0}' can be used as variable?"},
                                                                    };

        public Dictionary<string, string> Messages
        {
            get { return ResolveLang(); }
        }

        public static Dictionary<string, string> ResolveLang()
        {
            return CultureInfo.CurrentCulture.Name.StartsWith("pt") ? br : en;
        }

        public void ThrowNonDeclared(string name)
        {
            throw new RuntimeException(string.Format(Messages["no_variable"], name));
        }

        public void ThrowArrayNonInit(string name)
        {
            throw new RuntimeException(string.Format(Messages["no_array_init"], name));
        }

        public void ThrowNotVariable(string name)
        {
            throw new RuntimeException(string.Format(Messages["is_not_variable"], name));
        }

        public void ThrowVarNonInit(string name)
        {
            throw new RuntimeException(string.Format(Messages["no_var_init"], name));
        }
    }
}