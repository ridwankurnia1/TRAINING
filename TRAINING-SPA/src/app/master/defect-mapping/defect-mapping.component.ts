import { Component, OnInit, TemplateRef } from '@angular/core';
import { DefectMapping } from 'src/app/_model/DefectMapping';
import { DefectMappingService } from 'src/app/_service/defect-mapping.service';
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
  selector: 'app-defect-mapping',
  templateUrl: './defect-mapping.component.html',
  styleUrls: ['./defect-mapping.component.css']
})
export class DefectMappingComponent implements OnInit {
  defectMappingForm: FormGroup;
  loading = true;
  defectMappingList: DefectMapping[];
  pagination: Pagination;
  params: any = {};
  lineProcess: Dropdown[] = [];
  defectType: Dropdown[] = [];
  isEdit = false;
  modalRef: BsModalRef;
  configModal = {
    ignoreBackdropClick: true,
  };
  bsConfig: Partial<BsDatepickerConfig>;
  selectedDefectType = '';
  selectedLineProcess = '';
  popTittle: any;

  constructor(
    private defectMappingService: DefectMappingService,
    private fb: UntypedFormBuilder,
    private ui: UIService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private confirm: ConfirmationService
  ) { }

  ngOnInit(): void {
    this.bsConfig = {
      containerClass: 'theme-blue',
      dateInputFormat: 'DD-MM-YYYY',
    };
    this.createForm();
    this.loadDropdown();
    // this.selectedDefectType = '';
    // this.selectedLineProcess = '';

    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 0,
    };
    this.loadItems();
  }
  loadItems(): void {
    this.loading = true;
    // this.params.status = this.selectedStatus;
    this.defectMappingService
      .getDefectMapping(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.params
      )
      .subscribe(
        (res: PaginatedResult<DefectMapping[]>) => {
          this.defectMappingList = res.result;
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
    this.defectMappingService.getDropdown(this.params).subscribe((res) => {
      this.lineProcess = res.lineProcess;
    });

    this.defectMappingService.getDropdown(this.params).subscribe((res) => {
      this.defectType = res.zvarDefTy;
    });
  }

  createForm(): void {
    this.defectMappingForm = this.fb.group({
      defectType: ['', Validators.required],
      lineProcessGroup: ['', Validators.required],
      defectCode: ['', Validators.required]
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

      // this.defectDetailForm.controls.defectCode.enable();
      // // this.defectDetailForm.controls.changeUser.disable();
    }

    this.modalRef = this.modalService.show(template, this.configModal);
  }
}
