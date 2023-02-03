import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AutoCompleteModule } from "primeng/autocomplete";
import { RadioButtonModule } from "primeng/radiobutton";
import { SummaryComponent } from "./summary.component";
import { SummaryRoutes } from "./summary.routing";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        AutoCompleteModule,
        RadioButtonModule,
        SummaryRoutes,
        ReactiveFormsModule
    ],
    declarations: [
        SummaryComponent
    ]
})

export class SummaryModule {}