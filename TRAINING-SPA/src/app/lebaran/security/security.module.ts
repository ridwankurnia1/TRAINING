import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AutoCompleteModule } from "primeng/autocomplete";
import { RadioButtonModule } from "primeng/radiobutton";
import { DefectDetailComponent } from "src/app/master/defect-detail/defect-detail.component";
import { SecurityComponent } from "./security.component";
import { SecurityRoutes } from "./security.routing";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        AutoCompleteModule,
        RadioButtonModule,
        SecurityRoutes,
        ReactiveFormsModule
    ],
    declarations: [
        SecurityComponent
    ]
})

export class SecurityModule {}