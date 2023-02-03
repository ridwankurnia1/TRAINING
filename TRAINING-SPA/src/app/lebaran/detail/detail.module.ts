import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TableModule } from "primeng/table";
import { DetailComponent } from "./detail.component";
import { DetailRoutes } from "./detail.routing";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        TableModule,
        DetailRoutes
    ],
    declarations: [
        DetailComponent
    ]
})

export class DetailModule {}