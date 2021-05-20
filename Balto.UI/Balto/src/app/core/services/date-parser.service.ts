import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateParserService {

  constructor() { }

  public daysAgo(date: Date): number {
    const today = Date.now();
    const differenceInTime = today - date.getTime();
    return differenceInTime / (1000 * 3600 * 24);
  }
}
