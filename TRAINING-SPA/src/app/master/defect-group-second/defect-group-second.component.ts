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

@Component({
  selector: 'app-defect-group-second',
  templateUrl: './defect-group-second.component.html',
  styleUrls: ['./defect-group-second.component.css']
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
  param = {};
  statusDropdown = '';
  


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
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 0,
    };
    this.loadItems();
  }

  createForm(): void {
    this.defectGroupForm = this.fb.group({
      transactionId: [''],
      defectGroup: ['', Validators.required],
      remark: ['', Validators.required]
    });
  }

  loadItems() {
    this.loading = true;
    this.productionService.getMDF0(this.pagination.currentPage, this.pagination.itemsPerPage, this.params)
        .subscribe((res: PaginatedResult<DefectGroup[]>) => {
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
    this.param = {
      filter: event.globalFilter
    };
    
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

  edit(template: TemplateRef<any>, data: DefectGroup) {
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
      dataAd.createTime = date;
      dataAd.createUser = 'Selena';
      dataAd.changeTime = date;
      dataAd.changeUser = 'Selena';
    }
    // console.log(data);

    this.modalRef = this.modalService.show(template, this.configModal);
  }

  saveItem() {
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
      this.statusDropdown = 'Active';
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
        this.statusDropdown = 'Active';
      }
    }
    


  saveCheck(defect) {
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
    // this.confirm.confirm({
    //   message: 'Delete employee ' + data.defectGroup + ' ?',
    //   header: 'Confirmation',
    // accept: () => {
        this.productionService.deleteMDF0(data.transactionId)
          .subscribe(() => { 
          }, () =>{
            this.loadItems();
            this.toastr.success('Defect Group Deleted');
          });
      
    // });
    return;
  }

  setDate(date: Date): void{
    const hours = date.getHours();
    const minutes = date.getMinutes();
    const seconds = date.getSeconds();
  }

}

