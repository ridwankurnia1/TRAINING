import { Injectable } from '@angular/core';
import { PalletType } from '../_model/PalletType';

@Injectable({
  providedIn: 'root',
})
export class PalletTypeService {
  pallets: PalletType[] = [];

  constructor() {
    this._initData();
  }

  create(pallet: PalletType) {
    this.pallets.push(pallet);
  }

  all() {
    return this.pallets;
  }

  private _initData() {
    this.pallets = [
      {
        id: 1,
        name: 'GGASAG',
        type: 'ASADADD',
        codification: 1,
        app: 'OWP',
        materialType: 'WOODEN',
        color: 'BLACK',
        currency: 'IDR',
        recordStatus: 1,
        weight: 144,
        height: 145,
        length: 133,
        width: 143,
        widthType: 'MM',
        weightType: 'MR',
        lengthType: 'MM',
        price: 1453124,
        remark: 'OK',
        createdTimestamp: '13-04-2023',
      },
      {
        id: 2,
        name: 'NMHHSD',
        type: 'ASDAFAF',
        codification: 1,
        app: 'OWP',
        materialType: 'WOODEN',
        color: 'BLACK',
        currency: 'IDR',
        recordStatus: 1,
        weight: 144,
        height: 145,
        length: 133,
        width: 143,
        widthType: 'MM',
        weightType: 'MR',
        lengthType: 'MM',
        price: 1453124,
        remark: 'OK',
        createdTimestamp: '13-04-2023',
      },
      {
        id: 3,
        name: 'TTQQAS',
        type: 'ASDASFA',
        codification: 1,
        app: 'OWP',
        materialType: 'WOODEN',
        color: 'BLACK',
        currency: 'IDR',
        recordStatus: 1,
        weight: 144,
        height: 145,
        length: 133,
        width: 143,
        widthType: 'MM',
        weightType: 'MR',
        lengthType: 'MM',
        price: 1453124,
        remark: 'OK',
        createdTimestamp: '13-04-2023',
      },
      {
        id: 4,
        name: 'GHASAD',
        type: 'FFASSAE',
        codification: 1,
        app: 'OWP',
        materialType: 'WOODEN',
        color: 'BLACK',
        currency: 'IDR',
        recordStatus: 1,
        weight: 144,
        height: 145,
        length: 133,
        width: 143,
        widthType: 'MM',
        weightType: 'MR',
        lengthType: 'MM',
        price: 1453124,
        remark: 'OK',
        createdTimestamp: '13-04-2023',
      },
      {
        id: 5,
        name: 'QWAASD',
        type: 'FFASDAS',
        codification: 1,
        app: 'OWP',
        materialType: 'WOODEN',
        color: 'BLACK',
        currency: 'IDR',
        recordStatus: 1,
        weight: 144,
        height: 145,
        length: 133,
        width: 143,
        widthType: 'MM',
        weightType: 'MR',
        lengthType: 'MM',
        price: 1453124,
        remark: 'OK',
        createdTimestamp: '13-04-2023',
      },
      {
        id: 6,
        name: 'MMSSDS',
        type: 'ASDASFAS',
        codification: 1,
        app: 'OWP',
        materialType: 'WOODEN',
        color: 'BLACK',
        currency: 'IDR',
        recordStatus: 1,
        weight: 144,
        height: 145,
        length: 133,
        width: 143,
        widthType: 'MM',
        weightType: 'MR',
        lengthType: 'MM',
        price: 1453124,
        remark: 'OK',
        createdTimestamp: '13-04-2023',
      },
    ];
  }
}
