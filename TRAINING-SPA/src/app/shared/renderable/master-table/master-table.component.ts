import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
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

  // toggle related
  @Input('update') enableUpdate: boolean = false;
  @Input('delete') enableDelete: boolean = false;
  @Input('sortable') enableSort: boolean = false;

  // pagination related
  itemsPerPage: any;
  pagination: Pagination = {};
  @Input('paginated') paginator: boolean = false;
  @Input('rows') defaultTableRow: number = 10;
  // rowsPerpageDropdown : array of number

  // event related
  //  COMMONS //
  @Output('onChange') tableChangedEvent = new EventEmitter<any>();
  @Output('onReset') resetTableEvent = new EventEmitter<ElementRef<any>>();

  //  ACTIONS //
  @Output('onCreate') createDataEvent = new EventEmitter();
  @Output('onUpdate') updateDataEvent = new EventEmitter<any>();
  @Output('onDelete') deleteDataEvent = new EventEmitter<any>();
  @Output('onExport') exportDataEvent = new EventEmitter();

  // class variables
  globalSearch: string;

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

  onUpdate(obj: any) {
    this.updateDataEvent.emit(obj);
  }

  onDelete(obj: any) {
    this.deleteDataEvent.emit(obj);
  }

  onExport() {
    this.exportDataEvent.emit();
  }

  onReset(table: ElementRef<any>) {
    this.globalSearch = '';
    this.resetTableEvent.emit(table);
  }
}
