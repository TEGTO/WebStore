import { Injectable } from '@angular/core';
import { CustomErrorHandler } from './custom-error-handler';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService extends CustomErrorHandler {

  override handleError(error: any): void {
    console.log(error.messages);
  }
}