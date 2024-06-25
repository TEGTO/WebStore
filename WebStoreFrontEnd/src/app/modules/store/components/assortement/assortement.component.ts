import { Component } from '@angular/core';

@Component({
  selector: 'app-assortement',
  templateUrl: './assortement.component.html',
  styleUrl: './assortement.component.scss'
})
export class AssortementComponent {
  numbers: Array<number> = [];

  constructor() {
    this.numbers = Array(10).fill(1);
  }
}
