import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { EmployeeId } from 'src/app/_model/EmployeeId';
import { Lebaran } from 'src/app/_model/Lebaran';
import { ChecksheetService } from 'src/app/_service/checksheet.service';
import { UIService } from 'src/app/_service/ui.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-security',
  templateUrl: './security.component.html',
  styleUrls: ['./security.component.css']
})
export class SecurityComponent implements OnInit {
  nikorid = '';
  employee: Lebaran = {};
  listEmployee: Lebaran[] = [];
  display = 3;
  defaultImages = environment.imgEmpUrl + 'NoImage.png';
  constructor(
    private ui: UIService,
    private csservice: ChecksheetService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
  }

  getEmployee(): void {
    if (this.ui.isNullOrEmpty(this.nikorid)) {
      return;
    }

    const param: EmployeeId = {};
    if (this.nikorid.length === 10) {
      param.rfid = this.nikorid;
    } else {
      param.nik = this.nikorid;
    }

    this.csservice.updateSecurity(param).subscribe({
      next: (resp: Lebaran) => {
        if (this.listEmployee.length >= this.display) {
          this.listEmployee.splice((this.display - 1), 1);
        }
        if (this.ui.isNullOrEmpty(resp.fillDate)) {
          resp.statusDescription = 'Belum Mengisi';
          resp.status = 2;
        } else {
          if (resp.status === 1) {
            resp.statusDescription = 'Check Kesehatan Sebelum Mulai Bekerja';
          } else {
            resp.statusDescription = 'Sehat dan Siap Bekerja';
          }
        }
        this.listEmployee.unshift(resp);
        this.nikorid = '';
      }
    });
  }
  setDefaultImage(item: Lebaran): void {
    item.photo = this.defaultImages;
  }
}
