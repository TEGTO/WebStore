import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationModule } from '../authentication/authentication.module';
import { CustomErrorHandler, ErrorHandlerService, RedirectorContollerService, RedirectorService, URLDefiner, URLDefinerService } from '../shared';
import { AssortementComponent, CartComponent } from '../store';
import { StoreModule } from '../store/store.module';
import { AppComponent, MainViewComponent } from './index';

const routes: Routes = [
  {
    path: "", component: MainViewComponent,
    children: [
      { path: "", component: AssortementComponent },
      { path: "cart", component: CartComponent }
    ]
  }
];

@NgModule({
  declarations: [
    AppComponent,
    MainViewComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatDialogModule,
    AuthenticationModule,
    HttpClientModule,
    StoreModule
  ],
  providers: [
    { provide: URLDefiner, useClass: URLDefinerService },
    { provide: CustomErrorHandler, useClass: ErrorHandlerService },
    { provide: RedirectorService, useClass: RedirectorContollerService },
  ],
  bootstrap: [AppComponent]
})
export class CoreModule { }
