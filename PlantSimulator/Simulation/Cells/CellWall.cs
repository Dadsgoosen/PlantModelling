namespace PlantSimulator.Simulation.Cells
{
    public class CellWall : ICellWall
    {
        public bool Primary { get; set; }
        public float Thickness { get; set; }
        public float Rigidness { get; } = 1;
    }
}