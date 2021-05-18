import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { LogHeader } from '../_model/LogHeader';
import { ChecksheetService } from '../_service/checksheet.service';

@Component({
  selector: 'app-taplist',
  templateUrl: './taplist.component.html',
  styleUrls: ['./taplist.component.css']
})
export class TaplistComponent implements OnInit {
  data: LogHeader[] = [];

  constructor(
    private csservice: ChecksheetService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.csservice.getListLogHeader().subscribe({
      next: (response: LogHeader[]) => {
        this.data = response;
      },
      error: (err) => {
        this.toastr.error(err.error);
      }
    });
  }

}
