import { CommonModule, getLocaleDateFormat, getLocaleTimeFormat } from '@angular/common';
import { Component, OnInit, TemplateRef } from '@angular/core';
import {
  FormsModule,
  ReactiveFormsModule,
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import * as moment from 'moment';
import {
  BsDatepickerConfig,
  BsDatepickerModule,
} from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DropdownModule } from 'primeng/dropdown';
import { TableModule } from 'primeng/table';
import { filter } from 'rxjs/operators';
import { PaginatedResult, Pagination } from 'src/app/_model/Pagination';
import { Truck } from 'src/app/_model/Truck';
import { ExcelService } from 'src/app/_service/excel.service';
import { TruckService } from 'src/app/_service/truck.service';

@Component({
  selector: 'app-truck',
  templateUrl: './truck.component.html',
  styleUrls: ['./truck.component.css'],
  providers: [TruckService, ConfirmationService, BsModalService],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    TableModule,
    ReactiveFormsModule,
    ConfirmDialogModule,
    ModalModule,
    DropdownModule,
    BsDatepickerModule,
  ],
})
export class TruckComponent implements OnInit {
  param = {};

  modalRef: BsModalRef;
  listTruck: Truck[];
  pagination: Pagination;
  pageSize = 10;
  truckForm: UntypedFormGroup;
  configModal = {
    class : 'modal-xl',
    ignoreBackdropClick: true,
  };
  isEdit = false;
  loading = true;
  bsConfig: Partial<BsDatepickerConfig>;
  // truckId = 0;

  groupSearch: string = '';

  isDisabled = true;

  constructor(
    private truckService: TruckService,
    private fb: UntypedFormBuilder,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private confirm: ConfirmationService,
    private excel: ExcelService
  ) {}

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-blue',
      dateInputFormat: 'DD-MM-YYYY',
    };
    this.createForm();
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 0,
    };
    this.loadData();
  }

  createForm(): void {
    this.truckForm = this.fb.group({
      truckId: [''],
      truckName: ['', Validators.required],
      merk: ['', Validators.required],
      driver: ['', Validators.required],
      joinDate: ['', Validators.required],
      endDate: ['', Validators.required],
    });
  }

  //load all data
  loadData(): void {
    this.loading = true;
    this.truckService
      .getTrucks(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.param
      )
      .subscribe((res: PaginatedResult<Truck[]>) => {
        this.loading = false;
        this.listTruck = res.result;
        this.pagination = res.pagination;
      });
  }

  //pagination
  pageChanged(event): void {
    this.pagination.currentPage = event.first / event.rows + 1;
    this.pagination.itemsPerPage = event.rows;
    this.param = {
      filter: event.globalFilter,
    };

    if (event.sortField) {
      let sortString = event.sortField;

      if (event.sortOrder < 1) {
        sortString = '-' + sortString;
      }

      this.param['sortString'] = sortString;
    }

    this.loadData();
  }
  itemPerPageChange(event): void {
    this.loadData();
  }

  //ambil data truck
  getTruck(): void {
    const truckId = this.truckForm.controls.truckId.value;
    this.truckService.getTruck(truckId).subscribe({
      next: (item: Truck) => {
        if (item) {
          this.truckForm.setValue({
            truckId: item.truckId,
            truckName: item.truckName,
            merk: item.merk,
            driver: item.driver,
            joinDate: new Date(item.joinDate),
            endDate: new Date(item.endDate),
          });
        }
      },
    });
  }

  //menyimpan/mengedit truck
  save(): void {
    this.loading = true;
    const data = this.truckForm.getRawValue();

    if (data.endDate <= data.joinDate) {
      this.toastr.error('End Date harus lebih besar dari Join Date');
      this.loading = false;
      return;
    }

    if (this.isEdit) {
      this.truckService.editTruck(data).subscribe(
        () => {
          this.loadData();
          this.loading = false;
          this.modalRef.hide();
        },
        (error) => {
          this.toastr.error(error.error);
          this.loading = false;
        }
      );
    } else {
      if (this.truckForm.invalid) {
        this.validateFormEntry(this.truckForm);
        return;
      }
      this.truckService.addTruck(data).subscribe(
        () => {
          this.loadData();
          this.loading = false;
          this.modalRef.hide();
        },
        (error) => {
          this.toastr.error(error.error);
        }
      );
    }
  }

  // Edit data truck
  edit(item: Truck, template: TemplateRef<any>): void {
    this.truckForm.reset({ truckId: 0 });
    if (item) {
      this.isEdit = true;
      this.truckForm.setValue({
        truckId: item.truckId,
        truckName: item.truckName,
        merk: item.merk,
        driver: item.driver,
        joinDate: new Date(item.joinDate),
        endDate: new Date(item.endDate),
      });
    } else {
      this.isEdit = false;
    }
    this.modalRef = this.modalService.show(template, this.configModal);
  }

  // Hapus truck
  delete(item: Truck): void {
    this.confirm.confirm({
      message: 'Delete truck ' + item.truckName + ' ?',
      header: 'Confirmation',
      accept: () => {
        this.truckService.deleteTruck(item.truckId).subscribe(() => {
          this.loadData();
          this.toastr.success('Truck Deleted');
        });
      },
    });
  }

  // Restore truck
  restore(item: Truck): void {
    this.confirm.confirm({
      message: 'Restore truck ' + item.truckName + ' ?',
      header: 'Confirmation',

      accept: () => {
        this.truckService.restoreTruck(item).subscribe(() => {
          this.loadData();
          this.toastr.success('Truck Restored');
        });
      },
    });
  }

  // Export ke excel
  exportExcel() {

    var stillUtc = moment.utc(new Date).toDate();
  var localTime = moment(stillUtc).local().format('DD-MM-YYYY HH:mm:ss');

    let kopSheet = [
      { A1: 'PT Asahimas Flat Glass Tbk' },
      { A1: 'DATA TRUCK' },
      {
        A1:
          'Export Time : ' + localTime,
      },
    ];

    let headerSheet = [
      {
        A1: 'Truck Name',
        A2: 'Merk',
        A3: 'Driver',
        A4: 'Join Date',
        A5: 'End Date',
        A6: 'Updated By',
        A7: 'Status',
      },
    ];

    this.truckService.export(this.groupSearch).subscribe((res) => {
      var filtered = res.map((item) => {
        let status = item.recordStatus == 1 ? 'ACTIVE' : 'INACTIVE';
        let formattedJoinDate = moment(item.joinDate).format('DD-MMM-YYYY');
        let formattedEndDate = moment(item.endDate).format('DD-MMM-YYYY');
        return {
          a1: item.truckName,
          a2: item.merk,
          a3: item.driver,
          a4: formattedJoinDate,
          a5: formattedEndDate,
          a6: item.updatedBy,
          a7: status,
        };
      });
      this.excel.exportAsExcelFile(
        kopSheet,
        headerSheet,
        filtered,
        'DATA TRUCK'
      );
    });
  }

  // Export ke excel tapi 1 page yang tampil
  // exportThisPage() {

  //   var stillUtc = moment.utc(new Date).toDate();
  //   var localTime = moment(stillUtc).local().format('DD-MM-YYYY HH:mm:ss');
  //   let kopSheet = [
  //     { A1: 'PT Asahimas Flat Glass Tbk' },
  //     { A1: 'DATA TRUCK' },
  //     {
  //       A1:
  //         'Export Time : ' + localTime,
  //     },
  //     // { A1: '' },
  //   ];

  //   let headerSheet = [
  //     {
  //       A1: 'Truck Name',
  //       A2: 'Merk',
  //       A3: 'Driver',
  //       A4: 'Join Date',
  //       A5: 'End Date',
  //       A6: 'Updated By',
  //       A7: 'Status',
  //     },
  //   ];

  //   var listExport: any[] = JSON.parse(JSON.stringify(this.listTruck));
  //     listExport = listExport.map((data: Truck) => {
  //       let status = data.recordStatus == 1 ? 'ACTIVE' : 'INACTIVE';
  //       let formattedJoinDate = moment(data.joinDate).format('DD-MMM-YYYY');
  //       let formattedEndDate = moment(data.endDate).format('DD-MMM-YYYY');

  //       var emp: Truck = {
  //         truckName: data.truckName,
  //         merk: data.merk,
  //         driver: data.driver,
  //         tempJoinDate: formattedJoinDate,
  //         tempEndDate: formattedEndDate,
  //         updatedBy: data.updatedBy,
  //         // recordStatus: data.recordStatus,
  //         tempStatus:status,
  //       };

  //       return emp;
  //     });
  //     this.excel.exportAsExcelFile(
  //       kopSheet,
  //       headerSheet,
  //       listExport,
  //       'DATA TRUCK(Page)'
  //     );
  //   }

  validateFormEntry(formGroup: UntypedFormGroup): void {
    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      if (control instanceof UntypedFormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof UntypedFormGroup) {
        this.validateFormEntry(control);
      }
    });
  }
}
