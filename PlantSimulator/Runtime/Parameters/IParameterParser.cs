namespace PlantSimulator.Runtime.Parameters
{
    public interface IParameterParser
    {
        Parameters Parse(string[] args);
    }
}