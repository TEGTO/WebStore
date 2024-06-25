import { Component } from '@angular/core';
import { ProductDataDto } from '../../../shared';

@Component({
  selector: 'app-assortement',
  templateUrl: './assortement.component.html',
  styleUrl: './assortement.component.scss'
})
export class AssortementComponent {
  products: ProductDataDto[] = [];
  numbers: Array<number> = [];

  constructor() {
    this.numbers = Array(10).fill(1);
    this.products.push({
      id: 1,
      name: "Wireless Controller Carbon Black (XOA-0005, QAT-00001)",
      price: 200,
      avgRating: 4.5,
      imgUrl: "https://content1.rozetka.com.ua/goods/images/big/261296642.jpg"
    })
  }
}
