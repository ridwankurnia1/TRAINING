import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MasterTableComponent } from './master-table.component';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';

@NgModule({
  imports: [CommonModule, TableModule, ButtonModule, InputTextModule],
  declarations: [MasterTableComponent],
  exports: [MasterTableComponent],
})
export class MasterTableModule {}
