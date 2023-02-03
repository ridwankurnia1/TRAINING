import { Component, OnInit, TemplateRef } from '@angular/core';
import { DefectMapping } from 'src/app/_model/DefectMapping';
import { DefectMappingService } from 'src/app/_service/defect-mapping.service';
import { UIService } from 'src/app/_service/ui.service';
import { PaginatedResult, Pagination } from 'src/app/_model/Pagination';
import {
  FormControl,
  FormGroup,
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { Dropdown } from 'src/app/_model/Dropdown';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { ThisReceiver } from '@angular/compiler';
import * as XLSX from 'xlsx';
import { DefectDetail } from 'src/app/_model/DefectDetail';

@Component({
  selector: 'app-defect-mapping',
  templateUrl: './defect-mapping.component.html',
  styleUrls: ['./defect-mapping.component.css'],
})
export class DefectMappingComponent implements OnInit {
  defectMappingForm: FormGroup;
  loading = true;
  defectMappingList: DefectMapping[];
  defectDetailList: DefectDetail[];
  pagination: Pagination;
  params: any = {};
  lineProcess: Dropdown[] = [];
  defectType: Dropdown[] = [];
  defectCode: Dropdown[] = [];
  isEdit = false;
  modalRef: BsModalRef;
  configModal = {
    ignoreBackdropClick: true,
  };
  bsConfig: Partial<BsDatepickerConfig>;
  selectedDefectType = '';
  selectedLineProcess = '';
  selectedDefectCode = '';
  popTittle: any;
  defT: string;
  lineP: string;

  constructor(
    private defectMappingService: DefectMappingService,
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
    if (this.selectedDefectType !== '' || this.selectedLineProcess !== '') {
      this.loadItems();
    }
    this.selectedDefectType = '';
    this.selectedLineProcess = '';

    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 0,
    };
    this.loadDropdown();
  }
  loadItems(): void {
    this.loading = true;
    this.defectMappingService
      .getDefectMapping(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.params
      )
      .subscribe(
        (res: PaginatedResult<DefectMapping[]>) => {
          this.defectMappingList = res.result;
          this.defectDetailList = res.result;
          this.pagination = res.pagination;
          console.log(this.defectDetailList);
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
      DefectTypeFilter: this.selectedDefectType,
      LineProcessFilter: this.selectedLineProcess,
    };
    this.loadItems();

    console.log(this.selectedDefectType);
    console.log(this.selectedLineProcess);
  }

  loadDropdown(): void {
    this.defectMappingService.getDropdown(this.params).subscribe((res) => {
      this.lineProcess = res.lineProcess;
    });

    this.defectMappingService.getDropdown(this.params).subscribe((res) => {
      this.defectType = res.zvarDefTy;
    });

    this.defectMappingService.getDropdown(this.params).subscribe((res) => {
      this.defectCode = res.defectCode;
    });
  }

  createForm(): void {
    this.defectMappingForm = this.fb.group({
      defectType: ['', Validators.required],
      lineProcessGroup: ['', Validators.required],
      defectCode: ['', Validators.required],
    });
  }

  edit(template: TemplateRef<any>, data: DefectMapping): void {
    if (data) {
      this.popTittle = 'Edit Defect Mapping ';
      const date = new Date();
      this.isEdit = true;

      this.defectMappingForm.setValue({
        defectType: data.defectType,
        lineProcessGroup: data.lineProcessGroup,
        defectCode: data.defectCode,
      });

      // this.defectDetailForm.controls.changeUser.enable();
    } else {
      this.popTittle = 'Add Defect Mapping ';
      this.isEdit = false;
      const dataAd = this.defectMappingForm.getRawValue();
      console.log(this.defectType);

      // this.defectDetailForm.controls.defectCode.enable();
      // // this.defectDetailForm.controls.changeUser.disable();
    }

    this.modalRef = this.modalService.show(template, this.configModal);
  }

  save(): void {
    if (this.defectMappingForm.invalid) {
      this.ui.validateFormEntry(this.defectMappingForm);
      return;
    }
    const data = this.defectMappingForm.getRawValue();
    console.log(data);
    if (this.isEdit) {
      console.log(data);
      this.defectMappingService.editDefectMapping(data).subscribe({
        next: () => {
          this.toastr.success('Data Berhasil Diedit');
          this.loadItems();
        },
        error: (error) => {
          this.toastr.error();
        },
      });
    } else {
      const date = new Date();
      data.company = 'AMG';
      data.branch = 'CKP';
      data.createUser = 'PRIGUSTI A';

      this.defectMappingService.addDefectMapping(data).subscribe({
        next: () => {
          this.toastr.success('Save data success');
          this.loadItems();
        },
        error: (error) => {
          this.toastr.error('Defect Code Sudah Ada');
        },
      });
    }
  }
}
