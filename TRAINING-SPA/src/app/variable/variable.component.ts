// variable.component.ts
import { Component, OnInit } from '@angular/core';
import { VariableService } from '../_service/variable.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TableModule } from "primeng/table";
import { ConfirmDialogModule } from "primeng/confirmdialog";
import { VariableItem, CreateVariableItem, UpdateVariableItem } from '../_model/Variable';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-variable',
  templateUrl: './variable.component.html',
  styleUrls: ['./variable.component.css'],
  standalone:true,
  imports: [
    TableModule,
    ConfirmDialogModule,
    CommonModule,
    ReactiveFormsModule
  ]
})
export class VariableComponent implements OnInit {
  variables:VariableItem[] = []
  rowsPerPageOptions: any;
  pageSize: any;
  itemForm: FormGroup;
  loading = false;
  isEditing = false;
  editingId: number | null = null

  pageChanged($event: any) {
  throw new Error('Method not implemented.');
}
constructor(
  private variableService: VariableService,
  private fb:FormBuilder
) {
  this.itemForm = this.fb.group({
    user: ['', [Validators.required, Validators.minLength(2)]],
    name: ['', [Validators.required, Validators.minLength(5)]],
    code: ['', [Validators.required, Validators.minLength(5)]],
    value: ['', [Validators.required, Validators.minLength(5)]],
  });
}

ngOnInit(): void {
  this.loadVariable();
}

loadVariable():void {
  this.loading = true;
  this.variableService.getVariables().subscribe({
    next:(data) => {
      this.variables = data
      this.loading = false
    },
    error: (error) => {
      console.error('Error loading data:', error)
      this.loading = false;
    }
  })
}

createVariable(): void{
  const newItem: CreateVariableItem = this.itemForm.value;
  this.variableService.create(newItem).subscribe({
    next: (item) => {
      this.variables.push(item);
      this.resetForm()
    }
  })
}
onSubmit(): void {
  if (this.itemForm.valid) {
    if (this.isEditing) {
      this.updateVariable();
    } else {
      this.createVariable();
    }
  }
}

  deleteVariable(id: number): void {
  if (confirm('Are you sure you want to delete this item?')) {
    this.variableService.delete(id).subscribe({
      next: () => {
        this.variables = this.variables.filter(variable => variable.variableId !== id);
      },
      error: (error) => {
        console.error('Error deleting item:', error);
      }
    });
  }
}

  updateVariable(): void {
    if (this.editingId) {
      const updatedItem: UpdateVariableItem = {
        variableId: this.editingId,
        ...this.itemForm.value
      };
      
      this.variableService.update(updatedItem).subscribe({
        next: (item) => {
          const index = this.variables.findIndex(i => i.variableId === item.variableId);
          if (index !== -1) {
            this.variables[index] = item;
          }
          this.resetForm();
        },
        error: (error) => {
          console.error('Error updating item:', error);
        }
      });
    }
  }

  editVariable(item: VariableItem): void {
    this.isEditing = true;
    this.editingId = item.variableId;
    this.itemForm.patchValue({
      user: item.user,
      name: item.name,
      code: item.code,
      value: item.value
    });
  }
  

  cancelEdit(): void {
  this.resetForm();
  }

  resetForm(): void {
    this.itemForm.reset();
    this.itemForm.patchValue({ isActive: true });
    this.isEditing = false;
    this.editingId = null;
  }
}
