import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Dropdown } from 'src/app/_model/Dropdown';
import { Employee } from 'src/app/_model/Employee';
import { Lebaran } from 'src/app/_model/Lebaran';
import { PaginatedResult, Pagination } from 'src/app/_model/Pagination';
import { ChecksheetService } from 'src/app/_service/checksheet.service';
import { UIService } from 'src/app/_service/ui.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {
  param: any;
  detail: Lebaran[] = [];
  pagination: Pagination;
  department: Dropdown[] = [];
  selectedDept = '';
  loading = true;
  constructor(
    private ui: UIService,
    private route: ActivatedRoute,
    private csservice: ChecksheetService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!this.ui.isNullOrEmpty(id)) {
      this.selectedDept = id;
    }
    this.pagination = {
      currentPage: 1,
      itemPerPage: 10,
      totalItems: 0,
      totalPages: 0
    };
    this.dropdownInit();
  }
  dropdownInit(): void {
    this.csservice.getDepartment('CKP')
      .subscribe((res) => {
        this.department = res;
      });
  }
  pageChanged(event): void {
    this.pagination.currentPage = (event.first / event.rows) + 1;
    this.pagination.itemPerPage = event.rows;
    this.param = {
      filter: event.globalFilter
    };
    this.loadData();
  }
  loadData(): void {
    this.loading = true;
    this.param.dept = this.selectedDept;
    this.csservice.getEmployees(this.pagination.currentPage, this.pagination.itemPerPage, this.param)
      .subscribe({
        next: (res: PaginatedResult<Lebaran[]>) => {
          this.loading = false;
          this.detail = res.result;
          this.detail.forEach(x => {
            if (this.ui.isNullOrEmpty(x.fillDate)) {
              x.statusDescription = 'Belum Mengisi';
            } else {
              if (x.status === 1) {
                x.statusDescription = 'Check Kesehatan Sebelum Mulai Bekerja';
              } else {
                x.statusDescription = 'Sehat dan Siap Bekerja';
              }
            }
          });
          this.pagination = res.pagination;
        }
      });
  }
}
