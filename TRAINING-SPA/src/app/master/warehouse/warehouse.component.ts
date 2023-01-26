import { Component, OnInit } from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { WarehouseGroup } from 'src/app/_model/WarehouseGroup';
import { UIService } from 'src/app/_service/ui.service';
import { WarehouseService } from 'src/app/_service/warehouse.service';

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.css'],
})
export class WarehouseComponent implements OnInit {
  groupData: WarehouseGroup[];

  whFormGroup: UntypedFormGroup;
  ipRecordStatus: Boolean;

  isLoading = false;
  isFormVisible = false;
  isUpdating = false;
  isSubmitting = false;

  constructor(
    private warehouse: WarehouseService,
    private ui: UIService,
    private fb: UntypedFormBuilder
  ) {}

  ngOnInit() {
    this.getAllGroup();
    this.initForm();
  }

  initForm() {
    this.whFormGroup = this.fb.group({
      code: ['', [Validators.required, Validators.maxLength(2)]],
      name: ['', Validators.required],
      remark: [''],
      system: [''],
      status: [''],
      recordStatus: [false],
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
        this.toggleForm(res);
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
    if (this.whFormGroup.invalid) {
      this.ui.validateFormEntry(this.whFormGroup);
      this.isSubmitting = false;
      return;
    }

    this.warehouse.createGroup(this.whFormGroup.value).subscribe(
      () => {
        this.isSubmitting = false;

        this.toggleForm();
        this.whFormGroup.reset();
        this.getAllGroup();
      },
      (e) => {
        this.isSubmitting = false;
      }
    );
  }

  toggleForm(data?: any) {
    this.isFormVisible = !this.isFormVisible;

    if (data) {
      console.info(data);
      this.isUpdating = true;
      this.whFormGroup.reset();
      this.whFormGroup.patchValue(data);
      this.whFormGroup.controls['recordStatus'].setValue(
        data.recordStatus == 1
      );
    } else {
      this.isUpdating = false;
      this.whFormGroup.reset();
    }
  }
}
