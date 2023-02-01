import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';

@Injectable({
  providedIn: 'root',
})
export class ExcelService {
  constructor() {}
  public exportAsExcelFile(
    jsonPrm: any[],
    jsonHeader: any[],
    json: any[],
    excelFileName: string
  ): void {
    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(jsonPrm, {
      skipHeader: true,
    });
    let startCell: string = 'A' + (jsonPrm.length + 2);
    XLSX.utils.sheet_add_json(worksheet, jsonHeader, {
      origin: startCell,
      skipHeader: true,
    });
    startCell = 'A' + (jsonPrm.length + 3);
    XLSX.utils.sheet_add_json(worksheet, json, {
      origin: startCell,
      skipHeader: true,
    });
    const workbook: XLSX.WorkBook = {
      Sheets: { Sheet1: worksheet },
      SheetNames: ['Sheet1'],
    };
    XLSX.writeFile(workbook, this.toExportFileName(excelFileName, 'xlsx'));
  }

  toExportFileName(excelFileName: string, arg1: string): string {
    const d = new Date();
    const year = d.getFullYear();
    let month = '' + (d.getMonth() + 1);
    let day = '' + d.getDate();
    let hour = '' + d.getHours();
    let min = '' + d.getMinutes();
    let sec = '' + d.getSeconds();

    if (month.length < 2) {
      month = '0' + month;
    }
    if (day.length < 2) {
      day = '0' + day;
    }
    if (hour.length < 2) {
      hour = '0' + hour;
    }
    if (min.length < 2) {
      min = '0' + min;
    }
    if (sec.length < 2) {
      sec = '0' + sec;
    }

    const dt = [year, month, day, hour, min, sec].join('');
    return [dt, excelFileName].join('_') + '.' + arg1;
  }
}
