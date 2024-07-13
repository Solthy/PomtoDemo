using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;

namespace PomtoInfraData.Helpers
{
    public class EncryptedConverter : ValueConverter<string, string>
    {
        public EncryptedConverter(IDataProtector protector) : base(v => protector.Protect(v), v => protector.Unprotect(v))
        { }
    }

    public class EncryptedIntConverter : ValueConverter<int, string>
    {
        public EncryptedIntConverter(IDataProtector protector) : base(v => protector.Protect(v.ToString()), v => int.Parse(protector.Unprotect(v)))
        { }
    }

    public class EncryptedDateConverter : ValueConverter<DateTime, string>
    {
        public EncryptedDateConverter(IDataProtector protector) : base(v => protector.Protect(v.ToString()), v => DateTime.Parse(protector.Unprotect(v)))
        { }
    }

    public class EncryptedDecimalConverter : ValueConverter<decimal, string>
    {
        public EncryptedDecimalConverter(IDataProtector protector) : base(v => protector.Protect(v.ToString(CultureInfo.InvariantCulture)), v => decimal.Parse(protector.Unprotect(v), CultureInfo.InvariantCulture))
        { }
    }
}
