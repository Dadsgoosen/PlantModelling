export interface PlantNodeModel {
  coordinates: [number, number][];
  thickness: number;
  connections: PlantNodeModel[];
}

export interface PlantModel {
  shootSystem: PlantNodeModel[];
  rootSystem: PlantNodeModel[];
}

export interface SimulationReplay {
  [key: number]: SimulationState
}

export interface SimulationState {
  id: string;
  simulationTime: number;
  date: string;
  plant: PlantModel;
}
