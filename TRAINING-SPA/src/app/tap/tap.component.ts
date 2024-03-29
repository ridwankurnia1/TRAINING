import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormsModule, ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { Employee } from '../_model/Employee';
import { EmployeeId } from '../_model/EmployeeId';
import { LogHeader } from '../_model/LogHeader';
import { PaginatedResult } from '../_model/Pagination';
import { ChecksheetService } from '../_service/checksheet.service';
import { UIService } from '../_service/ui.service';
import { CommonModule } from '@angular/common';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { RadioButtonModule } from 'primeng/radiobutton';

@Component({
  selector: 'app-tap',
  templateUrl: './tap.component.html',
  styleUrls: ['./tap.component.css'],
  standalone: true,
  imports:[
    CommonModule,
    FormsModule,
    AutoCompleteModule,
    RadioButtonModule,
    ReactiveFormsModule
  ]
  
})
export class TapComponent implements OnInit {
  @ViewChild('popup', { static: true}) popUp: TemplateRef<any>;
  header: LogHeader = {};
  id = '1';
  // nikorid = '';
  employee: Employee = {};
  listEmployee: Employee[] = [];
  display = 10;
  today = new Date();
  todayCount = 0;
  totalCount = 0;
  defaultImages = environment.imgEmpUrl + 'NoImage.png';
  employeeForm: UntypedFormGroup;
  formSearch: UntypedFormGroup;
  config = {
    ignoreBackdropClick: true
  };
  modalRef: BsModalRef;
  
  constructor(
    private ui: UIService,
    private fb: UntypedFormBuilder,
    private csservice: ChecksheetService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private modalService: BsModalService,
  ) { }

  ngOnInit(): void {
    const docId = this.route.snapshot.paramMap?.get('id');
    if (this.ui.isNullOrEmpty(docId)) {
      return;
    }
    this.csservice.getLogHeader(docId).subscribe({
      next: (res: LogHeader) => {
        this.header = res;
        this.todayCount = this.header.count;
      },
      error: (err) => {
        this.toastr.error(err.error);
      }
    });

    this.id = docId;
    const prm = {
      id: this.id
    };
    this.csservice.getTapLog(1, 10, prm).subscribe({
      next: (res: PaginatedResult<Employee[]>) => {
        if (res.result) {
          this.listEmployee = res.result;
          this.totalCount = res.pagination.totalItems;
        }
      },
      error: (err) => {
        this.toastr.error(err.error);
      }
    });
    this.createEmployeeForm();
  }
  createEmployeeForm() {
    this.employeeForm = this.fb.group({
      nik: ['', Validators.required],
      nama: [{values:'', disabled: true}],
      rfid: [{values:'', disabled: true}],
    });
    this.createFormSearch();
  }
  createFormSearch(){
    this.formSearch= this.fb.group({
      nikorid:['']
    })
  }
  getEmployee(): void {
    let dataForm = this.formSearch.getRawValue();

    if (this.ui.isNullOrEmpty(dataForm.nikorid)) {
      return
    }

    const param: EmployeeId = {};
    if (dataForm.nikorid.length === 10) {
      param.rfid = dataForm.nikorid;
    } else {
      param.nik = dataForm.nikorid;
    }

    this.csservice.addTapLog(this.id, param).subscribe({
      next: (resp: Employee) => {
        if (this.listEmployee.length >= this.display) {
          this.listEmployee.splice((this.display - 1), 1);
        }
        this.listEmployee.unshift(resp);
        this.formSearch.controls['nikorid'].setValue('');
        this.totalCount++;
        this.todayCount++;
      }, error: (err) => {
        this.toastr.error(err.error);
        // if (this.nikorid.length === 10) {
        //   this.employeeForm.controls.rfid.setValue(param.rfid);
        // } else {
        //   this.employeeForm.controls.rfid.enable();
        // }
        // this.employeeForm.controls.rfid.setValue(param.rfid);
        // this.modalRef = this.modalService.show(this.popUp, this.config);
      }
    });
  }
  setDefaultImage(item: Employee): void {
    item.photo = this.defaultImages;
  }
  save() {

  }
}
