import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-version',
  templateUrl: './version.component.html',
  styleUrls: ['./version.component.css'],
  standalone: true,
  imports:[
    CommonModule,
  ]
})
export class VersionComponent implements OnChanges {
  @Input() major = 1;
  @Input() minor = 0;
  changeLog: string[] = [];

  ngOnChanges(changes: SimpleChanges) {
    const log: string[] = [];

    for (const prop in changes) {
      const changedProp = changes[prop];
      const changedValue = JSON.stringify(changedProp.currentValue);

      if (changedProp.isFirstChange()) {
        log.push(`Initial value of ${prop} is ${changedValue}`);
      } else {
        const prevValue = JSON.stringify(changedProp.previousValue);
        log.push(
          `${prop}'s value changed to ${changedValue} from ${prevValue}`
        );
      }
    }

    this.changeLog.push(log.join(', '));
  }

  reset() {
    this.changeLog = [];
  }
}
