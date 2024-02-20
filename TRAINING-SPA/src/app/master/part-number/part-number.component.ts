import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Apollo } from 'apollo-angular';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { GET_LIST_PART_NUMBER } from 'src/app/graphql/graphql.queries';
import { Dropdown } from 'src/app/_model/Dropdown';
import { GqlPagination } from 'src/app/_model/GqlPagination';
import { PartNumber } from 'src/app/_model/PartNumber';
import { UIService } from 'src/app/_service/ui.service';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CommonModule } from '@angular/common';
import { RadioButtonModule } from 'primeng/radiobutton';
import { TableModule } from 'primeng/table';
import { EmployeeRoutes } from '../employee/employee.routing';

@Component({
  selector: 'app-part-number',
  templateUrl: './part-number.component.html',
  styleUrls: ['./part-number.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RadioButtonModule,
    EmployeeRoutes,
    TableModule,
    ReactiveFormsModule,
    ConfirmDialogModule
  ]
})
export class PartNumberComponent implements OnInit {
  @ViewChild('popup', { static: true}) popup: TemplateRef<any>;
  data: PartNumber[] = [];
  popTitle = 'Add Part Number';
  isEdit = false;
  isCopy = false;
  process = false;
  loading = true;
  status: Dropdown[] = [];
  selectedStatus = '';
  params: any = null;
  modalRef: BsModalRef;
  pagination: GqlPagination;
  form: FormGroup;
  config = {
    ignoreBackdropClick: true
  };

  constructor(
    private apollo: Apollo,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private helper: UIService
  ) { }

  ngOnInit() {
    this.pagination = {
      skip: 0,
      take: 10,
      totalCount: 0
    };
    this.createForm();
    this.dropdownInit();
  }

  dropdownInit() {
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
  createForm() {
    this.form = this.fb.group({
      customerId: ['', Validators.required],
      partNumber: ['', Validators.required],
      modelName: ['', Validators.required],
      partDescription: [''],
      remark: [''],
      otherRemark: [''],
      alcNumber: [''],
      eoNumber: [''],
    });
  }

  pageChanged(event: any): void {
    this.loading = true;
    this.pagination.skip = event.first;
    this.pagination.take = event.rows;

    if (event.filters) {
      this.params = {};
      if (event.filters.partNumber) {
        this.params['partNumber'] = {};
        this.params.partNumber[event.filters.partNumber.matchMode] = event.filters.partNumber.value;
      }
      if (event.filters.customerId) {
        this.params['customerId'] = {};
        this.params.customerId[event.filters.customerId.matchMode] = event.filters.customerId.value;
      }
      if (event.filters.modelName) {
        this.params['modelName'] = {};
        this.params.modelName[event.filters.modelName.matchMode] = event.filters.modelName.value;
      }
      if (event.filters.eoNumber) {
        this.params['eoNumber'] = {};
        this.params.eoNumber[event.filters.eoNumber.matchMode] = event.filters.eoNumber.value;
      }
      if (event.filters.status) {
        this.params['status'] = {};
        this.params.status[event.filters.status.matchMode] = Number(event.filters.status.value);
      }
    }

    this.loadData();
  }

  loadData() {
    let filter = null;
    if (!this.helper.isNullOrEmpty(this.params)) {
      filter = {
        and: [
          this.params
        ]
      };
    }

    this.apollo.watchQuery({
      query: GET_LIST_PART_NUMBER,
      variables: {
        skip: this.pagination.skip,
        take: this.pagination.take,
        filter: filter
      }
    }).valueChanges.subscribe(({data, loading}: any) => {
      this.data = data.listPartNumber.items;
      this.loading = loading;
      this.pagination.totalCount = data.listPartNumber.totalCount;
      // console.log(data.listPartNumber);
    });
  }

  entry(data: PartNumber, copy = false, popUp = true) {
    if (data) {
      this.form.patchValue(data);
      this.isCopy = copy;
      this.isEdit = !copy;
      if (copy) {
        this.popTitle = 'Copy Part Number';
      } else {
        this.popTitle = 'Edit Part Number';
      }
    } else {
      this.form.reset();
      this.isCopy = false;
      this.isEdit = false;
      this.popTitle = 'Add Part Number';
    }

    if (popUp) {
      this.modalRef = this.modalService.show(this.popup, this.config);
    }
  }

  save() {
    if (this.form.invalid) {
      this.helper.validateFormEntry(this.form);
      return;
    }

    this.process = true;
    const data = this.form.getRawValue();
    // edit or add
  }

  delete(item: PartNumber) {
    this.confirmation.confirm({
      header: 'Confirmation',
      message: 'Delete Part Number ' + item.partNumber + ' ?',
      accept: () => {
        // delete
        this.toastr.info('Part number ' + item.partNumber + ' deleted');
      }
    });
  }

  restore(item: PartNumber) {
    this.confirmation.confirm({
      header: 'Confirmation',
      message: 'Restore Part Number ' + item.partNumber + ' ?',
      accept: () => {
        // delete
        this.toastr.info('Part number ' + item.partNumber + ' restored');
      }
    });
  }

  download() {

  }
}
