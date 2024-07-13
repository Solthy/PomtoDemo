using System.Globalization;
using System.Text;

namespace PomtoApplication.Helpers
{
    public class ResponseHelper
    {
        public bool IsConnect { get; set; }

        public string UserNomeFinal(string nomeCompleto)
        {
            string guid = Guid.NewGuid().ToString().Substring(0,3);
            string[] partesNome = nomeCompleto.Split(' ');

            string primeiroNome = partesNome[0];

            primeiroNome = RemoveAcentos(primeiroNome);

            primeiroNome = primeiroNome.ToLower();
            primeiroNome = char.ToUpper(primeiroNome[0]) + primeiroNome.Substring(1);

            string novoNome = primeiroNome + guid;

            return novoNome;
        }

        public string RemoveAcentos(string texto)
        {
            string normalizedString = texto.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public bool UserValidToWork(DateTime? date)
        {
            bool validDate = false;

            int userYears = DateTime.Now.Year - date.Value.Year;

            if (userYears >= 15)
                return validDate = true;
            else if (userYears < 14)
                return validDate = false;

            return validDate;
        }

        public bool UserHoursToWork(int value)
        {
            bool validhours = false;

            if (value <= 8)
                validhours = true;
            else
                validhours = false;

            return validhours;
        }

        public string EmailConvertToLowerCase(string email)
        {
            return email.ToLower();
        }

        public bool IsConnectOnInternet()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType == NetworkAccess.Internet)
                return IsConnect = true;
            else
                return IsConnect = false;
        }
    }
}
