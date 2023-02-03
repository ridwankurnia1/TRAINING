import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AutoCompleteModule } from "primeng/autocomplete";
import { RadioButtonModule } from "primeng/radiobutton";
import { TapComponent } from "./tap.component";
import { TapRoutes } from "./tap.routing";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        AutoCompleteModule,
        RadioButtonModule,
        TapRoutes,
        ReactiveFormsModule
    ],
    declarations: [
        TapComponent
    ]
})

export class TapModule {}