import { Component, ContentChildren, OnInit, QueryList, ViewEncapsulation } from '@angular/core';
import { Learning } from '../_model/Learning';
import { AppTemplateDirective } from './shared/directive/appTemplate.directive';

@Component({
  selector: 'app-learning',
  templateUrl: './learning.component.html',
  styleUrls: ['./learning.component.css'],
  // encapsulation: ViewEncapsulation.ShadowDom
})
export class LearningComponent implements OnInit {
  major: number;
  minor: number;

  @ContentChildren(AppTemplateDirective) contents!: QueryList<AppTemplateDirective>

  learnings: Learning[];

  constructor() {}

  ngOnInit() {
    this.learnings = [];
    this.versionOnInit();

    console.info(this.contents);
  }

  versionOnInit() {
    this.learnings.push({
      name: 'code-version',
      title: 'Learn Component Interaction with @Input()',
      subtitle: 'input with change detection',
      footer: 'version-footer'
    });

    this.major = 1;
    this.minor = 0;
  }

  versionNewMajor() {
    this.major++;
    this.minor = 0;
  }

  versionNewMinor() {
    this.minor++;
  }

  versionReset(element?: any) {
    console.log(element);
    this.versionOnInit();
    element.reset();
  }
}
