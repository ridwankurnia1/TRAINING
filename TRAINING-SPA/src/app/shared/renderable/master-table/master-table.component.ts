import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Pagination } from 'src/app/_model/Pagination';
import { MasterTableColumn } from '../../models/master-table-column';

@Component({
  selector: 'app-master-table',
  templateUrl: './master-table.component.html',
  styleUrls: ['./master-table.component.css'],
})
// based on dynamic table at primeng docs (https://primeng.org/table/dynamic)
export class MasterTableComponent implements OnInit {
  // inputs
  // data related
  @Input() value: any[] = [];
  @Input() columns: MasterTableColumn[] = [];
  @Input('summary') pageReportTemplate: string;

  // state related
  @Input('loading') isLoading: boolean = false;
  @Input('exporting') isExporting: boolean = false;

  // pagination related
  itemsPerPage: any;
  pagination: Pagination = {};
  @Input('paginated') paginator: boolean = false;
  @Input('rows') defaultTableRow: number = 10;
  // rowsPerpageDropdown : array of number

  // event related
  //  COMMONS //
  @Output('onChange') tableChangedEvent = new EventEmitter<any>();
  @Output('onReset') resetTableEvent = new EventEmitter();
  // onFiltering : Event

  //  ACTIONS //
  @Output('onCreate') createDataEvent = new EventEmitter();
  @Output('onUpdate') updateDataEvent = new EventEmitter<string>();
  @Output('onDelete') deleteDataEvent = new EventEmitter<string>();
  @Output('onExport') exportDataEvent = new EventEmitter();

  constructor() {}

  ngOnInit() {
    if (this.paginator) {
      this.setupPagination();
    }
  }

  private setupPagination() {
    this.pageReportTemplate =
      'Showing {first} to {last} of {totalRecords} entries';
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 1,
    };
  }

  onChange(event: any) {
    this.tableChangedEvent.emit(event);
  }

  onCreate() {
    this.createDataEvent.emit();
  }

  onUpdate(id: string) {
    this.updateDataEvent.emit(id);
  }

  onDelete(id: string) {
    this.deleteDataEvent.emit(id);
  }

  onExport() {
    this.exportDataEvent.emit();
  }

  onReset(table: any){
    table.reset();
    this.resetTableEvent.emit();
  }
}
