import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RadioButtonModule } from "primeng/radiobutton";
import { ClinicComponent } from "./clinic.component";
import { ClinicRoutes } from "./cllinic.routing";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RadioButtonModule,
        ClinicRoutes
    ],
    declarations: [
        ClinicComponent
    ]
})

export class ClinicModule {}