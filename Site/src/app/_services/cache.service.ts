import { Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';

import { Statistics } from '../_models/statistics';

@Injectable({
  providedIn: 'root'
})
export class CacheService {

  dateFormat = 'MMddyyyy';

  constructor(private datePipe: DatePipe) { }

  getCachedResult(startDate: Date, endDate: Date): Statistics[] {
    let key = this.createKey(startDate, endDate);

    let statistics = JSON.parse(localStorage.getItem(key) as string);
    return statistics;
  }

  cacheResult(startDate: Date, endDate: Date, statistics: Statistics[]) {
    let key = this.createKey(startDate, endDate);

    localStorage.setItem(key, JSON.stringify(statistics));
  }

  private createKey(startDate: Date, endDate: Date): string {
    let key = `${this.datePipe.transform(startDate, this.dateFormat)}_${this.datePipe.transform(endDate, this.dateFormat)}`;
    return key;
  }
}
