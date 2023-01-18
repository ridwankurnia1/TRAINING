import {
  Component,
  OnChanges,
  OnInit,
  SimpleChanges,
  TemplateRef,
} from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PalletType } from 'src/app/_model/PalletType';
import { PalletTypeService } from 'src/app/_service/pallet-type.service';
import { UIService } from 'src/app/_service/ui.service';
import { UntypedFormGroup, UntypedFormBuilder, Validators } from '@angular/forms';
import { delay } from 'rxjs/operators';

@Component({
  selector: 'app-pallet-type',
  templateUrl: './pallet-type.component.html',
  styleUrls: ['./pallet-type.component.css'],
})
export class PalletTypeComponent implements OnInit {
  modalRef: BsModalRef;
  isCollapsed = true;
  pallets: PalletType[];

  // table variables
  globalSearch: String;
  nameFilter: String;
  lengthFilter: Number;
  heightFilter: Number;
  isLoading: Boolean;
  isSubmitting: Boolean = false;
  isFormValid: Boolean = false;

  // form variables
  formGroup: UntypedFormGroup;
  ipType: String;
  ipName: String;
  ipCodification: Number;
  ipApp: String = '';
  ipMaterial: String = '';
  ipColor: Number;
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
  dCurrency = [];
  dMeasure = [];
  dWeight = [];

  constructor(
    private modalService: BsModalService,
    private palletService: PalletTypeService,
    private fb: UntypedFormBuilder,
    private ui: UIService
  ) {}

  ngOnInit() {
    this.initForm();
    this.initDropdowns();
  }

  getData() {
    this.isLoading = true;
    this.pallets = [];
    this.lazyLoad().then(() => {
      this.pallets = this.palletService.all();
      this.isLoading = false;
    });
  }

  async lazyLoad() {
    await new Promise((res) => setTimeout(res, 3000));
  }

  initForm() {
    this.formGroup = this.fb.group({
      type: ['', Validators.required],
      name: ['', Validators.required],
      codification: [1, Validators.required],
      app: ['', Validators.required],
      materialType: ['', Validators.required],
      color: [0, Validators.min(1)],
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
    this.dMaterial = [
      { label: 'SELECT MATERIAL', value: '' },
      { label: 'STEEL', value: 'STEEL' },
      { label: 'WOODEN', value: 'WOODEN' },
    ];

    this.dApp = [
      { label: 'SELECT APP', value: '' },
      { label: 'ONE WAY PALLET', value: 'OWP' },
      { label: 'RETURNABLE PALLET', value: 'RTN' },
    ];

    this.dColor = [
      { label: 'SELECT COLOR', value: '' },
      { label: 'GREEN', value: 1 },
      { label: 'GRAY', value: 2 },
      { label: 'BLACK', value: 3 },
    ];

    this.dCurrency = [
      { label: 'SELECT CURRENCY', value: '' },
      { label: 'IDR', value: 'IDR' },
      { label: 'USD', value: 'USD' },
    ];

    this.dMeasure = [
      { label: 'MILIMETERS', value: 'MM' },
      { label: 'METERS', value: 'M' },
    ];

    this.dWeight = [
      { label: 'KILOGRAMS', value: 'KG' },
      { label: 'GRAMS', value: 'GR' },
    ];
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
