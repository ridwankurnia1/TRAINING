import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LearningComponent } from './learning.component';
import { LearnCardComponent } from './shared/renderable/learn-card/learn-card.component';
import { QuestionsComponent } from './shared/renderable/questions/questions.component';
import { VersionComponent } from './shared/renderable/version/version.component';

@NgModule({
  imports: [CommonModule, FormsModule],
  declarations: [
    LearningComponent,
    LearnCardComponent,
    VersionComponent,
    QuestionsComponent,
  ],
})
export class LearningModule {}
