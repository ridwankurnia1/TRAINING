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
    name: 'ridwan'
  };
  listEmployee: Employee[] = [];
  pagination: Pagination;
  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.employeeService.getEmployees(1, 10, this.param)
      .subscribe((res: PaginatedResult<Employee[]>) => {
        this.listEmployee = res.result;
        this.pagination = res.pagination;
        console.log(this.listEmployee);
      });
  }
}
