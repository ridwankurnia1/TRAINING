import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// import { NgxSpinnerModule } from 'ngx-spinner';

import { PalletTypeComponent } from './pallet-type.component';
import { DropdownModule } from 'primeng/dropdown';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { InputNumberModule } from 'primeng/inputnumber';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
// import { ConfirmationService } from 'primeng/api';

@NgModule({
  imports: [
    CommonModule,
    CollapseModule,
    TableModule,
    BsDatepickerModule,
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    DropdownModule,
    RadioButtonModule,
    CheckboxModule,
    ButtonModule,
    CalendarModule,
    InputNumberModule,
    ConfirmDialogModule,
    ToastModule,
  ],
  declarations: [PalletTypeComponent],
})
export class PalletTypeModule {}
