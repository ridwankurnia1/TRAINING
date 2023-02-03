import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { TaplistComponent } from "./taplist.component";

const routes: Routes = [
    {
        path: '',
        component: TaplistComponent
    }
]

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule
    ]
})

export class TapListRoutes {}