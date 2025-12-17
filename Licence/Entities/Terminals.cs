using System.Collections.Generic;

namespace Licence.Entities
{
    public class Terminals
    {
        public short Id { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        public string CompanyModel => Company + " " + Model;

        public static List<Terminals> SeedTerminalData()
        {
            return new List<Terminals>
                {
                    new Terminals { Id = 1, Company = "CASPOS", Model = "SUNMI V1S" },
                    new Terminals { Id = 2, Company = "CASPOS", Model = "TİANYU S30" },
                    new Terminals { Id = 3, Company = "CASPOS", Model = "Z108" },
                    new Terminals { Id = 4, Company = "CASPOS", Model = "Z92" },
                    new Terminals { Id = 5, Company = "OMNITECH", Model = "TPS 575" },
                    new Terminals { Id = 6, Company = "OMNITECH", Model = "TPS 320" },
                    new Terminals { Id = 7, Company = "OMNITECH", Model = "AİSİNO A90" },
                    new Terminals { Id = 8, Company = "OMNITECH", Model = "M8" },
                    new Terminals { Id = 9, Company = "NBA", Model = "VERİFONE X990" },
                    new Terminals { Id = 10, Company = "NBA", Model = "PAX A35" },
                    new Terminals { Id = 11, Company = "EKASSAM", Model = "WIZARPOS Q2" },
                    new Terminals { Id = 12, Company = "DATAPAY", Model = "DATAPAY P20L" },
                    new Terminals { Id = 13, Company = "AZSMART", Model = "AZSMART" },
                    new Terminals { Id = 14, Company = "YOXDUR", Model = string.Empty },
                };
        }
    }
}
