import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { PalletType } from 'src/app/_model/PalletType';
import { PalletTypeService } from 'src/app/_service/pallet-type.service';
import { UIService } from 'src/app/_service/ui.service';
import {
  UntypedFormGroup,
  UntypedFormBuilder,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { PaginatedResult, Pagination } from 'src/app/_model/Pagination';
import { Dropdown2 } from 'src/app/_model/Dropdown2';
import { ExcelService } from 'src/app/_service/excel.service';
import { DropdownFilterOptions, DropdownModule } from 'primeng/dropdown';
import { ConfirmationService, Message, MessageService } from 'primeng/api';
import moment from 'moment';
import { CommonModule } from '@angular/common';
import { RadioButtonModule } from 'primeng/radiobutton';
import { TableModule } from 'primeng/table';
import { EmployeeRoutes } from '../employee/employee.routing';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { CheckboxModule } from 'primeng/checkbox';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-pallet-type',
  templateUrl: './pallet-type.component.html',
  styleUrls: ['./pallet-type.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    CollapseModule,
    TableModule,
    BsDatepickerModule,
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    DropdownModule,
    RadioButtonModule,
    CheckboxModule,
    ButtonModule,
    CalendarModule,
    InputNumberModule,
    ConfirmDialogModule,
    ToastModule,
  ],
})
export class PalletTypeComponent implements OnInit {
  // common
  pallets: PalletType[];
  param = {};

  // service
  modalRef: BsModalRef;

  // state variables
  isSubmitting: Boolean = false;
  isFormValid: Boolean = false;
  isCollapsed = true;
  isLoading: Boolean;
  isUpdating: Boolean = false;
  isExporting: Boolean = false;

  // table variables
  // filters
  globalSearch: String;
  typeFilter: String;
  appFilter: String;
  materialFilter: String;
  nameFilter: String;
  statusFilter: Boolean;
  // dropdown type filter
  dAppFilterable = [];
  dMaterialFilterable = [];
  dStatusFilterable = [];
  dCurrencyFilter = '';
  dColorFilter = '';

  // form variables
  formGroup: UntypedFormGroup;
  ipType: String;
  ipName: String;
  ipCodification: Number;
  ipApp: String = '';
  ipMaterial: String = '';
  ipColor: String;
  ipCurrency: String = '';
  ipLength: Number;
  ipLengthType: String;
  ipWidth: Number;
  ipWidthType: String;
  ipWeight: Number;
  ipWeightType: String = '';
  ipHeight: number;
  ipHeightType: String = '';
  ipPrice: Number;
  ipStatus: Number;
  ipFl1: Number;
  ipCarryIn: Number;
  ipCarryOut: Number;
  ipRemark: String;

  // dropdown variables
  dApp = [];
  dMaterial = [];
  dColor = [];
  dCurrency: Dropdown2[] = [];
  dMeasure = [];

  // paging
  itemsPerPage: any;
  pagination: Pagination;

  // element ref
  // @ViewChild('matySorter') materialSorter: ElementRef;

  // export vars
  kopSheet: any[];
  headerSheet: any[];
  exportables: PalletType[];

  constructor(
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private palletService: PalletTypeService,
    private fb: UntypedFormBuilder,
    private ui: UIService,
    private excel: ExcelService
  ) {}

  ngOnInit() {
    this.resetTable();
    this.initForm();
  }

  loadData(event: any) {
    this.pagination.currentPage = event.first / event.rows + 1;
    this.pagination.itemsPerPage = event.rows;

    this.param = {
      searchString: this.globalSearch,
      searchType: this.typeFilter,
      searchName: this.nameFilter,
      searchApp: this.appFilter,
      searchMaterial: this.materialFilter,
      searchStatus: this.statusFilter,
      // searchColor: this.colorFilter,
      // searchLength: this.lengthFilter,
      // searchWidth: this.widthFilter,
      // searchRemark: this.remarkFilter,
    };

    if (event.sortField) {
      let sortString = event.sortField;

      if (event.sortOrder < 1) {
        sortString = '-' + sortString;
      }

      this.param['sortString'] = sortString;
    }

    this.getData();
  }

  getData() {
    this.isLoading = true;
    this.palletService
      .all(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.param
      )
      .subscribe(
        (res: PaginatedResult<PalletType[]>) => {
          this.pallets = res.result;
          this.pagination = res.pagination;
          this.isLoading = false;
        },
        (e) => {
          this.isLoading = false;
          console.error(e);
          this.messageService.add({
            severity: 'error',
            summary: 'Cannot find any data with current filter(s)',
          });
        }
      );
  }

  resetTable(table?: any) {
    this.param = {};
    this.typeFilter = '';
    this.nameFilter = '';
    this.materialFilter = '';
    this.appFilter = '';
    this.statusFilter = null;

    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 1,
    };

    if (table) {
      /* table.first = 0;
      table.sortField = undefined;
      table.sortOrder = 1; */
      table.rows = 10;
      table.reset();
    }

    this.getData();
  }

  resetForm() {
    this.formGroup.reset();

    this.ipType = '';
    this.ipApp = '';
    this.ipColor = '';
    this.ipCodification = null;
    this.ipCurrency = '';
    this.ipHeight = null;
    this.ipWeight = null;
    this.ipLength = null;
    this.ipPrice = null;
    this.ipRemark = null;

    this.initForm();
  }

  initForm() {
    this.formGroup = this.fb.group({
      palletType: ['', Validators.required],
      palletName: ['', Validators.required],
      palletCodification: [1, Validators.required],
      palletApp: ['', Validators.required],
      materialType: ['', Validators.required],
      palletColor: ['', Validators.required],
      palletCurrency: ['', Validators.required],
      palletLength: [0],
      lengthUm: ['MM', Validators.required],
      palletWidth: [0],
      widthUm: ['MM', Validators.required],
      palletHeight: [0],
      heightUm: ['MM', Validators.required],
      palletWeight: [0],
      weightUm: ['KG', Validators.required],
      palletPrice: [0],
      recordStatus: [false],
      flag1: [false],
      carryInFlag: [false],
      carryOutFlag: [false],
      remark: [''],
    });

    this.initDropdowns();
  }

  initDropdowns() {
    this.palletService.getMaterialType().subscribe((res) => {
      this.dMaterial = res;
      this.dMaterialFilterable = res;
    });

    this.palletService.getPalletApp().subscribe((res) => {
      this.dApp = res;
      this.dAppFilterable = res;
    });

    this.palletService.getColorType().subscribe((res) => {
      this.dColor = res;
    });

    this.palletService.getCurrencies().subscribe((res) => {
      this.dCurrency = res;
    });

    this.palletService.getMeasurements().subscribe((res) => {
      this.dMeasure = res;
    });

    this.dStatusFilterable = [
      { name: 'ACTIVE', value: 1 },
      { name: 'INACTIVE', value: 2 },
    ];

    this.ipHeightType = 'MM';
    this.ipWidthType = 'MM';
    this.ipLengthType = 'MM';
    this.ipWeightType = 'KG';
  }

  resetDropdownFilter(options: DropdownFilterOptions) {
    options.reset();
  }

  submitForm() {
    this.isSubmitting = true;
    if (this.formGroup.invalid) {
      this.ui.validateFormEntry(this.formGroup);
      this.isSubmitting = false;
      return;
    }

    this.confirmationService.confirm({
      message: 'Submitting data requires confirmation, confirm saving data ?',
      accept: () => {
        this.formGroup.get('palletType').enable();

        if (this.isUpdating) {
          this.palletService.update(this.formGroup.value).subscribe(
            () => {
              this.isSubmitting = false;
              this.afterSubmit();

              this.showMessage({
                severity: 'success',
                summary: 'Data updated successfully!',
              });
            },
            (e) => {
              console.error(e);
              this.isSubmitting = false;
              this.showMessage({
                severity: 'error',
                summary:
                  'There was a problem with your request, please try again',
              });
            }
          );
        } else {
          this.palletService.create(this.formGroup.value).subscribe(
            () => {
              this.afterSubmit();
              this.isSubmitting = false;

              this.showMessage({
                severity: 'success',
                summary: 'Data created successfully!',
              });
            },
            (e) => {
              this.isSubmitting = false;
              console.error(e);
              this.showMessage({
                severity: 'error',
                summary:
                  'There was a problem with your request, please try again',
              });
            }
          );
        }
      },
      reject: () => {
        this.isSubmitting = false;
      },
    });
  }

  afterSubmit() {
    this.toggleForm();
    this.resetForm();
    this.resetTable();
  }

  toggleForm(data?: PalletType) {
    this.isCollapsed = !this.isCollapsed;

    if (data) {
      // set values
      this.isUpdating = true;
      this.formGroup.patchValue(data);

      if (data.recordStatus == 1) {
        this.formGroup.get('recordStatus').setValue(true);
      }

      if (data.flag1 == 1) {
        this.formGroup.get('flag1').setValue(true);
      }

      if (data.carryInFlag == 1) {
        this.formGroup.get('carryInFlag').setValue(true);
      }

      if (data.carryOutFlag == 1) {
        this.formGroup.get('carryOutFlag').setValue(true);
      }

      this.formGroup.get('palletType').disable();
    } else {
      this.formGroup.get('palletType').enable();
      this.resetForm();
      this.isUpdating = false;
    }
  }

  getItem(data?: any) {
    this.palletService.single(data.palletType).subscribe((result) => {
      this.toggleForm(result);
    });
  }

  onItemSelected(event) {
    /* this.palletService.single(event.data.palletType).subscribe((data) => {
      this.ipType = data.palletType
    }) */
  }

  deleteItem(data) {
    this.confirmationService.confirm({
      message: 'Are you sure wants to delete ' + data.palletType + ' ?',
      accept: () => {
        this.isLoading = true;
        this.palletService.delete(data.palletType).subscribe(
          () => {
            this.isLoading = false;
            this.resetTable();
            this.getData();
            this.messageService.add({
              severity: 'info',
              summary: 'Data deleted',
              detail: data.palletType + ' deleted successfully',
            });
          },
          (e) => {
            this.messageService.add({
              severity: 'error',
              summary: 'Cannot delete data',
              detail: e.toString(),
            });
          }
        );
      },
    });
  }

  showMessage(message: Message) {
    message.life = 5000;
    this.messageService.add(message);
  }

  export() {
    this.isExporting = true;
    // init export variables
    this.kopSheet = [
      { A1: 'PT Asahimas Flat Glass Tbk' },
      { A1: 'DATA PALLET TYPE' },
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

    this.headerSheet = [
      {
        A1: 'PALLET TYPE',
        A2: 'PALLET DESCRIPTION',
        A3: 'PALLET APP',
        A9: 'MATERIAL TYPE',
        A8: 'PALLET COLOR',
        A4: 'LENGTH',
        A5: 'WIDTH',
        A6: 'HEIGHT',
        A7: 'WEIGHT',
        A10: 'RECORD STATUS',
      },
    ];

    this.palletService.export(this.param).subscribe(
      (res) => {
        const v1 = res.map((item) => {
          let status = item.recordStatus == 1 ? 'ACTIVE' : 'INACTIVE';

          return {
            h1: item.palletType,
            h2: item.palletName,
            h3: item.palletApp ?? '-',
            h4: item.materialType ?? '-',
            h5: item.palletColor ?? '-',
            h6: item.palletLength ?? '-',
            h7: item.palletWidth ?? '-',
            h8: item.palletHeight ?? '-',
            h9: item.palletWeight ?? '-',
            h10: status,
          };
        });

        this.excel.exportAsExcelFile(
          this.kopSheet,
          this.headerSheet,
          v1,
          'DATA PALLET TYPE'
        );
        this.isExporting = false;
      },
      (e) => {
        this.showMessage({
          severity: 'error',
          summary:
            "There was a problem during request or current filter doesn't match any data, please try again",
        });
        this.isExporting = false;
      }
    );
  }
}
