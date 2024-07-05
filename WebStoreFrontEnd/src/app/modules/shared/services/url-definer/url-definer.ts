import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export abstract class URLDefiner {
  abstract combineWithAuthApiUrl(subpath: string): string;
  abstract combineWithWebStoreApiUrl(subpath: string): string;
}
