import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/_model/Employee';
import { PaginatedResult, Pagination } from 'src/app/_model/Pagination';
import { EmployeeService } from 'src/app/_service/employee.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {
  param = {
    name: ''
  };
  listEmployee: Employee[] = [];
  pagination: Pagination;
  pageSize = 10;
  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.pagination = {
      currentPage: 1,
      itemPerPage: 10,
      totalItems: 0,
      totalPages: 0
    };
    this.loadData(this.pagination.currentPage, this.pagination.itemPerPage);
  }

  loadData(page?, itemPerPage?) {
    // console.log(page, itemPerPage);
    this.employeeService.getEmployees(page, itemPerPage, this.param)
      .subscribe((res: PaginatedResult<Employee[]>) => {
        this.listEmployee = res.result;
        this.pagination = res.pagination;
        console.log(this.pagination);
      });
  }
  pageChanged(event): void {
    this.loadData(event.page, this.pageSize);
  }
  itemPerPageChange(event): void {
    this.loadData(1, event);
  }
}
