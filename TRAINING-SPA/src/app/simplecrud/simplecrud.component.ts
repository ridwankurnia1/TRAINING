import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SimplecrudService } from '../_service/simplecrud.service';
import { SimpleCrudItem, CreateSimpleCrudItem, UpdateSimpleCrudItem } from '../_model/Simplecrud';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-simplecrud',
  templateUrl: './simplecrud.component.html',
  styleUrls: ['./simplecrud.component.css'],
  standalone:true,
  imports: [ReactiveFormsModule, CommonModule]
})
export class SimplecrudComponent implements OnInit {
  items: SimpleCrudItem[] = [];
  itemForm: FormGroup;
  isEditing = false;
  editingId: number | null = null;
  loading = false;

  constructor(
    private simplecrudService: SimplecrudService,
    private fb: FormBuilder
  ) {
    this.itemForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      description: ['', [Validators.required, Validators.minLength(5)]],
      isActive: [true]
    });
  }

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems(): void {
    this.loading = true;
    this.simplecrudService.getAll().subscribe({
      next: (data) => {
        this.items = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading items:', error);
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.itemForm.valid) {
      if (this.isEditing) {
        this.updateItem();
      } else {
        this.createItem();
      }
    }
  }

  createItem(): void {
    const newItem: CreateSimpleCrudItem = this.itemForm.value;
    this.simplecrudService.create(newItem).subscribe({
      next: (item) => {
        this.items.push(item);
        this.resetForm();
      },
      error: (error) => {
        console.error('Error creating item:', error);
      }
    });
  }

  updateItem(): void {
    if (this.editingId) {
      const updatedItem: UpdateSimpleCrudItem = {
        id: this.editingId,
        ...this.itemForm.value
      };
      
      this.simplecrudService.update(updatedItem).subscribe({
        next: (item) => {
          const index = this.items.findIndex(i => i.id === item.id);
          if (index !== -1) {
            this.items[index] = item;
          }
          this.resetForm();
        },
        error: (error) => {
          console.error('Error updating item:', error);
        }
      });
    }
  }

  editItem(item: SimpleCrudItem): void {
    this.isEditing = true;
    this.editingId = item.id;
    this.itemForm.patchValue({
      name: item.name,
      description: item.description,
      isActive: item.isActive
    });
  }

  deleteItem(id: number): void {
    if (confirm('Are you sure you want to delete this item?')) {
      this.simplecrudService.delete(id).subscribe({
        next: (success) => {
          if (success) {
            this.items = this.items.filter(item => item.id !== id);
          }
        },
        error: (error) => {
          console.error('Error deleting item:', error);
        }
      });
    }
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

