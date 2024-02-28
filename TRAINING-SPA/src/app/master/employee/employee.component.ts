import { CommonModule } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import {
  FormsModule,
  ReactiveFormsModule,
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import {
  BsDatepickerConfig,
  BsDatepickerModule,
} from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { RadioButtonModule } from 'primeng/radiobutton';
import { Dropdown } from 'src/app/_model/Dropdown';
import { Employee } from 'src/app/_model/Employee';
import { PaginatedResult, Pagination } from 'src/app/_model/Pagination';
import { EmployeeService } from 'src/app/_service/employee.service';
import * as XLSX from 'xlsx';
import { TableModule } from 'primeng/table';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DropdownModule } from 'primeng/dropdown';
import { saveAs } from 'file-saver';
import * as moment from 'moment';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css'],
  standalone: true,
  providers: [EmployeeService, ConfirmationService, BsModalService],
  imports: [
    CommonModule,
    FormsModule,
    RadioButtonModule,
    TableModule,
    ReactiveFormsModule,
    ConfirmDialogModule,
    ModalModule,
    DropdownModule,
    BsDatepickerModule,
  ],
})
export class EmployeeComponent implements OnInit {
  param = {};
  department: Dropdown[] = [];
  grade: Dropdown[] = [];
  branch: Dropdown[] = [];
  modalRef: BsModalRef;
  listEmployee: Employee[] = [];
  pagination: Pagination;
  pageSize = 10;
  employeeForm: UntypedFormGroup;
  configModal = {
    ignoreBackdropClick: true,
  };
  isEdit = false;
  bsConfig: Partial<BsDatepickerConfig>;
  loading = true;
  constructor(
    private employeeService: EmployeeService,
    private fb: UntypedFormBuilder,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private confirm: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.bsConfig = {
      containerClass: 'theme-blue',
      dateInputFormat: 'DD-MM-YYYY',
    };
    this.createForm();
    this.loadDropdown();
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 0,
    };
    this.loadData();
  }

  createForm(): void {
    this.employeeForm = this.fb.group({
      nik: ['', Validators.required],
      nama: ['', Validators.required],
      branch: ['', Validators.required],
      departmentId: ['', Validators.required],
      grade: ['', Validators.required],
      birthDate: ['', Validators.required],
      address: ['', Validators.required],
    });
  }

  loadData(): void {
    this.loading = true;
    this.employeeService
      .getEmployees(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.param
      )
      .subscribe((res: PaginatedResult<Employee[]>) => {
        this.loading = false;
        this.listEmployee = res.result;
        this.pagination = res.pagination;
      });
  }
  loadDropdown(): void {
    this.employeeService.getDropdown(this.param).subscribe((res) => {
      this.department = res.dept;
      this.grade = res.grade;
      this.branch = res.branch;
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
  itemPerPageChange(event): void {
    this.loadData();
  }
  getEmployee(): void {
    const nik = this.employeeForm.controls.nik.value;
    this.employeeService.getEmployee(nik).subscribe({
      next: (item: Employee) => {
        if (item) {
          this.employeeForm.setValue({
            nik: item.nik,
            nama: item.nama,
            branch: item.branch,
            departmentId: item.departmentId,
            grade: item.grade,
            birthDate: new Date(item.birthDate),
            // birthDate: item.birthDate,
            address: item.address,
          });
          (this.isEdit = true), this.employeeForm.controls.nik.disable();
          this.employeeForm.controls.branch.disable();
          this.toastr.info('Employee already exists');
        }
      },
    });
  }
  exportExcel(): void {
    console.log('button ok');
    import('xlsx').then((xlsx) => {
      var Heading = [['PT. Asahimas Flat Glass Tbk']];
      var Address = [
        [
          'Cikampek, Bukit Indah Industrial Park Blok. J-L Sector 1-A Karangjaya Tirtamulya 41373 Kalihurip Jawa Barat · ~73,1 km',
        ],
      ];
      var listExport: any[] = JSON.parse(JSON.stringify(this.listEmployee));
      listExport = listExport.map((data: Employee) => {
        var emp: Employee = {
          nama: data.nama,
          branch: data.branch,
          department: data.department,
          birthDate: data.birthDate,
          address: data.address,
        };
        console.log(this.ucwords(emp.nama));
        console.log(emp.address);
        return emp;
      });

      const worksheet = xlsx.utils.aoa_to_sheet(Heading);
      xlsx.utils.sheet_add_aoa(worksheet, Address, { origin: 'A2' });
      xlsx.utils.sheet_add_json(worksheet, listExport, {
        origin: 'A3',
      });

      const workbook = { Sheets: { data: worksheet }, SheetNames: ['data'] };
      const excelBuffer: any = xlsx.write(workbook, {
        bookType: 'xlsx',
        type: 'array',
      });
      this.saveAsExcelFile(excelBuffer, 'employee');
    });
  }
  saveAsExcelFile(buffer: any, fileName: string): void {
    console.log('Fungsi saveAsExcelFile');
    import('file-saver').then((FileSaver) => {
      const EXCEL_TYPE =
        'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
      const EXCEL_EXTENSION = '.xlsx';
      const data: Blob = new Blob([buffer], {
        type: EXCEL_TYPE,
      });
      FileSaver.saveAs(
        data,
        fileName + '_export_' + new Date().getTime() + EXCEL_EXTENSION
      );
      console.log('filesaver save as aman');
    });
  }

  // exportExcel(): void {
  //   let kopSheet = [
  //     { A1: 'PT Asahimas Flat Glass Tbk' },
  //     { A1: 'Data Employee' },
  //     {
  //       A1:
  //         'Export Time : ' +
  //         moment
  //           .utc(new Date(), 'MM-DD-YYYY')
  //           .local()
  //           .format('DD-MM-YYYY hh:mm:ss'),
  //     },
  //     { A1: '' },
  //   ];

  //   let headerSheet = [
  //     {
  //       A1: 'Name',
  //       A2: 'Branch',
  //       A3: 'Department',
  //       A4: 'Birth Date',
  //     },
  //   ];

  //   // TODO : replace pageMetadata with corresponding meta data variable
  //   var listExport: any[] = JSON.parse(JSON.stringify(this.listEmployee));
  //     listExport = listExport.map((data: Employee) => {
  //       var emp: Employee = {
  //         nama: data.nama,
  //         branch: data.branch,
  //         department: data.department,
  //         birthDate: data.birthDate,
  //         address: data.address,

  //       };
  //       return emp;
  //     });

  //     this.excel.exportAsExcelFile(
  //       kopSheet,
  //       headerSheet,
  //       filtered,
  //       'DATA WAREHOUSE GROUP'
  //     );

  //     this.isExporting = false;
  //   });
  // }

  edit(item: Employee, template: TemplateRef<any>): void {
    if (item) {
      this.isEdit = true;

      this.employeeForm.setValue({
        nik: item.nik,
        nama: item.nama,
        branch: item.branch,
        departmentId: item.departmentId,
        grade: item.grade,

        birthDate: new Date(item.birthDate),
        // birthDate: item.birthDate,
        address: item.address,
      });

      this.employeeForm.controls.nik.disable();
      this.employeeForm.controls.branch.disable();
    } else {
      this.isEdit = false;
      this.employeeForm.reset();
      // this.employeeForm.controls.nik.enable();
      this.employeeForm.controls['nik'].enable();
      this.employeeForm.controls.branch.enable();
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
      this.employeeService.editEmployee(data).subscribe(
        () => {
          this.loadData();
          this.loading = false;
          this.modalRef.hide();
        },
        (error) => {
          this.toastr.error(error.error);
          this.loading = false;
        }
      );
    } else {
      console.log('data kosong, lanjut add');
      this.employeeService.addEmployee(data).subscribe(
        () => {
          console.log('sukses tambah data');
          this.loadData();
          this.loading = false;
          this.modalRef.hide();
        },
        (error) => {
          console.log('add data error');
          this.toastr.error(error.error);
        }
      );
    }
  }
  delete(data: Employee): void {
    this.confirm.confirm({
      message: 'Delete employee ' + data.nama + ' ?',
      header: 'Confirmation',
      accept: () => {
        this.employeeService.deleteEmployee(data.nik).subscribe(() => {
          this.loadData();
          this.toastr.success('Employee Deleted');
        });
      },
    });
  }


  // Generate kata jadi PascalCase
  ucwords(str) {
    var splitStr = str.toLowerCase().split(' ');
    for (var i = 0; i < splitStr.length; i++) {
      splitStr[i] =
        splitStr[i].charAt(0).toUpperCase() + splitStr[i].substring(1);
    }

    return splitStr.join(' ');
  }

  validateFormEntry(formGroup: UntypedFormGroup): void {
    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      if (control instanceof UntypedFormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof UntypedFormGroup) {
        this.validateFormEntry(control);
      }
    });
  }
}
