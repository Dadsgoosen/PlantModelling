import {SimulationState} from '../simulation/simulation-state';

export interface Drawer {
  draw(state: SimulationState): void;
}
