import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';

import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { map, retry } from 'rxjs/operators';

import { Statistics } from '../_models/statistics';

@Injectable({
  providedIn: 'root'
})
export class JobStatisticsService {
  baseUrl = environment.apiUrl;

  dateFormat = 'MM/dd/yyyy';

  constructor(private http: HttpClient, private datePipe: DatePipe) { }

  GetJobStatistics(startDate: Date, endDate: Date): Observable<Statistics[]> {
      let url = `${this.baseUrl}Job?startDate=${this.formatDate(startDate)}&endDate=${this.formatDate(endDate)}`;

      return this.http.get<Statistics[]>(url).pipe(
        map((result) => {
          return result;
        }), retry(3));
  }

  private formatDate(date: Date): string {
    return this.datePipe.transform(date, this.dateFormat);
  }
}
