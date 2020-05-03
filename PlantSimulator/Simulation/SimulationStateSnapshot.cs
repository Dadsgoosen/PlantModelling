namespace PlantSimulator.Simulation
{
    public struct SimulationStateSnapshot
    {
        public ulong CurrentTime { get; }

        public SimulationStateSnapshot(ulong currentTime)
        {
            CurrentTime = currentTime;
        }
    }
}