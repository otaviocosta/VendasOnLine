using System;

namespace VendasOnLine.Domain
{
    public class Id
    {
        public Id(DateTime data, int sequencial)
        {
            Value = $"{data.Year}{sequencial:00000000}";
        }

        public string Value { get; private set; }
    }
}
