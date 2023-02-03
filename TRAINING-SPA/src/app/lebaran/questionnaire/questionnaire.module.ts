import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { AutoCompleteModule } from "primeng/autocomplete";
import { RadioButtonModule } from "primeng/radiobutton";
import { QuestionnaireComponent } from "./questionnaire.component";
import { QuestionnnaireRoutes } from "./questionnaire.routing";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        AutoCompleteModule,
        RadioButtonModule,
        QuestionnnaireRoutes
    ],
    declarations: [
        QuestionnaireComponent
    ]
})

export class QuestionnaireModule {}