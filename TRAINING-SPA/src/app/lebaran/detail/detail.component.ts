import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TableModule } from 'primeng/table';
import { Dropdown } from 'src/app/_model/Dropdown';
import { Lebaran } from 'src/app/_model/Lebaran';
import { LebaranXLS } from 'src/app/_model/LebaranXLS';
import { PaginatedResult, Pagination } from 'src/app/_model/Pagination';
import { ChecksheetService } from 'src/app/_service/checksheet.service';
import { UIService } from 'src/app/_service/ui.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule, TableModule, RouterModule],
})
export class DetailComponent implements OnInit {
  param: any;
  detail: Lebaran[] = [];
  pagination: Pagination;
  department: Dropdown[] = [];
  status: Dropdown[] = [];
  selectedDept = '';
  selectedStatus = '';
  loading = true;
  constructor(
    private ui: UIService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private csservice: ChecksheetService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!this.ui.isNullOrEmpty(id)) {
      this.selectedDept = id;
    }
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 0,
    };
    this.dropdownInit();
  }
  dropdownInit(): void {
    this.csservice.getDepartment('CKP').subscribe((res) => {
      this.department = res;
    });
    this.status = [];
    this.status.push({
      value: '0',
      label: 'Belum Mengisi',
    });
    this.status.push({
      value: '1',
      label: 'Sudah Mengisi',
    });
    this.status.push({
      value: '2',
      label: 'Boleh Bekerja',
    });
    this.status.push({
      value: '3',
      label: 'Check Kesehatan',
    });
    this.status.push({
      value: '4',
      label: 'Belum Check Kesehatan',
    });
    this.status.push({
      value: '5',
      label: 'Sudah Check Kesehatan',
    });
  }
  pageChanged(event): void {
    this.pagination.currentPage = event.first / event.rows + 1;
    this.pagination.itemsPerPage = event.rows;
    this.param = {
      filter: event.globalFilter,
    };
    this.loadData();
  }
  loadData(): void {
    this.loading = true;
    this.param.dept = this.selectedDept;
    this.param.status = this.selectedStatus;
    this.param.xls = '';
    this.csservice
      .getEmployees(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.param
      )
      .subscribe({
        next: (res: PaginatedResult<Lebaran[]>) => {
          this.loading = false;
          this.detail = res.result;
          this.detail.forEach((x) => {
            if (this.ui.isNullOrEmpty(x.fillDate)) {
              x.statusDescription = 'Belum Mengisi';
            } else {
              if (x.status === 1) {
                x.statusDescription = 'Check Kesehatan';
              } else {
                x.statusDescription = 'Boleh Bekerja';
              }
            }
          });
          this.pagination = res.pagination;
        },
      });
  }
  download(): void {
    this.loading = true;
    this.param.dept = this.selectedDept;
    this.param.status = this.selectedStatus;
    this.param.xls = '1';
    const itemPerPage = 100;

    const idx1 = this.department.findIndex(
      (x) => x.value === this.selectedDept
    );
    const idx2 = this.status.findIndex((x) => x.value === this.selectedStatus);
    let department = 'All Department';
    let status = 'All Status';
    if (idx1 >= 0) {
      department = this.department[idx1].label;
    }
    if (idx2 >= 0) {
      status = this.status[idx2].label;
    }
    const parameter = [
      {
        l1: 'Department',
        v1: department,
        v2: '',
        v3: '',
      },
      {
        l1: 'Status',
        v1: status,
        v2: '',
        v3: '',
      },
    ];

    const header = [
      {
        h01: 'NIK',
        h02: 'NAMA',
        h03: 'DEPARTMENT',
        h04: 'TANGGAL ISI',
        h05: 'SCORE 1',
        h06: 'SCORE 2',
        h07: 'SCORE 3',
        h08: 'SCORE 4',
        h09: 'SCORE 5',
        h10: 'SCORE 6',
        h11: 'SCORE 7',
        h12: 'SCORE 8',
        h13: 'SCORE 9',
        h14: 'SCORE 10',
        h15: 'SCORE 11',
        h16: 'SCORE 12',
        h17: 'SCORE 13',
        h18: 'SCORE 14',
        h19: 'SCORE 15',
        h20: 'SCORE 16',
        h21: 'SCORE 17',
        h22: 'SCORE 18',
        h23: 'SCORE 19',
        h24: 'SCORE 20',
        h25: 'SCORE 21',
        h26: 'SCORE 22',
        h27: 'SCORE 23',
        h28: 'SCORE 24',
        h29: 'SCORE 25',
        h30: 'STATUS',
        h31: 'CHECK CLOCK',
        h32: 'TGL CHECK KESEHATAN',
        h33: 'PIC CHECK KESEHATAN',
        h34: 'KETERANGAN',
      },
    ];

    let startCell = '';
    let index = 1;
    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(parameter, {
      skipHeader: true,
    });
    index = parameter.length + 2;
    startCell = 'A' + index.toString();
    XLSX.utils.sheet_add_json(worksheet, header, {
      origin: startCell,
      skipHeader: true,
    });

    index++;

    const loop = (page: number) => {
      this.csservice.getEmployees(page, itemPerPage, this.param).subscribe(
        (resp: PaginatedResult<LebaranXLS[]>) => {
          resp.result.forEach((element) => {
            if (element.fillDate) {
              element.fillDate = new Date(element.fillDate);
            }
            if (element.attendDate) {
              element.attendDate = new Date(element.attendDate);
            }
            if (element.healthCheckDate) {
              element.healthCheckDate = new Date(element.healthCheckDate);
            }
          });

          startCell = 'A' + index.toString();
          XLSX.utils.sheet_add_json(worksheet, resp.result, {
            origin: startCell,
            skipHeader: true,
          });
          index += resp.result.length;

          if (resp.pagination.currentPage < resp.pagination.totalPages) {
            loop(resp.pagination.currentPage + 1);
          } else {
            const workbook: XLSX.WorkBook = {
              Sheets: { Sheet1: worksheet },
              SheetNames: ['Sheet1'],
            };
            XLSX.writeFile(workbook, 'Lebaran2021.xlsx');
            this.loading = false;
          }
        },
        (error) => {
          this.toastr.error(error.error);
          this.loading = false;
        }
      );
    };

    loop(1);
  }
}
