import { Component, OnInit } from '@angular/core';
import { LebaranSummary } from 'src/app/_model/LebaranSummary';
import { AuthService } from 'src/app/_service/auth.service';
import { ChecksheetService } from 'src/app/_service/checksheet.service';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.css']
})
export class SummaryComponent implements OnInit {
  data: LebaranSummary[] = [];
  constructor(
    private csservice: ChecksheetService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    const prm = {
      brno: this.authService.decodedToken.locality
    };
    this.csservice.getSummaryEmployee(prm).subscribe({
      next: (resp: LebaranSummary[]) => {
        this.data = resp;
        const total: LebaranSummary = {
          departmentId: '',
          department: 'TOTAL',
          filled: this.data.reduce((a, b) => a + b.filled, 0),
          unfilled: this.data.reduce((a, b) => a + b.unfilled, 0),
          mustCheck: this.data.reduce((a, b) => a + b.mustCheck, 0),
          noNeedCheck: this.data.reduce((a, b) => a + b.noNeedCheck, 0),
          alreadyCheck: this.data.reduce((a, b) => a + b.alreadyCheck, 0),
          notYetCheck: this.data.reduce((a, b) => a + b.notYetCheck, 0)
        };
        this.data.unshift(total);
      }
    });
  }

  exportExcel(): void {
    import('xlsx').then(xlsx => {
        const worksheet = xlsx.utils.json_to_sheet(this.data);
        const workbook = { Sheets: { data: worksheet }, SheetNames: ['data'] };
        const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });
        this.saveAsExcelFile(excelBuffer, 'SummaryLebaran2021');
    });
  }
  saveAsExcelFile(buffer: any, fileName: string): void {
    import('file-saver').then(FileSaver => {
      const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
      const EXCEL_EXTENSION = '.xlsx';
      const data: Blob = new Blob([buffer], {
        type: EXCEL_TYPE
      });
      FileSaver.saveAs(data, fileName + '_' + new Date().getTime() + EXCEL_EXTENSION);
    });
  }
}
