namespace VendasOnLine.Infra
{
    public interface ICalculadoraCepApi
    {
        double Calcular(string CepOrigem, string CepDestino);
    }
}
