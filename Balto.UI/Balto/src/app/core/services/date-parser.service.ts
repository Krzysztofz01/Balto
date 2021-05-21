import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateParserService {

  constructor() { }

  public daysAgo(date: Date): number {
    const today = Date.now();
    const differenceInTime = today - date.getTime();
    return Math.round(differenceInTime / (1000 * 3600 * 24));
  }

  public inDays(date: Date): number {
    const today = Date.now();
    const differenceInTime = date.getTime() - today;
    return Math.round(differenceInTime / (1000 * 3600 * 24));
  }
}
