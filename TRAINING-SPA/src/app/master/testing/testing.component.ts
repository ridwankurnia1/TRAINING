import { Component, OnInit } from '@angular/core';
import { MasterTableColumn } from 'src/app/shared/models/master-table-column';
import { WarehouseGroup } from 'src/app/_model/WarehouseGroup';
import { WarehouseService } from 'src/app/_service/warehouse.service';

@Component({
  selector: 'app-testing',
  templateUrl: './testing.component.html',
  providers: [WarehouseService],
})
export class TestingComponent implements OnInit {
  warehouseGroupData: WarehouseGroup[] = [];
  columns: MasterTableColumn[];

  isLoading: boolean = false;

  constructor(private warehouse: WarehouseService) {}

  ngOnInit() {
    this.columns = [
      { field: 'code', header: 'Code', width: '15%' },
      { field: 'name', header: 'Name', width: '30%' },
      { field: 'remark', header: 'Remark', width: '30%' },
      { field: 'recordStatus', header: 'Status', width: '16%' },
    ];

    this.getAll();
  }

  getAll() {
    this.isLoading = true;
    this.warehouse.allGroup().subscribe((res) => {
      this.warehouseGroupData = res;
      this.isLoading = false;
    });
  }
}
