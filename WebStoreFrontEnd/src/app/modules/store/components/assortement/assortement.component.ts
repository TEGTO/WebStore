import { Component, OnInit } from '@angular/core';
import { ProductService } from '../..';
import { AuthenticationService, UserAuthData } from '../../../authentication';
import { ProductDataDto } from '../../../shared';

@Component({
  selector: 'app-assortement',
  templateUrl: './assortement.component.html',
  styleUrl: './assortement.component.scss'
})
export class AssortementComponent implements OnInit {
  products: ProductDataDto[] = [];
  userAuthData!: UserAuthData;

  constructor(private authService: AuthenticationService, private productService: ProductService) {
  }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(products => {
      this.products = products;
    });
    this.authService.getAuthUserData().subscribe(userData => {
      this.userAuthData = userData;
    });
  }
}
