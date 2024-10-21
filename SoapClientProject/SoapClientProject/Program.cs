using System;
using SoapClientProject.CapitalCitySoapReference;

namespace SoapClientProject
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Isle();
        }
        public static void Isle()
        {
            string ulkeIsim;

            while (true)
            {
                Console.Write("Bir ülke gir: ");
                ulkeIsim = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(ulkeIsim))
                    break;
            }

            Country country = new Country();
            var IsoCode = country.GetIsoCode(ulkeIsim);
            CountryInfoServiceSoapTypeClient client = new CountryInfoServiceSoapTypeClient("CountryInfoServiceSoap");
            country.CapitalCity = client.CapitalCity(IsoCode);
            country.ISOCode = client.FullCountryInfo(IsoCode).sISOCode;
            country.PhoneCode = client.FullCountryInfo(IsoCode).sPhoneCode;
            country.Language = client.FullCountryInfo(IsoCode).Languages;

            EkranaYazdir(ulkeIsim, country.CapitalCity, country.ISOCode, country.PhoneCode, country.Language);
        }
        public static void EkranaYazdir(string result, string CapitalCity, string ISOCode, string PhoneCode, tLanguage[] language)
        {
            Console.WriteLine($"Girilen Ulke {result}; \n Baskent: {CapitalCity} \n IsoCode: {ISOCode} \n Telefon Kodu: {PhoneCode}");
            foreach (var tLan in language)
            {
                Console.WriteLine($" Dil: {tLan.sName}");
            }
            Console.ReadKey();
        }

    }
}
