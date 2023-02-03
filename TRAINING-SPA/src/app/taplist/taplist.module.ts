import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TaplistComponent } from "./taplist.component";
import { TapListRoutes } from "./taplist.routing";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        TapListRoutes
    ],
    declarations: [
        TaplistComponent
    ]
})

export class TapListModule {}