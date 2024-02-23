import { Component, OnInit, ViewChild } from '@angular/core';
import { Learning } from '../_model/Learning';
import { QuestionsComponent } from './shared/renderable/questions/questions.component';
import { VersionComponent } from './shared/renderable/version/version.component';
import { LearnCardComponent } from './shared/renderable/learn-card/learn-card.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-learning',
  templateUrl: './learning.component.html',
  styleUrls: ['./learning.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    LearnCardComponent,
    VersionComponent,
    QuestionsComponent,
  ],
  // encapsulation: ViewEncapsulation.ShadowDom
})
export class LearningComponent implements OnInit {
  // version component variables
  major: number;
  minor: number;

  // questions component variables
  listeningQuestion: boolean = false;
  questionSaved: boolean = false;
  userName: string;

  learnings: Learning[];

  @ViewChild(VersionComponent) versionRef: any;
  @ViewChild(QuestionsComponent) questionRef: any;

  constructor() {}

  ngOnInit() {
    this.learnings = [];
    this.versionOnInit();
    this.questionOnInit();
  }

  versionOnInit(reset = false) {
    if (!reset) {
      this.learnings.push({
        name: 'code-version',
        title: 'Learn Component Interaction with @Input()',
        subtitle: 'input with change detection',
        component: 'version',
        footer: 'version-footer',
      });
    }

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

  versionReset() {
    this.versionRef.reset();
    this.versionOnInit(true);
  }

  questionOnInit(reset = false) {
    if (!reset) {
      this.learnings.push({
        name: 'output-name',
        title: 'Learn Component Interaction with @Output()',
        subtitle: 'sends event from child to parent using EventEmitter',
        component: 'questions',
        footer: 'questions-footer',
      });
    }
    this.userName = '';
  }

  questionOnInput(value: string) {
    this.questionSaved = !this.listeningQuestion;
    this.userName = value;
  }

  questionOnSave(result: string) {
    this.questionSaved = true;
    this.userName = result;
  }

  questionReset() {
    this.questionRef.reset();
    this.listeningQuestion = false;
    this.questionSaved = false;
    this.questionOnInit(true);
  }
}
