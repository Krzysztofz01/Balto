import { Injectable } from '@angular/core';
import { LocalStorageOptions } from '../models/local-storage-options.model';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  public get(key: string): any {
    const itemSerialized = localStorage.getItem(key);
    if(itemSerialized !== null) {
      const item = JSON.parse(itemSerialized);
      const currentTime = new Date().getTime();
      
      if(!item || (item.hasExpiration && item.expiration <= currentTime)) {
        return null;
      } else {
        return JSON.parse(item.value)
      }
    }
    return null;
  }

  public set(options: LocalStorageOptions): void {
    options.expirationMinutes = options.expirationMinutes || 0;
    const expirationMilisec = (options.expirationMinutes !== 0) ? options.expirationMinutes * 60 * 1000 : 0;

    const item = {
      value: (typeof options.value === 'string') ? options.value : JSON.stringify(options.value),
      expiration: (expirationMilisec !== 0) ? new Date().getTime() + expirationMilisec : null,
      hasExpiration: (expirationMilisec !== 0) ? true : false
    }

    localStorage.setItem(options.key, JSON.stringify(item));
  }

  public unset(key: string): void {
    localStorage.removeItem(key);
  }

  public drop(): void {
    localStorage.clear();
  }
}
