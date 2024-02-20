import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { Lebaran } from 'src/app/_model/Lebaran';
import { LebaranQuiz } from 'src/app/_model/LebaranQuiz';
import { ChecksheetService } from 'src/app/_service/checksheet.service';
import { UIService } from 'src/app/_service/ui.service';
import { environment } from 'src/environments/environment';
import Quiz from '../../../assets/lebaran2021.json';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RadioButtonModule } from 'primeng/radiobutton';
import { TableModule } from 'primeng/table';
import { EmployeeRoutes } from 'src/app/master/employee/employee.routing';

@Component({
  selector: 'app-clinic',
  templateUrl: './clinic.component.html',
  styleUrls: ['./clinic.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RadioButtonModule,
    EmployeeRoutes,
    TableModule,
    ReactiveFormsModule
  ]
})
export class ClinicComponent implements OnInit {
  questions: LebaranQuiz[] = [];
  employeeData: Lebaran = {};
  mustCheck = false;
  displayAnswer = false;
  loading = false;
  nikorid = '';
  readOnly = true;
  bsConfig: Partial<BsDatepickerConfig>;
  defaultImages = environment.imgEmpUrl + 'NoImage.png';
  constructor(
    private ui: UIService,
    private csservice: ChecksheetService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.questions = Quiz.question;
    this.bsConfig = {
      dateInputFormat: 'DD-MM-YYYY'
    };
  }

  getEmployee(): void {
    this.loading = true;
    this.csservice.getEmployee(this.nikorid).subscribe({
      next: (resp: Lebaran) => {
        this.loading = false;
        this.employeeData = resp;
        this.nikorid = '';
        if (resp) {
          if (!this.ui.isNullOrEmpty(resp.fillDate)) {
            this.displayAnswer = true;
            this.setFormValue(resp);
            this.employeeData.fillDate = moment(resp.fillDate).toDate();
            this.employeeData.attendDate = moment(resp.attendDate).toDate();
            this.employeeData.healthCheckDate = new Date();
          } else {
            this.displayAnswer = false;
            this.toastr.error('Karyawan belum mengisi kuestioner');
          }
        } else {
          this.toastr.error('Data karyawan tidak ditemukan');
        }
      }, error: () => {
        this.loading = false;
      }
    });
  }
  setFormValue(data: Lebaran): void {
    this.questions[0].nilai = data.question01.toString();
    this.questions[1].nilai = data.question02.toString();
    this.questions[2].nilai = data.question03.toString();
    this.questions[3].nilai = data.question04.toString();
    this.questions[4].nilai = data.question05.toString();
    this.questions[5].nilai = data.question06.toString();
    this.questions[6].nilai = data.question07.toString();
    this.questions[7].nilai = data.question08.toString();
    this.questions[8].nilai = data.question09.toString();
    this.questions[9].nilai = data.question10.toString();
    this.questions[10].nilai = data.question11.toString();
    this.questions[11].nilai = data.question12.toString();
    this.questions[12].nilai = data.question13.toString();
    this.questions[13].nilai = data.question14.toString();
    this.questions[14].nilai = data.question15.toString();
    this.questions[15].nilai = data.question16.toString();
    this.questions[16].nilai = data.question17.toString();
    this.questions[17].nilai = data.question18.toString();
    this.questions[18].nilai = data.question19.toString();
    this.questions[19].nilai = data.question20.toString();
    this.questions[20].nilai = data.question21.toString();
    this.questions[21].nilai = data.question22.toString();
    this.questions[22].nilai = data.question23.toString();
    this.questions[23].nilai = data.question24.toString();
    this.questions[24].nilai = data.question25.toString();
    const nilai = this.questions.reduce((a, b) => a + Number(b.nilai), 0);
    if (nilai >= 11 || nilai === 0) {
      this.mustCheck = true;
    } else {
      this.mustCheck = false;
    }
  }

  save(): void {
    this.loading = true;
    this.csservice.updateClinic(this.employeeData).subscribe({
      next: () => {
        this.loading = false;
        this.toastr.success('Data berhasil di simpan');
      }, error: (err) => {
        this.loading = false;
        this.toastr.error(err.error);
        
      }
    });
  }
  setDefaultImage(): void {
    this.employeeData.photo = this.defaultImages;
  }
}
