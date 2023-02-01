import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestingComponent } from './testing.component';
import { MasterTableModule } from '../../shared/renderable/master-table/master-table.module';

@NgModule({
  declarations: [TestingComponent],
  imports: [CommonModule, MasterTableModule],
})
export class TestingModule {}
