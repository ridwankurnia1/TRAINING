import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MyCardComponent } from './example/my-card/my-card.component';

@Component({
  selector: 'app-content-projections',
  templateUrl: './content-projections.component.html',
  styleUrls: ['./content-projections.component.css'],
  standalone: true,
  imports: [CommonModule, MyCardComponent],
})
export class ContentProjectionsComponent implements OnInit {
  ngContentText = '<ng-content></ng-content>';

  constructor() {}

  ngOnInit() {}
}
