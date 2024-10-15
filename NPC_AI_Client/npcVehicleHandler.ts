import * as alt from 'alt-client';
import * as native from 'natives';
import { NPCVehicle } from '@/AI/npcVehicle';

export function registerEvents() {

}


alt.log("[NPCVehicleHandler] Loaded MainHandler");

class NPCVehicleHandler {
    NPCs: NPCVehicle[] = [];  // Define NPCs as an array of NPCVehicle instances
    interval: number;

    constructor() {
        alt.log("=================================");
        alt.log("=== NPCVehicleHandler Started ===");
        alt.log("=================================");

        this.interval = alt.setInterval(() => this._checkPeds(), 1000);
    }

    _checkPeds() {
        for (let i = 0; i < this.NPCs.length; i++) {
            this.NPCs[i].checkStatus();
        }
    }

    addNPCVehicle(vehicle: alt.Vehicle) {  // Explicitly type the vehicle parameter
        native.setVehicleEngineOn(vehicle.scriptID, true, true, false);  // Use vehicle.scriptID or vehicle.id as necessary
        this.NPCs.push(new NPCVehicle(vehicle));
    }

    removeNPCVehicle(vehicle: alt.Vehicle) {  // Explicitly type the vehicle parameter
        let index = this.NPCs.findIndex((x: NPCVehicle) => x.vehicle.id === vehicle.id);

        if (index < 0) return;

        let removed = this.NPCs.splice(index, 1);

        if (removed.length > 0) {
            removed[0].delete();  // Ensure that something is removed before calling delete
        }
    }

    updateNetOwner(vehicle: alt.Vehicle, state: boolean, transfer: any) {  // Explicitly type parameters
        let index = this.NPCs.findIndex((x: NPCVehicle) => x.vehicle.id === vehicle.id);

        if (index < 0) return;

        this.NPCs[index].updateNetOwner(state, transfer);
    }
}

export const NPCVehHandler = new NPCVehicleHandler();
