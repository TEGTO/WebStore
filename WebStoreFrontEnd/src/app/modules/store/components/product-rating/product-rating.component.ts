import { Component, Input } from '@angular/core';

@Component({
  selector: 'store-product-rating',
  templateUrl: './product-rating.component.html',
  styleUrl: './product-rating.component.scss'
})
export class ProductRatingComponent {
  @Input({ required: true })
  rating: number = 0;
}
