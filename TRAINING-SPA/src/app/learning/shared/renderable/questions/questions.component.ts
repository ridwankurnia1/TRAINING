import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  standalone: true,
  imports: [FormsModule],
})
export class QuestionsComponent implements OnInit {
  // this variable is used as two-way binding
  userName: string;

  // receive input from parent
  @Input() listen: boolean = false;
  @ViewChild('mainInput') inputRef: ElementRef<any>;

  // this is where the child can pass an output to parent
  // using @Output decorator as event output type of EventEmitter
  // <string> is the expected value
  @Output('onSave') exampleEvent = new EventEmitter<string>();
  // @Output('onReset') resetEvent = new EventEmitter();
  @Output('onInput') inputEvent = new EventEmitter<string>();

  ngOnInit() {}

  // called in on click binding from button
  save() {
    // raise an event using emit from event emitter with the value
    // the user input from userName variable binding
    this.exampleEvent.emit(this.userName);
  }

  onInput(event: Event) {
    const value = (event.target as HTMLInputElement).value;

    if (this.listen) {
      this.inputEvent.emit(value);
    }
  }

  reset() {
    this.inputRef.nativeElement.value = '';
  }
}
