import * as alt from 'alt-client';
import * as native from 'natives';

export function registerEvents() {
    //todo
}

alt.log("[NPCVehicleHandler] Loaded GlobalPeds");

class GlobalPeds {
    // Define the type of the peds array, assuming it's an array of numbers (handles)
    peds: number[];

    constructor() {
        // Initialize the peds array as an empty array
        this.peds = [];

        // Set an interval to periodically filter the peds
        //alt.setInterval(() => this._filterPeds(), 2000);
    }

    // Add a ped handle (number) to the array
    addPed(handle: number) {
        this.peds.push(handle);
    }

    // Private method to filter and delete peds
    private _filterPeds() {
        // Loop through each ped in the array
        for (let i = 0; i < this.peds.length; i++) {
            // Check if the entity is a ped
            if (!native.isEntityAPed(this.peds[i])) continue;

            // Skip if the ped is in any vehicle
            if (native.isPedInAnyVehicle(this.peds[i], true)) continue;

            // Delete the ped if it's not in a vehicle
            native.deletePed(this.peds[i]);
        }

        // Filter the array to keep valid peds
        this.peds = this.peds.filter(x => x != null && x != undefined && native.isEntityAPed(x));
    }
}

// Export an instance of GlobalPeds
export const PedHandler = new GlobalPeds();

