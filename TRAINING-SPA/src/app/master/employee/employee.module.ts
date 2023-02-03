import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RadioButtonModule } from "primeng/radiobutton";
import { EmployeeComponent } from "./employee.component";
import { EmployeeRoutes } from "./employee.routing";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RadioButtonModule,
        EmployeeRoutes
    ],
    declarations: [
        EmployeeComponent
    ]
})

export class EmployeeModule {}