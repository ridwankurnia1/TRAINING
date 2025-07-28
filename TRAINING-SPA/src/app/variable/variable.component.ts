// variable.component.ts
import { Component, OnInit, TemplateRef } from '@angular/core';
import { VariableService } from '../_service/variable.service';
import {
  FormGroup,
  ReactiveFormsModule,
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { VariableItem } from '../_model/Variable';
import { CommonModule } from '@angular/common';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastModule } from 'primeng/toast';
import { ToastrService } from 'ngx-toastr';
import { UIService } from '../_service/ui.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { PaginatedResult, Pagination } from '../_model/Pagination';

@Component({
  selector: 'app-variable',
  templateUrl: './variable.component.html',
  styleUrls: ['./variable.component.css'],
  standalone: true,
  imports: [
    TableModule,
    ConfirmDialogModule,
    CommonModule,
    ReactiveFormsModule,
    ConfirmDialogModule,
    ToastModule,
  ],
})
export class VariableComponent implements OnInit {
  variableForm: FormGroup;
  variable: VariableItem[] = [];
  rowsPerPageOptions: any;
  formItem: UntypedFormGroup;
  pageSize: any;
  loading = false;
  isEditing = false;
  editingId: number | null = null;
  statusDropdown = 'Inactive';
  process = false;
  
  // Modalpurpose
  modalRef?: BsModalRef;
  showModal = false;
  showDeleteModal = false;
  modalMode: 'create' | 'edit' = 'create';
  modalGroup: UntypedFormGroup;

  // lazy loading
  pagination : Pagination = {
    currentPage: 1,
    itemsPerPage: 10,
    totalItems: 0,
    totalPages: 0,
  };
  params;
  
  constructor(
    private variableService: VariableService,
    private fb: UntypedFormBuilder,
    private ui: UIService,
    private message: MessageService,
    private modal: BsModalService,
    private toastr: ToastrService,
    private confirm: ConfirmationService
  ) {
    this.variableForm = this.fb.group({
      variableId: [0],
      user: ['AMG'],
      name: ['CKP'],
      code: ['', [Validators.required, Validators.minLength(2)]],
      value: [''],
    });
    // this.initForm
  }
  
  ngOnInit(): void {
    this.params = {};
    this.loadVariable();
    this.pagination;
    // this.pagination = {
    //   currentPage: 1,
    //   itemsPerPage: 10,
    //   totalItems: 0,
    //   totalPages: 0,
    // };
  }
  
  // loadVariable(): void {
  //   this.loading = true;
  //   this.variableForm = this.fb.group({
  //     variableId: [0],
  //     user: ['AMG'],
  //     name: ['CKP'],
  //     code: ['', [Validators.required, Validators.minLength(2)]],
  //     value: [''],
  //   });
  //   this.variableService.getVariables().subscribe({
  //     next: (data) => {
  //       this.variable = data;
  //       this.loading = false;
  //     },
  //     error: (error) => {
  //       this.toastr.error('Data failed to load');
  //       console.error('Error loading data:', error);
  //       this.loading = false;
  //     },
  //   });
  // }

  editVariable(variable: VariableItem): void {
    this.isEditing = true;
    this.editingId = variable.variableId;
    this.variableForm.patchValue({
      variableId: variable.variableId,
      user: variable.user,
      name: variable.name,
      code: variable.code,
      value: variable.value,
    });
  }

  onSubmit(): void {
    if (this.variableForm.invalid) {
      this.ui.validateFormEntry(this.variableForm);
      console.log('Variable form is invalid');
      return;
    }

    this.process = true;
    const data: VariableItem = this.variableForm.getRawValue();

    if (this.isEditing) {
      if (!data.variableId) {
        console.error('Missing variableId for update operation', data);
        this.process = false;
        return;
      }

      this.variableService.update(data).subscribe({
        next: () => {
          console.log(data);
          this.toastr.success('Variable has been updated successfully');
          this.loadVariable();
        },
        error: (error) => {
          this.toastr.error('Failed to update variable');
          console.error('Update error:', error);
          console.log('Data sent for update:', data);
        },
        complete: () => (this.process = false),
      });
    } else {
      const createData: Omit<VariableItem, 'variableId'> = {
        user: data.user,
        name: data.name,
        code: data.code,
        value: data.value,
      };

      this.variableService.create(createData).subscribe({
        next: () => {
          this.toastr.success('Variable has been created successfully');
          this.loadVariable();
        },
        error: (error) => {
          this.toastr.error('Failed to create. Variable Should be Unique');
          console.error('Create error:', error);
          console.log('Data sent for creation:', createData);
        },
        complete: () => (this.process = false),
      });
    }
  }

  onSubmitModal(): void {
    console.log(this.variableForm);

    if (this.variableForm.invalid) {
      this.ui.validateFormEntry(this.variableForm);
      console.log('Variable form is invalid');
      return;
    }

    this.process = true;
    const data: VariableItem = this.variableForm.getRawValue();
    console.log('onsubmit is', data);
    if (this.isEditing) {
      if (!data.variableId) {
        console.error('Missing variableId for update operation', data);
        this.process = false;
        return;
      }

      this.variableService.update(data).subscribe({
        next: () => {
          console.log(data);
          this.toastr.success('Variable has been updated successfully');
          this.loadVariable();
        },
        error: (error) => {
          this.toastr.error('Failed to update variable');
          console.error('Update error:', error);
          console.log('Data sent for update:', data);
        },
        complete: () => (this.process = false),
      });
    } else {
      const createData: Omit<VariableItem, 'variableId'> = {
        user: data.user,
        name: data.name,
        code: data.code,
        value: data.value,
      };

      this.variableService.create(createData).subscribe({
        next: () => {
          this.toastr.success('Variable has been created successfully');
          this.loadVariable();
        },
        error: (error) => {
          this.toastr.error('Failed to create. Variable Should be Unique');
          console.error('Create error:', error);
          console.log('Data sent for creation:', createData);
        },
        complete: () => (this.process = false),
      });
    }
  }

  updateVariable(): void {
    if (this.editingId) {
      const updatedVariable: VariableItem = {
        variableId: this.editingId,
        ...this.variableForm.value,
      };

      this.variableService.update(updatedVariable).subscribe({
        next: (variable) => {
          const index = this.variable.findIndex(
            (i) => i.variableId === variable.variableId
          );
          if (index !== -1) {
            this.variable[index] = variable;
          }
          this.resetForm();
        },
        error: (error) => {
          console.error('Error updating item:', error);
        },
      });
    }
  }

  cancelEditReguler(): void {
    this.resetForm();
  }
  cancelEditModal(): void {
    this.loadVariable();
  }

  resetForm(): void {
    this.variableForm.reset();
    this.variableForm.patchValue({ isActive: true });
    this.isEditing = false;
    this.editingId = null;
    this.initForm();
    // this.variableForm = this.fb.group({
    //   variableId: [0],
    //   user: ['AMG'],
    //   name: ['CKP'],
    //   code: ['', [Validators.required, Validators.minLength(2)]],
    //   value: [''],
    // });
  }

  // // Modal Section
  //   openCreateModal(): void {
  //     this.modalMode = 'create';
  //     this.currentVariable = this.getEmptyVariable();
  //     this.showModal = true;
  // }

  openModalForm(id?: number, element?: TemplateRef<any>): void {
    this.modalMode = 'create';
    // this.variableForm = this.fb.group({
    //   variableId: [0],
    //   user: ['AMG'],
    //   name: ['CKP'],
    //   code: ['', [Validators.required, Validators.minLength(2)]],
    //   value: [''],
    // });
    this.initForm();
    this.modalRef = this.modal.show(element, { ignoreBackdropClick: true });
  }

  deleteVariable(variableId: number): void {
    if (!variableId || variableId <= 0) {
      this.toastr.error('Invalid variable ID');
      return;
    }

    this.confirm.confirm({
      message: `Are you sure you want to delete variable ${variableId}?`,
      accept: () => {
        this.variableService.delete(variableId).subscribe({
          next: () => {
            this.loadVariable();
            this.toastr.success(`Variable deleted successfully `);

            this.message.add({
              severity: 'success',
              summary: 'Variable deleted successfully',
            });
          },
          error: (err) => {
            console.error('Delete error:', err);
            const errorMessage =
              err.error?.message || err.message || 'Unknown error occurred';
            this.toastr.error(`Failed to delete variable: ${errorMessage}`);
          },
        });
      },
    });
  }

  pageChanged(event): void {
    if (!event || event.rows == null || event.first == null) return;

    this.pagination.currentPage = event.first / event.rows + 1;
    this.pagination.itemsPerPage = event.rows;
    // this.params = {
    //   filter: event.globalFilter,
    // };
    this.loadVariable();
  }

  initForm() {
    this.variableForm = this.fb.group({
      variableId: [0],
      user: ['AMG'],
      name: ['CKP'],
      code: ['', [Validators.required, Validators.minLength(2)]],
      value: [''],
    });
  }

  loadVariable(): void {
    this.loading = true;
    this.variableService
      .getVariablePaging(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.params
      )
      .subscribe({
        next: (data: PaginatedResult<VariableItem[]>) => {
          this.initForm();
          // console.log('Page Changed 1:', this.pagination);
          this.variable = data.result;
          this.pagination = data.pagination;
          this.loading = false;
          // console.log('Page Changed 2:', this.pagination);
        },
        error: (error) => {
          this.toastr.error('Data failed to load');
          console.error('Error loading data:', error);
          this.loading = false;
        },
      });
  }
}
