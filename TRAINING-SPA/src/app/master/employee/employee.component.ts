import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { Dropdown } from 'src/app/_model/Dropdown';
import { Employee } from 'src/app/_model/Employee';
import { PaginatedResult, Pagination } from 'src/app/_model/Pagination';
import { EmployeeService } from 'src/app/_service/employee.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {
  param = {};
  department: Dropdown[] = [];
  grade: Dropdown[] = [];
  modalRef: BsModalRef;
  listEmployee: Employee[] = [];
  pagination: Pagination;
  pageSize = 10;
  employeeForm: FormGroup;
  configModal = {
    ignoreBackdropClick: true
  };
  isEdit = false;
  bsConfig: Partial<BsDatepickerConfig>;
  loading = true;

  constructor(
    private employeeService: EmployeeService,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private confirm: ConfirmationService,
    private modalService: BsModalService) { }

  ngOnInit(): void {
    this.bsConfig = {
      containerClass: 'theme-blue',
      dateInputFormat: 'DD-MM-YYYY'
    };
    this.createForm();
    this.loadDropdown();
    this.pagination = {
      currentPage: 1,
      itemPerPage: 10,
      totalItems: 0,
      totalPages: 0
    };
    this.loadData();
  }

  createForm(): void {
    this.employeeForm = this.fb.group({
      nik: ['', Validators.required],
      nama: ['', Validators.required],
      departmentId: ['', Validators.required],
      grade: ['', Validators.required],
      birthDate: ['', Validators.required]
    });
  }

  loadData(): void {
    this.loading = true;
    this.employeeService.getEmployees(this.pagination.currentPage, this.pagination.itemPerPage, this.param)
      .subscribe((res: PaginatedResult<Employee[]>) => {
        this.loading = false;
        this.listEmployee = res.result;
        this.pagination = res.pagination;
      });
  }
  loadDropdown(): void {
    this.employeeService.getDropdown(this.param)
      .subscribe((res) => {
        this.department = res.dept;
        this.grade = res.grade;
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
  itemPerPageChange(event): void {
    this.loadData();
  }
  getEmployee(): void {
    const nik = this.employeeForm.controls.nik.value;
    this.employeeService.getEmployee(nik)
      .subscribe({
        next: (item: Employee) => {
          if (item) {
            this.employeeForm.setValue({
              nik: item.nik,
              nama: item.nama,
              departmentId: item.departmentId,
              grade: item.grade,
              birthDate: new Date(item.birthDate),
            });
            this.employeeForm.controls.nik.disable();
            this.toastr.info('Employee already exists');
          }
        }
      });
  }
  exportExcel(): void {
    import('xlsx').then(xlsx => {
        const worksheet = xlsx.utils.json_to_sheet(this.listEmployee);
        const workbook = { Sheets: { data: worksheet }, SheetNames: ['data'] };
        const excelBuffer: any = xlsx.write(workbook, { bookType: 'xlsx', type: 'array' });
        this.saveAsExcelFile(excelBuffer, 'employee');
    });
  }
  saveAsExcelFile(buffer: any, fileName: string): void {
    import('file-saver').then(FileSaver => {
        const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
        const EXCEL_EXTENSION = '.xlsx';
        const data: Blob = new Blob([buffer], {
            type: EXCEL_TYPE
        });
        FileSaver.saveAs(data, fileName + '_export_' + new Date().getTime() + EXCEL_EXTENSION);
    });
}
  edit(item: Employee, template: TemplateRef<any>): void {
    if (item) {
      this.isEdit = true;
      this.employeeForm.setValue({
        nik: item.nik,
        nama: item.nama,
        departmentId: item.departmentId,
        grade: item.grade,
        birthDate: new Date(item.birthDate),
      });
      this.employeeForm.controls.nik.disable();
    } else {
      this.isEdit = false;
      this.employeeForm.reset();
      this.employeeForm.controls.nik.enable();
    }
    this.modalRef = this.modalService.show(template, this.configModal);
  }
  save(): void {
    if (this.employeeForm.invalid) {
      this.validateFormEntry(this.employeeForm);
      return;
    }
    this.loading = true;
    const data = this.employeeForm.getRawValue();
    if (this.isEdit) {
      this.employeeService.editEmployee(data)
      .subscribe(() => {
        this.loadData();
        this.loading = false;
        this.modalRef.hide();
      }, (error) => {
        this.toastr.error(error.error);
        this.loading = false;
      });
    } else {
      this.employeeService.addEmployee(data)
      .subscribe(() => {
        this.loadData();
      }, (error) => {
        this.toastr.error(error.error);
      });
    }
  }
  delete(data: Employee): void {
    this.confirm.confirm({
      message: 'Delete employee ' + data.nama + ' ?',
      header: 'Confirmation',
      accept: () => {
        this.employeeService.deleteEmployee(data.nik)
          .subscribe(() => {
            this.loadData();
            this.toastr.success('Employee Deleted');
          });
      }
    });
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
  }
}
