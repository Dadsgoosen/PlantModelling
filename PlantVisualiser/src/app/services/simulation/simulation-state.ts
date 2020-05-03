export interface PlantNodeModel {
  x: number;
  y: number;
  thickness: number;
  connections: PlantNodeModel[][];
}

interface PlantModel {
  shootSystem: PlantNodeModel[];
  rootSystem: PlantNodeModel[];
}

export interface SimulationState {
  id: string;
  simulationTime: number;
  date: string;
  plant: PlantModel;
}
