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
import { CustomErrorHandler, ErrorHandlerService, URLDefiner, URLDefinerService } from '../shared';
import { AppComponent, MainViewComponent } from './index';

const routes: Routes = [
  {
    path: "", component: MainViewComponent,
    children: []
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
    HttpClientModule
  ],
  providers: [
    { provide: URLDefiner, useClass: URLDefinerService },
    { provide: CustomErrorHandler, useClass: ErrorHandlerService },
  ],
  bootstrap: [AppComponent]
})
export class CoreModule { }
