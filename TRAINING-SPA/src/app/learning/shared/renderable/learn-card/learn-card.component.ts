import {
  Component,
  ContentChildren,
  Input,
  OnInit,
  QueryList,
} from '@angular/core';
import { Learning } from 'src/app/_model/Learning';
import { AppTemplateDirective } from '../../directive/appTemplate.directive';

@Component({
  selector: 'app-learn-card',
  templateUrl: './learn-card.component.html',
})
export class LearnCardComponent implements OnInit {
  @Input() learning: Learning;
  @ContentChildren(AppTemplateDirective) contents!: QueryList<AppTemplateDirective>

  contentSelector: string;
  footerSelector: string;

  ngOnInit() {
    this.contentSelector = `#${this.learning.name}`;

    if (this.learning.footer) {
      this.footerSelector = `#${this.learning.footer}`;
    }

    console.info(this.contents);
  }
}
