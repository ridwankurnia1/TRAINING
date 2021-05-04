import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-questionnaire',
  templateUrl: './questionnaire.component.html',
  styleUrls: ['./questionnaire.component.css']
})
export class QuestionnaireComponent implements OnInit {
  checkInForm: FormGroup;
  loading = false;
  filteredEmp: any[] = [];
  constructor(
    private fb: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.createCheckInForm();
  }

  createCheckInForm() {
    this.checkInForm = this.fb.group({
      nik: ['', Validators.required],
      kondisi: ['', Validators.required],
      lokasiKerja: [''],
      keterangan: [{value: '', disabled: true}],
      keluhan: [{value: [], disabled: true}]
    });
  }
  filterSelect(event): void {

  }
  submit(): void {

  }
}
