import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AutoCompleteModule } from "primeng/autocomplete";
import { RadioButtonModule } from "primeng/radiobutton";
import { RegisterComponent } from "./register.component";
import { RegisterRoutes } from "./register.routing";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        AutoCompleteModule,
        RadioButtonModule,
        RegisterRoutes,
        ReactiveFormsModule
    ],
    declarations: [
        RegisterComponent
    ]
})

export class RegisterModule {}