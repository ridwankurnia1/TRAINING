import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LearningAdvanceRoutes } from './learning-advance.routing';
import { LearningAdvanceComponent } from './learning-advance.component';

@NgModule({
  declarations: [LearningAdvanceComponent],
  imports: [CommonModule, LearningAdvanceRoutes],
})
export class LearningAdvanceModule {}
