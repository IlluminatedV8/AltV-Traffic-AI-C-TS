import * as alt from 'alt-client';
import { NPCVehHandler } from '@/AI/npcVehicleHandler';

export function registerEvents() {

    alt.on("gameEntityCreate", (entity: alt.Entity) => {
        // Check if the entity is a vehicle
        if (!(entity instanceof alt.Vehicle)) return;

        // Check if the vehicle has the "NPCVehicle" stream synced metadata
        if (!entity.hasStreamSyncedMeta("NPCVehicle")) return;

        // Add the vehicle to the NPCVehHandler
        NPCVehHandler.addNPCVehicle(entity);
    });
}


