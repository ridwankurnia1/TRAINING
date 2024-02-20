import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormControl, FormGroup, UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { DefectGroup } from 'src/app/_model/DefectGroup';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ProductionService } from 'src/app/_service/production.service';
import { UIService } from 'src/app/_service/ui.service';
import { PaginatedResult, Pagination } from 'src/app/_model/Pagination';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Dropdown } from 'src/app/_model/Dropdown';
import * as XLSX from 'xlsx';
import { TableModule } from 'primeng/table';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

@Component({
  selector: 'app-defect-group-second',
  templateUrl: './defect-group-second.component.html',
  styleUrls: ['./defect-group-second.component.css'],
  standalone: true,
  imports:[
    TableModule,
    ConfirmDialogModule,
  ]
})
export class DefectGroupSecondComponent implements OnInit {
  defectGroupForm: FormGroup;
  isEdit = false;
  isAdd = false;
  modalRef: BsModalRef;
  configModal = {
    ignoreBackdropClick: true
  };
  transactionId = 0;
  tittle: any;
  process = false;
  loading = true;
  pagination: Pagination;
  params: any = {};
  defectGroupList: DefectGroup[];
  bsConfig: Partial<BsDatepickerConfig>;
  statusDropdown = 'Inactive';
  status: Dropdown[] = [];
  selectedStatus = '';
  param: any;



  constructor(
    private productionService: ProductionService,
    private fb: UntypedFormBuilder,
    private toastr: ToastrService,
    private confirm: ConfirmationService,
    private modalService: BsModalService,
    private ui: UIService
  ) { }

  ngOnInit(): void {
    this.bsConfig = {
      containerClass: 'theme-blue',
      dateInputFormat: 'DD-MM-YYYY',
    };
    // this.createForm();
    this.createForm();
    this.dropdownInit();
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 0,
    };
    this.loadItems();
  }

  dropdownInit(): void {
    this.status = [];
    this.status.push({
      value: '1',
      label: 'Active'
    });
    this.status.push({
      value: '0',
      label: 'Inactive'
    });
    console.log(this.status);
  }

  createForm(): void {
    this.defectGroupForm = this.fb.group({
      transactionId: [''],
      defectGroup: ['', Validators.required],
      remark: ['', Validators.required]
    });
  }

  loadItems(): void {
    this.loading = true;
    this.params.status = this.selectedStatus;
    this.productionService
      .getMDF0(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.params)
        .subscribe(
          (res: PaginatedResult<DefectGroup[]>) => {
          this.defectGroupList = res.result;
          this.pagination = res.pagination;
          // console.log(this.itemList);
      }, error => {
        this.loading = false;
      }, () => {
        this.loading = false;
      });
  }

  pageChanged(event): void {
    this.pagination.currentPage = (event.first / event.rows) + 1;
    this.pagination.itemsPerPage = event.rows;
    this.params = {
      filter: event.globalFilter,
    };
    // console.log(this.params);
    this.loadItems();
  }

  searchClick(search: string): void{
    const prm = {
      filter: search
    };
    this.productionService.searchMDF0(prm)
      .subscribe((res: any) => {
        this.loading = false;
        this.defectGroupList = res;
      });
    }

  edit(template: TemplateRef<any>, data: DefectGroup): void {
    if (data) {
      this.tittle = 'Edit ';
      this.isEdit = true;
      this.isAdd = false;
      if (data.recordStatus === 1){
        this.statusDropdown = 'Active';
      }else{
        this.statusDropdown = 'Inactive';
      }
      this.transactionId = data.transactionId;
      this.defectGroupForm.setValue({
        transactionId: data.transactionId,
        defectGroup: data.defectGroup,
        remark: data.remark
      });
    } else {
      this.isEdit = false;
      this.isAdd = true;
      this.tittle = 'Add';
      const date = new Date();
      this.setDate(date);

      const dataAd = this.defectGroupForm.getRawValue();

      dataAd.transactionId = 0;
    }
    // console.log(data);

    this.modalRef = this.modalService.show(template, this.configModal);
  }

  saveItem(): void {
    if (this.defectGroupForm.invalid) {
      this.ui.validateFormEntry(this.defectGroupForm);
      return;
    }
    if (this.isAdd){
      const date = new Date();
      this.setDate(date);

      const data = this.defectGroupForm.getRawValue();

      data.transactionId = 0;
      if (this.statusDropdown === 'Active'){
        data.recordStatus = 1;
      }else{
        data.recordStatus = 0;
      }
      data.createTime = date;
      data.createUser = 'Selena';
      data.changeTime = date;
      data.changeUser = 'Selena';

      this.productionService.addMDF0(data)
            .subscribe({
              next: () => {
                this.toastr.success('Save data success');
                this.loadItems();
              },
              error: (error) => {
                this.toastr.error('Data sudah ada');
              }
            });
      this.defectGroupForm.reset();
      this.statusDropdown = 'Inactive';
      // this.dataNotExist = true;
      }
    if (this.isEdit) {
          const date = new Date();
          this.setDate(date);
          const edit = this.defectGroupForm.getRawValue();
          if (this.statusDropdown === 'Active'){
            edit.recordStatus = 1;
          }else{
            edit.recordStatus = 0;
          }
          edit.createTime = date;
          edit.createUser = 'Selena';
          edit.changeTime = date;
          edit.changeUser = 'Selena';
          console.log(edit);

          this.productionService.editMDF0(edit)
          .subscribe({
            next: () => {
              this.toastr.success('Data Berhasil Diedit');
              this.loadItems();
            },
            error: (error) => {
              this.toastr.error('Defect Group Sudah Ada');
            }
          });

          this.defectGroupForm.reset();
          this.statusDropdown = 'Inactive';
        }
    }


  saveCheck(defect): void {
    this.productionService.addMDF0(defect).subscribe(
      () => {
        this.toastr.success('Save data success');
        this.loadItems();
      },
      error => {
        this.process = false;
      },
      () => {
        this.process = false;
        this.modalRef.hide();
        this.loadItems();
      });
    // console.log(sio);
  }

  deleteDat(data: DefectGroup): void {
    console.log(data);
    this.confirm.confirm({
      message: 'Delete Defect Group ' + data.defectGroup + ' ?',
      header: 'Confirmation',
    accept: () => {
    this.productionService.deleteMDF0(data.transactionId)
          .subscribe(() => {
          }, () => {
            this.loadItems();
            this.toastr.success('Defect Group Deleted');
          });

    }});
    return;
  }

  download(): void {
    this.loading = true;
    // this.param.dept = this.selectedDefect;
    // this.param.status = this.selectedStatus;
    // this.param.xls = '1';
    const itemPerPage = 100;

    // const idx1 = this..findIndex(x => x.value === this.selectedDefect);
    // const idx2 = this.status.findIndex(x => x.value === this.selectedStatus);
    // let defectName = 'All Defect';
    // let status = 'All Status';
    // if (idx1 >= 0) {
    //   defectName = this.defectName[idx1].label;
    // }
    // if (idx2 >= 0) {
    //   status = this.status[idx2].label;
    // }
    const parameter = [
      {
        l1: 'Defect Name',
        v1: 'All Defect',
        v2: '',
        v3: ''
      },
      {
        l1: 'Status',
        v1: 'All Status',
        v2: '',
        v3: ''
      },
    ];

    const header = [{
      h01: 'Transaction Id',
      h02: 'Defect Group',
      h03: 'Remark',
      h04: 'Record Status',
      h05: 'Create Time',
      h06: 'Create User',
      h07: 'Change Time',
      h08: 'Change User',
      h09: 'Record Status Text',
    }];

    let startCell = '';
    let index = 1;
    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(parameter, { skipHeader: true });
    index = parameter.length + 2;
    startCell = 'A' + index.toString();
    XLSX.utils.sheet_add_json(worksheet, header, { origin: startCell, skipHeader: true });

    index++;

    const loop = (page: number) => {
      this.productionService.getMDF0(
        page,
        itemPerPage,
        this.param
      ).subscribe((resp: PaginatedResult<DefectGroup[]>) => {
        resp.result.forEach(element => {});

        startCell = 'A' + index.toString();
        XLSX.utils.sheet_add_json(worksheet, resp.result, { origin: startCell, skipHeader: true });
        index += resp.result.length;

        if (resp.pagination.currentPage < resp.pagination.totalPages) {
          loop(resp.pagination.currentPage + 1);
        } else {
          const workbook: XLSX.WorkBook = {Sheets: {Sheet1: worksheet}, SheetNames: ['Sheet1'] };
          XLSX.writeFile(workbook, 'Defect Group.xlsx');
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

  closeClick(): void{
    this.defectGroupForm.reset();
  }

  setDate(date: Date): void{
    const hours = date.getHours();
    const minutes = date.getMinutes();
    const seconds = date.getSeconds();
  }

}

