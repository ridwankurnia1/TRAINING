import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  CdkDrag,
  CdkDragDrop,
  CdkDragPlaceholder,
  CdkDropList,
  CdkDropListGroup,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-drag-n-drop',
  templateUrl: './drag-n-drop.component.html',
  styleUrls: ['./drag-n-drop.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    CdkDrag,
    CdkDropList,
    CdkDragPlaceholder,
    CdkDropListGroup,
  ],
})
export class DragNDropComponent implements OnInit {
  exListItems: number[];
  exListA: number[];
  exListB: number[];
  exGroup: any[];

  constructor() {}

  ngOnInit() {
    this.exListItems = [1, 2, 3, 4];
    this.exListA = [1, 2, 3, 4];
    this.exListB = [4, 3, 2, 1];
    this.exGroup = [
      {
        title: 'Basket A',
        items: ['apple', 'banana', 'orange', 'grape', 'strawberry'],
      },
      {
        title: 'Basket B',
        items: ['carrot', 'broccoli', 'spinach', 'tomato', 'cucumber'],
      },
      {
        title: 'Basket C',
        items: ['red', 'green', 'blue', 'yellow', 'orange'],
      },
    ];
  }

  onDropped(event: CdkDragDrop<number[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
  }

  get groupConnections(): string[] {
    return this.exGroup?.map((x) => x.title);
  }
}
