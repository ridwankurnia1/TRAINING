import { Component, OnInit } from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Dropdown2 } from 'src/app/_model/Dropdown2';
import { Pagination } from 'src/app/_model/Pagination';
import { Warehouse } from 'src/app/_model/Warehouse';
import { WarehouseGroup } from 'src/app/_model/WarehouseGroup';
import { UIService } from 'src/app/_service/ui.service';
import { WarehouseService } from 'src/app/_service/warehouse.service';

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.css'],
})
export class WarehouseComponent implements OnInit {
  warehouseData: Warehouse[];
  groupData: WarehouseGroup[];
  warehouseType: Dropdown2[];

  formGroup2: UntypedFormGroup;
  formGroup: UntypedFormGroup;
  ipRecordStatus: Boolean;
  ipWhGroup: string;
  ipFday: Number = 0;

  pagination: Pagination;
  itemsPerPage: any;

  pageMetadata = {};
  globalSearch: String;

  isLoading = false;
  isLoading2 = false;
  isFormVisible = false;
  isGroupFormVisible = false;
  isUpdating = false;
  isSubmitting = false;
  isUpdating2 = false;
  isSubmitting2 = false;
  fDayCollapsed = true;

  constructor(
    private warehouse: WarehouseService,
    private ui: UIService,
    private fb: UntypedFormBuilder,
    private message: MessageService,
    private confirm: ConfirmationService
  ) {}

  ngOnInit() {
    this.getAllGroup();
    this.initForm();
    this.initWhForm();
    this.initWarehouseTable();
  }

  initForm() {
    this.formGroup = this.fb.group({
      code: ['', [Validators.required, Validators.maxLength(2)]],
      name: ['', Validators.required],
      remark: [''],
      system: [''],
      status: [''],
      recordStatus: [false],
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
      recordStatus: [false],
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
      table.reset();
    }

    this.globalSearch = '';

    this.pageMetadata = {};
  }

  warehouseTableChanged(event: any) {
    this.pagination.currentPage = event.first / event.rows + 1;
    this.pagination.itemsPerPage = event.rows;

    this.pageMetadata = {
      searchGlobal: this.globalSearch,
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
    this.warehouse.allGroup().subscribe(
      (res) => {
        this.groupData = res;
        this.isLoading = false;
      },
      (e) => {
        this.isLoading = false;
      }
    );
  }

  updateGroupItem(code: string) {
    this.warehouse.singleGroup(code).subscribe(
      (res) => {
        this.toggleGroupForm(res);
      },
      (e) => {}
    );
  }

  deleteGroupItem(code: String) {
    this.warehouse.deleteGroup(code).subscribe(
      () => {
        this.getAllGroup();
      },
      (e) => {}
    );
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
          this.warehouse.updateGroup(this.formGroup.value).subscribe(
            () => {
              this.isSubmitting = false;

              this.toggleGroupForm();
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

              this.toggleGroupForm();
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

  toggleGroupForm(data?: any) {
    this.isGroupFormVisible = !this.isGroupFormVisible;

    if (data) {
      console.info(data);
      this.isUpdating = true;
      this.formGroup.patchValue(data);
      this.formGroup.controls['recordStatus'].setValue(data.recordStatus == 1);
    } else {
      this.isUpdating = false;
      this.formGroup.reset();
    }
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
      this.isUpdating2 = false;
      this.formGroup2.reset();
      this.formGroup2.controls['code'].enable();
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
}
