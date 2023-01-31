import { Component, OnInit, TemplateRef } from '@angular/core';
import { DefectDetail } from 'src/app/_model/DefectDetail';
import { Production2Service } from 'src/app/_service/production2.service';
import { UIService } from 'src/app/_service/ui.service';
import { PaginatedResult, Pagination } from 'src/app/_model/Pagination';
import {FormControl, FormGroup, UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators} from '@angular/forms';
import { Dropdown } from 'src/app/_model/Dropdown';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { ThisReceiver } from '@angular/compiler';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-defect-detail',
  templateUrl: './defect-detail.component.html',
  styleUrls: ['./defect-detail.component.css'],
})
export class DefectDetailComponent implements OnInit {
  defectDetailForm: FormGroup;
  loading = true;
  defectDetailList: DefectDetail[];
  pagination: Pagination;
  params: any = {};
  param: any;
  popTittle: any;
  defectName: Dropdown[] = [];
  isEdit = false;
  modalRef: BsModalRef;
  configModal = {
    ignoreBackdropClick: true,
  };
  bsConfig: Partial<BsDatepickerConfig>;
  statusDropdown = 'Inactive';
  defectCode = '';
  selectedDefect = '';
  selectedStatus = '';
  status: Dropdown[] = [];

  constructor(
    private production2Service: Production2Service,
    private fb: UntypedFormBuilder,
    private ui: UIService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private confirm: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.bsConfig = {
      containerClass: 'theme-blue',
      dateInputFormat: 'DD-MM-YYYY',
    };
    this.createForm();
    this.loadDropdown();
    this.selectedStatus = '';
    this.selectedDefect = '';

    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 0,
    };
    this.loadItems();
  }

  createForm(): void {
    this.defectDetailForm = this.fb.group({
      defectCode: ['', Validators.required],
      defectName: ['', Validators.required],
      defectType: ['', Validators.required],
      defectGroup1: ['', Validators.required],
      defectGroup2: [''],
      remark: ['', Validators.required],
      recordStatus: [0],
    });
  }

  loadItems(): void {
    this.loading = true;
    this.params.status = this.selectedStatus;
    this.production2Service
      .getMDF1(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.params
      )
      .subscribe(
        (res: PaginatedResult<DefectDetail[]>) => {
          this.defectDetailList = res.result;
          this.pagination = res.pagination;
          // console.log(this.itemList);
        },
        (error) => {
          this.loading = false;
        },
        () => {
          this.loading = false;
        }
      );
  }
  pageChanged(event): void {
    this.pagination.currentPage = event.first / event.rows + 1;
    this.pagination.itemsPerPage = event.rows;
    this.params = {
      filter: event.globalFilter,
    };
    this.loadItems();
  }
  loadDropdown(): void {
    this.production2Service.getDropdown(this.param).subscribe((res) => {
      this.defectName = res.defg;
    });
    this.status = [];
    this.status.push({
      value: '1',
      label: 'Active'
    });
    this.status.push({
      value: '0',
      label: 'Inactive'
    });
    // console.log(this.defectName);
  }
  edit(template: TemplateRef<any>, data: DefectDetail): void {
    if (data) {
      this.popTittle = 'Edit ';
      const date = new Date();
      this.isEdit = true;
      // console.log(data);
      if (data.recordStatus === 1){
        this.statusDropdown = 'Active';
      }else{
        this.statusDropdown = 'Inactive';
      }
      this.defectCode = data.defectCode;
      this.defectDetailForm.setValue({
        defectCode: data.defectCode,
        defectName: data.defectName,
        defectType: data.defectType,
        defectGroup1: data.defectGroup1,
        defectGroup2: data.defectGroup2,
        remark: data.remark,
        recordStatus: data.recordStatus,
        // changeUser: data.changeUser
      });

      // this.defectDetailForm.controls.changeUser.enable();
      this.defectDetailForm.controls.defectCode.disable();
    } else {
      this.popTittle = 'Add ';
      this.isEdit = false;
      const dataAd = this.defectDetailForm.getRawValue();
      console.log(this.defectName);
      this.defectDetailForm.controls.defectCode.enable();
      // this.defectDetailForm.controls.changeUser.disable();
    }

    this.modalRef = this.modalService.show(template, this.configModal);
  }

  save(): void {
    if (this.defectDetailForm.invalid) {
      this.ui.validateFormEntry(this.defectDetailForm);
      return;
    }
    const data = this.defectDetailForm.getRawValue();
    // console.log(data);
    if (this.isEdit) {
      console.log(data);
      if (this.statusDropdown === 'Active') {
        data.recordStatus = 1;
      }
      if (this.statusDropdown === 'Inactive') {
        data.recordStatus = 0;
      }
      this.production2Service.editDefectDetail(data).subscribe({
        next: () => {
          this.toastr.success('Data Berhasil Diedit');
          this.loadItems();
        },
        error: (error) => {
          this.toastr.error();
        }
      });
    } else {
      const date = new Date();
      data.company = 'AMG';
      data.branch = 'CKP';
      data.idGroup = 0;
      data.createUser = 'PRIGUSTI A';
      if (this.statusDropdown === 'Active') {
        data.recordStatus = 1;
      }
      if (this.statusDropdown === 'Inactive') {
        data.recordStatus = 0;
      }
      this.production2Service.addDefectDetail(data).subscribe({
        next: () => {
          this.toastr.success('Save data success');
          this.loadItems();
        },
        error: (error) => {
          this.toastr.error('Defect Code Sudah Ada');
        }
      });
    }
  }

  delete(data: DefectDetail): void {
    console.log(data);
    this.confirm.confirm({
      header: 'Confirmation',
      message: 'Delete Defect Detail ' + data.defectName + ' ?',
      accept: () => {
        this.production2Service.deleteDefectDetail(data.defectCode).subscribe(
          () => {
            this.toastr.success('Defect Detail Deleted');
            this.loadItems();
          },
          (e) => {}
        );
      },
    });
    return;
  }

  download(): void {
    this.loading = true;
    // this.param.dept = this.selectedDefect;
    // this.param.status = this.selectedStatus;
    // this.param.xls = '1';
    const itemPerPage = 100;

    const idx1 = this.defectName.findIndex(x => x.value === this.selectedDefect);
    const idx2 = this.status.findIndex(x => x.value === this.selectedStatus);
    let defectName = 'All Defect';
    let status = 'All Status';
    if (idx1 >= 0) {
      defectName = this.defectName[idx1].label;
    }
    if (idx2 >= 0) {
      status = this.status[idx2].label;
    }
    const parameter = [
      {
        l1: 'Defect Name',
        v1: defectName,
        v2: '',
        v3: ''
      },
      {
        l1: 'Status',
        v1: status,
        v2: '',
        v3: ''
      },
    ];

    const header = [{
      h01: 'Company',
      h02: 'Branch',
      h03: 'Defect Code',
      h04: 'Defect Name',
      h05: 'Id Group',
      h06: 'Defect Type',
      h07: 'Defect Group 1',
      h08: 'Defect Group 2',
      h09: 'Remark',
      h10: 'Record Status',
      h11: 'Create Date',
      h12: 'Create Time',
      h13: 'Create User',
      h14: 'Change Date',
      h15: 'Change Time',
      h16: 'Change User',
      h17: 'Record Status Text',
    }];

    let startCell = '';
    let index = 1;
    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(parameter, { skipHeader: true });
    index = parameter.length + 2;
    startCell = 'A' + index.toString();
    XLSX.utils.sheet_add_json(worksheet, header, { origin: startCell, skipHeader: true });

    index++;

    const loop = (page: number) => {
      this.production2Service.getMDF1(
        page,
        itemPerPage,
        this.param
      ).subscribe((resp: PaginatedResult<DefectDetail[]>) => {
        resp.result.forEach(element => {});

        startCell = 'A' + index.toString();
        XLSX.utils.sheet_add_json(worksheet, resp.result, { origin: startCell, skipHeader: true });
        index += resp.result.length;

        if (resp.pagination.currentPage < resp.pagination.totalPages) {
          loop(resp.pagination.currentPage + 1);
        } else {
          const workbook: XLSX.WorkBook = {Sheets: {Sheet1: worksheet}, SheetNames: ['Sheet1'] };
          XLSX.writeFile(workbook, 'Defect detail.xlsx');
          this.loading = false;
        }
      },
      error => {
        this.toastr.error(error.error);
        this.loading = false;
      });
    };

    loop(1);
  }

  closeClick(): void {
    this.defectDetailForm.reset();
  }
}
