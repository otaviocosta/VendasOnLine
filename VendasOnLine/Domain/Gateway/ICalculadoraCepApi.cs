namespace VendasOnLine.Domain
{
    public interface ICalculadoraCepApi
    {
        double Calcular(string CepOrigem, string CepDestino);
    }
}
