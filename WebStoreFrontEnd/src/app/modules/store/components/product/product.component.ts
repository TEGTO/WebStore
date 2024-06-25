import { Component, Input } from '@angular/core';
import { ProductDataDto } from '../../../shared';

@Component({
  selector: 'store-product',
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})
export class ProductComponent {
  @Input({ required: true })
  product!: ProductDataDto;
}
