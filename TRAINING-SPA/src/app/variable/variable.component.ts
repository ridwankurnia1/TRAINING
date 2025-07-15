import { Component, OnInit } from '@angular/core';
import { Variable } from '../_model/Variable';
import { VariableService } from '../_service/variable.service';
import { TableModule } from "primeng/table";
import { ConfirmDialogModule } from "primeng/confirmdialog";
// import { Component, OnInit, TemplateRef } from '@angular/core';



@Component({
    templateUrl: './variable.component.html',
    standalone:true,
    imports:[
        TableModule,
        ConfirmDialogModule,
    ]
})
@Component({
  selector: 'app-variable',
  templateUrl: './variable.component.html',
  styleUrls: ['./variable.component.css'],
  standalone:true,
  imports: [TableModule, ConfirmDialogModule]
})
export class VariableComponent implements OnInit {
rowsPerPageOptions: any;
pageSize: any;
pageChanged($event: any) {
throw new Error('Method not implemented.');
}
  variables: Variable[] = [];
defectDetailList: any;
pagination: any;
loading: any;

  constructor(private variableService: VariableService) {}

  ngOnInit(): void {
    this.loadVariable();
  }

  loadVariable() {
    this.variableService.getVariables().subscribe(data => this.variables = data)
  }

  deleteVariable(id: number) {
    this.variableService.deleteVariable(id).subscribe(()=> this.loadVariable)
  }

  updateVarible(variable: Variable) {
    this.variableService.updateVariable(variable).subscribe(()=> this.loadVariable)
  }
} 

