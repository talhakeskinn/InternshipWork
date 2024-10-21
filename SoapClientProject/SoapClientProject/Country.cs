
using SoapClientProject.CapitalCitySoapReference;

namespace SoapClientProject
{
    public enum CountryCode
    {
        US,  // United States
        CA,  // Canada
        GB,  // United Kingdom
        DE,  // Germany
        FR,  // France
        IT,  // Italy
        ES,  // Spain
        TR,  // Turkey
        JP,  // Japan
        AU,   // Australia
        BE,
        CH
    }

    public class Country
    {
        public string CapitalCity { get; set; }
        public string ISOCode { get; set; }
        public tLanguage[] Language { get; set; }
        public string PhoneCode { get; set; }

        public string GetIsoCode(string Name)
        {

            switch (Name)
            {
                case "Amerika":
                    return CountryCode.US.ToString();
                case "Kanada":
                    return CountryCode.CA.ToString();
                case "Ingiltere":
                    return CountryCode.GB.ToString();
                case "Almanya":
                    return CountryCode.DE.ToString();
                case "Fransa":
                    return CountryCode.FR.ToString();
                case "Italya":
                    return CountryCode.IT.ToString();
                case "Ispanya":
                    return CountryCode.ES.ToString();
                case "Turkiye":
                    return CountryCode.TR.ToString();
                case "Japonya":
                    return CountryCode.JP.ToString();
                case "Avustralya":
                    return CountryCode.AU.ToString();
                case "Belcika":
                    return CountryCode.BE.ToString();
                case "Isvicre":
                    return CountryCode.CH.ToString();
                default:
                    return "Bilinmeyen";
            }
        }
    }
}

