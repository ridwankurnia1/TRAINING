import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { Learning } from 'src/app/_model/Learning';

@Component({
  selector: 'app-learn-card',
  templateUrl: './learn-card.component.html',
  standalone: true,
  imports: [CommonModule],
})
export class LearnCardComponent implements OnInit {
  @Input() learning: Learning;

  contentSelector: string;
  footerSelector: string;

  ngOnInit() {
    this.contentSelector = `#${this.learning.name}`;

    if (this.learning.footer) {
      this.footerSelector = `#${this.learning.footer}`;
    }
  }
}
