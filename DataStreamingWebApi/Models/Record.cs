namespace DataStreamingWebApi.Models
{
    public class Record
    {
        public string IsoCode { get; set; }
        public string Continent { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public string TotalCases { get; set; }
        public string NewCases { get; set; }
        public string NewCasesSmoothed { get; set; }
        public string TotalDeaths { get; set; }
        public string NewDeaths { get; set; }
        public string NewDeathsSmoothed { get; set; }
        public string TotalCasesPerMillion { get; set; }
        public string NewCasesPerMillion { get; set; }
        public string NewCasesSmoothedPerMillion { get; set; }
        public string TotalDeathsPerMillion { get; set; }
        public string NewDeathsPerMillion { get; set; }
        public string NewDeathsSmoothedPerMillion { get; set; }
        public string IcuPatients { get; set; }
        public string IcuPatientsPerMillion { get; set; }
        public string HospPatients { get; set; }

        // You can also add a constructor if needed
        public Record() { }

    }

}
