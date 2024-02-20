import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { Dropdown } from 'src/app/_model/Dropdown';
import { Employee } from 'src/app/_model/Employee';
import { PaginatedResult, Pagination } from 'src/app/_model/Pagination';
import { EmployeeService } from 'src/app/_service/employee.service';
import {ProductionService} from 'src/app/_service/production.service';
import { UIService } from 'src/app/_service/ui.service';
import { Mdf0 } from 'src/app/_model/Mdf0';
import { DefectGroup } from 'src/app/_model/DefectGroup';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { RadioButtonModule } from 'primeng/radiobutton';
import { EmployeeRoutes } from '../employee/employee.routing';



@Component({
  selector: 'app-defect-group',
  templateUrl: './defect-group.component.html',
  styleUrls: ['./defect-group.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RadioButtonModule,
    EmployeeRoutes,
    TableModule,
    ReactiveFormsModule
  ]
})


export class DefectGroupComponent implements OnInit {
  params: any = {};
  param = {};
  status: Dropdown[] = [];
  grade: Dropdown[] = [];
  modalRef: BsModalRef;
  listEmployee: Employee[] = [];
  listMdf0: Mdf0[] = [];
  listDefectGroup: DefectGroup[] = [];
  pagination: Pagination;
  pageSize = 10;
  employeeForm: UntypedFormGroup;
  Mdf0Form: FormGroup;
  form: FormGroup;
  popTitle = 'Add Group Defect';
  StatusDropdown = '';
  rowsPerPageOptions: any[];

  configModal = {
    ignoreBackdropClick: true
  };
  isEdit = false;
  isAdd = false;
  bsConfig: Partial<BsDatepickerConfig>;
  loading = true;
  popup: TemplateRef<any>;
  // dataNotExist = true;
  ddtrid = 0;
  search: string;

    dep: any;
    ActivateAddEditComp = false;

  constructor(
    private employeeService: EmployeeService,
    private fb: UntypedFormBuilder,
    private toastr: ToastrService,
    private confirm: ConfirmationService,
    private modalService: BsModalService,
    private productionService: ProductionService,
    private ui: UIService) { }

    ModalTitle: string ;
    Status: string;

  ngOnInit(): void {
    this.bsConfig = {
      containerClass: 'theme-blue',
      dateInputFormat: 'DD-MM-YYYY',
    };
    // this.createForm();
    this.createFormMDF0();
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 0,
    };
    this.loadData();
    this.dropdownInit();
  }

  dropdownInit(): void {
    this.status = [
      {
        label: 'Active',
        value: '1'
      },
      {
        label: 'Inactive',
        value: '0'
      }
    ];
  }

  // searchClick(name: string): void{
  //   console.log(name);
  //   this.productionService.getMDF0ByDddfgr('TESTER')
  //   .subscribe((res: any) => {
  //     this.loading = false;
  //     this.listMdf0 = res.result;
  //     console.log(res);
  //   });
  // }

  // loadData(): void {
  //   this.loading = true;
  //   this.productionService.getMDF0()
  //     .subscribe((res: any) => {
  //       this.loading = false;
  //       this.listDefectGroup = res;
  //     });
  // }

  loadData(): void {
    this.loading = true;
    this.productionService.getMDF0(this.pagination.currentPage, this.pagination.itemsPerPage, this.param)
      .subscribe((res: PaginatedResult<DefectGroup[]>) => {
        this.loading = false;
        this.listDefectGroup = res.result;
        this.pagination = res.pagination;
      });
  }

  // convert(data: any): void {
  //   if (data.ddrcst == 1){
  //     data.ddrcst = 'Active';
  //   }else{
  //     data.ddrcst = 'Not Active';
  //   }
  // }

  pageChanged(event: any): void {
    this.pagination.currentPage = (event.first / event.rows) + 1;
    this.pagination.itemsPerPage = event.rows;
    this.params = {
      filter: event.globalFilter,
      orderBy: event.sortField,
      order: event.sortOrder
    };
    if (event.filters) {
      this.params = {};
      if (event.filters.status) {

        this.params.status[event.filters.status.matchMode] = Number(event.filters.status.value);
      }
    }

    this.loadData();
  }

  searchClick(search: string): void{
    const prm = {
      filter: search
    };
    this.productionService.searchMDF0(prm)
      .subscribe((res: any) => {
        this.loading = false;
        this.listDefectGroup = res;
        // console.log(res);
        // console.log(this.listDefectGroup);
      });
    }

  createFormMDF0(): void {
    this.Mdf0Form = this.fb.group({
      transactionId : [''],
      defectGroup: ['', Validators.required],
      remark: ['', Validators.required],
    });
  }

  validateFormEntry(formGroup: UntypedFormGroup): void {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof UntypedFormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof UntypedFormGroup) {
        this.validateFormEntry(control);
      }
    });
  }


  cancelClick(): void{
    this.Mdf0Form.reset();
    // this.dataNotExist = false;
  }


  saveClick(): void{
    if (this.Mdf0Form.invalid) {
      this.ui.validateFormEntry(this.Mdf0Form);
      return;
    }
    if (this.isAdd){
    const date = new Date();
    this.setDate(date);

    const data = this.Mdf0Form.getRawValue();

    data.transactionId = 0;
    if (this.StatusDropdown === 'Active'){
      data.recordStatus = 1;
    }else{
      data.recordStatus = 0;
    }
    data.createTime = date;
    data.createUser = 'Selena';
    data.changeTime = date;
    data.changeUser = 'Selena';

    this.productionService.addMDF0(data)
          .subscribe({
            next: () => {
              this.toastr.success('Save data success');
              this.loadData();
            },
            error: (error) => {
              this.toastr.error('Data sudah ada');
            }
          });
    this.Mdf0Form.reset();
    this.StatusDropdown = 'Active';
    // this.dataNotExist = true;
    }


    if (this.isEdit) {
      const date = new Date();
      this.setDate(date);
      const edit = this.Mdf0Form.getRawValue();
      if (this.StatusDropdown === 'Active'){
        edit.recordStatus = 1;
      }else{
        edit.recordStatus = 0;
      }
      edit.createTime = date;
      edit.createUser = 'Selena';
      edit.changeTime = date;
      edit.changeUser = 'Selena';
      console.log(edit);
      // this.productionService.editMDF0(edit)
      //   .subscribe(() => {
      //     this.loadData();
      //   }, () => {
      //     this.toastr.success('Data Berhasil Diedit');
      //     this.loadData();
      //   });

      this.productionService.editMDF0(edit)
      .subscribe({
        next: () => {
          this.toastr.success('Data Berhasil Diedit');
          this.loadData();
        },
        error: (error) => {
          this.toastr.error('Defect Group Sudah Ada');
        }
      });

      this.Mdf0Form.reset();
      this.StatusDropdown = 'Active';
    }
  }

  edit(data: DefectGroup, popUp = true): void {
    if (data) {
      console.log(data);
      this.popTitle = 'Edit Defect Group';
      this.isEdit = true;
      this.isAdd = false;
      if (data.recordStatus === 1){
        this.StatusDropdown = 'Active';
      }else{
        this.StatusDropdown = 'Inactive';
      }
      this.Mdf0Form.patchValue({
        transactionId: data.transactionId,
        defectGroup: data.defectGroup,
        remark: data.remark
      });
    } else {

      this.isEdit = false;
      this.isAdd = true;
      this.popTitle = 'Add Defect Group ';
      const date = new Date();
      this.setDate(date);

      const dataAd = this.Mdf0Form.getRawValue();

      dataAd.transactionId = 0;
      dataAd.createTime = date;
      dataAd.createUser = 'Selena';
      dataAd.changeTime = date;
      dataAd.changeUser = 'Selena';
    }
    if (popUp) {
      this.modalRef = this.modalService.show(this.popup, this.configModal);
    }
  }

  // untuk menghapus data mdf0
  deleteClick(data: any): void{
    this.productionService.deleteMDF0(data.transactionId)
          .subscribe(() => {
          }, () => {
            this.toastr.info('Data Berhasil Dihapus');
            this.loadData();
          });
    return;
  }
// eof untuk menghapus data


// untuk mengedit data

setDate(date: Date): void{
  const hours = date.getHours();
  const minutes = date.getMinutes();
  const seconds = date.getSeconds();
}

}
