import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-my-card',
  templateUrl: './my-card.component.html',
  styleUrls: ['./my-card.component.css'],
  standalone: true,
  imports: [CommonModule],
})
export class MyCardComponent implements OnInit {
  constructor() {}

  ngOnInit() {}
}
