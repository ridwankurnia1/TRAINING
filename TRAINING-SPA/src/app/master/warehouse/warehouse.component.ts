import { Component, OnInit, TemplateRef } from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import moment from 'moment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Dropdown2 } from 'src/app/_model/Dropdown2';
import { Pagination } from 'src/app/_model/Pagination';
import { Warehouse } from 'src/app/_model/Warehouse';
import { WarehouseGroup } from 'src/app/_model/WarehouseGroup';
import { ExcelService } from 'src/app/_service/excel.service';
import { UIService } from 'src/app/_service/ui.service';
import { WarehouseService } from 'src/app/_service/warehouse.service';

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.css'],
  providers: [WarehouseService],
})
export class WarehouseComponent implements OnInit {
  warehouseData: Warehouse[];
  groupData: WarehouseGroup[];
  warehouseType: Dropdown2[];

  formGroup2: UntypedFormGroup;
  formGroup: UntypedFormGroup;
  ipWhGroup: string;
  ipFday: Number = 0;

  pagination: Pagination;
  itemsPerPage: any;

  pageMetadata = {};
  groupSearch: string = '';
  warehouseSearch: String = '';

  // TODO : try a better variable naming
  isLoading = false;
  isLoading2 = false;
  isFormVisible = false;
  isGroupFormVisible = false;
  isUpdating = false;
  isSubmitting = false;
  isUpdating2 = false;
  isSubmitting2 = false;
  fDayCollapsed = true;
  isExporting = false;
  isExporting2 = false;

  // elements
  modalRef?: BsModalRef;

  constructor(
    private warehouse: WarehouseService,
    private ui: UIService,
    private fb: UntypedFormBuilder,
    private message: MessageService,
    private confirm: ConfirmationService,
    private excel: ExcelService,
    private modal: BsModalService
  ) {}

  ngOnInit() {
    this.initGroupTable();
    this.initWarehouseTable();
    this.initForm();
    this.initWhForm();
  }

  initForm() {
    this.formGroup = this.fb.group({
      code: ['', [Validators.required, Validators.maxLength(2)]],
      name: ['', Validators.required],
      remark: [''],
      system: [''],
      status: [''],
      recordStatus: [true],
    });
  }

  initWhForm() {
    this.formGroup2 = this.fb.group({
      code: ['', [Validators.required, Validators.maxLength(10)]],
      name: ['', [Validators.required, Validators.maxLength(100)]],
      nickname: [''],
      group: ['', Validators.required],
      type: ['', Validators.required],
      /* address: [''],
      departmentCode: [''], */
      documentCode: ['', Validators.required],
      /* profitCode: [''],
      costCenter: [''], */
      remark: [''],
      fifoFlag: [false],
      fifoDays: [0, [Validators.min(0), Validators.max(256)]],
      stocktakingFlag: [false],
      carryOutFlag: [false],
      policeNumber: [false],
      transferModelFlag: [false],
      recordStatus: [true],
    });

    this.initDropdown();
  }

  initDropdown() {
    this.warehouse.allType().subscribe((res) => {
      this.warehouseType = res;
    });
  }

  initWarehouseTable(table?: any) {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 1,
    };

    if (table) {
      this.warehouseSearch = '';
      this.pageMetadata = {};
      table.rows = 10;
      table.reset();
      this.getAll();
    }
  }

  initGroupTable() {
    this.groupSearch = '';

    this.getAllGroup();
  }

  warehouseTableChanged(event: any) {
    this.pagination.currentPage = event.first / event.rows + 1;
    this.pagination.itemsPerPage = event.rows;

    this.pageMetadata = {
      searchGlobal: this.warehouseSearch,
    };

    this.getAll();
  }

  getAll() {
    this.isLoading2 = true;
    this.warehouse
      .all(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.pageMetadata
      )
      .subscribe((res) => {
        this.warehouseData = res.result;
        this.pagination = res.pagination;
        this.isLoading2 = false;
      });
  }

  getAllGroup() {
    this.isLoading = true;
    this.warehouse.allGroup(this.groupSearch).subscribe(
      (res) => {
        this.groupData = res;
        this.isLoading = false;
      },
      (e) => {
        this.isLoading = false;
      }
    );
  }

  deleteGroupItem(code: String) {
    this.confirm.confirm({
      message: `Are you sure wants to delete ${code} ? `,
      accept: () => {
        this.warehouse.deleteGroup(code).subscribe(
          () => {
            this.getAllGroup();

            this.message.add({
              severity: 'success',
              summary: 'Item deleted successfuly',
            });
          },
          (e) => {}
        );
      },
    });
  }

  submitWgForm() {
    this.isSubmitting = true;
    if (this.formGroup.invalid) {
      this.ui.validateFormEntry(this.formGroup);
      this.isSubmitting = false;
      return;
    }

    this.confirm.confirm({
      message:
        'Saving data requires confirmation, are you sure the inputs was correct ?',
      accept: () => {
        if (this.isUpdating) {
          this.formGroup.controls['code'].enable();
          this.warehouse.updateGroup(this.formGroup.value).subscribe(
            () => {
              this.isSubmitting = false;

              this.modalRef?.hide();
              this.formGroup.reset();
              this.getAllGroup();

              this.message.add({
                severity: 'success',
                summary: 'Data updated!',
              });
            },
            (e) => {
              this.message.add({
                severity: 'error',
                summary: 'Cannot save data',
              });
              this.isSubmitting = false;
            }
          );
        } else {
          this.warehouse.createGroup(this.formGroup.value).subscribe(
            () => {
              this.isSubmitting = false;

              this.modalRef?.hide();
              this.formGroup.reset();
              this.getAllGroup();

              this.message.add({
                severity: 'success',
                summary: 'Data created!',
              });
            },
            (e) => {
              this.isSubmitting = false;
            }
          );
        }
      },
      reject: () => {
        this.isSubmitting = false;
      },
    });
  }

  openGroupForm(code?: string, element?: TemplateRef<any>) {
    if (code) {
      this.warehouse.singleGroup(code).subscribe((data) => {
        this.isUpdating = true;
        this.formGroup.patchValue(data);
        this.formGroup.controls['recordStatus'].setValue(
          data.recordStatus == 1
        );

        this.formGroup.controls['code'].disable();
      });
    } else {
      this.formGroup.reset();
      this.isUpdating = false;
      this.formGroup.controls['recordStatus'].setValue(true);
      this.formGroup.controls['code'].enable();
    }

    this.modalRef = this.modal.show(element, { ignoreBackdropClick: true });
  }

  submitForm() {
    this.isSubmitting2 = true;
    if (this.formGroup2.invalid) {
      this.ui.validateFormEntry(this.formGroup2);
      this.isSubmitting2 = false;
      return;
    }

    this.confirm.confirm({
      message:
        'Saving data requires confirmation, are you sure the inputs was correct ?',
      accept: () => {
        if (this.isUpdating2) {
          this.formGroup2.controls['code'].enable();
          // update
          this.warehouse.update(this.formGroup2.value).subscribe(
            () => {
              this.isSubmitting2 = false;
              this.toggleForm();
              this.getAll();
              this.message.add({
                severity: 'success',
                summary: 'Data updated!',
              });
            },
            () => {
              this.message.add({
                severity: 'error',
                summary: 'Cannot save data',
              });
              this.isSubmitting2 = false;
            }
          );
        } else {
          // create
          this.warehouse.create(this.formGroup2.value).subscribe(
            (e) => {
              this.isSubmitting2 = false;
              this.toggleForm();
              this.getAll();
              this.fDayCollapsed = true;
              this.message.add({
                severity: 'success',
                summary: 'Data created!',
              });
            },
            (e) => {
              this.message.add({
                severity: 'error',
                summary: 'Cannot save data',
              });
              this.isSubmitting2 = false;
            }
          );
        }
      },
      reject: () => {
        this.isSubmitting2 = false;
      },
    });
  }

  toggleForm(data?: any) {
    this.isFormVisible = !this.isFormVisible;
    this.fDayCollapsed = true;

    if (data) {
      this.formGroup2.reset();
      this.isUpdating2 = true;
      this.formGroup2.patchValue(data);
      this.formGroup2.controls['fifoFlag'].setValue(data.fifoFlag == 1);
      this.formGroup2.controls['carryOutFlag'].setValue(data.carryOutFlag == 1);
      this.formGroup2.controls['policeNumber'].setValue(data.policeNumber == 1);
      this.formGroup2.controls['stocktakingFlag'].setValue(
        data.stocktakingFlag == 1
      );
      this.formGroup2.controls['transferModelFlag'].setValue(
        data.transferModelFlag == 1
      );
      this.formGroup2.controls['recordStatus'].setValue(data.recordStatus == 1);
      this.fDayCollapsed = data.fifoDays == 0 || data.fifoFlag != 1;
      this.formGroup2.controls['code'].disable();
    } else {
      this.formGroup2.reset();
      this.isUpdating2 = false;
      this.formGroup2.controls['code'].enable();
      this.formGroup2.controls['recordStatus'].setValue(true);
    }
  }

  updateItem(code: string) {
    this.warehouse.single(code).subscribe((res) => {
      this.toggleForm(res);
    });
  }

  deleteItem(code: string) {
    this.confirm.confirm({
      message: 'Are you sure wants to delete ' + code + ' ?',
      accept: () => {
        this.warehouse.delete(code).subscribe(
          () => {
            this.getAll();
            this.message.add({
              severity: 'success',
              summary: 'Item deleted successfully!',
            });
          },
          (e) => {
            console.error(e);
            this.message.add({
              severity: 'error',
              summary: 'There was a problem during request, please try again',
            });
          }
        );
      },
      reject: () => {},
      rejectButtonStyleClass: 'btn btn-danger',
    });
  }

  export() {
    this.isExporting2 = true;
    let kopSheet = [
      { A1: 'PT Asahimas Flat Glass Tbk' },
      { A1: 'DATA WAREHOUSE' },
      {
        A1:
          'Export Time : ' +
          moment
            .utc(new Date(), 'MM-DD-YYYY')
            .local()
            .format('DD-MM-YYYY hh:mm:ss'),
      },
      { A1: '' },
    ];

    let headerSheet = [
      {
        A1: 'CODE',
        A2: 'NAME',
        A3: 'NICKNAME',
        A4: 'GROUP',
        A5: 'DOC. BY WAREHOUSE',
        A6: 'FIFO FLAG',
        A7: 'FIFO DAYS',
        A8: 'RECORD STATUS',
      },
    ];

    this.warehouse.export(this.pageMetadata).subscribe((res) => {
      var filtered = res.map((item) => {
        let status = item.recordStatus == 1 ? 'ACTIVE' : 'INACTIVE';
        let fifo = item.fifoFlag == 1 ? 'ACTIVE' : 'INACTIVE';

        return {
          a1: item.code,
          a2: item.name,
          a3: item.nickname,
          a4: item.group,
          a5: item.documentCode,
          a6: fifo,
          a7: item.fifoDays,
          a8: status,
        };
      });

      this.excel.exportAsExcelFile(
        kopSheet,
        headerSheet,
        filtered,
        'DATA WAREHOUSE'
      );

      this.isExporting2 = false;
    });
  }

  exportGroup() {
    this.isExporting = true;
    let kopSheet = [
      { A1: 'PT Asahimas Flat Glass Tbk' },
      { A1: 'DATA WAREHOUSE GROUP' },
      {
        A1:
          'Export Time : ' +
          moment
            .utc(new Date(), 'MM-DD-YYYY')
            .local()
            .format('DD-MM-YYYY hh:mm:ss'),
      },
      { A1: '' },
    ];

    let headerSheet = [
      {
        A1: 'CODE',
        A2: 'NAME',
        A3: 'REMARK',
        A4: 'RECORD STATUS',
      },
    ];

    // TODO : replace pageMetadata with corresponding meta data variable
    this.warehouse.allGroup(this.groupSearch).subscribe((res) => {
      var filtered = res.map((item) => {
        let status = item.recordStatus == 1 ? 'ACTIVE' : 'INACTIVE';

        return {
          a1: item.code,
          a2: item.name,
          a3: item.remark,
          a4: status,
        };
      });

      this.excel.exportAsExcelFile(
        kopSheet,
        headerSheet,
        filtered,
        'DATA WAREHOUSE GROUP'
      );

      this.isExporting = false;
    });
  }
}
