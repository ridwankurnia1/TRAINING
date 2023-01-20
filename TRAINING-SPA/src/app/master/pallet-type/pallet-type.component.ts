import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PalletType } from 'src/app/_model/PalletType';
import { PalletTypeService } from 'src/app/_service/pallet-type.service';
import { UIService } from 'src/app/_service/ui.service';
import {
  UntypedFormGroup,
  UntypedFormBuilder,
  Validators,
} from '@angular/forms';
import { PaginatedResult, Pagination } from 'src/app/_model/Pagination';
import { Dropdown2 } from 'src/app/_model/Dropdown2';
import { DropdownFilterOptions } from 'primeng/dropdown';

@Component({
  selector: 'app-pallet-type',
  templateUrl: './pallet-type.component.html',
  styleUrls: ['./pallet-type.component.css'],
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

  // table variables

  // filters
  globalSearch: String;
  typeFilter: String;
  appFilter: String;
  materialFilter: String;
  nameFilter: String;
  // dropdown type filter
  dAppFilterable = [];
  dMaterialFilterable = [];
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
  ipStatus: Boolean;
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

  constructor(
    private modalService: BsModalService,
    private palletService: PalletTypeService,
    private fb: UntypedFormBuilder,
    private ui: UIService
  ) {}

  ngOnInit() {
    this.initForm();
    this.initDropdowns();
    this.initTable();
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
      // searchColor: this.colorFilter,
      // searchLength: this.lengthFilter,
      // searchWidth: this.widthFilter,
      // searchRemark: this.remarkFilter,
    };

    console.info(this.param);

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
      .subscribe((res: PaginatedResult<PalletType[]>) => {
        this.pallets = res.result;
        this.pagination = res.pagination;
      })
      .add(() => {
        this.isLoading = false;
      });
  }

  initTable() {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 1,
    };
  }

  resetTable() {
    this.pagination.currentPage = 1;
    this.pagination.itemsPerPage = 10;
  }

  initForm() {
    this.formGroup = this.fb.group({
      type: ['', Validators.required],
      name: ['', Validators.required],
      codification: [1, Validators.required],
      app: ['', Validators.required],
      materialType: ['', Validators.required],
      color: ['', Validators.required],
      currency: ['', Validators.required],
      length: [0, Validators.min(1)],
      lengthType: ['MM', Validators.required],
      width: [0, Validators.min(1)],
      widthType: ['MM', Validators.required],
      height: [0, Validators.min(1)],
      heightType: ['MM', Validators.required],
      weight: [0, Validators.min(1)],
      weightType: ['KG', Validators.required],
      price: [0, Validators.min(1)],
      recordStatus: [0],
      remark: ['', Validators.required],
    });
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

    this.ipHeightType = 'MM';
    this.ipWidthType = 'MM';
    this.ipLengthType = 'MM';
    this.ipWeightType = 'KG';
  }

  resetDropdownFilter(options: DropdownFilterOptions) {
    options.reset();
    this.dCurrencyFilter = '';
  }

  submitForm() {
    this.isSubmitting = true;
    if (this.formGroup.invalid) {
      this.ui.validateFormEntry(this.formGroup);
      return;
    }
    this.palletService.create(this.formGroup.value);
    console.info(this.formGroup.value);
    // this.formGroup.reset();
    this.toggleForm();
    this.getData();
  }

  toggleForm() {
    this.isCollapsed = !this.isCollapsed;
    // this.modalRef = this.modalService.show(template);
  }
}
