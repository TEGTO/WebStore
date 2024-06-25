import { Component, Input } from '@angular/core';

@Component({
  selector: 'store-product-rating',
  templateUrl: './product-rating.component.html',
  styleUrl: './product-rating.component.scss'
})
export class ProductRatingComponent {
  @Input({ required: true })
  rating: number = 0;

  readonly maxRating: number = 5;
  readonly starWidth: number = 22;

  get fullStars(): number[] {
    return Array(Math.floor(this.rating)).fill(0);
  }
  get emptyStars(): number[] {
    if (this.hasHalfStar) {
      return Array(this.maxRating - Math.floor(this.rating + 1)).fill(0);
    }
    return Array(this.maxRating - Math.floor(this.rating)).fill(0);
  }
  get hasHalfStar(): boolean {
    return this.rating % 1 !== 0;
  }
  get widthOfFilledStar(): number {
    let fullified = this.rating % 1;
    return this.starWidth * fullified;
  }
}
