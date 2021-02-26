import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
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
  modalRef: BsModalRef;
  listEmployee: Employee[] = [];
  pagination: Pagination;
  pageSize = 10;
  employeeForm: FormGroup;
  configModal = {
    ignoreBackdropClick: true
  };
  constructor(
    private employeeService: EmployeeService,
    private fb: FormBuilder,
    private modalService: BsModalService) { }

  ngOnInit(): void {
    this.createForm();
    this.pagination = {
      currentPage: 1,
      itemPerPage: 10,
      totalItems: 0,
      totalPages: 0
    };
    this.loadData(this.pagination.currentPage, this.pagination.itemPerPage);
  }

  createForm(): void {
    this.employeeForm = this.fb.group({
      nik: ['', Validators.required],
      nama: ['', Validators.required],
      departmentId: ['', Validators.required],
      grade: [''],
      birthDate: ['', Validators.required]
    });
  }

  loadData(page?, itemPerPage?): void {
    this.employeeService.getEmployees(page, itemPerPage, this.param)
      .subscribe((res: PaginatedResult<Employee[]>) => {
        this.listEmployee = res.result;
        this.pagination = res.pagination;
      });
  }
  pageChanged(event): void {
    this.loadData(event.page, this.pageSize);
  }
  itemPerPageChange(event): void {
    this.loadData(1, event);
  }
  edit(item: Employee, template: TemplateRef<any>): void {
    if (item) {
      this.employeeForm.setValue({
        nik: item.nik,
        nama: item.nama,
        departmentId: item.departmentId,
        grade: item.grade,
        birthDate: item.birthDate,
      });
    } else {
      this.employeeForm.reset();
    }
    this.modalRef = this.modalService.show(template, this.configModal);
  }
  save(): void {
    if (this.employeeForm.invalid) {
      this.validateFormEntry(this.employeeForm);
      return;
    }

  }
  validateFormEntry(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateFormEntry(control);
      }
    });
  }x
}
