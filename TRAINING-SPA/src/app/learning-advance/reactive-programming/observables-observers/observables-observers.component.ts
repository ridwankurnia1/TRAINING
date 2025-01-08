import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-observables-observers',
  templateUrl: './observables-observers.component.html',
  styleUrls: ['./observables-observers.component.css'],
  standalone: true,
  imports: [CommonModule],
})
export class ObservablesObserversComponent implements OnInit {
  numbers: number[];

  constructor() {}

  ngOnInit() {
    this.basicExample();
  }

  basicExample() {
    this.numbers = [];

    const observable = new Observable<number>((subscriber) => {
      subscriber.next(1);
      subscriber.next(2);
      subscriber.next(3);
      subscriber.complete();
    });

    observable.subscribe((num) => {
      this.numbers.push(num);
    });
  }
}
