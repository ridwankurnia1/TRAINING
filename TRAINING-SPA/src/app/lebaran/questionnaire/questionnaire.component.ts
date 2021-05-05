import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { Employee } from 'src/app/_model/Employee';
import { Lebaran } from 'src/app/_model/Lebaran';
import { LebaranQuiz } from 'src/app/_model/LebaranQuiz';
import { PaginatedResult } from 'src/app/_model/Pagination';
import { ChecksheetService } from 'src/app/_service/checksheet.service';
import { UIService } from 'src/app/_service/ui.service';
import * as Quiz from '../../../assets/lebaran2021.json';

@Component({
  selector: 'app-questionnaire',
  templateUrl: './questionnaire.component.html',
  styleUrls: ['./questionnaire.component.css']
})
export class QuestionnaireComponent implements OnInit {
  questions: LebaranQuiz[] = [];
  employee: Lebaran = {};
  answer: Lebaran = {};
  loading = false;
  readOnly = false;
  filteredEmp: any[] = [];
  firstView = true;
  secondView = false;
  mustCheck = false;
  constructor(
    private ui: UIService,
    private csservice: ChecksheetService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.questions = Quiz.question;
  }

  filterSelect(event): void {
    this.csservice.getEmployee(event.employeeId).subscribe({
      next: (resp: Lebaran) => {
        if (resp.fillDate) {
          this.readOnly = true;
          this.setFormValue(resp);
          this.toastr.error('Anda sudah mengisi kuesioner ini tanggal ' + moment(resp.fillDate).format('DD-MM-YYYY hh:mm'));
        }
      }
    });
  }
  setFormValue(data: Lebaran): void {
    this.employee = data;
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
    const nilai = this.questions.reduce((a, b) => a + Number(b.nilai), 0);
    if (nilai >= 11 || nilai === 0) {
      this.mustCheck = true;
    } else {
      this.mustCheck = false;
    }
  }
  filterUser(event): void {
    const prm = {
      filter: event.query
    };
    this.csservice.getEmployees(1, 10, prm)
      .subscribe(
        (data: PaginatedResult<Lebaran[]>) => {
          this.filteredEmp = data.result;
        },
        error => {
          this.toastr.error(error.error);
        });
  }
  radioClicked(item: LebaranQuiz): void {
    item.invalid = false;
  }
  submit(): void {
    if (this.ui.isNullOrEmpty(this.employee)) {
      this.toastr.error('Harap isi NIK');
      return;
    }
    let errorCount = 0;
    this.questions.forEach(x => {
      if (this.ui.isNullOrEmpty(x.nilai)) {
        x.invalid = true;
        errorCount++;
      }
    });

    if (errorCount > 0) {
      this.toastr.error('Harap isi semua pertanyaan');
      return;
    }

    this.loading = true;
    this.answer.employeeId = this.employee.employeeId;
    this.answer.employeeName = this.employee.employeeName;
    this.answer.department = this.employee.department;
    this.answer.question01 = Number(this.questions[0].nilai);
    this.answer.question02 = Number(this.questions[1].nilai);
    this.answer.question03 = Number(this.questions[2].nilai);
    this.answer.question04 = Number(this.questions[3].nilai);
    this.answer.question05 = Number(this.questions[4].nilai);
    this.answer.question06 = Number(this.questions[5].nilai);
    this.answer.question07 = Number(this.questions[6].nilai);
    this.answer.question08 = Number(this.questions[7].nilai);
    this.answer.question09 = Number(this.questions[8].nilai);
    this.answer.question10 = Number(this.questions[9].nilai);
    this.answer.question11 = Number(this.questions[10].nilai);
    this.answer.question12 = Number(this.questions[11].nilai);
    this.answer.question13 = Number(this.questions[12].nilai);
    this.answer.question14 = Number(this.questions[13].nilai);
    this.answer.question15 = Number(this.questions[14].nilai);
    this.answer.question16 = Number(this.questions[15].nilai);
    this.answer.question17 = Number(this.questions[16].nilai);
    this.answer.question18 = Number(this.questions[17].nilai);
    this.answer.question19 = Number(this.questions[18].nilai);
    this.answer.question20 = Number(this.questions[19].nilai);
    this.answer.question21 = Number(this.questions[20].nilai);
    this.answer.question22 = Number(this.questions[21].nilai);
    this.answer.question23 = Number(this.questions[22].nilai);
    this.answer.question24 = 0;
    this.answer.question25 = 0;
    this.answer.question26 = 0;
    this.answer.question27 = 0;
    this.answer.question28 = 0;
    this.answer.question29 = 0;
    this.answer.question30 = 0;

    this.csservice.addQuistioner(this.answer).subscribe({
      next: () => {
        const nilai = this.questions.reduce((a, b) => a + Number(b.nilai), 0);
        if (nilai >= 11 || nilai === 0) {
          this.mustCheck = true;
        } else {
          this.mustCheck = false;
        }
        this.loading = false;
        this.toggleView();
      }, error: () => {
        this.loading = false;
      }
    });
  }

  toggleView() {
    this.firstView = !this.firstView;
    this.secondView = !this.secondView;
  }
}
