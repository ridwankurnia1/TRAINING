import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RadioButtonModule } from "primeng/radiobutton";
import { TableModule } from "primeng/table";
import { EmployeeComponent } from "./employee.component";
import { EmployeeRoutes } from "./employee.routing";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RadioButtonModule,
        EmployeeRoutes,
        TableModule,
        ReactiveFormsModule
    ],
    declarations: [
        EmployeeComponent
    ]
})

export class EmployeeModule {}