import * as alt from 'alt-client';
import * as native from 'natives';
import {PedHandler} from '@/AI/globalPeds';

export function registerEvents() {
    //todo
}


alt.log("[NPCVehicleHandler] Loaded NPCVehicle");

export class NPCVehicle {
    vehicle: any = undefined;
    ped: any = undefined;
    netOwner = false;
    isCreated = false;
    lastDownCheck = false;

    constructor(vehicle: alt.Vehicle) {
        this.vehicle = vehicle;
        this._createPed();
    }

    _createPed() {
        this.ped = native.createRandomPedAsDriver(this.vehicle.scriptID, true);
        native.setVehicleDoorsLocked(this.vehicle.scriptID, 4);
        alt.setTimeout(() => {
            this._setCreated();
        }, 100);
    }

    _setCreated() {
        this.isCreated = true;
        if (this.netOwner) this._assignTask();
        PedHandler.addPed(this.ped);
    }

    _assignTask() {
        if (!this.isCreated || !this.netOwner) return

        native.taskVehicleDriveWander(this.ped, this.vehicle.scriptID, 20, 786491);
    }

    _clearTask(transfer: any) {
        if (!this.isCreated || this.netOwner) return;
        native.clearPedTasksImmediately(this.ped);

        if (!transfer) native.setEntityVelocity(this.vehicle, 0, 0, 0);
    }

    _checkStuck() {
        let roll = native.getEntityRoll(this.vehicle.scriptID);
        if (roll < 80 && roll > -80) {
            this.lastDownCheck = false;
            return;
        }

        if (this.lastDownCheck) {
            native.setVehicleOnGroundProperly(this.vehicle.scriptID, 5.0);
            this.lastDownCheck = false
            return
        }

        this.lastDownCheck = true;
    }

    checkStatus() {
        if (this.netOwner) this._checkStuck();

//        if (native.getPedInVehicleSeat(this.vehicle.scriptID, -1, undefined) == 0) {
//            this._createPed();
//            return;
//        }
        if (!native.getIsTaskActive(this.ped, 151)) {
            this._assignTask();
            return;
        }
    }

    updateNetOwner(state: boolean, transfer: any) {
        this.netOwner = state;

        if (state) this._assignTask();
        else this._clearTask(transfer);
    }

    delete() {
        native.deleteEntity(this.ped);
    }
}