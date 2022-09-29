import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Dropdown } from 'src/app/_model/Dropdown';
import { ChecksheetService } from 'src/app/_service/checksheet.service';
import { UIService } from 'src/app/_service/ui.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  formItem: UntypedFormGroup;
  process = false;
  department: Dropdown[] = [];
  constructor(
    private toastr: ToastrService,
    private fb: UntypedFormBuilder,
    private ui: UIService,
    private csservice: ChecksheetService
  ) { }

  ngOnInit() {
    this.createFormItem();
    this.dropdownInit();
  }
  createFormItem() {
    this.formItem = this.fb.group({
      nik: ['', Validators.required],
      nama: ['', Validators.required],
      rfid: [''],
      departmentId: ['', Validators.required]
    });
  }
  dropdownInit() {
    this.csservice.getDepartment('CKP').subscribe({
      next: (res: Dropdown[]) => {
        this.department = res;
      }
    });
  }
  save() {
    if (this.formItem.invalid) {
      this.ui.validateFormEntry(this.formItem);
      return;
    }

    this.process = true;
    const data = this.formItem.getRawValue();
    const idx = this.department.findIndex(x => x.value === data.departmentId);
    data.department = this.department[idx].label;
    this.csservice.addEmployee(data).subscribe({
      next: () => {
        this.process = false;
        this.toastr.success('Employee berhasil disimpan');
        this.formItem.reset();
      }, error: (err) => {
        this.process = false;
        this.toastr.error(err.error);
      }
    });
    
  }
}
