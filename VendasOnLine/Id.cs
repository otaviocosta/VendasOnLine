using System;

namespace VendasOnLine
{
    public class Id
    {
        public Id(int sequencial)
        {
            seq = sequencial;
            Value = $"{DateTime.Now.Year}{sequencial:00000000}";
        }

        int seq;

        public string Value { get; private set ; }
    }
}
