import * as alt from 'alt-client';
import { NPCVehHandler } from '@/AI/npcVehicleHandler';


export function registerEvents() {
    alt.onServer("NPCVehicle:NetOwner", (vehicle, state, transfer = undefined) => {
        alt.log(`[NPCVehicle:NetOwner] Vehicle: ${vehicle.id}, State: ${state}, Transfer: ${transfer}`);
        NPCVehHandler.updateNetOwner(vehicle, state, transfer);
    });
}


