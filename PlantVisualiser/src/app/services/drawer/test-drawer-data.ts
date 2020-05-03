import {PlantNodeModel, SimulationState} from '../simulation/simulation-state';

const connectionThree: PlantNodeModel[] = [
  {x: -100, y: 250, thickness: 20, connections: []},
  {x: -90, y: 260, thickness: 20, connections: []}
];

const connectionFour: PlantNodeModel[] = [
  {x: -100, y: 250, thickness: 20, connections: []},
  {x: -110, y: 260, thickness: 20, connections: []}
];

const connectionOne: PlantNodeModel[] = [
  {x: 0, y: 150, thickness: 20, connections: []},
  {x: 150, y: 200, thickness: 20, connections: [connectionThree, connectionFour]},
  {x: 155, y: 220, thickness: 20, connections: []}
];

const connectionTwo: PlantNodeModel[] = [
  {x: 0, y: 200, thickness: 20, connections: []},
  {x: -100, y: 250, thickness: 20, connections: []}
];

export const simulationStateTest: SimulationState = {
  id: 'test-gui',
  date: '12345',
  simulationTime: 1234,
  plant: {
    rootSystem: [

    ],
    shootSystem: [
      {x: 0, y: 0, thickness: 20, connections: []},
      {x: 0, y: 50, thickness: 20, connections: []},
      {x: 0, y: 150, thickness: 20, connections: [connectionOne]},
      {x: 0, y: 200, thickness: 20, connections: [connectionTwo]},
      {x: 20, y: 300, thickness: 20, connections: []}
    ]
  }
};

